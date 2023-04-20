using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MTCommon;
using LegLead.AppClasses;
using System.Reflection;
using System.IO;
using System.Globalization;
namespace LegLead
{

    public partial class LegLead : Form
    {
        public string currentExpiry = "";
        public string nextExpiry = "";
        public string farExpiry = "";
        public Dictionary<string, IvInfo> symbolPairIvMap;
        public Dictionary<string, IvInfo> symbolPairIvMapNext;
        public Dictionary<string, AtmInfo> futTokenAtmStrike;
        Dictionary<string, IvInfoForIvp> SymbolIvListMap;
        //Dictionary<int, double> futTokenQtyMap;
        public StreamWriter alert_dataWriter;
        public StreamWriter ivData_dataWriter;
        public List<string> holidays_list;
        DataTable dtexcelRead = new DataTable();
        public LegLead()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            ArisApi_a._arisApi.InitializeAPI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Initialization
            GetHolidaysList();
            SymbolIvListMap = new Dictionary<string, IvInfoForIvp>();
            //futTokenQtyMap = new Dictionary<int, double>();
            string file = ArisApi_a._arisApi.SystemConfig.Iv_PercentileFilePath;
            symbolPairIvMap = new Dictionary<string, IvInfo>();
            symbolPairIvMapNext = new Dictionary<string, IvInfo>();
            futTokenAtmStrike = new Dictionary<string, AtmInfo>();
            currentExpiry = Convert.ToDateTime(ATM.atm.threeExpiry[0]).ToString("yyyyMMdd");
            nextExpiry = Convert.ToDateTime(ATM.atm.threeExpiry[1]).ToString("yyyyMMdd");
            //farExpiry = Convert.ToDateTime(ATM.atm.threeExpiry[2]).ToString("yyyyMMdd");

            dgvAlert.MultiSelect = false;
            dgvAlert.UniqueName = MTEnums.StrategyType.Arbitrage.ToString();
            dgvAlert.LoadSaveSettings();


            LoadMap();
            //GetFutTokens();
            ReadPastivDile(ArisApi_a._arisApi.SystemConfig.IvPastData);

            GenerateColumns(dgvAlert);
            WriteAlertLog();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            //backgroundWorker3.DoWork += new DoWorkEventHandler(backgroundWorker3_DoWork);
            ArisApi_a._arisApi.OnMarketDepthUpdate += new ArisApi_a.MarketDepthUpdateDelegate(_arisApi_OnMarketDepthUpdate);
            NseFoBroadcastConnection nseFo = new NseFoBroadcastConnection();
        }

        //public void GetFutTokens()
        //{
        //    foreach (var item in ATM.atm.FutPriceMap)
        //    {
        //        futTokenQtyMap.Add(item.Key, 0);
        //    }
        //}

