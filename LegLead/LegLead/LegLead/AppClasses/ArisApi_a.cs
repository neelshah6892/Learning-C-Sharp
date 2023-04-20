using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO.Compression;
using MTApi;
using System.Globalization;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LegLead.AppClasses
{
    public class ArisApi_a
    {
        public static ArisApi_a _arisApi;

        static ArisApi_a()
        {
            _arisApi = new ArisApi_a();
        }

        public bool InitializeAPI()
        {
            try
            {
                _errorFileLock = new object();
                _transactionFileLock = new object();
                _TradeFileLock = new object();
                _CountTrade = new object();
                _ByteLog = new object();
                _fillerLock = new object();
                SystemConfig = new SystemConfiguration();
                ReadSystemConfiguration();
                ReadContract();
                GenerateLogFiles();
                initializeDisruptors();
            }
            catch (FileNotFoundException)
            {
                return CreateNewSystemConfiguration();
            }
            catch (XmlException)
            {
                MessageBox.Show("System configuration file had invalid data.", "API", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to access the System configuration file.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                return false;
            }
            return true;
        }

        

        internal void OnDownloadComplete()
        {
            OnSystemUpdate("LoadPreviousOrder");
        }

        internal void OnMarketDepthUpdateProcess(MTBCastPackets.MarketPicture _marketPic , int time)
        {
            try
            {
                OnMarketDepthUpdate(_marketPic,time);
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }




        internal void OnIndexBroadCastProcess(NseCmApi.Broadcast.Indices _marketPic)
        {
            try
            {
                //OnIndexBroadCast(_marketPic);
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }



        internal void OnLogonStatusChangedProcess(uint Gateway, bool _isLoggonOn, string _reason)
        {
            try
            {
                OnLogonStatusChanged(Gateway, _isLoggonOn, _reason);
            }
            catch (Exception)
            {
            }
        }

        internal void OnSystemUpdateProcess(string message)
        {
            try
            {
            if (OnSystemUpdate != null)
                OnSystemUpdate(message);
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        #region Methods

        internal void ReadSystemConfiguration()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SystemConfiguration));
            FileStream fileStream = new FileStream(FilePath, FileMode.Open);
            SystemConfig = xmlSerializer.Deserialize(fileStream) as SystemConfiguration;
            fileStream.Close();
        }

        internal bool CreateNewSystemConfiguration()
        {
            try
            {
                SystemConfig.ApplicationName = "LegLead";
                SystemConfig.Version = "1.0.0";
                SystemConfig.NseCmBroadcastIp = "233.1.2.5";
                SystemConfig.NseCmBroadcastPort = 34074;
                SystemConfig.NseFoBroadcastIp = "233.1.2.5";
                SystemConfig.NseFoBroadcastPort = 34330;
                SystemConfig.Gateway = "172.16.2.91";
                SystemConfig.IndexIntervalAboveTwenty = 1;
                SystemConfig.IndexIntervalTenToTwenty = 1.25;
                SystemConfig.IndexIntervalFiveToTen = 1.5;
                SystemConfig.IndexIntervalBelowFive = 2;
                SystemConfig.StockIntervalAboveTwenty = 3;
                SystemConfig.StockIntervalTenToTwenty = 4;
                SystemConfig.StockIntervalFiveToTen = 4.5;
                SystemConfig.StockIntervalBelowFive = 5;
                SystemConfig.SymbolPairFilePath = "D:\\nseContractFile\\SymbolSector.csv";
                SystemConfig.SymboldiffFilePath = "D:\\Symbol_Diff\\symboldiff.txt";
                SystemConfig.Iv_PercentileFilePath = "D:\\DATABASETABLE\\implied_volatility_rank_percentile.csv";
                SystemConfig.LogFilePath = "D:\\LegLeadLogs\\";
                SystemConfig.IVPHigh = 15;
                SystemConfig.IVPLow = 5;
                SystemConfig.IvPastData = "D:\\DATABASETABLE\\iv_past_data.csv";
                SaveSystemConfiguration();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to access the System configuration file.", SystemConfig.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                return false;
            }
            return true;
        }

        internal bool SaveSystemConfiguration()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SystemConfiguration));
                StreamWriter streamWriter = new StreamWriter(FilePath);
                xmlSerializer.Serialize(streamWriter, SystemConfig);
                streamWriter.Close();
            }
            catch (XmlException)
            {
                MessageBox.Show("Unable to save data to System configuration file.", "API", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to access the System configuration file.", "API", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                return false;
            }
            return true;
        }

        internal void GenerateLogFiles()
        {
            try
            {
                string path = Application.StartupPath + "\\" + "Logs" + "\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string date = DateTime.Now.ToString("ddMMMyyyy") + ".txt";

                string dateError = DateTime.Now.ToString("ddMMMyyyy_hh_mm_ss") + ".txt";

                string fileName = path + "ErrorLog" + "-" + dateError;
                _errorLog = new StreamWriter(fileName, true);
                _errorLog.AutoFlush = true;

                //fileName = path + "TransactionLog" + "-" + date;
                //_transactionLog = new StreamWriter(fileName, true);
                //_transactionLog.AutoFlush = true;

                //string file_name = path + "byteLog" + "-" + date;
                //_byteLog = new StreamWriter(file_name, true);
                //_byteLog.AutoFlush = true;

                //string namefile = path + "messageLog" + "-" + date;
                //_messageLog = new StreamWriter(namefile, true);
                //_messageLog.AutoFlush = true;

                
            }
            catch (Exception)
            { }
        }


        internal void GenerateTradeFiles()
        {
            try
            {
                //string path = Application.StartupPath + "\\" + "Logs" + "\\";
                //if (!Directory.Exists(path))
                //    Directory.CreateDirectory(path);
                //string date = DateTime.Now.ToString("ddMMMyyyy") + ".txt";

                //string fileName = path + ArisApi_a._arisApi.SystemConfig.ApplicationName.ToString() + "TradeLog" +  ArisApi_a._arisApi.SystemConfig.UserName.ToString() + "-" + date;
                //_TradeLog = new StreamWriter(fileName, true);
                //_TradeLog.AutoFlush = true;

            }
            catch (Exception)
            { }
        }

        


        internal void ReadContract()
        {
            try
            {
                
                CreateContractTable();
                using (Stream fileStream = File.OpenRead(AppGlobal.ReadContract + "security.gz"),
                              zippedStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(zippedStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            string[] data = reader.ReadLine().Split('|');
                            if (data.Length == 54 && data[2] == "EQ")
                            {
                                Contract.Rows.Add("2", "NSECM", data[0], data[1], "", "", "0", data[2], "0.05", "1", "", "", "", "100", "", "1", "", "", "", "", "", "", "", "", "", "", "");
                            }
                        }
                    }
                }

                using (Stream fileStream = File.OpenRead(AppGlobal.ReadContract + "contract.gz"),
                      zippedStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(zippedStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            string[] data = reader.ReadLine().Split('|');
                            if (data.Length == 69 && data[2] != "")
                            {
                                
                                double strike = Convert.ToDouble(data[7]) / 100;                                
                                // Contract.Rows.Add("1", "NSEFO", data[0], data[3], SecondToDateTime(Market.NseCm, uint.Parse(data[6])).ToString(), data[2], strike, data[8], "0.05", data[30], "", "", "", "100", "", "1", "", "", "", "", "", "", "", "", "", SecondToDateTime(Market.NseCm, uint.Parse(data[6])).ToString());
                                Contract.Rows.Add("1", "NSEFO", data[0], data[3], SecondToDateTime(Market.NseCm, uint.Parse(data[6])).ToString("yyyyMMdd"), data[2], strike, data[8], "0.05", data[30], uint.Parse(data[6]), "", "", "100", "", "1", "", "", "", "", "", "", "", "", "", SecondToDateTime(Market.NseCm, uint.Parse(data[6])).ToString(), "");
                            }
                        }
                    }
                }


                DataTable _nseCmContract = Contract.AsEnumerable().Where(x => x["GatewayId"].ToString() == "2").CopyToDataTable();
                DataTable _nseFoContract = Contract.AsEnumerable().Where(x => x["GatewayId"].ToString() == "1").CopyToDataTable();

                _nseCmContract.TableName = "NSECM";
                _nseFoContract.TableName = "NSEFO";

                DsContract.Tables.Add(_nseCmContract);
                DsContract.Tables.Add(_nseFoContract);
                var a = DsContract.Tables["NSEFO"];
            }
            catch (Exception)
            {
            }
        }

        public void DownloadLatestContract()
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential("FAOGUEST", "FAOGUEST");

                    byte[] _FoContract = request.DownloadData("ftp://ftp.connect2nse.com/faoftp/faocommon/contract.gz");
                    byte[] _CmContract = request.DownloadData("ftp://ftp.connect2nse.com/faoftp/faocommon/security.gz");

                    using (FileStream file = File.Create(AppDomain.CurrentDomain.BaseDirectory + "contract.gz"))
                    {
                        file.Write(_FoContract, 0, _FoContract.Length);
                        file.Close();
                      
                    }
                    using (FileStream file = File.Create(AppDomain.CurrentDomain.BaseDirectory + "security.gz"))
                    {
                        file.Write(_CmContract, 0, _CmContract.Length);
                        file.Close();
                        
                    }

                    MessageBox.Show("Download Complete");
                }
                ReadContract();
            }
            catch (Exception)
            {
                OnSystemUpdate("Error Downloading Contract/Security Files.");
            }
        }

        internal void CreateContractTable()
        {
            try
            {
                Contract = new DataTable();
                Contract.Columns.Add("GatewayId", typeof(string));//1
                Contract.Columns.Add("Exchange", typeof(string));//2
                Contract.Columns.Add("TokenNo", typeof(string));//3
                Contract.Columns.Add("Symbol", typeof(string));//4
                Contract.Columns.Add("ExpiryDate", typeof(string));//5
                Contract.Columns.Add("InstrumentName", typeof(string));//6
                Contract.Columns.Add("StrikePrice", typeof(double));//7
                Contract.Columns.Add("Series", typeof(string));//8
                Contract.Columns.Add("PriceTick", typeof(string));//9
                Contract.Columns.Add("LotSize", typeof(string));//10
                Contract.Columns.Add("SymbolDesc", typeof(string));//11
                Contract.Columns.Add("TradingUnit", typeof(string));
                Contract.Columns.Add("Currency", typeof(string));
                Contract.Columns.Add("PriceDivisor", typeof(string));
                Contract.Columns.Add("ExchPointValue", typeof(string));
                Contract.Columns.Add("Multiplier", typeof(string));
                Contract.Columns.Add("DprHigh", typeof(string));
                Contract.Columns.Add("DprLow", typeof(string));
                Contract.Columns.Add("ClosePrice", typeof(string));
                Contract.Columns.Add("RBIViolation", typeof(string));
                Contract.Columns.Add("ISINNumber", typeof(string));
                Contract.Columns.Add("MaxSingleTransactionQty", typeof(string));
                Contract.Columns.Add("MaxSingleTransactionValue", typeof(string));
                Contract.Columns.Add("PermittedToTrade", typeof(string));
                Contract.Columns.Add("IsAutoAllowed", typeof(string));
                Contract.Columns.Add("Expiry", typeof(string));
                Contract.Columns.Add("Segment", typeof(string));
            }
            catch (Exception)
            {
            }
        }

        public void WriteToErrorLog(string message)
        {
            try
            {
                lock (_errorFileLock)
                {
                    
                    ArisApi_a._arisApi._errorLog.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff >> ") + message);
                }
            }
            catch (Exception)
            {
            }
        }

        public void WriteToTradeLog(string message)
        {
            try
            {
                lock (_TradeFileLock)
                {
                    ArisApi_a._arisApi._TradeLog.WriteLine(message);
                }
            }
            catch (Exception)
            {
            }
        }

        //public void WriteToByte(string message)
        //{
        //    try
        //    {
        //        lock (_ByteLog)
        //        {
        //            ArisApi_a._arisApi._byteLog.Write(message);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        public void WriteToTransactionLog(string message)
        {
            try
            {
                lock (_transactionFileLock)
                {
                    //ArisApi._arisApi._transactionLog.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff >> ") + message);
                }
            }
            catch (Exception)
            {
            }
        }

        internal DataRow getContract(Market market, string symbol, string series)
        {
            try
            {
                if (market == Market.NseCm)
                {
                    return DsContract.Tables["NSECM"].AsEnumerable().Where(x => x["Symbol"].ToString() == symbol.Trim() && x["Series"].ToString() == series).ElementAt(0);
                }
                else
                {
                    return DsContract.Tables["NSEFO"].AsEnumerable().Where(x => x["Symbol"].ToString() == symbol && x["Series"].ToString() == series).ElementAt(0);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region API releated Methods

        internal static double DoubleTwiddling(double value)
        {
            try
            {
                byte[] bytes = BitConverter.GetBytes(value);
                Array.Reverse(bytes, 0, bytes.Length);
                return BitConverter.ToDouble(bytes, 0);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        internal static byte[] StructureToByte(object packet)
        {
            try
            {
                int length = Marshal.SizeOf(packet);
                byte[] data = new byte[length];
                IntPtr intPtr = Marshal.AllocHGlobal(length);
                Marshal.StructureToPtr(packet, intPtr, true);
                Marshal.Copy(intPtr, data, 0, length);
                Marshal.FreeHGlobal(intPtr);
                return data;
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
            return null;
        }

        internal static T PinnedPacket<T>(byte[] data)
        {
            object packet = new object();
            try
            {
                GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                IntPtr IntPtrOfObject = handle.AddrOfPinnedObject();
                packet = Marshal.PtrToStructure(IntPtrOfObject, typeof(T));
                handle.Free();
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
            return (T)packet;
        }

        internal static double DoubleBitReverse(double data)
        {
            try
            {
                byte[] bytes = BitConverter.GetBytes(data);
                Array.Reverse(bytes, 0, bytes.Length);
                return BitConverter.ToDouble(bytes, 0);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #region Date converstion methods

        public DateTime SecondToDateTime(Market market, UInt32 second)
        {
            try
            {
                DateTime date = new DateTime();
                if (market == Market.NseCm || market == Market.NseFO)
                    date = new DateTime(1980, 1, 1, 0, 0, 0, 0);
                else if (market == Market.Own || market == Market.Mcx || market == Market.Mcxsx)
                    date = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                date = date.AddSeconds(second);
                return date;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public DateTime SecondToDateTime(Market market, Int32 second)
        {
            try
            {
                DateTime date = new DateTime();
                if (market == Market.NseCm || market == Market.NseFO)
                    date = new DateTime(1980, 1, 1, 0, 0, 0, 0);
                else if (market == Market.Own || market == Market.Mcx || market == Market.Mcxsx)
                    date = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                date = date.AddSeconds(second);
                return date;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public UInt32 DateTimeToSecond(Market market, DateTime date)
        {
            try
            {
                DateTime startDate = new DateTime();
                if (market == Market.NseCm || market == Market.NseFO)
                    startDate = new DateTime(1980, 1, 1, 0, 0, 0, 0);
                else if (market == Market.Own || market == Market.Mcx || market == Market.Mcxsx)
                    startDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                TimeSpan ts = date - startDate;

                return (UInt32)ts.TotalSeconds;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        #endregion

        #region Disruptor
        /// <summary>
        /// 
        /// </summary>
        public void initializeDisruptors()
        {
            //IClaimStrategy ClaimstrategyProcessPacket2 = new MultiThreadedClaimStrategy(8192);
            //IWaitStrategy WaitStrategyProcessPacket2 = new BlockingWaitStrategy();
            // Global.RequestDisruptor = new Disruptor.Dsl.Disruptor<ProcessPacket>(() => new ProcessPacket(), ClaimstrategyProcessPacket2, WaitStrategyProcessPacket2, TaskScheduler.Default);
            //Global.RequestDisruptor.HandleEventsWith(new HandleTradeNotifications());
            //Global.ringBufferRequest = Global.RequestDisruptor.Start();
        }

        #endregion

        #region Member Variables

        public SystemConfiguration SystemConfig;
        internal static string FilePath
        {
            get
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + "Config"))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\" + "Config");
                return AppDomain.CurrentDomain.BaseDirectory + "\\" + "Config" + "\\" + "SystemConfig.xml";
            }
        }
        internal StreamWriter _errorLog; 
        public StreamWriter _TradeLog;
        internal object _errorFileLock;
        internal object _transactionFileLock;
        internal object _TradeFileLock;
        internal object _ByteLog;
        internal object _CountTrade;
        internal object _fillerLock;

        
        public Dictionary<int, MTBCastPackets.MarketPicture> MarketPictureCollection = new Dictionary<int, MTBCastPackets.MarketPicture>();
        internal DataTable Contract;
        internal int FillerCounter = 0;
        public DataSet DsContract = new DataSet();
        private int FillerId = 0;
        internal int _FillerId { get { return FillerId++; } set { } }
        public DataTable FOContract = new DataTable();


        public delegate void OrderResponseDelegate(MTPackets.OrderRequest _response);
    

        public delegate void MarketDepthUpdateDelegate(MTBCastPackets.MarketPicture _response,int time);
        public event MarketDepthUpdateDelegate OnMarketDepthUpdate;

        public delegate void IndexBroadCastUpdateDelegate(NseCmApi.Broadcast.Indices _response);
        //public event IndexBroadCastUpdateDelegate OnIndexBroadCast;

        public NseCmBroadcastConnection _nseCmBroadcastConnection;
        public NseFoBroadcastConnection _nseFoBroadcastConnection;


        public delegate void LoginStatusChangeDelegate(uint Gateway, bool _isLoggedOn, string _reason);
        public event LoginStatusChangeDelegate OnLogonStatusChanged;

        public delegate void OrderBookUpdate(int Id);
       

        public delegate void SystemUpdates(string message);
        public event SystemUpdates OnSystemUpdate;

        #endregion
    }
}
