using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;
using ClientCommon;
using LogWriter;
using MTApi;
using System.IO;
using MTCommon;

namespace LegLead
{
    [Serializable]
    public class MarketWatch
    {
        #region CashFuture Trader
        public Leg Leg1;

        public double AtmIVCe;

        public string alert;
        public DateTime highlight_timer_iv;
        public DateTime highlight_timer_price;
        public int SeqaureOff;

        public Leg niftyLeg;
       
        public double symdiff;
        public int Ruleno;
        public double SDiff;

        public string Expiry;
        public double strikediff;
        public int AtmToken;
        public int AtmTokenPe;
        public int AtmTokenPE;
        public double HighStrike;
        public double LowStrike;
        
        public double AtmStrike;
        public double AtmLtp;
        public string Selected_exp;
        public double NextAtmStrike;
        public double NextAtmLtp;
        public int NextAtmToken;

        public double FarAtmStrike;
        public double FarAtmLtp;
        public int FarAtmToken;

        public double NextFutLtp;
        public double FarFutLtp;
        public string Sector;
        public string Ot1;
        public string Ot2;
        public double delta1;
        public double delta2;
       
        public double prv_iv_diff_low;
        public double prv_iv_diff_high;
        public double iv_diff;
        public double alert_diff;


        [XmlIgnore]
        public DataGridViewRow RowData;

        #endregion

        #region Read/Write
        public static void WriteXmlProfile(ref List<MarketWatch> watch)
        {
            try
            {

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<MarketWatch>));
                StreamWriter streamWriter = new StreamWriter(MTClientEnvironment.SpecialFolder.CurrentDirectory + AppGlobal.Watch + ".tst");

                string date = DateTime.Now.ToString("ddMMMyyyy");
                StreamWriter streamWriterDaily = new StreamWriter(MTClientEnvironment.SpecialFolder.CurrentDirectory + AppGlobal.Watch + date + ".tst");                


                xmlSerializer.Serialize(streamWriter, watch);
                xmlSerializer.Serialize(streamWriterDaily, watch);
                streamWriter.Close();
                streamWriterDaily.Close();
            }
            catch (Exception)
            {
                TransactionWatch.ErrorMessage("WriteXmlProfile error occured");
                //Program._form.WriteToTransactionWatch(MTMethods.GetErrorMessage(ex, "WriteXmlProfile")
                //                          , LogEnums.WriteOption.LogWindow_ErrorLogFile, color: AppLog.RedColor);
            }
        }

        

        public static List<MarketWatch> ReadXmlProfile()
        {
            List<MarketWatch> Result = new List<MarketWatch>();
            try
            {
                if (File.Exists(MTClientEnvironment.SpecialFolder.CurrentDirectory + AppGlobal.Watch + ".tst"))
                {
                    FileStream fileStream = null;
                    try
                    {
                        fileStream = new FileStream(MTClientEnvironment.SpecialFolder.CurrentDirectory + AppGlobal.Watch + ".tst", FileMode.Open);
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<MarketWatch>));
                        return Result = (List<MarketWatch>)xmlSerializer.Deserialize(fileStream);
                    }
                    catch (Exception)
                    {
                        Result = new List<MarketWatch>();
                        Result[0] = new MarketWatch();
                        return Result;
                    }
                    finally
                    {
                        if (fileStream != null)
                            fileStream.Close();
                    }
                }
            }
            catch (Exception)
            {
                TransactionWatch.ErrorMessage("ReadXmlProfile error occured");
                //Program._form.WriteToTransactionWatch(MTMethods.GetErrorMessage(ex, "ReadXmlProfile")
                //                           , LogEnums.WriteOption.LogWindow_ErrorLogFile, color: AppLog.RedColor);
            }
            return Result;
        }

        #endregion

    }

    [Serializable]
    public class Leg
    {
        public uint GatewayId;
        public MTPackets.ContractInformation ContractInfo;
        public ContractDetails ContDetail = new ContractDetails();

        public double LastTradedPrice;

        [XmlIgnore()]
        public MTBCastPackets.MarketPicture MarketPicture;

        [XmlIgnore]
        public DataGridViewRow RowData;

        [XmlIgnore]
        public double futPriceOptionTrade;

        [XmlIgnore]
        public double PrevTotalTradeQty;
    }
   
}