        public void GetHolidaysList()
        {
            try
            {
                string path = "D:\\nseContractFile\\NseHolidays.txt";
                ReadHolidayfile(path);
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        public void ReadHolidayfile(string file)
        {
            try
            {
                holidays_list = new List<string>();
                const char fieldSeparator = '\t';
                using (StreamReader readFile = new StreamReader(file))
                {
                    string line;
                    while ((line = readFile.ReadLine().Trim(fieldSeparator)) != null)
                    {
                        holidays_list.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        //public DataTable readFileIVExcel(string file)
        //{
        //    DataTable dst = new DataTable();
        //    try
        //    {
        //        dst.Columns.Add("Date", typeof(string));
        //        dst.Columns.Add("Symbol", typeof(string));
        //        //dst.Columns.Add("Open", typeof(string));
        //        //dst.Columns.Add("High", typeof(string));
        //        //dst.Columns.Add("Low", typeof(string));
        //        //dst.Columns.Add("Close", typeof(string));
        //        //dst.Columns.Add("IV", typeof(string));
        //        dst.Columns.Add("iv_rank", typeof(string));
        //        dst.Columns.Add("iv_per", typeof(string));
        //        //dst.Columns.Add("historic_vol_20", typeof(string));

        //        const char fieldSeparator = ',';
        //        using (StreamReader readFile = new StreamReader(file))
        //        {
        //            string line;
        //            string Exp = "";
        //            int i = 0;
        //            string file_date = "";
        //            while ((line = readFile.ReadLine()) != null)
        //            {
        //                List<string> split = line.Split(fieldSeparator).ToList();
        //                if (i == 0)
        //                {
        //                    file_date = (split[0].Remove(0, 9).ToString());
        //                    DateTime date = DateTime.ParseExact(file_date, "yyyyMMdd", null);
        //                    string dt = Convert.ToDateTime(date).ToString("ddMMMyyyy");
        //                    string yesterday = DateTime.Now.AddDays(-1).DayOfWeek.ToString();
        //                    if (yesterday != "Saturday" && yesterday != "Sunday" && !holidays_list.Contains(dt))
        //                    {
        //                        if (file_date.Contains(DateTime.Now.AddDays(-1).ToString("yyyyMMdd")) || file_date.Contains(DateTime.Now.ToString("yyyyMMdd")))
        //                        {
        //                            i++;
        //                            continue;
        //                        }
        //                        else
        //                        {
        //                            bool go_ahead = ConfirmFileDFromUser(dt, "Iv_Rank_Percentile");
        //                            if (go_ahead)
        //                            {
        //                                i++;
        //                                continue;
        //                            }
        //                            else
        //                            {
        //                                AppGlobal.Should_procced = false;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        bool go_ahead = ConfirmFileDFromUser(dt, "Iv_Rank_Percentile");
        //                        if (go_ahead)
        //                        {
        //                            i++;
        //                            continue;
        //                        }
        //                        else
        //                        {
        //                            AppGlobal.Should_procced = false;
        //                            break;
        //                        }
        //                    }
        //                }

        //                if (Convert.ToString(split[0].Trim()) == "infodate" + "_" + file_date)
        //                {
        //                    i++;
        //                    continue;
        //                }
        //                else
        //                {
        //                    Exp = Convert.ToString(split[0].Trim());
        //                    string symbol = Convert.ToString(split[1].Trim());
        //                    DataRow d = dst.NewRow();
        //                    d["Date"] = Convert.ToString(Exp);
        //                    d["Symbol"] =symbol;
        //                    //d["Open"] = Convert.ToString(split[2].Trim());
        //                    //d["High"] = Convert.ToString(split[3].Trim());
        //                    //d["Low"] = Convert.ToString(split[4].Trim());
        //                    //d["Close"] = Convert.ToString(split[5].Trim());
        //                    //d["IV"] = Convert.ToString(split[6].Trim());
        //                    //d["iv_rank"] = Convert.ToString(split[7].Trim());
        //                    d["iv_rank"] = Convert.ToString(split[13].Trim());
        //                    d["iv_per"] = Convert.ToString(split[14].Trim());
        //                    //d["historic_vol_20"] = Convert.ToString(split[10].Trim());
        //                    dst.Rows.Add(d);

        //                    if (!symbolIvPMap.ContainsKey(symbol))
        //                    {
        //                        symbolIvPMap.Add(symbol, new IvRankPer());
        //                        symbolIvPMap[symbol].rank = Convert.ToDouble(d["iv_rank"]);
        //                        symbolIvPMap[symbol].per = Convert.ToDouble(d["iv_per"]);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ArisApi_a._arisApi.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
        //    }
        //    return dst;
        //}

        public bool ConfirmFileDFromUser(string date, string file)
        {
            bool isOk = false;
            try
            {
                DialogResult dialogResult = MessageBox.Show(file + " file updated on " + date + " \n Do you want to continue? ", "Some Title", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    isOk = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    isOk = false;
                }
            }
            catch (Exception)
            {

            }
            return isOk;
        }

        void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            //Arguments args = e.Argument as Arguments;
            //string time = args.time;
            //string exp = args.expiry;
            //GetTokensForFarExp(time, exp);
        }

        void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

            Arguments args = e.Argument as Arguments;
            string time = args.time;
            string exp = args.expiry;
            GetTokensForNextExp(time, exp);
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Arguments args = e.Argument as Arguments;
            string time = args.time;
            string  exp = args.expiry;
            GetTokens(time,exp);
            
        }

        public void WriteAlertLog()
        {
            try
            {
                string path = ArisApi_a._arisApi.SystemConfig.LogFilePath; //"D:\\AlertLogs\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string date = "LegLeadLog" + DateTime.Now.ToString("ddMMMyyyy_hh_mm_ss") + ".txt";
                string filename = path + date;

                if (!File.Exists(filename))
                {
                    alert_dataWriter = new StreamWriter(filename, true);
                    alert_dataWriter.AutoFlush = true;
                }

                string date1 = "LegLeadIvLog" + DateTime.Now.ToString("ddMMMyyyy_hh_mm_ss") + ".txt";
                string filename1 = path + date1;

                if (!File.Exists(filename1))
                {
                    ivData_dataWriter = new StreamWriter(filename1, true);
                    ivData_dataWriter.AutoFlush = true;
                }
            }
            catch (Exception)
            {
            }
        }

        public void LoadMap()
        {
            try
            {
                string path = ArisApi_a._arisApi.SystemConfig.SymbolPairFilePath;//"D:\\Symbol_Diff\\symboldiff.txt";
                const char fieldSeparator = ',';
                using (StreamReader readFile = new StreamReader(path))
                {
                    string line;
                    while ((line = readFile.ReadLine()) != null)
                    {
                        List<string> split = line.Split(fieldSeparator).ToList();
                        if (split[0] == "Symbol")
                            continue;
                        string symPair = split[0] + "_" + split[1];
                        if (!symbolPairIvMap.ContainsKey(symPair))
                        {
                            symbolPairIvMap.Add(symPair, new IvInfo());
                            symbolPairIvMapNext.Add(symPair, new IvInfo());
                        }
                    }
                }

                foreach (var item in ATM.atm.futtokenmap)
                {
                    if (!futTokenAtmStrike.ContainsKey(item.Key))
                    {
                        futTokenAtmStrike.Add(item.Key, new AtmInfo());
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void ReadPastivDile(string file)
        {
            try
            {
                const char fieldSeparator = ',';
                using (StreamReader readFile = new StreamReader(file))
                {
                    string line;
                    string Exp = "";
                    int i = 0;
                    string file_date = "";
                    while ((line = readFile.ReadLine()) != null)
                    {
                        List<string> split = line.Split(fieldSeparator).ToList();
                        if (i == 0)
                        {
                            file_date = (split[0].Remove(0, 9).ToString());
                            DateTime date = DateTime.ParseExact(file_date, "yyyyMMdd", null);
                            string dt = Convert.ToDateTime(date).ToString("ddMMMyyyy");
                            string yesterday = DateTime.Now.AddDays(-1).DayOfWeek.ToString();
                            if (yesterday != "Saturday" && yesterday != "Sunday" && !holidays_list.Contains(dt))
                            {
                                if (file_date.Contains(DateTime.Now.AddDays(-1).ToString("yyyyMMdd")) || file_date.Contains(DateTime.Now.ToString("yyyyMMdd")))
                                {
                                    i++;
                                    continue;
                                }
                                else
                                {
                                    bool go_ahead = ConfirmFileDFromUser(dt, "Iv_Pat_Data");
                                    if (go_ahead)
                                    {
                                        i++;
                                        continue;
                                    }
                                    else
                                    {
                                        AppGlobal.Should_procced = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                bool go_ahead = ConfirmFileDFromUser(dt, "Iv_Pat_Data");
                                if (go_ahead)
                                {
                                    i++;
                                    continue;
                                }
                                else
                                {
                                    AppGlobal.Should_procced = false;
                                    break;
                                }
                            }
                        }

                        if (Convert.ToString(split[0].Trim()) == "infodate" + "_" + file_date)
                        {
                            i++;
                            continue;
                        }
                        else
                        {
                            string symbol = Convert.ToString(split[2].Trim());

                            if (!SymbolIvListMap.ContainsKey(symbol))
                            {
                                SymbolIvListMap.Add(symbol, new IvInfoForIvp());
                                SymbolIvListMap[symbol].count =0;
                                SymbolIvListMap[symbol].list = new List<double>();
                                SymbolIvListMap[symbol].list.Add(Convert.ToDouble(split[1]));
                            }
                            else
                            {
                                if (SymbolIvListMap[symbol].count < 99)
                                {
                                    SymbolIvListMap[symbol].list.Add(Convert.ToDouble(split[1]));
                                    SymbolIvListMap[symbol].count++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        public double  CalculateLiveIvp(string symbol,double curr_iv)
        {
            double ivp = 0;
            if(SymbolIvListMap.ContainsKey(symbol))
            {
                double belowdays = SymbolIvListMap[symbol].list.Where(x=> x < curr_iv).Count();
                ivp = Convert.ToDouble(belowdays / 100) * 100;
            }
            return ivp;
        
        }

        public void GetTokens(string time, string exp)
        {
            try
            {
                const char fieldSeparator = '_';
                foreach (var data in symbolPairIvMap)
                {
                    string key = data.Key;
                    List<string> split = key.Split(fieldSeparator).ToList();
                    string sym1 = split[0];
                    double sym1Iv = 0;
                    double sym1Strike = 0;
                    double ivp1 = 0; 
                    foreach (var item in futTokenAtmStrike.Where(x => x.Key.Contains(sym1 + "_" + exp)))
                    {
                        if (item.Value.CeToken != 0)
                        {
                            sym1Strike = item.Value.atmStrike;
                            sym1Iv = ATM.atm.OptTokenIvMap[item.Value.CeToken].iv;
                             ivp1 =  ATM.atm.OptTokenIvMap[item.Value.CeToken].ivp;
                        }
                    }
                    double sym2Iv = 0;
                    double sym2Strike = 0;
                    string sym2 = split[1];
                    double ivp2 =0; 
                    foreach (var item in futTokenAtmStrike.Where(x => x.Key.Contains(sym2 + "_" + exp)))
                    {
                        if (item.Value.CeToken != 0)
                        {
                            sym2Strike = item.Value.atmStrike;
                            sym2Iv = ATM.atm.OptTokenIvMap[item.Value.CeToken].iv;
                            ivp2 =  ATM.atm.OptTokenIvMap[item.Value.CeToken].ivp;
                        }
                    }
                    if (sym1Iv != 0 && sym2Iv != 0)
                    {
                        symbolPairIvMap[data.Key].Sym1Strike = sym1Strike;
                        symbolPairIvMap[data.Key].Sym2Strike = sym2Strike;
                        symbolPairIvMap[data.Key].sym1Iv = sym1Iv;
                        symbolPairIvMap[data.Key].sym2Iv = sym2Iv;
                        symbolPairIvMap[data.Key].ivDiff = (Math.Round(sym1Iv- sym2Iv,2));
                            symbolPairIvMap[data.Key].IVP1 = ivp1;
                            symbolPairIvMap[data.Key].IVP2 = ivp2;
                        if (ivp1 != 0 && ivp2 != 0)
                        {
                            symbolPairIvMap[data.Key].ivperDiff = Math.Abs(ivp1 - ivp2);
                        }
                       // symbolPairIvMap[data.Key].alertValue = symbolPairIvMap[data.Key].ivDiff;

                        //string date = DateTime.ParseExact(exp, "yyyyMMdd", null).ToString("ddMMMyyyy");
                        //DateTime remaindt = Convert.ToDateTime(date);
                        //double RemainingDay = CalculatorUtils.CalculateDay(remaindt);
                        //double interval = 0;
                        //if (sym1 == "NIFTY" || sym1 == "BANKNIFTY")
                        //{
                        //    if (RemainingDay >= 20)
                        //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalAboveTwenty;
                        //    else if (RemainingDay < 20 && RemainingDay >= 10)
                        //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalTenToTwenty;
                        //    else if (RemainingDay < 10 && RemainingDay >= 5)
                        //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalFiveToTen;
                        //    else if (RemainingDay < 5)
                        //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalBelowFive;
                        //}
                        //else
                        //{
                        //    if (RemainingDay >= 20)
                        //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalAboveTwenty;
                        //    else if (RemainingDay < 20 && RemainingDay >= 10)
                        //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalTenToTwenty;
                        //    else if (RemainingDay < 10 && RemainingDay >= 5)
                        //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalFiveToTen;
                        //    else if (RemainingDay < 5)
                        //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalBelowFive;
                        //}

                        //commented recently
                        //CheckForAlertNew(data.Value, sym1, sym2, time, exp);
                        //CheckIVPAlert(sym1Iv,sym2Iv,data.Value.ivDiff,sym1, sym2,time,exp
                        //
                        //if (ATM.atm.OptTokenIvMap[token].ivUpdateTime > DateTime.Now.AddMinutes(-5))
                        //{
                        //    CheckForAlertNewIVPNew(data.Value, sym1, sym2, time, exp);
                        //}
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void GetTokensForNextExp(string time, string exp)
        {

            try
            {
                const char fieldSeparator = '_';
                foreach (var data in symbolPairIvMapNext)
                {
                    string key = data.Key;
                    List<string> split = key.Split(fieldSeparator).ToList();
                    string sym1 = split[0];
                    double sym1Iv = 0;
                    double sym1Strike = 0;
                    double ivp1 = 0;
                    foreach (var item in futTokenAtmStrike.Where(x => x.Key.Contains(sym1 + "_" + exp)))
                    {
                        if (item.Value.CeToken != 0)
                        {
                            sym1Strike = item.Value.atmStrike;
                            sym1Iv = ATM.atm.OptTokenIvMap[item.Value.CeToken].iv;
                            ivp1 = ATM.atm.OptTokenIvMap[item.Value.CeToken].ivp;
                        }
                    }
                    double sym2Iv = 0;
                    double sym2Strike = 0;
                    string sym2 = split[1];
                    double ivp2 = 0;
                    foreach (var item in futTokenAtmStrike.Where(x => x.Key.Contains(sym2 + "_" + exp)))
                    {
                        if (item.Value.CeToken != 0)
                        {
                            sym2Strike = item.Value.atmStrike;
                            sym2Iv = ATM.atm.OptTokenIvMap[item.Value.CeToken].iv;
                            ivp2 = ATM.atm.OptTokenIvMap[item.Value.CeToken].ivp;
                        }
                    }
                    if (sym1Iv != 0 && sym2Iv != 0)
                    {
                        symbolPairIvMapNext[data.Key].Sym1Strike = sym1Strike;
                        symbolPairIvMapNext[data.Key].Sym2Strike = sym2Strike;
                        symbolPairIvMapNext[data.Key].sym1Iv = sym1Iv;
                        symbolPairIvMapNext[data.Key].sym2Iv = sym2Iv;
                        symbolPairIvMapNext[data.Key].ivDiff = (Math.Round(sym1Iv - sym2Iv, 2));
                        symbolPairIvMapNext[data.Key].IVP1 = ivp1;
                        symbolPairIvMapNext[data.Key].IVP2 = ivp2;
                        if (ivp1 != 0 && ivp2 != 0)
                        {
                            symbolPairIvMapNext[data.Key].ivperDiff = Math.Abs(ivp1 - ivp2);
                        }
                      
                        CheckForAlertNewIVPNew(data.Value, sym1, sym2, time, exp);
                    }
                }
            }
            catch (Exception)
            {

            }
            //try
            //{
            //    const char fieldSeparator = '_';
            //    foreach (var data in symbolPairIvMap)
            //    {
            //        string key = data.Key;
            //        List<string> split = key.Split(fieldSeparator).ToList();
            //        string sym1 = split[0];
            //        double sym1Iv = 0;
            //        double sym1Strike = 0;
            //        double ivp1 = 0;
            //        //symbolPairIvMap[data.Key].sym1PrvIv = 0;
            //        //symbolPairIvMap[data.Key].sym2PrvIv = 0;
            //        foreach (var item in futTokenAtmStrike.Where(x => x.Key.Contains(sym1 + "_" + exp)))
            //        {
            //            if (item.Value.CeToken != 0)
            //            {
            //                sym1Strike = item.Value.atmStrike;
            //                sym1Iv = ATM.atm.OptTokenIvMap[item.Value.CeToken].iv;
            //                ivp1 = ATM.atm.OptTokenIvMap[item.Value.CeToken].ivp;
            //               // symbolPairIvMap[data.Key].sym1PrvIv = symbolPairIvMap[data.Key].sym1Iv;
            //            }
            //        }
            //        double sym2Iv = 0;
            //        double sym2Strike = 0;
            //        string sym2 = split[1];
            //        double ivp2 = 0;
            //        foreach (var item in futTokenAtmStrike.Where(x => x.Key.Contains(sym2 + "_" + exp)))
            //        {
            //            if (item.Value.CeToken != 0)
            //            {
            //                sym2Strike = item.Value.atmStrike;
            //                sym2Iv = ATM.atm.OptTokenIvMap[item.Value.CeToken].iv;
            //                ivp2 = ATM.atm.OptTokenIvMap[item.Value.CeToken].ivp;
            //               // symbolPairIvMap[data.Key].sym2PrvIv = symbolPairIvMap[data.Key].sym2Iv;
            //            }
            //        }
            //        if (sym1Iv != 0 && sym2Iv != 0)
            //        {

            //            symbolPairIvMap[data.Key].Sym1Strike = sym1Strike;
            //            symbolPairIvMap[data.Key].Sym2Strike = sym2Strike;
            //            symbolPairIvMap[data.Key].sym1Iv = sym1Iv;
            //            symbolPairIvMap[data.Key].sym2Iv = sym2Iv;
            //            //symbolPairIvMap[data.Key].sym1BS = sym1bs;
            //            //symbolPairIvMap[data.Key].sym2BS = sym2bs;
            //            symbolPairIvMap[data.Key].ivDiff = (Math.Round(sym1Iv - sym2Iv, 2));
            //            symbolPairIvMap[data.Key].IVP1 = ivp1;
            //            symbolPairIvMap[data.Key].IVP2 = ivp2;

            //            if (ivp1 != 0 && ivp2 != 0)
            //            {
            //                symbolPairIvMap[data.Key].ivperDiff = Math.Abs(ivp1 - ivp2);
            //            }
            //            //symbolPairIvMap[data.Key].ivperDiff = (Math.Round(ivp1 - ivp2, 2)); ;
            //            //symbolPairIvMap[data.Key].alertValue = symbolPairIvMap[data.Key].ivDiff;

            //            //ivp
            //            //SymbolPairIvpRankMap[data.Key].iv_diff = (Math.Round(sym1Iv - sym2Iv, 2));
            //            //SymbolPairIvpRankMap[data.Key]. = symbolPairIvMap[data.Key].ivDiff;

            //            string date = DateTime.ParseExact(exp, "yyyyMMdd", null).ToString("ddMMMyyyy");
            //            DateTime remaindt = Convert.ToDateTime(date);
            //            double RemainingDay = CalculatorUtils.CalculateDay(remaindt);
            //            //double interval = 0;
            //            //if (sym1 == "NIFTY" || sym1 == "BANKNIFTY")
            //            //{
            //            //    if (RemainingDay >= 20)
            //            //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalAboveTwenty;
            //            //    else if (RemainingDay < 20 && RemainingDay >= 10)
            //            //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalTenToTwenty;
            //            //    else if (RemainingDay < 10 && RemainingDay >= 5)
            //            //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalFiveToTen;
            //            //    else if (RemainingDay < 5)
            //            //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalBelowFive;
            //            //}
            //            //else
            //            //{
            //            //    if (RemainingDay >= 20)
            //            //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalAboveTwenty;
            //            //    else if (RemainingDay < 20 && RemainingDay >= 10)
            //            //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalTenToTwenty;
            //            //    else if (RemainingDay < 10 && RemainingDay >= 5)
            //            //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalFiveToTen;
            //            //    else if (RemainingDay < 5)
            //            //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalBelowFive;
            //            //}
            //            //CheckForAlertNew(data.Value, sym1, sym2, time, exp);
            //        }

                  
            //    }
            //}
            //catch (Exception)
            //{

            //}
        }


        //public void GetTokensForFarExp(string time, string exp)
        //{
        //    try
        //    {

        //        const char fieldSeparator = '_';
        //        foreach (var data in symbolPairIvMap)
        //        {
        //            string key = data.Key;
        //            List<string> split = key.Split(fieldSeparator).ToList();
        //            string sym1 = split[0];
        //            double sym1Iv = 0;
        //            double sym1Strike = 0;
        //            double ivp1 = 0;
        //            //symbolPairIvMap[data.Key].sym1PrvIv = 0;
        //            //symbolPairIvMap[data.Key].sym2PrvIv = 0;
        //            foreach (var item in futTokenAtmStrike.Where(x => x.Key.Contains(sym1 + "_" + exp)))
        //            {
        //                if (item.Value.CeToken != 0)
        //                {
        //                    sym1Strike = item.Value.atmStrike;
        //                    sym1Iv = ATM.atm.OptTokenIvMap[item.Value.CeToken].iv;
        //                    ivp1 = ATM.atm.OptTokenIvMap[item.Value.CeToken].ivp;
        //                    //symbolPairIvMap[data.Key].sym1PrvIv = symbolPairIvMap[data.Key].sym1Iv;
        //                }
        //            }
        //            double sym2Iv = 0;
        //            double sym2Strike = 0;
        //            string sym2 = split[1];
        //            double ivp2 = 0;
        //            foreach (var item in futTokenAtmStrike.Where(x => x.Key.Contains(sym2 + "_" + exp)))
        //            {
        //                if (item.Value.CeToken != 0)
        //                {
        //                    sym2Strike = item.Value.atmStrike;
        //                    sym2Iv = ATM.atm.OptTokenIvMap[item.Value.CeToken].iv;
        //                    ivp2 = ATM.atm.OptTokenIvMap[item.Value.CeToken].ivp;
        //                    //symbolPairIvMap[data.Key].sym2PrvIv = symbolPairIvMap[data.Key].sym2Iv;
        //                }
        //            }
        //            if (sym1Iv != 0 && sym2Iv != 0)
        //            {

        //                symbolPairIvMap[data.Key].Sym1Strike = sym1Strike;
        //                symbolPairIvMap[data.Key].Sym2Strike = sym2Strike;
        //                symbolPairIvMap[data.Key].sym1Iv = sym1Iv;
        //                symbolPairIvMap[data.Key].sym2Iv = sym2Iv;
        //                //symbolPairIvMap[data.Key].sym1BS = sym1bs;
        //                //symbolPairIvMap[data.Key].sym2BS = sym2bs;
        //                symbolPairIvMap[data.Key].ivDiff = (Math.Round(sym1Iv - sym2Iv, 2));
        //                symbolPairIvMap[data.Key].IVP1 = ivp1;
        //                symbolPairIvMap[data.Key].IVP2 = ivp2;

        //                if (ivp1 != 0 && ivp2 != 0)
        //                {
        //                    symbolPairIvMap[data.Key].ivperDiff = Math.Abs(ivp1 - ivp2);
        //                }
        //              //  symbolPairIvMap[data.Key].ivperDiff = (Math.Round(ivp1 - ivp2, 2)); ;
        //                //symbolPairIvMap[data.Key].alertValue = symbolPairIvMap[data.Key].ivDiff;

        //                //string date = DateTime.ParseExact(exp, "yyyyMMdd", null).ToString("ddMMMyyyy");
        //                //DateTime remaindt = Convert.ToDateTime(date);
        //                //double RemainingDay = CalculatorUtils.CalculateDay(remaindt);
        //                //double interval = 0;
        //                //if (sym1 == "NIFTY" || sym1 == "BANKNIFTY")
        //                //{
        //                //    if (RemainingDay >= 20)
        //                //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalAboveTwenty;
        //                //    else if (RemainingDay < 20 && RemainingDay >= 10)
        //                //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalTenToTwenty;
        //                //    else if (RemainingDay < 10 && RemainingDay >= 5)
        //                //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalFiveToTen;
        //                //    else if (RemainingDay < 5)
        //                //        interval = ArisApi_a._arisApi.SystemConfig.IndexIntervalBelowFive;
        //                //}
        //                //else
        //                //{
        //                //    if (RemainingDay >= 20)
        //                //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalAboveTwenty;
        //                //    else if (RemainingDay < 20 && RemainingDay >= 10)
        //                //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalTenToTwenty;
        //                //    else if (RemainingDay < 10 && RemainingDay >= 5)
        //                //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalFiveToTen;
        //                //    else if (RemainingDay < 5)
        //                //        interval = ArisApi_a._arisApi.SystemConfig.StockIntervalBelowFive;
        //                //}
        //                //recent
        //                //CheckForAlertNew(data.Value, sym1, sym2, time, exp);
        //            }

                
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        void _arisApi_OnMarketDepthUpdate(MTApi.MTBCastPackets.MarketPicture _response, int time)
        {
            try
            {
                string strTime = ArisApi_a._arisApi.SecondToDateTime(Market.NseCm, time).ToString("H:mm");
                int token = Convert.ToInt32(_response.TokenNo);
                if (ATM.atm.contractinfomap.ContainsKey(token))
                {
                    int series = ATM.atm.contractinfomap[token].series;
                    double strike = ATM.atm.contractinfomap[token].strike;
                    double daysleft = ATM.atm.contractinfomap[token].daysleft;
                    string expiry = ATM.atm.contractinfomap[token].expiry;
                    string symbol = ATM.atm.contractinfomap[token].symbol;
                    if (series == 0)
                    {
                        string key = symbol + "_" + expiry;
                        ATM.atm.FutPriceMap[token] = Math.Round((Convert.ToDouble(_response.LastTradedPrice) / 100), 2);
                        if (futTokenAtmStrike.ContainsKey(key))
                        {
                            double atmStrike = ATM.atm.GetAtmStrikeAndCePeTokens(token).Item1;
                            int CeToken = ATM.atm.GetAtmStrikeAndCePeTokens(token).Item2;
                            int PeToken = ATM.atm.GetAtmStrikeAndCePeTokens(token).Item3;
                            futTokenAtmStrike[key].atmStrike = atmStrike;
                            futTokenAtmStrike[key].CeToken = CeToken;
                            futTokenAtmStrike[key].PeToken = PeToken;
                        }
                    }
                    else
                    {
                        if (ATM.atm.OptionFutMapping.ContainsKey(token))
                        {
                            int futtoken = ATM.atm.OptionFutMapping[token];
                            if (ATM.atm.FutPriceMap[futtoken] != 0)
                            {
                                if (ATM.atm.OptTokenIvMap.ContainsKey(token))
                                {
                                    if (ATM.atm.OptTokenIvMap[token].prvTradedQty != 0)
                                    {
                                        if (ATM.atm.OptTokenIvMap[token].prvTradedQty != _response.TotalQtyTraded)
                                        {
                                            double futltp = ATM.atm.FutPriceMap[futtoken];
                                            double optLtp = Math.Round((Convert.ToDouble(_response.LastTradedPrice) / 100), 2);
                                            double buyLtp = Math.Round((Convert.ToDouble(_response.Best5Buy[0].OrderPrice) / 100), 2);
                                            double sellLtp = Math.Round((Convert.ToDouble(_response.Best5Sell[0].OrderPrice) / 100), 2);
                                            //Console.WriteLine(daysleft);
                                            CalculateIv(optLtp, strike, series, futltp, daysleft, token, strTime, expiry, buyLtp, sellLtp);
                                        }
                                    }
                                    ATM.atm.OptTokenIvMap[token].prvTradedQty = _response.TotalQtyTraded;
                                }
                            }
                        }
                    }
                }
                if (!backgroundWorker1.IsBusy)
                {
                    Arguments args = new Arguments();
                    args.time = strTime;
                    args.expiry = currentExpiry;
                    backgroundWorker1.RunWorkerAsync(args);
                }

                if (!backgroundWorker2.IsBusy)
                {
                    // string strTime = ArisApi_a._arisApi.SecondToDateTime(Market.NseCm, time).ToString("H:mm");
                    Arguments args = new Arguments();
                    args.time = strTime;
                    args.expiry = nextExpiry;
                    backgroundWorker2.RunWorkerAsync(args);
                }

                //if (!backgroundWorker3.IsBusy)
                //{
                //   // string strTime = ArisApi_a._arisApi.SecondToDateTime(Market.NseCm, time).ToString("H:mm");
                //    Arguments args = new Arguments();
                //    args.time = strTime;
                //    args.expiry = farExpiry;
                //    backgroundWorker3.RunWorkerAsync(args);
                //}
            }
            catch (Exception)
            {    
            }
        }

        //private string GetFirstWeeklyExp(DataTable expTable, int index)
        //{
        //    try
        //    {
        //        var dateList = new HashSet<String>();
        //        var dateList1 = new HashSet<String>();
        //        AppGlobal.monthint = new List<int>();
        //        foreach (DataRow r1 in expTable.Rows)
        //        {
        //            if (r1[DBConst.InstrumentName].ToString() != "OPTIDX" || r1[DBConst.Symbol].ToString() != "BANKNIFTY")
        //            {
        //                continue;
        //            }
        //            string eDate = r1[DBConst.ExpiryDate].ToString();
        //            dateList.Add(eDate);
        //        }

        //        AppGlobal.monthint.Clear();
        //        foreach (string s1 in dateList)
        //        {
        //            string s2 = s1.Substring(0, 4);
        //            string s3 = s1.Substring(4, 2);
        //            string s4 = s1.Substring(6, 2);
        //            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //            string month = mfi.GetMonthName(Convert.ToInt32(s3)).ToString();
        //            month = month.Substring(0, 3);
        //            string s5 = s2 + month + s4;

        //            int dateno = (int)ArisApi_a._arisApi.DateTimeToSecond(Market.NseFO, Convert.ToDateTime(s5));
        //            AppGlobal.monthint.Add(dateno);
        //        }

        //        AppGlobal.monthint.Sort();
        //        foreach (int k in AppGlobal.monthint)
        //        {
        //            string month = Convert.ToString(ArisApi_a._arisApi.SecondToDateTime(Market.NseFO, k));
        //            dateList1.Add(month);
        //        }
        //        string weekly_exp = "";
        //        if (dateList1.Count != 0)
        //        {

        //            weekly_exp = Convert.ToDateTime(dateList1.ElementAt(index)).ToString("yyyyMMdd");
        //        }
        //        else
        //        {
        //            weekly_exp = "";
        //        }
        //        return weekly_exp;
        //    }
        //    catch (Exception ex)
        //    {
        //        ArisApi_a._arisApi.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
        //        return null;
        //    }
        //}

        private void CalculateIv(double optLtp, double strike, int series, double spotPrice, double daysLeft, int token,string time,string exp,double sellprice,double buyprice)
        {
            try
            {
                double ltpIv = 0; double ivp=0;
                string symbol = ATM.atm.contractinfomap[token].symbol;
                if (optLtp != 0 && spotPrice != 0)
                {
                    GreeksVariable stk1 = new GreeksVariable();
                    stk1.SpotPrice = spotPrice;
                    stk1.IntrestRate = 0;
                    stk1.StrikePrice = strike;
                    stk1.TimeToExpiry = daysLeft;
                    stk1.DividentYield = 0;
                    stk1.ActualValue = optLtp;
                    if (series == 3)
                    {
                        ltpIv = Math.Round(Convert.ToDouble(CalculatorUtils.CallVolatility(stk1)), 2);
                        ATM.atm.OptTokenIvMap[token].iv = ltpIv;
                        ATM.atm.OptTokenIvMap[token].ivUpdateTime = DateTime.Now; 
                    }
                    else if (series == 4)
                    {
                        ltpIv = Math.Round(Convert.ToDouble(CalculatorUtils.PutVolatility(stk1)), 2);
                        ATM.atm.OptTokenIvMap[token].iv = ltpIv;
                        ATM.atm.OptTokenIvMap[token].ivUpdateTime = DateTime.Now;
                    }

                    //if (ATM.atm.OptTokenIvMap[token].iv != 0)
                    //{
                    //    ivp = CalculateLiveIvp(symbol, ATM.atm.OptTokenIvMap[token].iv);
                    //    ATM.atm.OptTokenIvMap[token].ivp =  ivp;
                    //}
                    ////Console.WriteLine(daysLeft + "||" + exp);
                    //ivData_dataWriter.WriteLine(time + "," + symbol + "," + daysLeft +","+ exp + "," + series + "," + strike + "," + spotPrice + "," + optLtp + "," + ltpIv + "," + ivp);
                }

                if (buyprice != 0 && spotPrice != 0)
                {
                    GreeksVariable stk1 = new GreeksVariable();
                    stk1.SpotPrice = spotPrice;
                    stk1.IntrestRate = 0;
                    stk1.StrikePrice = strike;
                    stk1.TimeToExpiry = daysLeft;
                    stk1.DividentYield = 0;
                    stk1.ActualValue = buyprice;
                    if (series == 3)
                    {
                        double buyiv = Math.Round(Convert.ToDouble(CalculatorUtils.CallVolatility(stk1)), 2);
                        ATM.atm.OptTokenIvMap[token].bidiv = buyiv;
                    }
                    else if (series == 4)
                    {
                        double buyiv = Math.Round(Convert.ToDouble(CalculatorUtils.CallVolatility(stk1)), 2);
                        ATM.atm.OptTokenIvMap[token].bidiv = buyiv;
                    }
                }


                if (sellprice != 0 && spotPrice != 0)
                {
                    GreeksVariable stk1 = new GreeksVariable();
                    stk1.SpotPrice = spotPrice;
                    stk1.IntrestRate = 0;
                    stk1.StrikePrice = strike;
                    stk1.TimeToExpiry = daysLeft;
                    stk1.DividentYield = 0;
                    stk1.ActualValue = sellprice;
                    if (series == 3)
                    {
                        double selliv = Math.Round(Convert.ToDouble(CalculatorUtils.CallVolatility(stk1)), 2);
                        ATM.atm.OptTokenIvMap[token].selliv = selliv;
                    }
                    else if (series == 4)
                    {
                        double selliv = Math.Round(Convert.ToDouble(CalculatorUtils.CallVolatility(stk1)), 2);
                        ATM.atm.OptTokenIvMap[token].selliv = selliv;
                    }
                }

                double diff = Math.Abs(ATM.atm.OptTokenIvMap[token].selliv - ATM.atm.OptTokenIvMap[token].bidiv);
                if (diff < 5)
                {
                    if (ATM.atm.OptTokenIvMap[token].iv != 0)
                    {
                        ivp = CalculateLiveIvp(symbol, ATM.atm.OptTokenIvMap[token].iv);
                        ATM.atm.OptTokenIvMap[token].ivp = ivp;
                    }
                    ivData_dataWriter.WriteLine(time + "," + symbol + "," + daysLeft + "," + exp + "," + series + "," + strike + "," + spotPrice + "," + optLtp + "," + ltpIv + "," + ivp);
                }
            }
            catch (Exception)
            {
            }
        }

        //public void CheckForNewAlert(IvInfo data, string sym1, string sym2, string time, string exp)
        //{
        //    if (InvokeRequired)
        //        BeginInvoke((MethodInvoker)(() => CheckForNewAlert(data, sym1, sym2, time, exp)));
        //    else
        //    {
        //        try
        //        {
        //            if (data.ivperDiff != 0)
        //            {
        //                if (data.ivperDiff > 15)
        //                {
        //                    if (data.ivDiff != 0)
        //                    {
        //                        if (data.isFirstTimeHigh)
        //                        {
        //                            data.prvAlertValueHigh = data.ivDiff;
        //                            data.isFirstTimeHigh = false;
        //                        }
        //                        else
        //                        { 
                                
        //                        }
        //                    }
        //                }
        //                else if (data.ivperDiff < 5)
        //                {
        //                    if (data.ivDiff != 0)
        //                    {
        //                        if (data.isFirstTimeLow)
        //                        {
        //                            data.prvAlertValueLow = data.ivDiff;
        //                            data.isFirstTimeLow = false;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception)
        //        {
                    
        //        }
        //    }
        //}

        public void CheckForAlertNewIVPNew(IvInfo data, string sym1, string sym2, string time, string exp)//current
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)(() => CheckForAlertNewIVPNew(data, sym1, sym2, time, exp)));
            else
            {
                try
                {
                    if (data.ivperDiff != 0)
                    {
                        #region HIGH
                        if (data.ivperDiff >= ArisApi_a._arisApi.SystemConfig.IVPLow)
                        {
                            data.sym1BS = ""; data.sym2BS = ""; data.Sym1Diff = 0; data.Sym2Diff = 0;
                            if (data.isFirstTimeHigh)
                            {
                                //first Alert at ivp-diff=15
                                if (data.IVP1 < data.IVP2)
                                {
                                    data.sym1BS = "BUY";
                                    data.sym2BS = "SELL";
                                }
                                else
                                {
                                    data.sym1BS = "SELL";
                                    data.sym2BS = "BUY";
                                }
                                string key1 = sym1 + "_" + sym2;
                                AddAlert(time, key1, data.Sym1Strike, data.sym1Iv, data.Sym2Strike, data.sym2Iv, ArisApi_a._arisApi.SystemConfig.IVPLow, data.ivperDiff,
                                    data.sym1PrvIv, data.sym2PrvIv, data.sym1BS, data.sym2BS, data.IVP1, data.IVP2, dgvAlert, exp, "HIGH", Color.Black);
                                data.prvAlertValueHigh = data.ivperDiff + ArisApi_a._arisApi.SystemConfig.IVPHigh;
                                data.prvAlertFixedValueHigh = data.ivperDiff;
                                data.sym1PrvIv = data.IVP1;
                                data.sym2PrvIv = data.IVP2;

                                data.prvAlertValueHigh = data.ivperDiff + ArisApi_a._arisApi.SystemConfig.IVPHigh;
                                data.prvAlertFixedValueHigh = data.ivperDiff;
                                data.sym1PrvIv = data.IVP1;
                                data.sym2PrvIv = data.IVP2;
                                data.isFirstTimeHigh = false;
                            }
                        }
                        
                        
                        if (!data.isFirstTimeHigh)
                        {
                            double diff = (Math.Abs(data.ivperDiff - data.prvAlertFixedValueHigh));
                            if (diff > ArisApi_a._arisApi.SystemConfig.IVPHigh)
                            {
                                if (data.ivperDiff > data.prvAlertValueHigh)//6
                                {
                                    if (data.IVP1 < data.IVP2)
                                    {
                                        data.sym1BS = "BUY";
                                        data.sym2BS = "SELL";
                                    }
                                    else
                                    {
                                        data.sym1BS = "SELL";
                                        data.sym2BS = "BUY";
                                    }
                                    string key = sym1 + "_" + sym2;
                                    AddAlert(time, key, data.Sym1Strike, data.sym1Iv, data.Sym2Strike, data.sym2Iv, data.prvAlertFixedValueHigh, data.ivperDiff,
                                        data.sym1PrvIv, data.sym2PrvIv, data.sym1BS, data.sym2BS, data.IVP1, data.IVP2, dgvAlert, exp, "HIGH",Color.SeaGreen);
                                    data.prvAlertValueHigh = data.ivperDiff + ArisApi_a._arisApi.SystemConfig.IVPHigh;
                                    data.prvAlertFixedValueHigh = data.ivperDiff;
                                    data.sym1PrvIv = data.IVP1;
                                    data.sym2PrvIv = data.IVP2;
                                }
                            }
                        }
                        #endregion

                        #region LOW
                        ////if (data.ivperDiff < 5)
                        //{
                        //    //if (data.ivDiff != 0)
                        //    {
                        //        if (data.isFirstTimeLow)
                        //        {
                        //          //  if (data.ivperDiff != 0)
                        //            {
                        //               // if (data.ivperDiff < 5)
                        //                {
                        //                    data.prvAlertValueLow = data.ivperDiff - ArisApi_a._arisApi.SystemConfig.IVPLow;
                        //                    data.prvAlertFixedValueLow = data.ivperDiff;
                        //                    data.sym1PrvIv = data.IVP1;
                        //                    data.sym2PrvIv = data.IVP2;
                        //                    data.isFirstTimeLow = false;
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            double diff = (Math.Abs(data.ivperDiff - data.prvAlertFixedValueLow));

                        //            if (diff > ArisApi_a._arisApi.SystemConfig.IVPLow)
                        //            {
                        //                if (data.ivperDiff < data.prvAlertValueLow)//6
                        //                {
                                           
                        //                    if (data.IVP1 < data.IVP2)
                        //                    {
                        //                        data.sym1BS = "BUY";
                        //                        data.sym2BS = "SELL";
                        //                    }
                        //                    else
                        //                    {
                        //                        data.sym1BS = "SELL";
                        //                        data.sym2BS = "BUY";
                        //                    }
                        //                    string key = sym1 + "_" + sym2;
                        //                    AddAlert(time, key, data.Sym1Strike, data.sym1Iv, data.Sym2Strike, data.sym2Iv, data.prvAlertFixedValueLow, data.ivperDiff, data
                        //                        .sym1PrvIv, data.sym2PrvIv, data.sym1BS, data.sym2BS, data.IVP1, data.IVP2, dgvAlert, exp, "LOW");
                        //                    data.prvAlertValueLow = data.ivperDiff - ArisApi_a._arisApi.SystemConfig.IVPLow;
                        //                    data.prvAlertFixedValueLow = data.ivperDiff;
                        //                    data.sym1PrvIv = data.IVP1;
                        //                    data.sym2PrvIv = data.IVP2;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}

                        #endregion
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        //public void CheckForAlertNew(IvInfo data, string sym1, string sym2, string time, string exp)
        //{
        //    if (InvokeRequired)
        //        BeginInvoke((MethodInvoker)(() => CheckForAlertNew(data, sym1, sym2, time, exp)));
        //    else
        //    {
        //        try
        //        {

        //            if(data.ivperDiff != 0)
        //            {

        //                if (data.ivDiff != 0)
        //                {
        //                    data.sym1BS = ""; data.sym2BS = ""; data.Sym1Diff = 0; data.Sym2Diff = 0;

        //                    if (data.isFirstTimeHigh)
        //                    {

        //                        if (data.ivperDiff != 0)
        //                        {
        //                            if (data.ivperDiff > ArisApi_a._arisApi.SystemConfig.IVPHigh)
        //                            {
        //                                data.prvAlertValueHigh = data.ivDiff + ArisApi_a._arisApi.SystemConfig.IVPHigh;
        //                                data.prvAlertFixedValueHigh = data.ivDiff;
        //                                data.sym1PrvIv = data.sym1Iv;
        //                                data.sym2PrvIv = data.sym2Iv;
        //                                data.isFirstTimeHigh = false;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        double diff = (Math.Abs(data.ivDiff - data.prvAlertFixedValueHigh));

        //                        if (diff > ArisApi_a._arisApi.SystemConfig.IVPHigh)
        //                        {
        //                            if (data.ivDiff > data.prvAlertValueHigh)//6
        //                            {
        //                                if (data.sym1PrvIv != data.sym1Iv)
        //                                {
        //                                    data.Sym1Diff = Math.Abs(data.sym1PrvIv - data.sym1Iv);
        //                                    if (data.sym1Iv > data.sym1PrvIv)
        //                                    {
        //                                        data.sym1BS = "SELL";
        //                                    }
        //                                    else if (data.sym1Iv < data.sym1PrvIv)
        //                                    {
        //                                        data.sym1BS = "BUY";
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    data.sym1BS = "";
        //                                }

        //                                if (data.sym2PrvIv != data.sym2Iv)
        //                                {
        //                                    data.Sym2Diff = Math.Abs(data.sym2PrvIv - data.sym2Iv);
        //                                    if (data.sym2Iv > data.sym2PrvIv)
        //                                    {
        //                                        data.sym2BS = "SELL";
        //                                    }
        //                                    else if (data.sym2Iv < data.sym2PrvIv)
        //                                    {
        //                                        data.sym2BS = "BUY";
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    data.sym2BS = "";
        //                                }

        //                                if (data.sym1BS == "SELL" && data.sym2BS == "SELL")
        //                                {
        //                                    if (data.Sym1Diff > data.Sym2Diff)
        //                                    {
        //                                        data.sym1BS = "SELL";
        //                                        data.sym2BS = "BUY";
        //                                    }
        //                                    else
        //                                    {
        //                                        data.sym2BS = "SELL";
        //                                        data.sym1BS = "BUY";
        //                                    }
        //                                }

        //                                if (data.sym1BS == "BUY" && data.sym2BS == "BUY")
        //                                {
        //                                    if (data.Sym1Diff < data.Sym2Diff)
        //                                    {
        //                                        data.sym1BS = "BUY";
        //                                        data.sym2BS = "SELL";
        //                                    }
        //                                    else
        //                                    {
        //                                        data.sym2BS = "BUY";
        //                                        data.sym1BS = "SELL";
        //                                    }
        //                                }
        //                                string key = sym1 + "_" + sym2;
        //                                AddAlert(time, key, data.Sym1Strike, data.sym1Iv, data.Sym2Strike, data.sym2Iv, data.prvAlertFixedValueHigh, data.ivDiff,
        //                                    data.sym1PrvIv, data.sym2PrvIv, data.sym1BS, data.sym2BS, data.IVP1, data.IVP2, dgvAlert, exp, "HIGH");
        //                                data.prvAlertValueHigh = data.ivDiff + ArisApi_a._arisApi.SystemConfig.IVPHigh;
        //                                data.prvAlertFixedValueHigh = data.ivDiff;
        //                                data.sym1PrvIv = data.sym1Iv;
        //                                data.sym2PrvIv = data.sym2Iv;

        //                            }
        //                        }
        //                    }

        //                    if (data.isFirstTimeLow)
        //                    {
        //                        if (data.ivperDiff != 0)
        //                        {
        //                            if (data.ivperDiff < ArisApi_a._arisApi.SystemConfig.IVPLow)
        //                            {
        //                                data.prvAlertValueLow = data.ivDiff - ArisApi_a._arisApi.SystemConfig.IVPLow;
        //                                data.prvAlertFixedValueLow = data.ivDiff;
        //                                data.sym1PrvIv = data.sym1Iv;
        //                                data.sym2PrvIv = data.sym2Iv;
        //                                data.isFirstTimeLow = false;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        double diff = (Math.Abs(data.ivDiff - data.prvAlertFixedValueLow));

        //                        if (diff > ArisApi_a._arisApi.SystemConfig.IVPLow)
        //                        {
        //                            if (data.ivDiff < data.prvAlertValueLow)//6
        //                            {
        //                                if (data.sym1PrvIv != data.sym1Iv)
        //                                {
        //                                    if (data.sym1Iv > data.sym1PrvIv)
        //                                    {
        //                                        data.sym1BS = "SELL";
        //                                        data.Sym1Diff = Math.Abs(data.sym1PrvIv - data.sym1Iv);

        //                                    }
        //                                    else if (data.sym1Iv < data.sym1PrvIv)
        //                                    {
        //                                        data.sym1BS = "BUY";
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    data.sym1BS = "";
        //                                }

        //                                if (data.sym2PrvIv != data.sym2Iv)
        //                                {
        //                                    if (data.sym2Iv > data.sym2PrvIv)
        //                                    {
        //                                        data.sym2BS = "SELL";
        //                                        data.Sym2Diff = Math.Abs(data.sym2PrvIv - data.sym2Iv);
        //                                    }
        //                                    else if (data.sym2Iv < data.sym2PrvIv)
        //                                    {
        //                                        data.sym2BS = "BUY";
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    data.sym2BS = "";
        //                                }

        //                                if (data.sym1BS == "SELL" && data.sym2BS == "SELL")
        //                                {
        //                                    if (data.Sym1Diff > data.Sym2Diff)
        //                                    {
        //                                        data.sym1BS = "SELL";
        //                                        data.sym2BS = "BUY";
        //                                    }
        //                                    else
        //                                    {
        //                                        data.sym2BS = "SELL";
        //                                        data.sym1BS = "BUY";
        //                                    }
        //                                }

        //                                if (data.sym1BS == "BUY" && data.sym2BS == "BUY")
        //                                {
        //                                    if (data.Sym1Diff < data.Sym2Diff)
        //                                    {
        //                                        data.sym1BS = "BUY";
        //                                        data.sym2BS = "SELL";
        //                                    }
        //                                    else
        //                                    {
        //                                        data.sym2BS = "BUY";
        //                                        data.sym1BS = "SELL";
        //                                    }
        //                                }

        //                                string key = sym1 + "_" + sym2;
        //                                AddAlert(time, key, data.Sym1Strike, data.sym1Iv, data.Sym2Strike, data.sym2Iv, data.prvAlertFixedValueLow, data.ivDiff, data
        //                                    .sym1PrvIv, data.sym2PrvIv, data.sym1BS, data.sym2BS, data.IVP1, data.IVP2, dgvAlert, exp, "LOW");
        //                                data.prvAlertValueLow = data.ivDiff - ArisApi_a._arisApi.SystemConfig.IVPLow;
        //                                data.prvAlertFixedValueLow = data.ivDiff;
        //                                data.sym1PrvIv = data.sym1Iv;
        //                                data.sym2PrvIv = data.sym2Iv;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //}


        //public void CheckIVPAlert(double sym1iv, double sym2iv,double iv_diff,string sym1, string sym2, string time, string exp)
        //{
        //    try
        //    {
        //        string pair = sym1 + "_" + sym2;
        //        if (SymbolPairIvpRankMap.ContainsKey(pair))
        //        {
        //            IVPInfo data = SymbolPairIvpRankMap[pair];
        //            //HIGH
        //            #region HIGH IVP
        //            if (data.isFirstHigh)
        //            {
        //                double sym1ivp = 0; double sym2ivp = 0; double ivp_diff = 0;
        //                if (symbolIvPMap.ContainsKey(sym1))
        //                {
        //                    sym1ivp = symbolIvPMap[sym1].per;
        //                }
        //                if (symbolIvPMap.ContainsKey(sym2))
        //                {
        //                    sym2ivp = symbolIvPMap[sym2].per;
        //                }
        //                if (sym1ivp != 0 && sym2ivp != 0)
        //                {
        //                    ivp_diff = Math.Abs(sym1ivp - sym2ivp);
        //                    data.iv_diff = ivp_diff;
        //                    data.sym1Ivp = sym1ivp;
        //                    data.sym2Ivp = sym2ivp;
        //                }
        //                if (data.iv_diff != 0)
        //                {
        //                    if (data.iv_diff >= ArisApi_a._arisApi.SystemConfig.IVPHigh)//17
        //                    {
        //                        AddIVPAlert(time, pair, ArisApi_a._arisApi.SystemConfig.IVPHigh, data.iv_diff, data.sym1Ivp, data.sym2Ivp, dgvAlert, exp, "HIGH");
        //                        data.prvAlertValueHigh = data.iv_diff + ArisApi_a._arisApi.SystemConfig.IVPHigh;//32
        //                        data.prvAlertFixedValueHigh = data.iv_diff;//17
        //                        data.isFirstHigh = false;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                data.sym1Ivp = sym1iv;
        //                data.sym2Ivp = sym2iv;
        //                data.iv_diff = Math.Abs(iv_diff);
        //                double diff = Math.Abs(data.iv_diff - data.prvAlertFixedValueHigh);//35-20=15
        //                if (diff >= ArisApi_a._arisApi.SystemConfig.IVPHigh)//15 >15
        //                {
        //                    if (data.iv_diff > data.prvAlertValueHigh)//50 >32
        //                    {
        //                        AddIVPAlert(time, pair, data.prvAlertFixedValueHigh, data.iv_diff, data.sym1Ivp, data.sym2Ivp, dgvAlert, exp, "HIGH");
        //                        data.prvAlertValueHigh = data.iv_diff + ArisApi_a._arisApi.SystemConfig.IVPHigh;
        //                        data.prvAlertFixedValueHigh = data.iv_diff;
        //                    }
        //                }
        //            }
                    
        //            #endregion

        //            //LOW
        //            #region LOW IVP
        //            if (data.isFirstLow)
        //            {
        //                if (data.iv_diff != 0)
        //                {
        //                    if (data.iv_diff < ArisApi_a._arisApi.SystemConfig.IVPLow)//5
        //                    {
        //                        AddIVPAlert(time, pair, ArisApi_a._arisApi.SystemConfig.IVPLow, data.iv_diff, data.sym1Ivp, data.sym2Ivp, dgvAlert, exp, "LOW");
        //                        data.prvAlertValueLow = data.iv_diff - ArisApi_a._arisApi.SystemConfig.IVPLow;//32
        //                        data.prvAlertFixedValueLow = data.iv_diff;//17
        //                        data.isFirstLow = false;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                data.sym1Ivp = sym1iv;
        //                data.sym2Ivp = sym2iv;
        //                data.iv_diff = Math.Abs(iv_diff);//4
        //                double diff = Math.Abs(data.iv_diff - data.prvAlertFixedValueLow);
        //                if (diff > ArisApi_a._arisApi.SystemConfig.IVPLow)//18>15
        //                {
        //                    if (data.iv_diff < data.prvAlertValueLow)//50 >32
        //                    {
        //                        AddIVPAlert(time, pair, data.prvAlertFixedValueLow, data.iv_diff, data.sym1Ivp, data.sym2Ivp, dgvAlert, exp, "LOW");
        //                        data.prvAlertValueLow = data.iv_diff - ArisApi_a._arisApi.SystemConfig.IVPLow;
        //                        data.prvAlertFixedValueLow = data.iv_diff;
        //                    }
        //                }
        //            } 
        //            #endregion
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        //public void CheckForIVPAlert(double ivdiff,string sym1, string sym2)
        //{
        //    try
        //    {
        //        if (ivdiff != 0)
        //        {
        //            string pair = sym1 + "_" + sym2;
        //            if (SymbolPairIvpRankMap.ContainsKey(pair))
        //            {
                        
        //                if (symbolPairIvMap[pair].isFirstTimeHigh)
        //                {
        //                    double sym1ivp = 0; double sym2ivp = 0; double ivp_diff = 0;
        //                    if (symbolIvPMap.ContainsKey(sym1))
        //                    {
        //                        sym1ivp = symbolIvPMap[sym1].per;
        //                    }
        //                    if (symbolIvPMap.ContainsKey(sym2))
        //                    {
        //                        sym2ivp = symbolIvPMap[sym2].per;
        //                    }
        //                    ivp_diff = Math.Abs(sym1ivp - sym2ivp);
        //                    SymbolPairIvpRankMap[pair].iv_diff = ivp_diff;

        //                    if (SymbolPairIvpRankMap[pair].iv_diff > ArisApi_a._arisApi.SystemConfig.IVPHigh)
        //                    {
        //                        symbolPairIvMap[pair].isFirstTimeHigh = false;
        //                    }
        //                }
        //                else
        //                {
        //                    SymbolPairIvpRankMap[pair].iv_diff = ivdiff;
        //                    if(SymbolPairIvpRankMap[pair].iv_diff >)
                           
        //                }


        //                if (symbolPairIvMap[pair].isFirstTimeLow)
        //                {
        //                    if (SymbolPairIvpRankMap[pair].iv_diff < ArisApi_a._arisApi.SystemConfig.IVPLow)
        //                    {
        //                        symbolPairIvMap[pair].isFirstTimeLow = false;
        //                    }
        //                }
        //                else
        //                { 

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}



        //public void CheckForAlert(IvInfo data,string sym1, string sym2, string time)
        //{
        //    try
        //    {
        //        if (data.ivDiff != 0)
        //        {
        //            if (data.isFirstTimeHigh)
        //            {
        //                if (sym1 == "NIFTY" || sym1 == "BANKNIFTY")
        //                {
        //                    data.prvAlertValueHigh = data.ivDiff + ArisApi_a._arisApi.SystemConfig.AlertIntervalIndex;
        //                }
        //                else
        //                {
        //                    data.prvAlertValueHigh = data.ivDiff + ArisApi_a._arisApi.SystemConfig.AlertIntervalStock;
        //                }
        //                data.prvAlertFixedValueHigh = data.ivDiff;
        //                data.isFirstTimeHigh = false;
        //            }
        //            else
        //            {
        //                double diff = (Math.Abs(data.ivDiff - data.prvAlertFixedValueHigh));
        //                if (sym1 == "NIFTY" || sym1 == "BANKNIFTY")
        //                {
        //                    if (diff > ArisApi_a._arisApi.SystemConfig.AlertIntervalIndex)
        //                    {
        //                        if (data.ivDiff > data.prvAlertValueHigh)//6
        //                        {
        //                            if (data.sym1PrvIv != data.sym1Iv)
        //                            {
        //                                if (data.sym1Iv > data.sym1PrvIv)
        //                                {
        //                                    data.sym1BS = "SELL";
        //                                }
        //                                else if (data.sym1Iv < data.sym1PrvIv)
        //                                {
        //                                    data.sym1BS = "BUY";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                data.sym1BS = "";
        //                            }

        //                            if (data.sym2PrvIv != data.sym2Iv)
        //                            {
        //                                if (data.sym2Iv > data.sym2PrvIv)
        //                                {
        //                                    data.sym2BS = "SELL";
        //                                }
        //                                else if (data.sym2Iv < data.sym2PrvIv)
        //                                {
        //                                    data.sym2BS = "BUY";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                data.sym2BS = "";
        //                            }

                                    

        //                            string key = sym1 + "_" + sym2;
        //                            AddAlert(time, key, data.Sym1Strike, data.sym1Iv, data.Sym2Strike, data.sym2Iv, data.prvAlertFixedValueHigh, data.ivDiff,
        //                                data.sym1PrvIv, data.sym2PrvIv, data.sym1BS, data.sym2BS, dgvAlertHigh);
        //                            //Console.WriteLine(key + "," + data.Sym1Strike + "," + data.sym1Iv + "," + data.Sym2Strike + "," + data.sym2Iv + "," + data.prvAlertFixedValueHigh + "," + data.ivDiff);
        //                            data.prvAlertValueHigh = data.ivDiff + ArisApi_a._arisApi.SystemConfig.AlertIntervalIndex;
        //                            data.prvAlertFixedValueHigh = data.ivDiff;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (diff > ArisApi_a._arisApi.SystemConfig.AlertIntervalStock)
        //                    {
        //                        if (data.ivDiff > data.prvAlertValueHigh)//6
        //                        {
        //                            if (data.sym1PrvIv != data.sym1Iv)
        //                            {
        //                                if (data.sym1Iv > data.sym1PrvIv)
        //                                {
        //                                    data.sym1BS = "SELL";
        //                                }
        //                                else if (data.sym1Iv < data.sym1PrvIv)
        //                                {
        //                                    data.sym1BS = "BUY";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                data.sym1BS = "";
        //                            }

        //                            if (data.sym2PrvIv != data.sym2Iv)
        //                            {
        //                                if (data.sym2Iv > data.sym2PrvIv)
        //                                {
        //                                    data.sym2BS = "SELL";
        //                                }
        //                                else if (data.sym2Iv < data.sym2PrvIv)
        //                                {
        //                                    data.sym2BS = "BUY";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                data.sym2BS = "";
        //                            }
                                    

        //                            string key = sym1 + "_" + sym2;
        //                            AddAlert(time, key, data.Sym1Strike, data.sym1Iv, data.Sym2Strike, data.sym2Iv, data.prvAlertFixedValueHigh, data.ivDiff, data
        //                                .sym1PrvIv, data.sym2PrvIv, data.sym1BS, data.sym2BS, dgvAlertHigh);
        //                            //Console.WriteLine(key + "," + data.Sym1Strike + "," + data.sym1Iv + "," + data.Sym2Strike + "," + data.sym2Iv + "," + data.prvAlertFixedValueHigh + "," + data.ivDiff);
        //                            data.prvAlertValueHigh = data.ivDiff + ArisApi_a._arisApi.SystemConfig.AlertIntervalStock;
        //                            data.prvAlertFixedValueHigh = data.ivDiff;
        //                        }
        //                    }
        //                }
        //            }

        //            if (data.isFirstTimeLow)
        //            {
        //                if (sym1 == "NIFTY" || sym1 == "BANKNIFTY")
        //                {
        //                    data.prvAlertValueLow = data.ivDiff - ArisApi_a._arisApi.SystemConfig.AlertIntervalIndex;
        //                }
        //                else
        //                {
        //                    data.prvAlertValueLow = data.ivDiff - ArisApi_a._arisApi.SystemConfig.AlertIntervalStock;
        //                }
        //                data.prvAlertFixedValueLow = data.ivDiff;
        //                data.isFirstTimeLow = false;
        //            }
        //            else
        //            {
        //                double diff = (Math.Abs(data.ivDiff - data.prvAlertFixedValueLow));
        //                if (sym1 == "NIFTY" || sym1 == "BANKNIFTY")
        //                {
        //                    if (diff > ArisApi_a._arisApi.SystemConfig.AlertIntervalIndex)
        //                    {
        //                        if (data.ivDiff < data.prvAlertValueLow)//6
        //                        {
        //                            if (data.sym1PrvIv != data.sym1Iv)
        //                            {
        //                                if (data.sym1Iv > data.sym1PrvIv)
        //                                {
        //                                    data.sym1BS = "SELL";
        //                                }
        //                                else if (data.sym1Iv < data.sym1PrvIv)
        //                                {
        //                                    data.sym1BS = "BUY";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                data.sym1BS = "";
        //                            }

        //                            if (data.sym2PrvIv != data.sym2Iv)
        //                            {
        //                                if (data.sym2Iv > data.sym2PrvIv)
        //                                {
        //                                    data.sym2BS = "SELL";
        //                                }
        //                                else if (data.sym2Iv < data.sym2PrvIv)
        //                                {
        //                                    data.sym2BS = "BUY";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                data.sym2BS = "";
        //                            }
                                    

        //                            string key = sym1 + "_" + sym2;
        //                            AddAlert(time, key, data.Sym1Strike, data.sym1Iv, data.Sym2Strike, data.sym2Iv, data.prvAlertFixedValueLow, data.ivDiff, data
        //                                .sym1PrvIv, data.sym2PrvIv, data.sym1BS, data.sym2BS, dgvAlertLow);
        //                            //Console.WriteLine(key + "," + data.Sym1Strike + "," + data.sym1Iv + "," + data.Sym2Strike + "," + data.sym2Iv + "," + data.prvAlertFixedValueLow + "," + data.ivDiff);
        //                            data.prvAlertValueLow = data.ivDiff - ArisApi_a._arisApi.SystemConfig.AlertIntervalIndex;
        //                            data.prvAlertFixedValueLow = data.ivDiff;
        //                        }
        //                    }
        //                }
        //                else
        //                {

        //                    if (diff > ArisApi_a._arisApi.SystemConfig.AlertIntervalStock)
        //                    {
        //                        if (data.ivDiff < data.prvAlertValueLow)//6
        //                        {
        //                            if (data.sym1PrvIv != data.sym1Iv)
        //                            {
        //                                if (data.sym1Iv > data.sym1PrvIv)
        //                                {
        //                                    data.sym1BS = "SELL";
        //                                }
        //                                else if (data.sym1Iv < data.sym1PrvIv)
        //                                {
        //                                    data.sym1BS = "BUY";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                data.sym1BS = "";
        //                            }

        //                            if (data.sym2PrvIv != data.sym2Iv)
        //                            {
        //                                if (data.sym2Iv > data.sym2PrvIv)
        //                                {
        //                                    data.sym2BS = "SELL";
        //                                }
        //                                else if (data.sym2Iv < data.sym2PrvIv)
        //                                {
        //                                    data.sym2BS = "BUY";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                data.sym2BS = "";
        //                            }
                                    

        //                            string key = sym1 + "_" + sym2;
        //                            AddAlert(time, key, data.Sym1Strike, data.sym1Iv, data.Sym2Strike, data.sym2Iv, data.prvAlertFixedValueLow, data.ivDiff, data
        //                                .sym1PrvIv, data.sym2PrvIv, data.sym1BS, data.sym2BS, dgvAlertLow);
        //                            //Console.WriteLine(key + "," + data.Sym1Strike + "," + data.sym1Iv + "," + data.Sym2Strike + "," + data.sym2Iv + "," + data.prvAlertFixedValueLow + "," + data.ivDiff);
        //                            data.prvAlertValueLow = data.ivDiff - ArisApi_a._arisApi.SystemConfig.AlertIntervalStock;
        //                            data.prvAlertFixedValueLow = data.ivDiff;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        public void AddIVPAlert(string time, string symPair, double prvVal, double currVal,double sym1ivp,double sym2ivp ,DataGridView dgv, string exp, string highlow)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)(() => AddIVPAlert(time, symPair, prvVal, currVal,sym1ivp,sym2ivp ,dgv, exp, highlow)));
            else
            {
                try
                {
                    string[] syms = symPair.Split('_');
                    if (dgv.Rows.Count > 200)
                    {
                        dgv.Rows.RemoveAt(dgv.Rows.Count - 1);
                    }
                    DataGridViewRow row = new DataGridViewRow();
                    dgv.Rows.Insert(0, row);
                    dgv.Rows[0].Cells[WatchConst.Time].Value = time;
                    dgv.Rows[0].Cells[WatchConst.HighLow].Value = highlow;
                    dgv.Rows[0].Cells[WatchConst.Symbol].Value = symPair;
                    DateTime expiryDate = DateTime.ParseExact(exp, "yyyyMMdd", CultureInfo.InvariantCulture);
                    dgv.Rows[0].Cells[WatchConst.Expiry].Value = expiryDate.ToString("ddMMMyyyy");
                    dgv.Rows[0].Cells[WatchConst.PrvAlert].Value = prvVal;
                    dgv.Rows[0].Cells[WatchConst.CurrAlert].Value = currVal;
                    dgv.Rows[0].Cells[WatchConst.L1Iv].Value = sym1ivp;
                    dgv.Rows[0].Cells[WatchConst.L2Iv].Value = sym2ivp;
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void AddAlert(string time, string symPair, double l1Strike, double l1Iv, double l2Strike, double l2Iv, double prvVal, double currVal, double ivDiff1,double ivDiff2,string l1bs, string l2bs,double ivp1,double ivp2, DataGridView dgv,string exp,string highlow,Color clr)
        {
            if (InvokeRequired)
                BeginInvoke((MethodInvoker)(() => AddAlert(time, symPair, l1Strike, l1Iv, l2Strike, l2Iv, prvVal, currVal, ivDiff1, ivDiff2,l1bs, l2bs,ivp1,ivp2, dgv,exp,highlow,clr)));
            else
            {
                try
                {
                    string[] syms = symPair.Split('_');
                    if (dgv.Rows.Count > 200)
                    {
                        dgv.Rows.RemoveAt(dgv.Rows.Count - 1);
                    }
                    DateTime expiryDate = DateTime.ParseExact(exp, "yyyyMMdd", CultureInfo.InvariantCulture);
                    if (l1bs == "BUY")
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        dgv.Rows.Insert(0, row);
                        dgv.Rows[0].Cells[WatchConst.Time].Value = time;
                        dgv.Rows[0].Cells[WatchConst.HighLow].Value = highlow;
                        dgv.Rows[0].Cells[WatchConst.Symbol].Value = symPair;

                        dgv.Rows[0].Cells[WatchConst.Symbol].Style.BackColor = clr;
                        string NextExpiry = Convert.ToDateTime(ATM.atm.threeExpiry[1]).ToString("ddMMMyyyy");
                        if (NextExpiry == expiryDate.ToString("ddMMMyyyy"))
                            dgv.Rows[0].Cells[WatchConst.Expiry].Style.BackColor = Color.Red;

                        dgv.Rows[0].Cells[WatchConst.Expiry].Value = expiryDate.ToString("ddMMMyyyy");
                        dgv.Rows[0].Cells[WatchConst.L1Strike].Value = l1Strike;
                        dgv.Rows[0].Cells[WatchConst.L1Iv].Value = l1Iv;
                        dgv.Rows[0].Cells[WatchConst.L1PrvIv].Value = ivDiff1;
                        dgv.Rows[0].Cells[WatchConst.L1BuyOrSell].Value = l1bs;
                        dgv.Rows[0].Cells[WatchConst.L2Strike].Value = l2Strike;
                        dgv.Rows[0].Cells[WatchConst.L2Iv].Value = l2Iv;
                        dgv.Rows[0].Cells[WatchConst.L2PrvIv].Value = ivDiff2;
                        dgv.Rows[0].Cells[WatchConst.L1IVP].Value = ivp1;
                        dgv.Rows[0].Cells[WatchConst.L2IVP].Value = ivp2;
                        dgv.Rows[0].Cells[WatchConst.L2BuyOrSell].Value = l2bs;
                        dgv.Rows[0].Cells[WatchConst.PrvAlert].Value = prvVal;
                        dgv.Rows[0].Cells[WatchConst.CurrAlert].Value = currVal;
                    }
                    else
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        dgv.Rows.Insert(0, row);
                        dgv.Rows[0].Cells[WatchConst.Time].Value = time;
                        dgv.Rows[0].Cells[WatchConst.HighLow].Value = highlow;
                        dgv.Rows[0].Cells[WatchConst.Symbol].Value = syms[1] + "_" + syms[0];
                        dgv.Rows[0].Cells[WatchConst.Symbol].Style.BackColor = clr;
                        string NextExpiry = Convert.ToDateTime(ATM.atm.threeExpiry[1]).ToString("ddMMMyyyy");
                        if (NextExpiry == expiryDate.ToString("ddMMMyyyy"))
                            dgv.Rows[0].Cells[WatchConst.Expiry].Style.BackColor = Color.Red;
                        dgv.Rows[0].Cells[WatchConst.Expiry].Value = expiryDate.ToString("ddMMMyyyy");
                        dgv.Rows[0].Cells[WatchConst.L1Strike].Value = l2Strike;
                        dgv.Rows[0].Cells[WatchConst.L1Iv].Value = l2Iv;
                        dgv.Rows[0].Cells[WatchConst.L1PrvIv].Value = ivDiff2;
                        dgv.Rows[0].Cells[WatchConst.L1BuyOrSell].Value = l2bs;
                        dgv.Rows[0].Cells[WatchConst.L2Strike].Value = l1Strike;
                        dgv.Rows[0].Cells[WatchConst.L2Iv].Value = l1Iv;
                        dgv.Rows[0].Cells[WatchConst.L2PrvIv].Value = ivDiff1;
                        dgv.Rows[0].Cells[WatchConst.L1IVP].Value = ivp2;
                        dgv.Rows[0].Cells[WatchConst.L2IVP].Value = ivp1;
                        dgv.Rows[0].Cells[WatchConst.L2BuyOrSell].Value = l1bs;
                        dgv.Rows[0].Cells[WatchConst.PrvAlert].Value = prvVal;
                        dgv.Rows[0].Cells[WatchConst.CurrAlert].Value = currVal;
                    }

                    dgv.ClearSelection();
                    alert_dataWriter.WriteLine(time + "," + symPair + "," + expiryDate.ToString("ddMMMyyyy")+ "," + l1Strike + "," + l1Iv + "," + 
                         "," + ivDiff1+ "," + l1bs + "," + l2Strike + "," + l2Iv + "," + "," + ivDiff2 + "," + l2bs + "," + ","+ prvVal + "," + currVal + ","+ ivp1 + ","+ ivp2);                
                }
                catch (Exception)
                {


                }
            }
        }

        private void GenerateColumn(string clName, MTEnums.FieldType fieldType, bool Editable, DataGridView dgv, int width)
        {
            dgv.Columns.Add(clName, clName);
            dgv.Columns[clName].ReadOnly = Editable;

            switch (fieldType)
            {
                case MTEnums.FieldType.None:
                    break;
                case MTEnums.FieldType.Date:
                    dgv.Columns[clName].DefaultCellStyle.Format = MTConstant.DateFormatGrid;
                    dgv.Columns[clName].Width = width;
                    break;
                case MTEnums.FieldType.Time:
                    dgv.Columns[clName].DefaultCellStyle.Format = MTConstant.TimeFormatGrid;
                    dgv.Columns[clName].Width = width;
                    break;
                case MTEnums.FieldType.Price:
                    dgv.Columns[clName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Columns[clName].Width = width;
                    break;
                case MTEnums.FieldType.Quantity:
                    dgv.Columns[clName].DefaultCellStyle.Format = "0.00";
                    dgv.Columns[clName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Columns[clName].Width = width;
                    break;
                case MTEnums.FieldType.Percentage:
                    dgv.Columns[clName].DefaultCellStyle.Format = "0.00%";
                    dgv.Columns[clName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Columns[clName].Width = width;
                    break;
                case MTEnums.FieldType.Indicator:
                    dgv.Columns[clName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[clName].Width = width;
                    break;
                case MTEnums.FieldType.DateTime:
                    break;
            }

        }

        private void GenerateColumns(DataGridView dgv)
        {
            try
            {
                GenerateColumn(WatchConst.Time, MTEnums.FieldType.Quantity, true, dgv,50);
                GenerateColumn(WatchConst.HighLow, MTEnums.FieldType.Quantity, true, dgv, 30);
                GenerateColumn(WatchConst.Symbol, MTEnums.FieldType.None, true, dgv,220);
                GenerateColumn(WatchConst.Expiry, MTEnums.FieldType.Price, true, dgv,60);
                GenerateColumn(WatchConst.L1Strike, MTEnums.FieldType.Price, true, dgv,60);
                GenerateColumn(WatchConst.L1Iv, MTEnums.FieldType.Price, true, dgv,50);
                GenerateColumn(WatchConst.L1PrvIv, MTEnums.FieldType.Price, true, dgv, 30);
                //GenerateColumn(WatchConst.L1Rank, MTEnums.FieldType.Price, true, dgv, 50);
                GenerateColumn(WatchConst.L1IVP, MTEnums.FieldType.Price, true, dgv, 50);
                GenerateColumn(WatchConst.L1BuyOrSell, MTEnums.FieldType.Price, true, dgv, 30);
                GenerateColumn(WatchConst.L2Strike, MTEnums.FieldType.Price, true, dgv,60);
                GenerateColumn(WatchConst.L2Iv, MTEnums.FieldType.Price, true, dgv,50);
                GenerateColumn(WatchConst.L2PrvIv, MTEnums.FieldType.Price, true, dgv, 30);
               // GenerateColumn(WatchConst.L2Rank, MTEnums.FieldType.Price, true, dgv, 50);
                GenerateColumn(WatchConst.L2IVP, MTEnums.FieldType.Price, true, dgv, 50);
                GenerateColumn(WatchConst.L2BuyOrSell, MTEnums.FieldType.Price, true, dgv, 30);
                GenerateColumn(WatchConst.PrvAlert, MTEnums.FieldType.Price, true, dgv,50);
                GenerateColumn(WatchConst.CurrAlert, MTEnums.FieldType.Price, true, dgv,50); 
            }
            catch (Exception)
            {

            }
        }
    }

    public class IvInfo
    {
        public bool isFirstTimeHigh=true;
        public bool isFirstTimeLow = true;
        public double IVP1;
        public double IVP2;
        public double Sym1Diff;
        public double Sym2Diff;
        public double sym1PrvIv;
        public double sym2PrvIv;
        public double sym1Iv;
        public double sym2Iv;
        public double Sym1Strike;
        public double Sym2Strike;
        public double ivDiff;
        public double ivperDiff;
        public double prvAlertValueHigh;
        public double prvAlertValueLow;
        public double prvAlertFixedValueHigh;
        public double prvAlertFixedValueLow;
        public string sym1BS;
        public string sym2BS;
    }

    public class IVPInfo
    {
        public bool isFirstHigh =true;
        public bool isFirstLow=true;
        public double currAlertLow;
        public double currAlertHigh;
        public double prvHigh;
        public double prvLow;
        public double sym1Ivp;
        public double sym2Ivp;
        public double iv_diff;
        public double prvAlertValueHigh;
        public double prvAlertValueLow;
        public double prvAlertFixedValueHigh;
        public double prvAlertFixedValueLow;
    }

    public class IvRankPer
    {
    public double rank;
        public double per;
    }

    public class AtmInfo
    {
        public double atmStrike;
        public int CeToken;
        public int PeToken;
    }

    public class Arguments
    {
        public string  expiry;
        public string time;
    }

    public class IvInfoForIvp
    {
    public List<double> list;
        public int count;
    }
}
