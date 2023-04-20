using System.Net;
using System.Runtime.InteropServices;
using LegLead.NseCmApi.Header;
using LegLead.AppClasses;

namespace LegLead.NseCmApi
{
    namespace Header
    {
        #region MessageHeader

        #region Message Header

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class MessageHeader
        {
            #region Constructor

            /// <summary>
            /// Constructor
            /// </summary>
            internal MessageHeader()
            {
            }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="nseFoMessageCode"></param>
            /// <param name="obj"></param>
            internal MessageHeader(NseCmMessageCode nseFoMessageCode, object obj)
            {
                AlphaChar = string.Empty;
                TransCode = (short)nseFoMessageCode;
                MessageLength = (short)Marshal.SizeOf(obj);
            }

            #endregion

            #region Property
            /// <summary>
            /// 
            /// </summary>
            internal short IApiTCode
            {
                get { return IPAddress.NetworkToHostOrder(_iApiTCode); }
                set { _iApiTCode = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            internal short IApiFuncId
            {
                get { return IPAddress.NetworkToHostOrder(_iApiFuncId); }
                set { _iApiFuncId = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            internal string AlphaChar
            {
                get { return new string(_alphaChar); }
                set { _alphaChar = value.PadRight(2, ' ').ToCharArray(); }
            }
            /// <summary>
            /// 
            /// </summary>
            internal short TransCode
            {
                get { return IPAddress.NetworkToHostOrder(_transCode); }
                set { _transCode = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            internal short ErrorCode
            {
                get { return IPAddress.NetworkToHostOrder(_errorCode); }
                set { _errorCode = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            internal int SeqNo
            {
                get { return IPAddress.NetworkToHostOrder(_seqNo); }
                set { _seqNo = IPAddress.HostToNetworkOrder(value); }
            }
            internal int LogTime
            {
                get { return IPAddress.NetworkToHostOrder(_logTime); }
                set { _logTime = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            internal double TimeStamp1
            {
                get { return ArisApi_a.DoubleTwiddling(_timeStamp1); }
                set { _timeStamp1 = ArisApi_a.DoubleTwiddling(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            internal double TimeStamp2
            {
                get { return ArisApi_a.DoubleTwiddling(_timeStamp2); }
                set { _timeStamp2 = ArisApi_a.DoubleTwiddling(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            internal short MessageLength
            {
                get { return IPAddress.NetworkToHostOrder(_messageLength); }
                set { _messageLength = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member Variables

            /// <summary>
            /// 
            /// </summary>            
            private short _iApiTCode;
            /// <summary>
            /// 
            /// </summary>
            private short _iApiFuncId;
            /// <summary>
            /// 
            /// </summary>
            private int _logTime;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _alphaChar;
            /// <summary>
            /// 
            /// </summary>
            private short _transCode;
            /// <summary>
            /// 
            /// </summary>
            private short _errorCode;
            /// <summary>
            /// 
            /// </summary>
            private int _seqNo;
            /// <summary>
            /// 
            /// </summary>
            internal byte CRC;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            private byte[] _reserved2 = new byte[3];
            /// <summary>
            /// 
            /// </summary>
            private double _timeStamp1;
            /// <summary>
            /// 
            /// </summary>
            private double _timeStamp2;
            /// <summary>
            /// 
            /// </summary>
            private short _messageLength;

            #endregion
        }

        #endregion

        #region Sec Info

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SecInfo
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            internal string Symbol
            {
                get { return new string(_symbol); }
                set { _symbol = value.PadRight(10, ' ').ToCharArray(); }
            }
            /// <summary>
            /// 
            /// </summary>
            internal string Series
            {
                get { return new string(_series); }
                set { _series = value.PadRight(2, ' ').ToCharArray(); }
            }

            #endregion

            #region Member Variable

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _symbol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _series;

            #endregion
        }

        #endregion

        #region Error Message Response

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class ErrorMessageResponse
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            internal string ErrorMessage
            {
                get { return new string(_errorMessage); }
            }

            #endregion

            #region Member Variable

            internal MessageHeader MessageHeader;
            internal SecInfo SecInfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            private char[] _errorMessage;

            #endregion
        }

        #endregion

        #endregion
    }

    namespace LogonProcess
    {
        #region The Logon Process

        #region Nested Structure

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct BrokerEligibilityPerMkt
        {
            #region Member Variables

            internal byte Market;
            internal byte Reserved;

            #endregion
        }

        #endregion

        #region Logon request

        /// <summary>
        /// 
        /// </summary> 
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class LogonRequest
        {
            #region Constructor

            /// <summary>
            /// constructor
            /// </summary>
            internal LogonRequest()
            {
                MessageHeader = new MessageHeader(NseCmMessageCode.LogonRequest, this);

                BrokerStatus = ' ';
                ShowIndex = ' ';
                Reserved1 = ' ';
                Reserved2 = ' ';
                HostSwitchContext = ' ';
                WsClassName = string.Empty;
                Colour = string.Empty;
                TraderName = string.Empty;
                BrokerID = string.Empty;
                BrokerName = string.Empty;
            }

            #endregion

            #region Property

            /// <summary>
            /// 
            /// </summary>
            internal int UserID
            {
                get { return IPAddress.NetworkToHostOrder(_userID); }
                set { _userID = IPAddress.HostToNetworkOrder(value); }
            }

            /// <summary>
            /// 
            /// </summary>
            internal string Password
            {
                get { return new string(_password); }
                set { _password = value.PadRight(8, ' ').ToCharArray(); }
            }

            /// <summary>
            /// 
            /// </summary>
            internal string NewPassword
            {
                get { return new string(_newPassword); }
                set { _newPassword = value.PadRight(8, ' ').ToCharArray(); }
            }

            /// <summary>
            /// 
            /// </summary>
            internal string TraderName
            {
                get { return new string(_traderName); }
                set { _traderName = value.PadRight(26, ' ').ToCharArray(); }
            }

            /// <summary>
            /// 
            /// </summary>
            internal string BrokerID
            {
                get { return new string(_brokerID); }
                set { _brokerID = value.PadRight(5, ' ').ToCharArray(); }
            }

            /// <summary>
            /// 
            /// </summary>
            internal string Colour
            {
                get { return new string(_colour); }
                set { _colour = value.PadRight(51, ' ').ToCharArray(); }
            }

            /// <summary>
            /// 
            /// </summary>
            internal int VersionNumber
            {
                get { return IPAddress.NetworkToHostOrder(_versionNumber); }
                set { _versionNumber = IPAddress.HostToNetworkOrder(value); }
            }

            /// <summary>
            /// 
            /// </summary>
            internal short UserType
            {
                get { return IPAddress.NetworkToHostOrder(_userType); }
                set { _userType = IPAddress.HostToNetworkOrder(value); }
            }

            /// <summary>
            /// 
            /// </summary>
            internal string WsClassName
            {
                get { return new string(_wsClassName); }
                set { _wsClassName = value.PadRight(15, ' ').ToCharArray(); }
            }

            /// <summary>
            /// 
            /// </summary>
            internal string BrokerName
            {
                get { return new string(_brokerName); }
                set { _brokerName = value.PadRight(26, ' ').ToCharArray(); }
            }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal int _userID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            private char[] _password;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            internal char[] _newPassword;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            internal char[] _traderName;
            internal int _lastPasswordChangeDate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            internal char[] _brokerID;
            internal char Reserved1;
            internal short _branchID;
            internal int _versionNumber;
            internal int _batch2StartTime;
            internal char HostSwitchContext;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            internal char[] _colour;
            internal char Reserved2;
            internal short _userType;
            internal double _sequenceNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            internal char[] _wsClassName;
            internal char BrokerStatus;
            internal char ShowIndex;
            internal BrokerEligibilityPerMkt BrokerEligibilityPerMkt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            internal char[] _brokerName;

            #endregion
        }

        #endregion

        #region Logon conformation response

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class LogonResponse
        {
            #region Property

            internal int UserID { get { return IPAddress.NetworkToHostOrder(_userID); } }
            internal string Password { get { return new string(_password); } }
            internal string NewPassword { get { return new string(_newPassword); } }
            internal string TraderName { get { return new string(_traderName); } }
            internal int LastPasswordChangeDate { get { return IPAddress.NetworkToHostOrder(_lastPasswordChangeDate); } }
            internal string BrokerID { get { return new string(_brokerID); } }
            internal short BranchID { get { return IPAddress.NetworkToHostOrder(_branchID); } }
            internal int VersionNumber { get { return IPAddress.NetworkToHostOrder(_versionNumber); } }
            internal int EndTime { get { return IPAddress.NetworkToHostOrder(_endTime); } }
            internal short UserType { get { return IPAddress.NetworkToHostOrder(_userType); } }
            internal double SequenceNumber { get { return ArisApi_a.DoubleTwiddling(_sequenceNumber); } }
            internal string BrokerName { get { return new string(_brokerName); } }

            #endregion

            #region Member Variable

            internal MessageHeader MessageHeader;
            private int _userID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            private char[] _password;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            private char[] _newPassword;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            private char[] _traderName;
            private int _lastPasswordChangeDate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _brokerID;
            internal byte Reseverd1;
            private short _branchID;
            private int _versionNumber;
            private int _endTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 52)]
            internal byte[] Reseverd2;
            private short _userType;
            private double _sequenceNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            internal byte[] Reserved3;
            internal char BrokerStatus;
            internal char ShowIndex;
            internal BrokerEligibilityPerMkt STBrokerEligibilityPerMkt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            internal char[] _brokerName;

            #endregion
        }

        #endregion

        #region Logoff request

        /// <summary>
        /// trans code 2320
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class LogoffRequest
        {
            #region Constructor

            internal LogoffRequest()
            {
                MessageHeader = new MessageHeader(NseCmMessageCode.LogoffRequest, this);
            }

            #endregion

            #region Member variable

            internal MessageHeader MessageHeader;

            #endregion
        }

        #endregion

        #region Logoff Response

        /// <summary>
        /// trans code 2320
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class LogoffResponse
        {
            #region Property

            internal short UserID { get { return IPAddress.NetworkToHostOrder(_userId); } }

            #endregion

            #region Member variable

            internal MessageHeader MessageHeader;
            private short _userId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 145)]
            internal byte[] Reserved;

            #endregion
        }

        #endregion

        #endregion

        #region Invitation Message

        /// <summary>
        /// trans code 15000
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class InvitationMessage
        {
            #region Property

            internal short InvitationCount { get { return IPAddress.NetworkToHostOrder(_invitationCount); } }

            #endregion

            #region Member variable

            internal MessageHeader MessageHeader;
            private short _invitationCount;

            #endregion
        }

        #endregion
    }

    namespace DownloadProcess
    {
        #region Download Process

        #region Nested Structure

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct MarketStatus
        {
            #region Properties

            internal short Normal
            {
                get { return IPAddress.NetworkToHostOrder(_normal); }
                set { _normal = IPAddress.HostToNetworkOrder(value); }
            }
            internal short Oddlot
            {
                get { return IPAddress.NetworkToHostOrder(_oddlot); }
                set { _oddlot = IPAddress.HostToNetworkOrder(value); }
            }
            internal short Spot
            {
                get { return IPAddress.NetworkToHostOrder(_spot); }
                set { _spot = IPAddress.HostToNetworkOrder(value); }
            }
            internal short Auction
            {
                get { return IPAddress.NetworkToHostOrder(_auction); }
                set { _auction = IPAddress.HostToNetworkOrder(value); }
            }
            internal short CallAuction1
            {
                get { return IPAddress.NetworkToHostOrder(_callAuction1); }
                set { _callAuction1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal short CallAuction2
            {
                get { return IPAddress.NetworkToHostOrder(_callAuction2); }
                set { _callAuction2 = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            private short _normal;
            private short _oddlot;
            private short _spot;
            private short _auction;
            private short _callAuction1;
            private short _callAuction2;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct StockEligibleIndicators
        {
            internal byte Indicators;
            internal byte Reserved;
        }

        #endregion

        #region System Information Request

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SystemInformationRequest
        {
            #region Constructor

            internal SystemInformationRequest()
            {
                MessageHeader = new MessageHeader(NseCmMessageCode.SystemInformationRequest, this);
            }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;

            #endregion
        }

        #endregion

        #region System Information Response

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SystemInformationResponse
        {
            #region Property

            internal int MarketIndex { get { return IPAddress.NetworkToHostOrder(_marketIndex); } }
            internal short DefaultSettlementPeriodNormal { get { return IPAddress.NetworkToHostOrder(_defaultSettlementPeriodNormal); } }
            internal short DefaultSettlementPeriodSpot { get { return IPAddress.NetworkToHostOrder(_defaultSettlementPeriodSpot); } }
            internal short DefaultSettlementPeriodAuction { get { return IPAddress.NetworkToHostOrder(_defaultSettlementPeriodAuction); } }
            internal short CompetitorPeriod { get { return IPAddress.NetworkToHostOrder(_competitorPeriod); } }
            internal short SolicitorPeriod { get { return IPAddress.NetworkToHostOrder(_solicitorPeriod); } }
            internal short WarningPercent { get { return IPAddress.NetworkToHostOrder(_warningPercent); } }
            internal short VolumeFreezePercent { get { return IPAddress.NetworkToHostOrder(_volumeFreezePercent); } }
            internal short TerminalIdleTime { get { return IPAddress.NetworkToHostOrder(_terminalIdleTime); } }
            internal int BoardLotQuantity { get { return IPAddress.NetworkToHostOrder(_boardLotQuantity); } }
            internal int TickSize { get { return IPAddress.NetworkToHostOrder(_tickSize); } }
            internal short MaximumGtcDays { get { return IPAddress.NetworkToHostOrder(_maximumGtcDays); } }
            internal short DisclosedQuantityPercentAllowed { get { return IPAddress.NetworkToHostOrder(_disclosedQuantityPercentAllowed); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal MarketStatus MarketStatus;
            private int _marketIndex;
            private short _defaultSettlementPeriodNormal;
            private short _defaultSettlementPeriodSpot;
            private short _defaultSettlementPeriodAuction;
            private short _competitorPeriod;
            private short _solicitorPeriod;
            private short _warningPercent;
            private short _volumeFreezePercent;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal byte[] Reserved1;
            private short _terminalIdleTime;
            private int _boardLotQuantity;
            private int _tickSize;
            private short _maximumGtcDays;
            internal StockEligibleIndicators StockEligibleIndicators;
            private short _disclosedQuantityPercentAllowed;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            internal byte[] Reserved2;

            #endregion
        }

        #endregion

        #region Update Local Database Request

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class UpdateLocalDatabaseRequest
        {
            #region Constructor

            /// <summary>
            /// Constructor
            /// </summary>
            internal UpdateLocalDatabaseRequest()
            {
                MessageHeader = new MessageHeader(NseCmMessageCode.UpdateLocalDatabaseRequest, this);
            }

            #endregion

            #region Property

            internal int LastUpdateSecurityTime { get { return IPAddress.NetworkToHostOrder(_lastUpdateSecurityTime); } }
            internal int LastUpdateParticipantTime { get { return IPAddress.NetworkToHostOrder(_lastUpdateParticipantTime); } }

            #endregion

            #region Member variable

            internal MessageHeader MessageHeader;
            private int _lastUpdateSecurityTime;
            private int _lastUpdateParticipantTime;
            internal char RequestForOpenOrders;
            internal char Reserved1;
            internal MarketStatus MarketStatus;

            #endregion
        }

        #endregion

        #region Update Local Database Header

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class UpdateLocalDatabaseHeader
        {
            #region Member variables

            internal MessageHeader MessageHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal char[] Reserved;

            #endregion
        }

        #endregion

        #region Update Local Database Data

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class UpdateLocalDatabaseData
        {
            #region Property

            internal MessageHeader MessageHeaderInner { get { return ArisApi_a.PinnedPacket<MessageHeader>(Data); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 472)]
            internal byte[] Data;

            #endregion
        }

        #endregion

        #region Update Loacal DataBase Trailer

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class UpdateLoacalDataBaseTrailer
        {
            #region Member variables

            internal MessageHeader MessageHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal char[] Reserved;

            #endregion
        }

        #endregion

        #region Message Download Request

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class MessageDownloadRequest
        {
            #region Constructor

            /// <summary>
            ///Constructor
            /// </summary>
            internal MessageDownloadRequest()
            {
                MessageHeader = new MessageHeader(NseCmMessageCode.MessageDownloadRequest, this);
            }

            #endregion

            #region Property

            internal double SequenceNumber
            {
                get { return ArisApi_a.DoubleTwiddling(_sequenceNumber); }
                set { _sequenceNumber = ArisApi_a.DoubleTwiddling(value); }
            }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private double _sequenceNumber;

            #endregion
        }

        #endregion

        #region Message Download Header Response

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class MessageDownloadHeaderResponse
        {
            internal MessageHeader MessageHeader;
        }

        #endregion

        #region Message Download Data Response

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class MessageDownloadDataResponse
        {
            #region Property

            internal MessageHeader MessageHeaderInner { get { return ArisApi_a.PinnedPacket<MessageHeader>(Data); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 472)]
            internal byte[] Data;

            #endregion
        }

        #endregion

        #region Message Download Trailer Response

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class MessageDownloadTrailerResponse
        {
            internal MessageHeader MessageHeader;
        }

        #endregion

        #region Partial System Information

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class PartialSystemInformation
        {
            #region Member Variables

            internal SystemInformationResponse SystemInformationResponse;

            #endregion
        }

        #endregion

        #endregion
    }

    namespace OrderAndTradeManagement
    {
        #region Order and Trade Management

        #region Nested Structure

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct OrderFlags
        {
            internal byte OrderType;
            internal byte OrderType1;
        }

        #endregion

        #region Order Proceess

        /// <summary>
        /// MessageHeader = new MessageHeader(NseCmMessageCode.LogonRequest, this);
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderEntryRequest
        {
            #region Constructor

            /// <summary>
            /// 
            /// </summary>
            internal OrderEntryRequest()
            {
                MessageHeader = new MessageHeader(NseCmMessageCode.OrderEntryRequest, this);
                ParticipantType = ' ';
                ModifiedOrCancelledBy = ' ';
                Reserved3 = string.Empty.PadRight(4, ' ').ToCharArray();
                Suspended = ' ';
                Settlor = string.Empty;
            }

            /// <summary>
            /// Constructor
            /// </summary>
            internal OrderEntryRequest(NseCmMessageCode NseCmMessageCode)
            {
                MessageHeader = new MessageHeader(NseCmMessageCode, this);
                ParticipantType = ' ';
                ModifiedOrCancelledBy = ' ';
                Reserved3 = string.Empty.PadRight(4, ' ').ToCharArray();
                Suspended = ' ';
                Settlor = string.Empty;
            }

            #endregion

            #region Property

            internal short CompetitorPeriod
            {
                get { return IPAddress.NetworkToHostOrder(_competitorPeriod); }
                set { _competitorPeriod = IPAddress.HostToNetworkOrder(value); }
            }
            internal short SolicitorPeriod
            {
                get { return IPAddress.NetworkToHostOrder(_solicitorPeriod); }
                set { _solicitorPeriod = IPAddress.HostToNetworkOrder(value); }
            }
            internal short ReasonCode
            {
                get { return IPAddress.NetworkToHostOrder(_reasonCode); }
                set { _reasonCode = IPAddress.HostToNetworkOrder(value); }
            }
            internal short AuctionNo
            {
                get { return IPAddress.NetworkToHostOrder(_auctionNo); }
                set { _auctionNo = IPAddress.HostToNetworkOrder(value); }
            }
            internal string CounterPartyBrokerID
            {
                get { return new string(_counterPartyBrokerID); }
                set { _counterPartyBrokerID = value.PadRight(5, ' ').ToCharArray(); }
            }
            internal double OrderNumber
            {
                get { return ArisApi_a.DoubleTwiddling(_orderNumber); }
                set { _orderNumber = ArisApi_a.DoubleTwiddling(value); }
            }
            internal string AccountNumber
            {
                get { return new string(_accountNumber); }
                set { _accountNumber = value.PadRight(10, ' ').ToCharArray(); }
            }
            internal short BookType
            {
                get { return IPAddress.NetworkToHostOrder(_bookType); }
                set { _bookType = IPAddress.HostToNetworkOrder(value); }
            }
            internal short BuyOrSellIndicator
            {
                get { return IPAddress.NetworkToHostOrder(_buyOrSellIndicator); }
                set { _buyOrSellIndicator = IPAddress.HostToNetworkOrder(value); }
            }
            internal int DiscloseVolume
            {
                get { return IPAddress.NetworkToHostOrder(_discloseVolume); }
                set { _discloseVolume = IPAddress.HostToNetworkOrder(value); }
            }
            internal int DisclosedVolumeRemaining
            {
                get { return IPAddress.NetworkToHostOrder(_disclosedVolumeRemaining); }
                set { _disclosedVolumeRemaining = IPAddress.HostToNetworkOrder(value); }
            }
            internal int TotalVolumeRemaining
            {
                get { return IPAddress.NetworkToHostOrder(_totalVolumeRemaining); }
                set { _totalVolumeRemaining = IPAddress.HostToNetworkOrder(value); }
            }
            internal int Volume
            {
                get { return IPAddress.NetworkToHostOrder(_volume); }
                set { _volume = IPAddress.HostToNetworkOrder(value); }
            }
            internal int VolumeFilledToday
            {
                get { return IPAddress.NetworkToHostOrder(_volumeFilledToday); }
                set { _volumeFilledToday = IPAddress.HostToNetworkOrder(value); }
            }
            internal int Price
            {
                get { return IPAddress.NetworkToHostOrder(_price); }
                set { _price = IPAddress.HostToNetworkOrder(value); }
            }
            internal int TriggerPrice
            {
                get { return IPAddress.NetworkToHostOrder(_triggerPrice); }
                set { _triggerPrice = IPAddress.HostToNetworkOrder(value); }
            }
            internal int GoodTillDate
            {
                get { return IPAddress.NetworkToHostOrder(_goodTillDate); }
                set { _goodTillDate = IPAddress.HostToNetworkOrder(value); }
            }
            internal int EntryDateTime
            {
                get { return IPAddress.NetworkToHostOrder(_entryDateTime); }
                set { _entryDateTime = IPAddress.HostToNetworkOrder(value); }
            }
            internal int MinimumFillOrAONVolume
            {
                get { return IPAddress.NetworkToHostOrder(_minimumFillOrAONVolume); }
                set { _minimumFillOrAONVolume = IPAddress.HostToNetworkOrder(value); }
            }
            internal int LastModified
            {
                get { return IPAddress.NetworkToHostOrder(_lastModified); }
                set { _lastModified = IPAddress.HostToNetworkOrder(value); }
            }
            internal short BranchID
            {
                get { return IPAddress.NetworkToHostOrder(_branchID); }
                set { _branchID = IPAddress.HostToNetworkOrder(value); }
            }
            internal int TraderID
            {
                get { return IPAddress.NetworkToHostOrder(_traderID); }
                set { _traderID = IPAddress.HostToNetworkOrder(value); }
            }
            internal string BrokerID
            {
                get { return new string(_brokerID); }
                set { _brokerID = value.PadRight(5, ' ').ToCharArray(); }
            }
            internal string Remarks
            {
                get { return new string(_remarks); }
                set { _remarks = value.PadRight(25, ' ').ToCharArray(); }
            }
            internal string Settlor
            {
                get { return new string(_settlor); }
                set { _settlor = value.PadRight(12, ' ').ToCharArray(); }
            }
            internal short ProOrClientIndicator
            {
                get { return IPAddress.NetworkToHostOrder(_proOrClientIndicator); }
                set { _proOrClientIndicator = IPAddress.HostToNetworkOrder(value); }
            }
            internal short SettlementPeriod
            {
                get { return IPAddress.NetworkToHostOrder(_settlementPeriod); }
                set { _settlementPeriod = IPAddress.HostToNetworkOrder(value); }
            }
            internal double NnfField
            {
                get { return ArisApi_a.DoubleTwiddling(_nnfField); }
                set { _nnfField = ArisApi_a.DoubleTwiddling(value); }
            }
            internal double ExecTimeStamp
            {
                get { return ArisApi_a.DoubleTwiddling(_execTimeStamp); }
                set { _execTimeStamp = ArisApi_a.DoubleTwiddling(value); }
            }
            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char ParticipantType;
            internal byte Reserved1;
            private short _competitorPeriod;
            private short _solicitorPeriod;
            internal char ModifiedOrCancelledBy;
            internal byte Reserved2;
            private short _reasonCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            internal char[] Reserved3 = new char[4];
            internal SecInfo SecInfo;
            private short _auctionNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _counterPartyBrokerID;
            internal char Suspended;
            private double _orderNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _accountNumber;
            private short _bookType;
            private short _buyOrSellIndicator;
            private int _discloseVolume;
            private int _disclosedVolumeRemaining;
            private int _totalVolumeRemaining;
            private int _volume;
            private int _volumeFilledToday;
            private int _price;
            private int _triggerPrice;
            private int _goodTillDate;
            private int _entryDateTime;
            private int _minimumFillOrAONVolume;
            private int _lastModified;
            internal OrderFlags OrderFlag;
            private short _branchID;
            private int _traderID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _brokerID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            private char[] _remarks;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _settlor;
            private short _proOrClientIndicator;
            private short _settlementPeriod;
            private double _nnfField;
            private double _execTimeStamp;
            internal int Filler;
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderRequestResponse
        {
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class MarketOrderResponse
        {
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderConfirmationResponse
        {
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderFreezeResponse
        {
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderEntryErrorResponse
        {
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderModificationRequest
        {
            internal OrderModificationRequest()
            {
                OrderEntry = new OrderEntryRequest(NseCmMessageCode.OrderModifyRequest);
            }
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderModificationResponse
        {
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderModificationConfirmationResponse
        {
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderModificationErrorResponse
        {
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderCancellationRequest
        {
            internal OrderCancellationRequest()
            {
                OrderEntry = new OrderEntryRequest(NseCmMessageCode.OrderCancelRequest);
            }
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderCancellationResponse
        {
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderCancellationConfirmationResponse
        {
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderCancellationErrorResponse
        {
            internal OrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class BatchOrderCancel
        {
            internal OrderEntryRequest OrderEntry;
        }

        #endregion

        #region Trade Inquiry

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeInquiryData
        {
            #region Propery

            internal int IFillNumber { get { return IPAddress.NetworkToHostOrder(_iFillNumber); } }

            internal int IFillQty { get { return IPAddress.NetworkToHostOrder(_iFillQty); } }
            internal int IFillPrice { get { return IPAddress.NetworkToHostOrder(_iFillPrice); } }
            internal short IMktType { get { return IPAddress.NetworkToHostOrder(_iMktType); } }
            internal int INewVolume { get { return IPAddress.NetworkToHostOrder(_iNewVolume); } }
            internal string BuyParticipantId { get { return new string(_buyParticipantId); } }
            internal string SellParticipantId { get { return new string(_sellParticipantId); } }
            internal int TraderId { get { return IPAddress.NetworkToHostOrder(_traderId); } }
            internal short RequestedBy { get { return IPAddress.NetworkToHostOrder(_requestedBy); } }
            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private int _iFillNumber;
            private int _iFillQty;
            private int _iFillPrice;
            private short _iMktType;
            private int _iNewVolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _buyParticipantId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _sellParticipantId;
            private int _traderId;
            private short _requestedBy;

            #endregion
        }

        #endregion

        #endregion
    }

    namespace UnsolicitedMessages
    {
        #region Unsolicited Messages

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeConfirmation
        {
            #region Propery

            internal double ResponseOrderNumber { get { return ArisApi_a.DoubleTwiddling(_responseOrderNumber); } }
            internal string BrokerId { get { return new string(_brokerId); } }
            internal int TraderNumber { get { return IPAddress.NetworkToHostOrder(_traderNumber); } }
            internal string AccountNumber { get { return new string(_accountNumber); } }
            internal short BuyOrSell { get { return IPAddress.NetworkToHostOrder(_buyOrSell); } }
            internal int OriginalVolume { get { return IPAddress.NetworkToHostOrder(_originalVolume); } }
            internal int DisclosedVolume { get { return IPAddress.NetworkToHostOrder(_disclosedVolume); } }
            internal int RemainingVolume { get { return IPAddress.NetworkToHostOrder(_remainingVolume); } }
            internal int DisclosedVolumeRemaining { get { return IPAddress.NetworkToHostOrder(_disclosedVolumeRemaining); } }
            internal int Price { get { return IPAddress.NetworkToHostOrder(_price); } }
            internal int GoodTillDate { get { return IPAddress.NetworkToHostOrder(_goodTillDate); } }
            internal int FillNumber { get { return IPAddress.NetworkToHostOrder(_fillNumber); } }
            internal int FillQuantity { get { return IPAddress.NetworkToHostOrder(_fillQuantity); } }
            internal int FillPrice { get { return IPAddress.NetworkToHostOrder(_fillPrice); } }
            internal int VolumeFilledToday { get { return IPAddress.NetworkToHostOrder(_volumeFilledToday); } }
            internal string ActivityType { get { return new string(_activityType); } }
            internal int ActivityTime { get { return IPAddress.NetworkToHostOrder(_activityTime); } }
            internal double CounterTraderOrderNumber { get { return ArisApi_a.DoubleTwiddling(_counterTraderOrderNumber); } }
            internal string CounterBrokerId { get { return new string(_counterBrokerId); } }
            internal int NewVolume { get { return IPAddress.NetworkToHostOrder(_newVolume); } }
            internal short ProClient { get { return IPAddress.NetworkToHostOrder(_proClient); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private double _responseOrderNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _brokerId;
            internal byte Reserved1;
            private int _traderNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _accountNumber;
            private short _buyOrSell;
            private int _originalVolume;
            private int _disclosedVolume;
            private int _remainingVolume;
            private int _disclosedVolumeRemaining;
            private int _price;
            //internal OrderFlags OrderFlag;
            private int _goodTillDate;
            private int _fillNumber;
            private int _fillQuantity;
            private int _fillPrice;
            private int _volumeFilledToday;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _activityType;
            private int _activityTime;
            private double _counterTraderOrderNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _counterBrokerId;
            internal SecInfo SecInfo;
            private byte Reserved;
            internal short BookType;
            private int _newVolume;
            private short _proClient;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class BroadMsgSendFromControl
        {
            #region Property

            internal int TraderID { get { return IPAddress.NetworkToHostOrder(_traderID); } }
            internal short BroadMsgLength { get { return IPAddress.NetworkToHostOrder(_broadMsgLength); } }
            internal string BroadcastMessage { get { return new string(_broadcastMessage); } }
            internal string ActionCode { get { return new string(_actionCode); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private int _traderID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            internal char[] _actionCode;
            internal char Reserved2;
            private short _broadMsgLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 239)]
            private char[] _broadcastMessage;

            #endregion
        }

        #region Nested Structure

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct BroadcastDestination
        {
            internal byte TraderWs;
            internal byte Reserved;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class HeartBeatMessageResponse
        {
            internal MessageHeader MessageHeader;
        }

        #endregion
    }

    namespace BhavCopy
    {
        #region BhavCopy

        #region Nested Structure

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct MarketStatisticsData
        {
            #region Property

            internal short MarketType
            {
                get { return IPAddress.NetworkToHostOrder(_marketType); }
                set { _marketType = IPAddress.HostToNetworkOrder(value); }
            }
            internal int OpenPrice
            {
                get { return IPAddress.NetworkToHostOrder(_openPrice); }
                set { _openPrice = IPAddress.HostToNetworkOrder(value); }
            }
            internal int HighPrice
            {
                get { return IPAddress.NetworkToHostOrder(_highPrice); }
                set { _highPrice = IPAddress.HostToNetworkOrder(value); }
            }
            internal int LowPrice
            {
                get { return IPAddress.NetworkToHostOrder(_lowPrice); }
                set { _lowPrice = IPAddress.HostToNetworkOrder(value); }
            }
            internal int ClosingPrice
            {
                get { return IPAddress.NetworkToHostOrder(_closingPrice); }
                set { _closingPrice = IPAddress.HostToNetworkOrder(value); }
            }
            internal int TotalQuantityTraded
            {
                get { return IPAddress.NetworkToHostOrder(_totalQuantityTraded); }
                set { _totalQuantityTraded = IPAddress.HostToNetworkOrder(value); }
            }
            internal double TotalValueTraded
            {
                get { return ArisApi_a.DoubleTwiddling(_totalValueTraded); }
                set { _totalValueTraded = ArisApi_a.DoubleTwiddling(value); }
            }
            internal int PreviousClosePrice
            {
                get { return IPAddress.NetworkToHostOrder(_previousClosePrice); }
                set { _previousClosePrice = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int FiftyTwoWeekHigh
            {
                get { return IPAddress.NetworkToHostOrder(_fiftyTwoWeekHigh); }
                set { _fiftyTwoWeekHigh = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int FiftyTwoWeekLow
            {
                get { return IPAddress.NetworkToHostOrder(_fiftyTwoWeekLow); }
                set { _fiftyTwoWeekLow = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public string CorporateActionIndicator
            {
                get { return new string(_corporateActionIndicator); }
                set { _corporateActionIndicator = value.PadRight(4, ' ').ToCharArray(); }
            }

            #endregion

            #region Member variable

            private short _marketType;
            private int _openPrice;
            private int _highPrice;
            private int _lowPrice;
            private int _closingPrice;
            private int _totalQuantityTraded;
            private double _totalValueTraded;
            private int _previousClosePrice;
            private int _fiftyTwoWeekHigh;
            private int _fiftyTwoWeekLow;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            private char[] _corporateActionIndicator;

            #endregion
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class HeaderOfReportOnMarketStatistics
        {
            #region Property

            internal short UserType
            {
                get { return IPAddress.NetworkToHostOrder(_userType); }
                set { _userType = IPAddress.HostToNetworkOrder(value); }
            }
            internal string BrokerId
            {
                get { return new string(_brokerId); }
                set { _brokerId = value.PadRight(5, ' ').ToCharArray(); }
            }
            internal string FirmName
            {
                get { return new string(_firmName); }
                set { _firmName = value.PadRight(25, ' ').ToCharArray(); }
            }
            internal int TraderNumber
            {
                get { return IPAddress.NetworkToHostOrder(_traderNumber); }
                set { _traderNumber = IPAddress.HostToNetworkOrder(value); }
            }
            internal string TraderName
            {
                get { return new string(_traderName); }
                set { _traderName = value.PadRight(26, ' ').ToCharArray(); }
            }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char MessageType;
            private int _reportDate;
            private short _userType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _brokerId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            private char[] _firmName;
            private int _traderNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            private char[] _traderName;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class ReportOnMarketStatistics
        {
            #region Property

            internal short NumberOfRecords
            {
                get { return IPAddress.NetworkToHostOrder(_numberOfRecords); }
                set { _numberOfRecords = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char MessageType;
            internal byte Reserved1;
            private short _numberOfRecords;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            internal MarketStatisticsData[] MarketStatisticsData;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TrailerOfReportOnMarketStatistics
        {
            #region Property

            internal int NoOfPackets
            {
                get { return IPAddress.NetworkToHostOrder(_noOfPackets); }
                set { _noOfPackets = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char MessageType;
            private int _noOfPackets;
            private char Reserved;

            #endregion
        }

        #endregion

        #region Report on Index

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class MarketIndex
        {
            #region Property

            internal string IndexName { get { return new string(_indexName); } }
            internal int IndexPreClose { get { return IPAddress.NetworkToHostOrder(_indexPreClose); } }
            internal int IdnexOpenining { get { return IPAddress.NetworkToHostOrder(_idnexOpenining); } }
            internal int IndexHigh { get { return IPAddress.NetworkToHostOrder(_indexHigh); } }
            internal int IndexLow { get { return IPAddress.NetworkToHostOrder(_indexLow); } }
            internal int IndexClosing { get { return IPAddress.NetworkToHostOrder(_indexClosing); } }
            internal int IndexPercent { get { return IPAddress.NetworkToHostOrder(_indexPercent); } }
            internal int IndexYearHigh { get { return IPAddress.NetworkToHostOrder(_indexYearHigh); } }
            internal int IndexYearLow { get { return IPAddress.NetworkToHostOrder(_indexYearLow); } }
            internal int IndexStart { get { return IPAddress.NetworkToHostOrder(_indexStart); } }

            #endregion

            #region Member variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            private char[] _indexName;
            private int _indexPreClose;
            private int _idnexOpenining;
            private int _indexHigh;
            private int _indexLow;
            private int _indexClosing;
            private int _indexPercent;
            private int _indexYearHigh;
            private int _indexYearLow;
            private int _indexStart;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class ReportMarketIndex
        {
            #region Property

            internal short NoOfIndexRecs { get { return IPAddress.NetworkToHostOrder(_noOfIndexRecs); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char MessageType;
            internal byte Reserved;
            private short _noOfIndexRecs;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            internal MarketIndex[] MarkeIndex;

            #endregion
        }

        #endregion
    }

    namespace Inquiry
    {
        #region Inquiry

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class AuctionInquiryRequest
        {
            #region Constructor

            public AuctionInquiryRequest()
            {
                MessageHeader = new MessageHeader(NseCmMessageCode.AuctionInquiryRequest, this);
            }

            #endregion

            #region Property

            internal short AuctionNo { get { return IPAddress.NetworkToHostOrder(_auctionNo); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private SecInfo SecInfo;
            private short _auctionNo;
            internal char PageIndicator;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class InquiryData
        {
            #region Property

            internal short Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            internal short AuctionNumber { get { return IPAddress.NetworkToHostOrder(_auctionNumber); } }
            internal short AuctionStatus { get { return IPAddress.NetworkToHostOrder(_auctionStatus); } }
            internal short InitiatorType { get { return IPAddress.NetworkToHostOrder(_initiatorType); } }
            internal int TotalBuy { get { return IPAddress.NetworkToHostOrder(_totalBuy); } }
            internal int BestBuyPrice { get { return IPAddress.NetworkToHostOrder(_bestBuyPrice); } }
            internal int TotalSell { get { return IPAddress.NetworkToHostOrder(_totalSell); } }
            internal int BestSellPrice { get { return IPAddress.NetworkToHostOrder(_bestSellPrice); } }
            internal int AuctionPrice { get { return IPAddress.NetworkToHostOrder(_auctionPrice); } }
            internal int AuctionQuantity { get { return IPAddress.NetworkToHostOrder(_auctionQuantity); } }
            internal int SettlementPeriod { get { return IPAddress.NetworkToHostOrder(_settlementPeriod); } }

            #endregion

            #region Member variables

            private short _token;
            private short _auctionNumber;
            private short _auctionStatus;
            private short _initiatorType;
            private int _totalBuy;
            private int _bestBuyPrice;
            private int _totalSell;
            private int _bestSellPrice;
            private int _auctionPrice;
            private int _auctionQuantity;
            private int _settlementPeriod;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class AuctionInquiryResponse
        {
            #region Property

            internal short NumberOfRecords { get { return IPAddress.NetworkToHostOrder(_numberOfRecords); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private short _numberOfRecords;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            internal InquiryData[] InquiryData;

            #endregion
        }

        #endregion
    }

    namespace Broadcast
    {
        #region Compress Packet

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct CompressPacket
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public string NetId { get { return new string(_netId); } }
            /// <summary>
            /// 
            /// </summary>
            public short NoOfPacket { get { return IPAddress.NetworkToHostOrder(_noOfPacket); } }

            #endregion

            #region Member variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _netId;
            private short _noOfPacket;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 508)]
            public byte[] PacketData;

            #endregion
        }

        #endregion

        #region Compress Sub Packet

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct CompressSubPacket
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short CompressLength { get { return IPAddress.NetworkToHostOrder(_compressLength); } }

            #endregion

            #region Member variables

            private short _compressLength;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 506)]
            public byte[] PacketData;

            #endregion
        }

        #endregion

        #region Nested Structure

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct Sec_Info
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public string Symbol { get { return new string(_symbol); } }
            /// <summary>
            /// 
            /// </summary>
            public string Series { get { return new string(_series); } }

            #endregion

            #region Member variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _symbol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _series;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct Index
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public string IndexName { get { return new string(_indexName); } }
            /// <summary>
            /// 
            /// </summary>
            public int PreviousClose { get { return IPAddress.NetworkToHostOrder(_previousClose); } }
            /// <summary>
            /// 
            /// </summary>
            public int OpeningIndex { get { return IPAddress.NetworkToHostOrder(_openingIndex); } }
            /// <summary>
            /// 
            /// </summary>
            public int HighIndex { get { return IPAddress.NetworkToHostOrder(_highIndex); } }
            /// <summary>
            /// 
            /// </summary>
            public int LowIndex { get { return IPAddress.NetworkToHostOrder(_lowIndex); } }
            /// <summary>
            /// 
            /// </summary>
            public int ClosingIndex { get { return IPAddress.NetworkToHostOrder(_closingIndex); } }
            /// <summary>
            /// 
            /// </summary>
            public int PercentChange { get { return IPAddress.NetworkToHostOrder(_percentChange); } }
            /// <summary>
            /// 
            /// </summary>
            public int YearlyHigh { get { return IPAddress.NetworkToHostOrder(_yearlyHigh); } }
            /// <summary>
            /// 
            /// </summary>
            public int YearlyLow { get { return IPAddress.NetworkToHostOrder(_yearlyLow); } }
            /// <summary>
            /// 
            /// </summary>
            public int IndexStart { get { return IPAddress.NetworkToHostOrder(_indexStart); } }

            #endregion

            #region Member Variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            private char[] _indexName;
            private int _previousClose;
            private int _openingIndex;
            private int _highIndex;
            private int _lowIndex;
            private int _closingIndex;
            private int _percentChange;
            private int _yearlyHigh;
            private int _yearlyLow;
            private int _indexStart;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct BroadCastDestination
        {
            /// <summary>
            /// 
            /// </summary>
            public byte Flag;
            /// <summary>
            /// 
            /// </summary>
            public byte Reserved;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct SecurityEligibleIndicators
        {
            /// <summary>
            /// 
            /// </summary>
            public byte Indicators;
            /// <summary>
            /// 
            /// </summary>
            public byte Reserved;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct SecurityEligibilityPerMarket
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short Status { get { return IPAddress.NetworkToHostOrder(_status); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            public byte Filler;
            private short _status;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct EligibilityIndicators
        {
            /// <summary>
            /// 
            /// </summary>
            public byte Flag;
            /// <summary>
            /// 
            /// </summary>
            public byte Reserved;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct Purpose
        {
            /// <summary>
            /// 
            /// </summary>
            public byte Flag;
            /// <summary>
            /// 
            /// </summary>
            public byte Flag1;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct SecurityStatusPerMarket
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short Status { get { return IPAddress.NetworkToHostOrder(_status); } }

            #endregion

            #region Member variables

            private short _status;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TokenAndEligibility
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short Token { get { return IPAddress.NetworkToHostOrder(_token); } }

            #endregion

            #region Member variables

            private short _token;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public SecurityStatusPerMarket[] SecurityStatusPerMarket;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct AuctionInquiryInfo
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            /// <summary>
            /// 
            /// </summary>
            public short AuctionNumber { get { return IPAddress.NetworkToHostOrder(_auctionNumber); } }
            /// <summary>
            /// 
            /// </summary>
            public short AuctionStatus { get { return IPAddress.NetworkToHostOrder(_auctionStatus); } }
            /// <summary>
            /// 
            /// </summary>
            public short InitiatorType { get { return IPAddress.NetworkToHostOrder(_initiatorType); } }
            /// <summary>
            /// 
            /// </summary>
            public int TotalBuyQty { get { return IPAddress.NetworkToHostOrder(_totalBuyQty); } }
            /// <summary>
            /// 
            /// </summary>
            public int BestBuyPrice { get { return IPAddress.NetworkToHostOrder(_bestBuyPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int TotalSellQty { get { return IPAddress.NetworkToHostOrder(_totalSellQty); } }
            /// <summary>
            /// 
            /// </summary>
            public int BestSellPrice { get { return IPAddress.NetworkToHostOrder(_bestSellPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int AuctionPrice { get { return IPAddress.NetworkToHostOrder(_auctionPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int AuctionQty { get { return IPAddress.NetworkToHostOrder(_auctionQty); } }
            /// <summary>
            /// 
            /// </summary>
            public short SettlementPeriod { get { return IPAddress.NetworkToHostOrder(_settlementPeriod); } }

            #endregion

            #region Member variables

            private short _token;
            private short _auctionNumber;
            private short _auctionStatus;
            private short _initiatorType;
            private int _totalBuyQty;
            private int _bestBuyPrice;
            private int _totalSellQty;
            private int _bestSellPrice;
            private int _auctionPrice;
            private int _auctionQty;
            private short _settlementPeriod;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TickerIndexInfo
        {
            #region property

            /// <summary>
            /// 
            /// </summary>
            public short Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            /// <summary>
            /// 
            /// </summary>
            public short MarketType { get { return IPAddress.NetworkToHostOrder(_marketType); } }
            /// <summary>
            /// 
            /// </summary>
            public int FillPrice { get { return IPAddress.NetworkToHostOrder(_fillPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int FillVolume { get { return IPAddress.NetworkToHostOrder(_fillVolume); } }
            /// <summary>
            /// 
            /// </summary>
            public int MarketIndexValue { get { return IPAddress.NetworkToHostOrder(_marketIndexValue); } }

            #endregion

            #region Member variables

            private short _token;
            private short _marketType;
            private int _fillPrice;
            private int _fillVolume;
            private int _marketIndexValue;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct InteractiveMBOData
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            /// <summary>
            /// 
            /// </summary>
            public short BookType { get { return IPAddress.NetworkToHostOrder(_bookType); } }
            /// <summary>
            /// 
            /// </summary>
            public short TradingStatus { get { return IPAddress.NetworkToHostOrder(_tradingStatus); } }
            /// <summary>
            /// 
            /// </summary>
            public int VolumeTradedToday { get { return IPAddress.NetworkToHostOrder(_volumeTradedToday); } }
            /// <summary>
            /// 
            /// </summary>
            public int LastTradedPrice { get { return IPAddress.NetworkToHostOrder(_lastTradedPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int NetPriceChangeFromClosingPrice { get { return IPAddress.NetworkToHostOrder(_netPriceChangeFromClosingPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int LastTradeQuantity { get { return IPAddress.NetworkToHostOrder(_lastTradeQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public int LastTradeTime { get { return IPAddress.NetworkToHostOrder(_lastTradeTime); } }
            /// <summary>
            /// 
            /// </summary>
            public int AverageTradePrice { get { return IPAddress.NetworkToHostOrder(_averageTradePrice); } }
            /// <summary>
            /// 
            /// </summary>
            public short AuctionNumber { get { return IPAddress.NetworkToHostOrder(_auctionNumber); } }
            /// <summary>
            /// 
            /// </summary>
            public short AuctionStatus { get { return IPAddress.NetworkToHostOrder(_auctionStatus); } }
            /// <summary>
            /// 
            /// </summary>
            public short InitiatorType { get { return IPAddress.NetworkToHostOrder(_initiatorType); } }
            /// <summary>
            /// 
            /// </summary>
            public int InitiatorPrice { get { return IPAddress.NetworkToHostOrder(_initiatorPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int InitiatorQuantity { get { return IPAddress.NetworkToHostOrder(_initiatorQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public int AuctionPrice { get { return IPAddress.NetworkToHostOrder(_auctionPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int AuctionQuantity { get { return IPAddress.NetworkToHostOrder(_auctionQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public double BbTotalBuyQuantity { get { return ArisApi_a.DoubleBitReverse(_bbTotalBuyQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public double BbTotalSellQuantity { get { return ArisApi_a.DoubleBitReverse(_bbTotalSellQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public double TotalBuyQuantity { get { return ArisApi_a.DoubleBitReverse(_totalBuyQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public double TotalSellQuantity { get { return ArisApi_a.DoubleBitReverse(_totalSellQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public int ClosingPrice { get { return IPAddress.NetworkToHostOrder(_closingPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int OpenPrice { get { return IPAddress.NetworkToHostOrder(_openPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int HighPrice { get { return IPAddress.NetworkToHostOrder(_highPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int LowPrice { get { return IPAddress.NetworkToHostOrder(_lowPrice); } }

            #endregion

            #region Member Variables

            private short _token;
            private short _bookType;
            private short _tradingStatus;
            private int _volumeTradedToday;
            private int _lastTradedPrice;
            /// <summary>
            /// 
            /// </summary>
            public char NetChangeIndicator;
            private int _netPriceChangeFromClosingPrice;
            private int _lastTradeQuantity;
            private int _lastTradeTime;
            private int _averageTradePrice;
            private short _auctionNumber;
            private short _auctionStatus;
            private short _initiatorType;
            private int _initiatorPrice;
            private int _initiatorQuantity;
            private int _auctionPrice;
            private int _auctionQuantity;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public MBOInformation[] BuyMBOInformation;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public MBOInformation[] SellMBOInformation;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public MBPInfo[] BuyMBPInfo;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public MBPInfo[] SellMBPInfo;
            private short _bbTotalBuyQuantity;
            private short _bbTotalSellQuantity;
            private double _totalBuyQuantity;
            private double _totalSellQuantity;
            /// <summary>
            /// 
            /// </summary>
            public Indicator Indicator;
            private int _closingPrice;
            private int _openPrice;
            private int _highPrice;
            private int _lowPrice;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct MBOInformation
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short TraderId { get { return IPAddress.NetworkToHostOrder(_traderId); } }
            /// <summary>
            /// 
            /// </summary>
            public int Quantity { get { return IPAddress.NetworkToHostOrder(_quantity); } }
            /// <summary>
            /// 
            /// </summary>
            public int Price { get { return IPAddress.NetworkToHostOrder(_price); } }
            /// <summary>
            /// 
            /// </summary>
            public int MinFillQuantity { get { return IPAddress.NetworkToHostOrder(_minFillQuantity); } }

            #endregion

            #region Member variables
            private short _traderId;
            private int _quantity;
            private int _price;
            /// <summary>
            /// 
            /// </summary>
            public MBOMBPTerms ST_MBO_MBP_TERMS;
            private int _minFillQuantity;
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct MBPInfo
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public int Quantity { get { return IPAddress.NetworkToHostOrder(_quantity); } }
            /// <summary>
            /// 
            /// </summary>
            public int Price { get { return IPAddress.NetworkToHostOrder(_price); } }
            /// <summary>
            /// 
            /// </summary>
            public short NoOfOrders { get { return IPAddress.NetworkToHostOrder(_noOfOrders); } }
            /// <summary>
            /// 
            /// </summary>
            public short BbBuySellFlag { get { return IPAddress.NetworkToHostOrder(_bbBuySellFlag); } }

            #endregion

            #region Member variables

            private int _quantity;
            private int _price;
            private short _noOfOrders;
            private short _bbBuySellFlag;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct Indicator
        {
            /// <summary>
            /// 
            /// </summary>
            public byte Flag;
            /// <summary>
            /// 
            /// </summary>
            public byte Reserved;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct MBOMBPTerms
        {
            /// <summary>
            /// 
            /// </summary>
            public byte Flag;
            /// <summary>
            /// 
            /// </summary>
            public byte Reserved;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct InteractiveMBPData
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short Token
            {
                get { return IPAddress.NetworkToHostOrder(_token); }
                set { _token = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public short BookType
            {
                get { return IPAddress.NetworkToHostOrder(_bookType); }
                set { _bookType = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public short TradingStatus
            {
                get { return IPAddress.NetworkToHostOrder(_tradingStatus); }
                set { _tradingStatus = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int VolumeTradedToday
            {
                get { return IPAddress.NetworkToHostOrder(_volumeTradedToday); }
                set { _volumeTradedToday = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int LastTradedPrice
            {
                get { return IPAddress.NetworkToHostOrder(_lastTradedPrice); }
                set { _lastTradedPrice = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int NetPriceChangeFromClosingPrice
            {
                get { return IPAddress.NetworkToHostOrder(_netPriceChangeFromClosingPrice); }
                set { _netPriceChangeFromClosingPrice = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int LastTradeQuantity
            {
                get { return IPAddress.NetworkToHostOrder(_lastTradeQuantity); }
                set { _lastTradeQuantity = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int LastTradeTime
            {
                get { return IPAddress.NetworkToHostOrder(_lastTradeTime); }
                set { _lastTradeTime = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int AverageTradePrice
            {
                get { return IPAddress.NetworkToHostOrder(_averageTradePrice); }
                set { _averageTradePrice = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public short AuctionNumber
            {
                get { return IPAddress.NetworkToHostOrder(_auctionNumber); }
                set { _auctionNumber = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public short AuctionStatus
            {
                get { return IPAddress.NetworkToHostOrder(_auctionStatus); }
                set { _auctionStatus = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public short InitiatorType
            {
                get { return IPAddress.NetworkToHostOrder(_initiatorType); }
                set { _initiatorType = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int InitiatorPrice
            {
                get { return IPAddress.NetworkToHostOrder(_initiatorPrice); }
                set { _initiatorPrice = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int InitiatorQuantity
            {
                get { return IPAddress.NetworkToHostOrder(_initiatorQuantity); }
                set { _initiatorQuantity = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int AuctionPrice
            {
                get { return IPAddress.NetworkToHostOrder(_auctionPrice); }
                set { _auctionPrice = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int AuctionQuantity
            {
                get { return IPAddress.NetworkToHostOrder(_auctionQuantity); }
                set { _auctionQuantity = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public short BbTotalBuyFlag
            {
                get { return IPAddress.NetworkToHostOrder(_bbTotalBuyFlag); }
                set { _bbTotalBuyFlag = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public short BbTotalSellFlag
            {
                get { return IPAddress.NetworkToHostOrder(_bbTotalSellFlag); }
                set { _bbTotalSellFlag = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public double TotalBuyQuantity
            {
                get { return ArisApi_a.DoubleBitReverse(_totalBuyQuantity); }
                set { _totalBuyQuantity = ArisApi_a.DoubleBitReverse(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public double TotalSellQuantity
            {
                get { return ArisApi_a.DoubleBitReverse(_totalSellQuantity); }
                set { _totalSellQuantity = ArisApi_a.DoubleBitReverse(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int ClosingPrice
            {
                get { return IPAddress.NetworkToHostOrder(_closingPrice); }
                set { _closingPrice = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int OpenPrice
            {
                get { return IPAddress.NetworkToHostOrder(_openPrice); }
                set { _openPrice = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int HighPrice
            {
                get { return IPAddress.NetworkToHostOrder(_highPrice); }
                set { _highPrice = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int LowPrice
            {
                get { return IPAddress.NetworkToHostOrder(_lowPrice); }
                set { _lowPrice = IPAddress.NetworkToHostOrder(value); }
            }

            #endregion

            #region Member variables
            private short _token;
            private short _bookType;
            private short _tradingStatus;
            private int _volumeTradedToday;
            private int _lastTradedPrice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _netChangeIndicator;
            private int _netPriceChangeFromClosingPrice;
            private int _lastTradeQuantity;
            private int _lastTradeTime;
            private int _averageTradePrice;
            private short _auctionNumber;
            private short _auctionStatus;
            private short _initiatorType;
            private int _initiatorPrice;
            private int _initiatorQuantity;
            private int _auctionPrice;
            private int _auctionQuantity;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public MBPInformation[] BuyMBPInformation;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public MBPInformation[] SellMBPInformation;
            private short _bbTotalBuyFlag;
            private short _bbTotalSellFlag;
            private double _totalBuyQuantity;
            private double _totalSellQuantity;
            /// <summary>
            /// 
            /// </summary>
            public Indicator Indicator;
            private int _closingPrice;
            private int _openPrice;
            private int _highPrice;
            private int _lowPrice;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct MBPInformation
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public int Quantity
            {
                get { return IPAddress.NetworkToHostOrder(_quantity); }
                set { _quantity = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int Price
            {
                get { return IPAddress.NetworkToHostOrder(_price); }
                set { _price = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public short NumberOfOrders
            {
                get { return IPAddress.NetworkToHostOrder(_numberOfOrders); }
                set { _numberOfOrders = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public short BbBuySellFlag
            {
                get { return IPAddress.NetworkToHostOrder(_bbBuySellFlag); }
                set { _bbBuySellFlag = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            private int _quantity;
            private int _price;
            private short _numberOfOrders;
            private short _bbBuySellFlag;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct MarketWatch
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short Token { get { return IPAddress.NetworkToHostOrder(_token); } }

            #endregion

            #region Member variables

            private short _token;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public MarketWiseInfo[] MarketWiseInfo;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct MarketWiseInfo
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public int BuyVolume { get { return IPAddress.NetworkToHostOrder(_buyVolume); } }
            /// <summary>
            /// 
            /// </summary>
            public int BuyPrice { get { return IPAddress.NetworkToHostOrder(_buyPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int SellVolume { get { return IPAddress.NetworkToHostOrder(_sellVolume); } }
            /// <summary>
            /// 
            /// </summary>
            public int SellPrice { get { return IPAddress.NetworkToHostOrder(_sellPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int LastTradePrice { get { return IPAddress.NetworkToHostOrder(_lastTradePrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int LastTradeTime { get { return IPAddress.NetworkToHostOrder(_lastTradeTime); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            public Indicator Indicator;
            private int _buyVolume;
            private int _buyPrice;
            private int _sellVolume;
            private int _sellPrice;
            private int _lastTradePrice;
            private int _lastTradeTime;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct Indices
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public string IndexName { get { return new string(_indexName); } }
            /// <summary>
            /// 
            /// </summary>
            public int IndexValue { get { return IPAddress.NetworkToHostOrder(_indexValue); } }
            /// <summary>
            /// 
            /// </summary>
            public int HighIndexValue { get { return IPAddress.NetworkToHostOrder(_highIndexValue); } }
            /// <summary>
            /// 
            /// </summary>
            public int LowIndexValue { get { return IPAddress.NetworkToHostOrder(_lowIndexValue); } }
            /// <summary>
            /// 
            /// </summary>
            public int OpeningIndex { get { return IPAddress.NetworkToHostOrder(_openingIndex); } }
            /// <summary>
            /// 
            /// </summary>
            public int ClosingIndex { get { return IPAddress.NetworkToHostOrder(_closingIndex); } }
            /// <summary>
            /// 
            /// </summary>
            public int PercentChange { get { return IPAddress.NetworkToHostOrder(_percentChange); } }
            /// <summary>
            /// 
            /// </summary>
            public int YearlyHigh { get { return IPAddress.NetworkToHostOrder(_yearlyHigh); } }
            /// <summary>
            /// 
            /// </summary>
            public int YearlyLow { get { return IPAddress.NetworkToHostOrder(_yearlyLow); } }
            /// <summary>
            /// 
            /// </summary>
            public int NoOfUpmoves { get { return IPAddress.NetworkToHostOrder(_noOfUpmoves); } }
            /// <summary>
            /// 
            /// </summary>
            public int NoOfDownmoves { get { return IPAddress.NetworkToHostOrder(_noOfDownmoves); } }
            /// <summary>
            /// 
            /// </summary>
            public double MarketCapitalisation { get { return ArisApi_a.DoubleBitReverse(_marketCapitalisation); } }

            #endregion

            #region Member Variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
            private char[] _indexName;
            private int _indexValue;
            private int _highIndexValue;
            private int _lowIndexValue;
            private int _openingIndex;
            private int _closingIndex;
            private int _percentChange;
            private int _yearlyHigh;
            private int _yearlyLow;
            private int _noOfUpmoves;
            private int _noOfDownmoves;
            private double _marketCapitalisation;
            /// <summary>
            /// 
            /// </summary>
            public char NetChangeIndicator;
            /// <summary>
            /// 
            /// </summary>
            public char Filler;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct IndustryIndices
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public string IndexName { get { return new string(_indexName); } }
            /// <summary>
            /// 
            /// </summary>
            public int IndexValue { get { return IPAddress.NetworkToHostOrder(_indexValue); } }

            #endregion

            #region Member Variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
            private char[] _indexName;
            private int _indexValue;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct BuyBackData
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            private short Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            /// <summary>
            /// 
            /// </summary>
            public string Symbol { get { return new string(_symbol); } }
            /// <summary>
            /// 
            /// </summary>
            public string Series { get { return new string(_series); } }
            /// <summary>
            /// 
            /// </summary>
            public double PDayComVol { get { return ArisApi_a.DoubleBitReverse(_pDayComVol); } }
            /// <summary>
            /// 
            /// </summary>
            public int PDayHighPrice { get { return IPAddress.NetworkToHostOrder(_pDayHighPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int PDayLowPrice { get { return IPAddress.NetworkToHostOrder(_pDayLowPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int PDayWtAvg { get { return IPAddress.NetworkToHostOrder(_pDayWtAvg); } }
            /// <summary>
            /// 
            /// </summary>
            public double CDayComVol { get { return ArisApi_a.DoubleBitReverse(_cDayComVol); } }
            /// <summary>
            /// 
            /// </summary>
            public int CDayHighPrice { get { return IPAddress.NetworkToHostOrder(_cDayHighPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int CDayLowPrice { get { return IPAddress.NetworkToHostOrder(_cDayLowPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int CDayWtAvg { get { return IPAddress.NetworkToHostOrder(_cDayWtAvg); } }
            /// <summary>
            /// 
            /// </summary>
            public int StartDate { get { return IPAddress.NetworkToHostOrder(_startDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int EndDate { get { return IPAddress.NetworkToHostOrder(_endDate); } }

            #endregion

            #region Member Variables

            private short _token;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _symbol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _series;
            private double _pDayComVol;
            private int _pDayHighPrice;
            private int _pDayLowPrice;
            private int _pDayWtAvg;
            private double _cDayComVol;
            private int _cDayHighPrice;
            private int _cDayLowPrice;
            private int _cDayWtAvg;
            private int _startDate;
            private int _endDate;

            #endregion
        }

        #endregion

        #region GeneralMessageBroadcast

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct GeneralMessageBroadcast
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short BranchNumber { get { return IPAddress.NetworkToHostOrder(_branchNumber); } }
            /// <summary>
            /// 
            /// </summary>
            public string BrokerNumber { get { return new string(_brokerNumber); } }
            /// <summary>
            /// 
            /// </summary>
            public string ActionCode { get { return new string(_actionCode); } }
            /// <summary>
            /// 
            /// </summary>
            public short BroadcastMessageLength { get { return IPAddress.NetworkToHostOrder(_broadcastMessageLength); } }
            /// <summary>
            /// 
            /// </summary>
            public string BroadcastMessage { get { return new string(_broadcastMessage); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private short _branchNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _brokerNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            private char[] _actionCode;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Reserved;
            /// <summary>
            /// 
            /// </summary>
            public BroadCastDestination BroadcastDestination;
            private short _broadcastMessageLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 239)]
            private char[] _broadcastMessage;

            #endregion
        }

        #endregion

        #region ChangeInSystemStatus

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct ChangeInSystemStatus
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short Normal { get { return IPAddress.NetworkToHostOrder(_normal); } }
            /// <summary>
            /// 
            /// </summary>
            public short Oddlot { get { return IPAddress.NetworkToHostOrder(_oddlot); } }
            /// <summary>
            /// 
            /// </summary>
            public short Spot { get { return IPAddress.NetworkToHostOrder(_spot); } }
            /// <summary>
            /// 
            /// </summary>
            public short Auction { get { return IPAddress.NetworkToHostOrder(_auction); } }
            /// <summary>
            /// 
            /// </summary>
            public int MarketIndex { get { return IPAddress.NetworkToHostOrder(_marketIndex); } }
            /// <summary>
            /// 
            /// </summary>
            public short DefaultSettlementPeriodNormal { get { return IPAddress.NetworkToHostOrder(_defaultSettlementPeriodNormal); } }
            /// <summary>
            /// 
            /// </summary>
            public short DefaultSettlementPeriodSpot { get { return IPAddress.NetworkToHostOrder(_defaultSettlementPeriodSpot); } }
            /// <summary>
            /// 
            /// </summary>
            public short DefaultSettlementPeriodAuction { get { return IPAddress.NetworkToHostOrder(_defaultSettlementPeriodAuction); } }
            /// <summary>
            /// 
            /// </summary>
            public short CompetitorPeriod { get { return IPAddress.NetworkToHostOrder(_competitorPeriod); } }
            /// <summary>
            /// 
            /// </summary>
            public short SolicitorPeriod { get { return IPAddress.NetworkToHostOrder(_solicitorPeriod); } }
            /// <summary>
            /// 
            /// </summary>
            public short WarningPercent { get { return IPAddress.NetworkToHostOrder(_warningPercent); } }
            /// <summary>
            /// 
            /// </summary>
            public short VolumeFreezePercent { get { return IPAddress.NetworkToHostOrder(_volumeFreezePercent); } }
            /// <summary>
            /// 
            /// </summary>
            public short TerminalIdleTime { get { return IPAddress.NetworkToHostOrder(_terminalIdleTime); } }
            /// <summary>
            /// 
            /// </summary>
            public int BoardLotQuantity { get { return IPAddress.NetworkToHostOrder(_boardLotQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public int TickSize { get { return IPAddress.NetworkToHostOrder(_tickSize); } }
            /// <summary>
            /// 
            /// </summary>
            public short MaximumGtcDays { get { return IPAddress.NetworkToHostOrder(_maximumGtcDays); } }
            /// <summary>
            /// 
            /// </summary>
            public short DisclosedQuantityPercentAllowed { get { return IPAddress.NetworkToHostOrder(_disclosedQuantityPercentAllowed); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private short _normal;
            private short _oddlot;
            private short _spot;
            private short _auction;
            private int _marketIndex;
            private short _defaultSettlementPeriodNormal;
            private short _defaultSettlementPeriodSpot;
            private short _defaultSettlementPeriodAuction;
            private short _competitorPeriod;
            private short _solicitorPeriod;
            private short _warningPercent;
            private short _volumeFreezePercent;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Reserved1;
            private short _terminalIdleTime;
            private int _boardLotQuantity;
            private int _tickSize;
            private short _maximumGtcDays;
            /// <summary>
            /// 
            /// </summary>
            public SecurityEligibleIndicators SecurityEligibleIndicators;
            private short _disclosedQuantityPercentAllowed;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Reserved2;

            #endregion
        }

        #endregion

        #region SecurityMasterUpdate

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct SecurityMasterUpdate
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            /// <summary>
            /// 
            /// </summary>
            public short InstrumentType { get { return IPAddress.NetworkToHostOrder(_instrumentType); } }
            /// <summary>
            /// 
            /// </summary>
            public short PermittedToTrade { get { return IPAddress.NetworkToHostOrder(_permittedToTrade); } }
            /// <summary>
            /// 
            /// </summary>
            public double IssuedCapital { get { return ArisApi_a.DoubleBitReverse(_issuedCapital); } }
            /// <summary>
            /// 
            /// </summary>
            public int WarningQuantity { get { return IPAddress.NetworkToHostOrder(_warningQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public int FreezeQuantity { get { return IPAddress.NetworkToHostOrder(_freezeQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public string CreditRating { get { return new string(_creditRating); } }
            /// <summary>
            /// 
            /// </summary>
            public short IssueRate { get { return IPAddress.NetworkToHostOrder(_issueRate); } }
            /// <summary>
            /// 
            /// </summary>
            public int IssueStartDate { get { return IPAddress.NetworkToHostOrder(_issueStartDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int InterestPaymentDate { get { return IPAddress.NetworkToHostOrder(_interestPaymentDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int IssueMaturityDate { get { return IPAddress.NetworkToHostOrder(_issueMaturityDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int BoardLotQuantity { get { return IPAddress.NetworkToHostOrder(_boardLotQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public int TickSize { get { return IPAddress.NetworkToHostOrder(_tickSize); } }
            /// <summary>
            /// 
            /// </summary>
            public string Name { get { return new string(_name); } }
            /// <summary>
            /// 
            /// </summary>
            public int ListingDate { get { return IPAddress.NetworkToHostOrder(_listingDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int ExpulsionDate { get { return IPAddress.NetworkToHostOrder(_expulsionDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int ReAdmissionDate { get { return IPAddress.NetworkToHostOrder(_reAdmissionDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int RecordDate { get { return IPAddress.NetworkToHostOrder(_recordDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int ExpiryDate { get { return IPAddress.NetworkToHostOrder(_expiryDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int NoDeliveryStartDate { get { return IPAddress.NetworkToHostOrder(_noDeliveryStartDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int NoDeliveryEndDate { get { return IPAddress.NetworkToHostOrder(_noDeliveryEndDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int BookClosureStartDate { get { return IPAddress.NetworkToHostOrder(_bookClosureStartDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int BookClosureEndDate { get { return IPAddress.NetworkToHostOrder(_bookClosureEndDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int LocalUpdateDateTime { get { return IPAddress.NetworkToHostOrder(_localUpdateDateTime); } }
            /// <summary>
            /// 
            /// </summary>
            public string Remark { get { return new string(_remark); } }
            /// <summary>
            /// 
            /// </summary>
            public int FaceValue { get { return IPAddress.NetworkToHostOrder(_faceValue); } }
            /// <summary>
            /// 
            /// </summary>
            public string IsinNumber { get { return new string(_isinNumber); } }
            /// <summary>
            /// 
            /// </summary>
            public int Spread { get { return IPAddress.NetworkToHostOrder(_spread); } }
            /// <summary>
            /// 
            /// </summary>
            public int MinQty { get { return IPAddress.NetworkToHostOrder(_minQty); } }
            /// <summary>
            /// 
            /// </summary>
            public short CallAuction1Flag { get { return IPAddress.NetworkToHostOrder(_callAuction1Flag); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private short _token;
            /// <summary>
            /// 
            /// </summary>
            public Sec_Info Sec_Info;
            private short _instrumentType;
            private short _permittedToTrade;
            private double _issuedCapital;
            private short _warningQuantity;
            private short _freezeQuantity;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
            private char[] _creditRating;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public SecurityEligibilityPerMarket[] SecurityEligibilityPerMarket;
            private short _issueRate;
            private int _issueStartDate;
            private int _interestPaymentDate;
            private int _issueMaturityDate;
            private int _boardLotQuantity;
            private int _tickSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            private char[] _name;
            /// <summary>
            /// 
            /// </summary>
            public byte Reserved;
            private int _listingDate;
            private int _expulsionDate;
            private int _reAdmissionDate;
            private int _recordDate;
            private int _expiryDate;
            private int _noDeliveryStartDate;
            private int _noDeliveryEndDate;
            /// <summary>
            /// 
            /// </summary>
            public EligibilityIndicators EligibilityIndicators;
            private int _bookClosureStartDate;
            private int _bookClosureEndDate;
            /// <summary>
            /// 
            /// </summary>
            public Purpose Purpose;
            private int _localUpdateDateTime;
            /// <summary>
            /// 
            /// </summary>
            public char DeleteFlag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            private char[] _remark;
            private int _faceValue;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _isinNumber;
            /// <summary>
            /// 
            /// </summary>
            private int _spread;
            /// <summary>
            /// 
            /// </summary>
            private int _minQty;
            /// <summary>
            /// 
            /// </summary>
            private short _callAuction1Flag;
            /// <summary>
            /// 
            /// </summary>
            public short Reserved2;

            #endregion
        }

        #endregion

        #region ChangeParticipantStatus

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct ChangeParticipantStatus
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public string ParticipantId { get { return new string(_participantId); } }
            /// <summary>
            /// 
            /// </summary>
            public string ParticipantName { get { return new string(_participantName); } }
            /// <summary>
            /// 
            /// </summary>
            public int ParticipantUpdateDateTime { get { return IPAddress.NetworkToHostOrder(_participantUpdateDateTime); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _participantId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            private char[] _participantName;
            /// <summary>
            /// 
            /// </summary>
            public char ParticipantStatus;
            private int _participantUpdateDateTime;
            /// <summary>
            /// 
            /// </summary>
            public char DeleteFlag;

            #endregion
        }

        #endregion

        #region ChangeOfSecurityStatus

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct ChangeOfSecurityStatus
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short NumberOfRecords { get { return IPAddress.NetworkToHostOrder(_numberOfRecords); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private short _numberOfRecords;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 43)]
            public TokenAndEligibility[] TokenAndEligibility;

            #endregion
        }

        #endregion

        #region TurnoverLimitExceededOrBrokerReactived

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct TurnoverLimitExceededOrBrokerReactived
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public string BrokerCode { get { return new string(_brokerCode); } }
            /// <summary>
            /// 
            /// </summary>
            public string CounterBrokerCode { get { return new string(_counterBrokerCode); } }
            /// <summary>
            /// 
            /// </summary>
            public short WarningType { get { return IPAddress.NetworkToHostOrder(_warningType); } }
            /// <summary>
            /// 
            /// </summary>
            public int TradeNumber { get { return IPAddress.NetworkToHostOrder(_tradeNumber); } }
            /// <summary>
            /// 
            /// </summary>
            public int TradePrice { get { return IPAddress.NetworkToHostOrder(_tradePrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int TradeVolume { get { return IPAddress.NetworkToHostOrder(_tradeVolume); } }

            #endregion

            #region Member variable

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _brokerCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _counterBrokerCode;
            private short _warningType;
            /// <summary>
            /// 
            /// </summary>
            public Sec_Info Sec_Info;
            private int _tradeNumber;
            private int _tradePrice;
            private int _tradeVolume;
            /// <summary>
            /// 
            /// </summary>
            public char Final;

            #endregion
        }

        #endregion

        #region AuctionAcitivityMessage

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct AuctionAcitivityMessage
        {
            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            /// <summary>
            /// 
            /// </summary>
            public AuctionInquiryInfo AuctionInquiryInfo;

            #endregion
        }

        #endregion

        #region ChangeOfAuctionStatus

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct ChangeOfAuctionStatus
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short AuctionNumber { get { return IPAddress.NetworkToHostOrder(_auctionNumber); } }
            /// <summary>
            /// 
            /// </summary>
            public string AuctionCode { get { return new string(_actionCode); } }
            /// <summary>
            /// 
            /// </summary>
            public short BroadcastMessageLength { get { return IPAddress.NetworkToHostOrder(_broadcastMessageLength); } }
            /// <summary>
            /// 
            /// </summary>
            public string BroadcastMessage { get { return new string(_broadcastMessage); } }

            #endregion

            #region Member variable

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            /// <summary>
            /// 
            /// </summary>
            public Sec_Info Sec_Info;
            private short _auctionNumber;
            /// <summary>
            /// 
            /// </summary>
            public char AuctionStatus;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            private char[] _actionCode;
            /// <summary>
            /// 
            /// </summary>
            public BroadCastDestination BroadCastDestination;
            private short _broadcastMessageLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 239)]
            private char[] _broadcastMessage;

            #endregion
        }

        #endregion

        #region ChangeofMarketStatus

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct ChangeofMarketStatus
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short MarketType { get { return IPAddress.NetworkToHostOrder(_marketType); } }
            /// <summary>
            /// 
            /// </summary>
            public short BroadcastMessageLength { get { return IPAddress.NetworkToHostOrder(_broadcastMessageLength); } }
            /// <summary>
            /// 
            /// </summary>
            public string BroadcastMessage { get { return new string(_broadcastMessage); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            /// <summary>
            /// 
            /// </summary>
            public Sec_Info Sec_Info;
            private short _marketType;
            /// <summary>
            /// 
            /// </summary>
            public BroadCastDestination BroadCastDestination;
            private short _broadcastMessageLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 239)]
            private char[] _broadcastMessage;

            #endregion
        }

        #endregion

        #region TickerAndMarketIndex

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public class TickerAndMarketIndex
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short NumberOfRecords
            {
                get { return IPAddress.NetworkToHostOrder(_numberOfRecords); }
                set { _numberOfRecords = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variable

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private short _numberOfRecords;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
            public TickerIndexInfo[] TickerIndexInfo;

            #endregion
        }

        #endregion

        #region MarketByOrderOrMarketByPriceUpdate

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct MarketByOrderOrMarketByPriceUpdate
        {
            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            /// <summary>
            /// 
            /// </summary>
            public InteractiveMBOData InteractiveMBOData;

            #endregion
        }

        #endregion

        #region OnlyMarketByPriceUpdate

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public class OnlyMarketByPriceUpdate
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short NumberOfRecords
            {
                get { return IPAddress.NetworkToHostOrder(_numberOfRecords); }
                set { _numberOfRecords = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private short _numberOfRecords;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public InteractiveMBPData[] InteractiveMBPData;

            #endregion
        }

        #endregion

        #region MarketWatchUpdate

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public class MarketWatchUpdate
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short NumberOfRecords
            {
                get { return IPAddress.NetworkToHostOrder(_numberOfRecords); }
                set { _numberOfRecords = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private short _numberOfRecords;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public MarketWatch[] MarketWatch;

            #endregion
        }

        #endregion

        #region SecurityOpenMessage

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct SecurityOpenMessage
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            /// <summary>
            /// 
            /// </summary>
            public int OpeningPrice { get { return IPAddress.NetworkToHostOrder(_openingPrice); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            /// <summary>
            /// 
            /// </summary>
            public Sec_Info Sec_Info;
            private short _token;
            private int _openingPrice;

            #endregion
        }

        #endregion

        #region BroadcastCircuitCheck

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct BroadcastCircuitCheck
        {
            #region Member Variable

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;

            #endregion
        }

        #endregion

        #region MultipleIndexBroadcast

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct MultipleIndexBroadcast
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short NumberOfRecords { get { return IPAddress.NetworkToHostOrder(_numberOfRecords); } }

            #endregion

            #region Member Variable

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private short _numberOfRecords;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public Indices[] Indices;

            #endregion
        }

        #endregion

        #region BroadcastIndustryIndex

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct BroadcastIndustryIndex
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short NumberOfRecords { get { return IPAddress.NetworkToHostOrder(_numberOfRecords); } }

            #endregion

            #region Member Variable

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private short _numberOfRecords;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
            public IndustryIndices[] IndustryIndices;

            #endregion
        }

        #endregion

        #region BroadcastBuyBackInformation

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct BroadcastBuyBackInformation
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short NumberOfRecords { get { return IPAddress.NetworkToHostOrder(_numberOfRecords); } }

            #endregion

            #region Member Variable

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private short _numberOfRecords;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public BuyBackData[] BuyBackData;

            #endregion
        }

        #endregion

        #region ErrorMessage

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct ErrorMessage
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public string Error { get { return new string(_error); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            /// <summary>
            /// 
            /// </summary>
            public Sec_Info Sec_Info;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            private char[] _error;

            #endregion
        }

        #endregion
    }

}
