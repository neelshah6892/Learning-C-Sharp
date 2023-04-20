using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MTCommon;
using System.Reflection;
using System.IO;
using System.Globalization;

namespace LegLead.AppClasses
{
    //CONTRACT FILE IS REQUIRED TO USE THIS CLASS.
    public class ATM
    {
        public static ATM atm;
        public static List<string> SymList = new List<string>();
        public Dictionary<int, Contractinfo> contractinfomap; // key token
        public Dictionary<string, int> futtokenmap;       //delete this , intermediate        
        public Dictionary<int, int> OptionFutMapping; // key option token
        public Dictionary<int, double> FutPriceMap;
        public Dictionary<int, IvData> OptTokenIvMap;
        public string[] threeExpiry;
        static ATM()
        {
            atm = new ATM();
            atm.contractinfomap = new Dictionary<int, Contractinfo>();
            atm.futtokenmap = new Dictionary<string, int>();
            atm.OptionFutMapping = new Dictionary<int, int>();
            atm.FutPriceMap = new Dictionary<int, double>();
            atm.OptTokenIvMap = new Dictionary<int, IvData>();

            atm.threeExpiry = atm.GetExpiryDates(ArisApi_a._arisApi.DsContract.Tables["NSEFO"]);
            atm.saveContractinfoMap();
            atm.savefuturetokenmap();
            atm.SaveOptionFutureMapping();
            atm.SaveFutTokenFutPrice();
            atm.SaveIVInfoMap();
            atm.GetStrikeDiff(ArisApi_a._arisApi.SystemConfig.SymboldiffFilePath);
        }

