using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Sockets;
using System.Windows.Forms;
using LegLead.AppClasses;
using LegLead.NseFoApi.Header;
using System.IO;
using LegLead.NseFoApi;
using LegLead.NseFoApi.Broadcast;

namespace LegLead
{
    /// <summary>
    /// NseFo Broadcast Connection class
    /// </summary>
    public class NseFoBroadcastConnection
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public NseFoBroadcastConnection()
        {
            ConnectSocket();
        }

        #endregion

        #region Udp Sockets events

        /// <summary>
        /// 
        /// </summary>
        public void ConnectSocket()
        {
            try
            {
                    _receivedBytes = new byte[0];
                    _dataLock = new object();

                    if (_udp != null)
                        _udp.DisconnectConnection();

                    _udp = new Udp(AppDomain.CurrentDomain.BaseDirectory, TypeOfCompression.Lzo);
                    _udp.DataArrival += _udp_DataArrival;
                    _udp.Disconnect += _udp_Disconnect;
                    _udp.Error += _udp_Error;
                    //_udp.Connect(ArisApi_a._arisApi.SystemConfig.NseFoBroadcastIp, ArisApi_a._arisApi.SystemConfig.NseFoBroadcastPort);
                    _udp.Connect(ArisApi_a._arisApi.SystemConfig.NseFoBroadcastIp, ArisApi_a._arisApi.SystemConfig.NseFoBroadcastPort,ArisApi_a._arisApi.SystemConfig.Gateway);
                    if (!string.IsNullOrEmpty(_udp.ComressLibError))
                    {
                        MessageBox.Show(_udp.ComressLibError);
                    }
               
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        /// <summary>
        /// Event of Sockets error
        /// </summary>
        /// <param name="error"></param>
        private void _udp_Error(string error)
        {
            //LogWriter.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + GeneralConstants.COLON +
            //       MethodBase.GetCurrentMethod().Name + GeneralConstants.COLON + error);
        }

        /// <summary>
        /// Event of Sockets disconnect
        /// </summary>
        private void _udp_Disconnect()
        {
            //LogWriter.WriteToConnectionLog("Manager disconnected from : " + Market.NseFO + GeneralConstants.COLON +
            //    _udp.RemoteHostIpAddress + GeneralConstants.COLON + _udp.RemoteHostPort);
        }


      

       

        /// <summary>
        /// Event of data arrival on it decompress data and call process packet
        /// </summary>
        /// <param name="data"></param>
        private void _udp_DataArrival(byte[] data)
        {
            lock (_dataLock)
            {
                try
                {
                   // File.WriteAllBytes("D:\\ByteToFile.txt", data);
                    
                   

                    int destinationLength = _receivedBytes.Length;
                    Array.Resize(ref _receivedBytes, (destinationLength + data.Length));
                    Array.Copy(data, 0, _receivedBytes, destinationLength, data.Length);
                    byte[] processData;

                    int sourceIndex = 0;
                    int noPackets = 0;
                    int subIndex = 0;
                    CompressPacket compressPacket;
                    CompressSubPacket compressSubPacket;

                    while ((_receivedBytes.Length - sourceIndex) > 511)
                    {
                      //  TransactionWatch.ByteMessage("receivedBytes Length | " + _receivedBytes.Length + "| sourceIndex | " + sourceIndex);

                        processData = new byte[512];
                        Array.Copy(_receivedBytes, sourceIndex, processData, 0, processData.Length);
                        //TransactionWatch.ByteMessage("processData Length | " + processData.Length);

                        compressPacket = PinnedPacket<CompressPacket>(processData);

                        #region Process Packet

                        noPackets = 0;
                        subIndex = 0;

                       // TransactionWatch.ByteMessage("compressPacket NoOfPacket | " + compressPacket.NoOfPacket + " |noPackets| " + noPackets);
                        while (compressPacket.NoOfPacket > 0 && compressPacket.NoOfPacket != noPackets &&
                            subIndex < 507)
                        {
                            processData = new byte[compressPacket.PacketData.Length - subIndex];
                            Array.Copy(compressPacket.PacketData, subIndex, processData, 0, processData.Length);
                            compressSubPacket = PinnedPacket<CompressSubPacket>(processData);
                          //  TransactionWatch.ByteMessage("compressSubPacket NoOfPacket | " + compressSubPacket.CompressLength);
                            if (compressSubPacket.CompressLength > 0 && compressSubPacket.CompressLength < 507)
                            {
                                #region Compress data
                               // TransactionWatch.ByteMessage("compressSubPacket NoOfPacket | " + compressSubPacket.CompressLength + "| compressSubPacket.PacketData.Length |" + compressSubPacket.PacketData.Length);
                                if (compressSubPacket.CompressLength > 9 &&
                                    compressSubPacket.CompressLength <= compressSubPacket.PacketData.Length)
                                {
                                    processData = new byte[compressSubPacket.CompressLength];
                                    Array.Copy(compressSubPacket.PacketData, processData, processData.Length);

                                    byte[] unCompressData = _udp.LzoCompressor.UncompressData(processData);
                                    //TransactionWatch.ByteMessage("compressSubPacket NoOfPacket | " + unCompressData.Length);
                                    if (unCompressData.Length > 0)
                                    {
                                        if (unCompressData[0] == 2)
                                        {
                                            processData = new byte[unCompressData.Length - 8];
                                            Array.Copy(unCompressData, 8, processData, 0, processData.Length);

                                            ProcessPacket(processData);
                                        }
                                    }
                                    else
                                        break;
                                }
                                subIndex += 2 + compressSubPacket.CompressLength;

                                #endregion
                            }
                            else if (compressSubPacket.CompressLength == 0)
                            {
                                #region Uncompress data

                                processData = new byte[compressSubPacket.PacketData.Length - 8];
                                Array.Copy(compressSubPacket.PacketData, 8, processData, 0, processData.Length);

                                MessageHeader messageHeader = PinnedPacket<MessageHeader>(processData);
                                //TransactionWatch.ByteMessage("processData.Length | " + processData.Length + " | messageHeader.MessageLength | " + messageHeader.MessageLength + " |messageHeader.ErrorCode | ");
                                if (processData.Length > 0 && processData.Length >= messageHeader.MessageLength &&
                                    messageHeader.ErrorCode == 0)
                                {
                                    if (compressSubPacket.PacketData[subIndex] == 4)
                                    {
                                        processData = new byte[messageHeader.MessageLength];
                                        Array.Copy(compressSubPacket.PacketData, subIndex + 8, processData, 0, messageHeader.MessageLength);

                                        ProcessPacket(processData);
                                    }
                                    else
                                        break;
                                }

                                if (messageHeader.MessageLength > 0)
                                    subIndex += 2 + 8 + messageHeader.MessageLength;

                                #endregion
                            }
                            else
                                break;

                            noPackets++;
                        }
                        #endregion

                        sourceIndex += 512;
                    }

                    processData = new byte[_receivedBytes.Length - sourceIndex];
                    Array.Copy(_receivedBytes, sourceIndex, processData, 0, processData.Length);
                    _receivedBytes = processData;

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Input string was not in a correct format"))
                        _receivedBytes = new byte[0];
                    else
                    {
                        if (ex.Message.Contains("System.OutOfMemoryException"))
                            _receivedBytes = new byte[0];
                        //LogWriter.WriteToErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name + GeneralConstants.COLON +
                        //    MethodBase.GetCurrentMethod().Name + GeneralConstants.COLON + ex.Message);
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receivePacket"></param>
        /// <returns></returns>
        public void ProcessPacket(byte[] receivePacket)
        {
            try
            {
                MessageHeader messageHeader = PinnedPacket<MessageHeader>(receivePacket);



                switch ((NseFoMessageCode)messageHeader.TransCode)
                {
                    #region Uncompress Packet

                    case NseFoMessageCode.ContractOpenMessage:
                        break;

                    case NseFoMessageCode.GeneralMessageBroadcast:
                        Thread generalMessageBroadcast = new Thread(GeneralMessageBroadcastProcess);
                        generalMessageBroadcast.Priority = ThreadPriority.Highest;
                        generalMessageBroadcast.IsBackground = true;
                        generalMessageBroadcast.Start(receivePacket);
                        break;

                    case NseFoMessageCode.MarketStatusOpen:
                    case NseFoMessageCode.MarketStatusClose:
                    case NseFoMessageCode.MarketStatusPostClose:
                    case NseFoMessageCode.MarketStatusPreOrPostDay:
                    case NseFoMessageCode.MarketStatusOpenEnded:
                        break;

                    case NseFoMessageCode.BroadCastCircuitCheck:
                        break;

                    //case NseFoMessageCode.SpreadMarketbyPrice:
                    //    //todo:future develope
                    //    break;

                    case NseFoMessageCode.ChangeInInstrumentMaster:
                        Thread changeInInstrumentMaster = new Thread(ChangeInInstrumentMasterProcess);
                        changeInInstrumentMaster.Priority = ThreadPriority.Highest;
                        changeInInstrumentMaster.IsBackground = true;
                        changeInInstrumentMaster.Start(receivePacket);
                        break;

                    case NseFoMessageCode.ChangeInSecurityMaster:
                        Thread changeInContractMaster = new Thread(ChangeInSecurityMasterProcess);
                        changeInContractMaster.Priority = ThreadPriority.Highest;
                        changeInContractMaster.IsBackground = true;
                        changeInContractMaster.Start(receivePacket);
                        break;

                    case NseFoMessageCode.ChangeParticipantStatus:
                        break;

                    case NseFoMessageCode.ChangeofSecurityStatusPreopen:
                    case NseFoMessageCode.ChangeofSecurityStatus:
                        Thread securityStatus = new Thread(ChangeofSecurityStatusProcess);
                        securityStatus.Priority = ThreadPriority.Highest;
                        securityStatus.IsBackground = true;
                        securityStatus.Start(receivePacket);
                        break;

                    case NseFoMessageCode.TurnoverLimitExceededOrBrokerReactivated:
                    case NseFoMessageCode.TurnoverLimitExceededOrBrokerReactivatedBrokerReactivated:
                        Thread turnoverLimitExceeded = new Thread(TurnoverLimitExceededOrBrokerReactivedProcess);
                        turnoverLimitExceeded.Priority = ThreadPriority.Highest;
                        turnoverLimitExceeded.IsBackground = true;
                        turnoverLimitExceeded.Start(receivePacket);
                        break;

                    case NseFoMessageCode.IndexBroadcast:
                        //BroadCastByPriceUpdatePrice(PinnedPacket<MultipleIndexBroadcast>(receivePacket));
                        break;

                    case NseFoMessageCode.IndustryIndexBroadcast:
                        break;

                    case NseFoMessageCode.UnderlyingOpenInterest:
                        break;

                    #endregion

                    #region Compress packet

                    case NseFoMessageCode.MarketByOrderOrMarketByPriceUpdate:
                        break;

                    case NseFoMessageCode.MarketByPriceUpdate:

                        MarketByPriceUpdateProcess(PinnedPacket<NseFoApi.Broadcast.OnlyMarketByPriceUpdate>(receivePacket), messageHeader);
                        break;

                    #endregion

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        private void MarketByPriceUpdateProcess(NseFoApi.Broadcast.OnlyMarketByPriceUpdate onlyMarketByPriceUpdate, NseFoApi.Header.MessageHeader messageHeader)
        {
            try
            {
                for (int i = 0; i < onlyMarketByPriceUpdate.NumberOfRecords; i++)
                {
                    //if (ArisApi._arisApi.MarketPictureCollection.ContainsKey(onlyMarketByPriceUpdate.InteractiveMBPData[i].Token))
                        PacketConvertion.GetOwnMarketPictureNseFo(onlyMarketByPriceUpdate.InteractiveMBPData[i],messageHeader);
                }
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        #region Uncompress data processs

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void ReportOnMarketStatisticsProcess(object data)
        {
            try
            {
                byte[] receivePacket = (byte[])data;

                char messageType = Convert.ToChar(receivePacket[Marshal.SizeOf(typeof(MessageHeader)) + 1]);

                switch (messageType)
                {
                    case 'H':
                        //TransactionWatch.TransactionMessage(Market.NseFO + GeneralConstants.COLON + "Market Statistics started.", Color.Black);
                        break;

                    case 'R':
                        //ContractBhavCopy.Bhavcopy.UpdateBhavCopy(Market.NseFO, Utilities.PinnedPacket<ReportOnMarketStatistics>(receivePacket));
                        break;

                    case 'T':
                        //TransactionWatch.TransactionMessage(Market.NseFO + GeneralConstants.COLON + "Market Statistics completed.", Color.Black);
                        break;
                }
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void TurnoverLimitExceededOrBrokerReactivedProcess(object data)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void ChangeofSecurityStatusProcess(object data)
        {
            try
            {
                //byte[] receivePacket = (byte[])data;
                //ChangeOfSecurityStatus changeOfSecurityStatus = Utilities.PinnedPacket<ChangeOfSecurityStatus>(receivePacket);
                //ContractBhavCopy.Contract.UpdateSecurityStatus(Market.NseFO, changeOfSecurityStatus);
                //ContractBhavCopy.Contract.Save(Market.NseFO);
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void ChangeInInstrumentMasterProcess(object data)
        {
            try
            {
                //byte[] receivePacket = (byte[])data;
                //ChangeInInstrumentMaster changeInInstrumentMaster = Utilities.PinnedPacket<ChangeInInstrumentMaster>(receivePacket);
                //ContractBhavCopy.Contract.UpdateInstrument(Market.NseFO, changeInInstrumentMaster);
                //ContractBhavCopy.Contract.Save(Market.NseFO);
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void ChangeInSecurityMasterProcess(object data)
        {
            try
            {
                //byte[] receivePacket = (byte[])data;
                //ChangeInSecurityMaster changeInContractMaster = Utilities.PinnedPacket<ChangeInSecurityMaster>(receivePacket);
                //ContractBhavCopy.Contract.UpdateContractMaster(Market.NseFO, changeInContractMaster);

                //ContractBhavCopy.Contract.Save(Market.NseFO);
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void ChangeofMarketStatusProcess(object data)
        {
            try
            {
                //byte[] receivePacket = (byte[])data;
                //ChangeofMarketStatus changeofMarketStatus = Utilities.PinnedPacket<ChangeofMarketStatus>(receivePacket);

                //Global.LoginData.Update(changeofMarketStatus);
                //TransactionWatch.TransactionMessage(Market.NseFO.ToString() + GeneralConstants.COLON + "ExchangeMessages" + GeneralConstants.COLON +
                //    changeofMarketStatus.BroadcastMessage.Trim(), Color.Black);
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void GeneralMessageBroadcastProcess(object data)
        {
            try
            {
                //byte[] receivePacket = (byte[])data;
                //GeneralMessageBroadcast generalMessageBroadcast = Utilities.PinnedPacket<GeneralMessageBroadcast>(receivePacket);
                //TransactionWatch.TransactionMessage(Market.NseFO.ToString() + GeneralConstants.COLON + "ExchangeMessages" + GeneralConstants.COLON +
                //    generalMessageBroadcast.BroadcastMessage.Trim(), Color.Black);
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Disconnect()
        {
            if (_udp != null)
                _udp.DisconnectConnection();
        }

        public T PinnedPacket<T>(byte[] data)
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

        #endregion

        #region Member variables

        /// <summary>
        /// Udp object
        /// </summary>
        private Udp _udp;

        /// <summary>
        /// On data receive lock
        /// </summary>
        private object _dataLock;

        /// <summary>
        /// udp data
        /// </summary>
        private byte[] _receivedBytes;

        #endregion
    }
}