        public void saveContractinfoMap()
        {
            try
            {
                string month1 = Convert.ToDateTime(threeExpiry[0]).ToString("yyyyMMdd").Substring(4, 2);
                string month2 = Convert.ToDateTime(threeExpiry[1]).ToString("yyyyMMdd").Substring(4, 2);
                string month3 = Convert.ToDateTime(threeExpiry[2]).ToString("yyyyMMdd").Substring(4, 2);
                DataTable Contracttable = new DataTable();
                Contracttable = ArisApi_a._arisApi.DsContract.Tables["NseFo"];
                string curr_exp=Convert.ToDateTime(threeExpiry[0]).ToString("yyyyMMdd"); 
                  string next_exp=Convert.ToDateTime(threeExpiry[1]).ToString("yyyyMMdd"); 
                foreach (DataRow row in Contracttable.Rows)
                {
                    int token = Convert.ToInt32(row["TokenNo"]);
                    DateTime expiry = DateTime.ParseExact(row["ExpiryDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    string exp = expiry.ToString("yyyyMMdd");
                    if (exp == curr_exp || exp == next_exp)
                    {
                        if (!contractinfomap.ContainsKey(token))
                        {
                            contractinfomap.Add(token, new Contractinfo());
                            if (row["Series"].ToString() == "CE")
                            {
                                contractinfomap[token].series = 3;
                            }
                            else if (row["Series"].ToString() == "PE")
                            {
                                contractinfomap[token].series = 4;
                            }
                            else
                            {
                                contractinfomap[token].series = 0;
                            }
                            contractinfomap[token].strike = Convert.ToDouble(row["StrikePrice"]);
                            //DateTime expiry = DateTime.ParseExact(row["ExpiryDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                            contractinfomap[token].expiry = expiry.ToString("yyyyMMdd");
                            string month = contractinfomap[token].expiry.Substring(4, 2);
                             if (month == month1)
                            {
                                string date = DateTime.Now.ToString("yyyyMMdd");
                                if (contractinfomap[token].expiry == date)
                                {
                                    contractinfomap[token].daysleft = 0.50;
                                }
                                else
                                    contractinfomap[token].daysleft = CalculatorUtils.CalculateDay(Convert.ToDateTime(threeExpiry[0]));
                            }
                            else if (month == month2)
                            {
                                contractinfomap[token].daysleft = CalculatorUtils.CalculateDay(Convert.ToDateTime(threeExpiry[1]));
                            }
                            //else
                            //{
                            //    contractinfomap[token].daysleft = CalculatorUtils.CalculateDay(Convert.ToDateTime(threeExpiry[2]));
                            //}
                            contractinfomap[token].symbol = (row["Symbol"]).ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void savefuturetokenmap()
        {
            try
            {
                DataTable Contracttable = new DataTable();
                string symbol = "";
                Contracttable = ArisApi_a._arisApi.DsContract.Tables["NseFo"];
                string filter = DBConst.Series + "='" + "XX" + "' AND " + DBConst.ExpiryDate + "='" + Convert.ToDateTime(threeExpiry[0]).ToString("yyyyMMdd") + "'";
                DataRow[] dr = Contracttable.Select(filter);
                foreach (DataRow row in dr)
                {
                    symbol = row["Symbol"].ToString();
                    int expiry = Convert.ToInt32(row["ExpiryDate"]);
                    string key = symbol + "_" + expiry.ToString();
                    int token = Convert.ToInt32(row["TokenNo"]);
                    if (!futtokenmap.ContainsKey(key))
                    {
                        futtokenmap.Add(key, token);
                    }
                }

                string filter1 = DBConst.Series + "='" + "XX" + "' AND " + DBConst.ExpiryDate + "='" + Convert.ToDateTime(threeExpiry[1]).ToString("yyyyMMdd") + "'";
                DataRow[] dr1= Contracttable.Select(filter1);
                foreach (DataRow row in dr1)
                {
                    symbol = row["Symbol"].ToString();
                    int expiry = Convert.ToInt32(row["ExpiryDate"]);
                    string key = symbol + "_" + expiry.ToString();
                    int token = Convert.ToInt32(row["TokenNo"]);
                    if (!futtokenmap.ContainsKey(key))
                    {
                        futtokenmap.Add(key, token);
                    }
                }
                //DepenentoptionMap = new Dictionary<int, List<int>>();
                //foreach (var key in futtokenmap)
                //{
                //    int futtoken = key.Value;
                //    if (!DepenentoptionMap.ContainsKey(futtoken))
                //    {
                //        DepenentoptionMap.Add(futtoken, new List<int>());                        
                //    }
                //}              
                // savedependentdata();
            }
            catch (Exception)
            {

            }
        }

        public void SaveOptionFutureMapping()
        {
            try
            {
                //option token -> future token
                foreach (var contractData in contractinfomap.Where(x => x.Value.series == 3 || x.Value.series == 4))
                {
                    int opttoken = contractData.Key;
                    string expiry = contractData.Value.expiry;
                    string symbol = contractData.Value.symbol;
                    int futtoken = 0;
                    string key = symbol + "_" + expiry;
                    if (symbol == "NIFTY" || symbol == "BANKNIFTY" || symbol == "FINNIFTY")
                    {
                        string month = Convert.ToString(expiry).Substring(4, 2);
                        foreach (var futData in futtokenmap.Where(x => x.Key.Contains(symbol) && x.Key.Substring(symbol.Length + 5, 2).ToString() == month))
                        {
                            futtoken = futData.Value;
                        }
                    }
                    else
                    {
                        foreach (var futData in futtokenmap.Where(x => x.Key == key))
                        {
                            futtoken = futData.Value;
                        }
                    }
                    if (futtoken != 0)
                    {
                        if (!OptionFutMapping.ContainsKey(opttoken))
                        {
                            OptionFutMapping.Add(opttoken, futtoken);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void SaveFutTokenFutPrice()
        {
            try
            {
                foreach (var item in futtokenmap)
                {
                    int futtoken = item.Value;
                    if (!FutPriceMap.ContainsKey(futtoken))
                    {
                        FutPriceMap.Add(futtoken, new double());
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void SaveIVInfoMap()
        {
            try
            {
                foreach (var contractData in contractinfomap.Where(x => x.Value.series == 3 || x.Value.series == 4))
                {
                    if (!OptTokenIvMap.ContainsKey(contractData.Key))
                    {
                        OptTokenIvMap.Add(contractData.Key, new IvData());
                    }
                }
            }
            catch (Exception)
            {

            }
        }  
      
        private string[] GetExpiryDates(DataTable expTable)
        {
            try
            {
                var dateList = new HashSet<String>();
                var dateList1 = new HashSet<String>();
                AppGlobal.monthint = new List<int>();
                foreach (DataRow r1 in expTable.Rows)
                {
                    if (r1[DBConst.InstrumentName].ToString() != "FUTSTK")
                        continue;
                    string eDate = r1[DBConst.ExpiryDate].ToString();
                    dateList.Add(eDate);
                }
                AppGlobal.monthint.Clear();
                foreach (string s1 in dateList)
                {
                    string s2 = s1.Substring(0, 4);
                    string s3 = s1.Substring(4, 2);
                    string s4 = s1.Substring(6, 2);
                    System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                    string month = mfi.GetMonthName(Convert.ToInt32(s3)).ToString();
                    month = month.Substring(0, 3);
                    string s5 = s2 + month + s4;

                    int dateno = (int)ArisApi_a._arisApi.DateTimeToSecond(Market.NseFO, Convert.ToDateTime(s5));
                    AppGlobal.monthint.Add(dateno);
                }

                AppGlobal.monthint.Sort();
                foreach (int k in AppGlobal.monthint)
                {
                    string month = Convert.ToString(ArisApi_a._arisApi.SecondToDateTime(Market.NseFO, k));
                    dateList1.Add(month);
                }
                string[] threeDates = { "", "", "" };
                int i = 0;
                foreach (string s1 in dateList1)
                {
                    if (s1.Length != 0)
                    {
                        threeDates[i] = s1;
                        i++;
                        if (i > 2) break;
                    }
                }
                return threeDates;
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
                return null;
            }
        }

        public void GetStrikeDiff(string fileName)
        {
            try
            {
                string path = fileName;//"D:\\Symbol_Diff\\symboldiff.txt";
                const char fieldSeparator = ',';
                using (StreamReader readFile = new StreamReader(fileName))
                {
                    string line;
                    while ((line = readFile.ReadLine()) != null)
                    {
                        List<string> split = line.Split(fieldSeparator).ToList();
                        if (split[0] == "Symbol")
                            continue;
                        foreach (var item in contractinfomap.Where(x => x.Value.symbol == split[0]))
                        {
                            item.Value.strikeDiff = Convert.ToDouble(split[1]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        public Tuple<double,int, int> GetAtmStrikeAndCePeTokens(int token)
        {
            double atm_strike = 0; int tokenCe=0; int tokenPe =0;
            try
            {
                if (contractinfomap.ContainsKey(token))
                {
                    string symbol = contractinfomap[token].symbol;
                    string expiry = contractinfomap[token].expiry;
                    double diff = contractinfomap[token].strikeDiff;
                    double futLtp = FutPriceMap[token];
                    if (futLtp != 0)
                    {
                        double strike = 0;
                        double remainder = Math.Round(Convert.ToDouble(futLtp % diff), 2);
                        if (remainder > (diff / 2))
                        {
                            strike = Convert.ToDouble(futLtp + diff) - Convert.ToDouble(futLtp % diff);
                        }
                        else
                        {
                            strike = Convert.ToDouble(futLtp - (futLtp % diff));
                        }
                        double closest_strike = contractinfomap.Values.Where(x=> x.symbol== symbol && x.expiry == expiry && x.series == 3)
                            .Aggregate((x, y) => Math.Abs(x.strike - strike) < Math.Abs(y.strike - strike) ? x : y).strike;
                        atm_strike = closest_strike;
                        foreach (var watch in contractinfomap.Where(x => x.Value.symbol == symbol && x.Value.strike == closest_strike && x.Value.expiry == expiry && x.Value.series == 3))
                        {
                            tokenCe = watch.Key;
                            //Console.WriteLine(watch.Value.symbol + "," + watch.Value.strike + "," + watch.Value.expiry + "," + watch.Value.series + "," + watch.Key);
                        }

                        foreach (var watch in contractinfomap.Where(x => x.Value.symbol == symbol && x.Value.strike == closest_strike && x.Value.expiry == expiry && x.Value.series == 4))
                        {
                            tokenPe = watch.Key;
                            //Console.WriteLine(watch.Value.symbol + "," + watch.Value.strike + "," + watch.Value.expiry + "," +  watch.Value.series +","  + watch.Key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
            return Tuple.Create(atm_strike, tokenCe, tokenPe); ;
        }
    }


    public class Contractinfo
    {
        public double strike;
        public double strikeDiff;
        public int series;
        public double daysleft;
        public string expiry;
        public string symbol;
        public string instrument;
    }

    public class IvData
    {
        public double iv;
        public double ivp;
        public double prvTradedQty;
        public double bidiv;
        public double selliv;
        public DateTime ivUpdateTime;
    }

    //public class FutureInfo
    //{
    //    public double futLtp;
    //    public double atmStrike;
    //    public int CeToken;
    //    public int PeToken;
    //}


    //int token = Convert.ToInt32(_response.TokenNo);
    //            if (ATM.atm.contractinfomap.ContainsKey(token))
    //            {
    //                int series = ATM.atm.contractinfomap[token].series; 
    //                if (series == 0)
    //                {
    //                    ATM.atm.FutPriceMap[token] = Math.Round((Convert.ToDouble(_response.LastTradedPrice) / 100), 2);     
    //                }
    //                else
    //                {
    //                    if (ATM.atm.OptionFutMapping.ContainsKey(token))
    //                    {
    //                        int futtoken = ATM.atm.OptionFutMapping[token];
    //                        if (ATM.atm.FutPriceMap[futtoken] != 0)
    //                        {

    //                        }
    //                    }
    //                }
    //            }
}
