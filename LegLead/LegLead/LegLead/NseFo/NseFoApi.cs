using System.Net;
using System.Runtime.InteropServices;
using LegLead.NseFoApi.Header;
using LegLead.NseFoApi.SpreadOrderAndTradeManagement;
using LegLead.NseFoApi.OrderAndTradeManagement;
using LegLead.AppClasses;


namespace LegLead.NseFoApi
{
    namespace Header
    {
        #region MessageHeader

        #region Message Header

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class MessageHeader
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
            /// <param name="NseFoMessageCode"></param>
            /// <param name="obj"></param>
            internal MessageHeader(NseFoMessageCode NseFoMessageCode, object obj)
            {
                AlphaChar = string.Empty;
                TransCode = (short)NseFoMessageCode;
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

        #region Error Message Response

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class ErrorMessageResponse
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            internal string Key
            {
                get { return new string(_key); }
            }
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            private char[] _key;
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
                MessageHeader = new MessageHeader(NseFoMessageCode.LogonRequest, this);
                BrokerStatus = ' ';
                ShowIndex = ' ';
                ClearingStatus = ' ';
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
                set { _brokerName = value.PadRight(25, ' ').ToCharArray(); }
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
            internal short _memberType;
            internal char ClearingStatus;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
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
            internal short MemberType { get { return IPAddress.NetworkToHostOrder(_memberType); } }
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
            internal char Reseverd2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            internal byte[] Colour;
            internal char Reseverd3;
            private short _userType;
            private double _sequenceNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            internal byte[] Reserved3;
            internal char BrokerStatus;
            internal char ShowIndex;
            internal BrokerEligibilityPerMkt STBrokerEligibilityPerMkt;
            private short _memberType;
            internal char ClearingStatus;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
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
                MessageHeader = new MessageHeader(NseFoMessageCode.LogoffRequest, this);
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

            #endregion

            #region Member variables

            private short _normal;
            private short _oddlot;
            private short _spot;
            private short _auction;

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

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct IndexDetails
        {
            #region Properties

            internal string IndexName { get { return new string(_indexName); } }
            internal int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            internal int LastUpdateDateTime { get { return IPAddress.NetworkToHostOrder(_lastUpdateDateTime); } }

            #endregion

            #region Member variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            private char[] _indexName;
            internal char Reserved;
            private int _token;
            private int _lastUpdateDateTime;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct IndexMapDetails
        {
            #region Properties

            internal string BcastName { get { return new string(_bcastName); } }
            internal string ChangedName { get { return new string(_changedName); } }
            internal int LastUpdateDateTime { get { return IPAddress.NetworkToHostOrder(_lastUpdateDateTime); } }

            #endregion

            #region Member variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            private char[] _bcastName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _changedName;
            internal char DeleteFlag;
            private int _lastUpdateDateTime;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct PortfolioData
        {
            #region Properties

            internal string Portfolio { get { return new string(_portfolio); } }
            internal int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            internal int LastUpdtDtTime { get { return IPAddress.NetworkToHostOrder(_lastUpdtDtTime); } }

            #endregion

            #region Member variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            private char[] _portfolio;
            private int _token;
            private int _lastUpdtDtTime;
            internal char DeleteFlag;

            #endregion
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
                MessageHeader = new MessageHeader(NseFoMessageCode.SystemInformationRequest, this);
            }

            #endregion

            #region Property

            internal int LastUpdatePortfolioTime
            {
                get { return IPAddress.NetworkToHostOrder(_lastUpdatePortfolioTime); }
                set { _lastUpdatePortfolioTime = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private int _lastUpdatePortfolioTime;

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
            internal short SnapQuoteTime { get { return IPAddress.NetworkToHostOrder(_snapQuoteTime); } }
            internal int BoardLotQuantity { get { return IPAddress.NetworkToHostOrder(_boardLotQuantity); } }
            internal int TickSize { get { return IPAddress.NetworkToHostOrder(_tickSize); } }
            internal short MaximumGtcDays { get { return IPAddress.NetworkToHostOrder(_maximumGtcDays); } }
            internal short DisclosedQuantityPercentAllowed { get { return IPAddress.NetworkToHostOrder(_disclosedQuantityPercentAllowed); } }
            internal int RiskFreeInterestRate { get { return IPAddress.NetworkToHostOrder(_riskFreeInterestRate); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal MarketStatus MarketStatus;
            internal MarketStatus ExMarketStatus;
            internal MarketStatus PlMarketStatus;
            internal char UpdatePoftfolio;
            private int _marketIndex;
            internal char Reserved1;
            private short _defaultSettlementPeriodNormal;
            private short _defaultSettlementPeriodSpot;
            private short _defaultSettlementPeriodAuction;
            private short _competitorPeriod;
            private short _solicitorPeriod;
            private short _warningPercent;
            private short _volumeFreezePercent;
            private short _snapQuoteTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal byte[] Reseverd2;
            private int _boardLotQuantity;
            private int _tickSize;
            private short _maximumGtcDays;
            internal StockEligibleIndicators StockEligibleIndicators;
            private short _disclosedQuantityPercentAllowed;
            private int _riskFreeInterestRate;

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
                MessageHeader = new MessageHeader(NseFoMessageCode.UpdateLocalDatabaseRequest, this);
            }

            #endregion

            #region Property

            internal int LastUpdateSecurityTime { get { return IPAddress.NetworkToHostOrder(_lastUpdateSecurityTime); } }
            internal int LastUpdateParticipantTime { get { return IPAddress.NetworkToHostOrder(_lastUpdateParticipantTime); } }
            internal int LastUpdateInstrumentTime { get { return IPAddress.NetworkToHostOrder(_lastUpdateInstrumentTime); } }
            internal int LastUpdateIndexTime { get { return IPAddress.NetworkToHostOrder(_lastUpdateIndexTime); } }

            #endregion

            #region Member variable

            internal MessageHeader MessageHeader;
            private int _lastUpdateSecurityTime;
            private int _lastUpdateParticipantTime;
            private int _lastUpdateInstrumentTime;
            private int _lastUpdateIndexTime;
            internal char RequestForOpenOrders;
            internal char Reserved1;
            internal MarketStatus MarketStatus;
            internal MarketStatus ExMarketStatus;
            internal MarketStatus PlMarketStatus;

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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 436)]
            internal byte[] Data;

            #endregion
        }

        #endregion

        #region Change in Index Master

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class ChangeinIndexMaster
        {
            #region Property

            internal short NoOfRecords { get { return IPAddress.NetworkToHostOrder(_noOfRecords); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private short _noOfRecords;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
            internal IndexDetails[] IndexDetails;

            #endregion
        }

        #endregion

        #region Index Map Table

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class IndexMapTable
        {
            #region Property

            internal short NoOfRecords { get { return IPAddress.NetworkToHostOrder(_noOfRecords); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private short _noOfRecords;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            internal IndexMapDetails[] IndexMapDetails;

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

        #region Portfolio Request

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class PortfolioRequest
        {
            #region Constructor

            internal PortfolioRequest()
            {
                MessageHeader = new MessageHeader(NseFoMessageCode.PortfolioRequest, this);
            }

            #endregion

            #region Property

            internal int LastUpdateDateTime
            {
                get { return IPAddress.NetworkToHostOrder(_lastUpdateDateTime); }
                set { _lastUpdateDateTime = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private int _lastUpdateDateTime;

            #endregion
        }

        #endregion

        #region Portfolio Response

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class PortfolioResponse
        {
            #region Property

            internal int NoOfRecords { get { return IPAddress.NetworkToHostOrder(_noOfRecords); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private short _noOfRecords;
            internal char MoreRecords;
            internal char Filler;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            internal PortfolioData[] PortfolioData;

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
                MessageHeader = new MessageHeader(NseFoMessageCode.MessageDownloadRequest, this);
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 436)]
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
        internal struct ContractDescription
        {
            #region Property

            internal string InstrumentName
            {
                get { return new string(_instrumentName); }
                set { _instrumentName = value.PadRight(6, ' ').ToCharArray(); }
            }
            internal string Symbol
            {
                get { return new string(_symbol); }
                set { _symbol = value.PadRight(10, ' ').ToCharArray(); }
            }
            internal int ExpiryDate
            {
                get { return IPAddress.NetworkToHostOrder(_expiryDate); }
                set { _expiryDate = IPAddress.HostToNetworkOrder(value); }
            }
            internal int StrikePrice
            {
                get { return IPAddress.NetworkToHostOrder(_strikePrice); }
                set { _strikePrice = IPAddress.HostToNetworkOrder(value); }
            }
            internal string OptionType
            {
                get { return new string(_optionType); }
                set { _optionType = value.PadRight(2, ' ').ToCharArray(); }
            }
            internal short CALevel
            {
                get { return IPAddress.NetworkToHostOrder(_cALevel); }
                set { _cALevel = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            private char[] _instrumentName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _symbol;
            private int _expiryDate;
            private int _strikePrice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _optionType;
            private short _cALevel;

            #endregion
        }

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
        /// MessageHeader = new MessageHeader(NseFoMessageCode.LogonRequest, this);
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
                MessageHeader = new MessageHeader(NseFoMessageCode.OrderEntryRequest, this);
                //Reserved3 = string.Empty.PadRight(5, ' ').ToCharArray();
                //Reserved4 = string.Empty.PadRight(2, ' ').ToCharArray();
                //FillerOptions1 = string.Empty;
                //Fillerx1 = ' ';
                //Fillery1 = ' ';
                //Settlor = string.Empty;
                //ParticipantType = ' ';
            }

            /// <summary>
            /// Constructor
            /// </summary>
            internal OrderEntryRequest(NseFoMessageCode NseFoMessageCode)
            {
                MessageHeader = new MessageHeader(NseFoMessageCode, this);
                //Reserved3 = string.Empty.PadRight(5, ' ').ToCharArray();
                //Reserved4 = string.Empty.PadRight(2, ' ').ToCharArray();
                //FillerOptions1 = string.Empty;
                //Fillerx1 = ' ';
                //Fillery1 = ' ';
                //Settlor = string.Empty;
                //ParticipantType = ' ';
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
            internal int TokenNo
            {
                get { return IPAddress.NetworkToHostOrder(_tokenNo); }
                set { _tokenNo = IPAddress.HostToNetworkOrder(value); }
            }
            internal string CounterPartyBrokerID
            {
                get { return new string(_counterPartyBrokerID); }
                set { _counterPartyBrokerID = value.PadRight(5, ' ').ToCharArray(); }
            }
            internal string FillerOptions1
            {
                get { return new string(_fillerOptions1); }
                set { _fillerOptions1 = value.PadRight(3, ' ').ToCharArray(); }
            }
            internal short OrderType
            {
                get { return IPAddress.NetworkToHostOrder(_orderType); }
                set { _orderType = IPAddress.HostToNetworkOrder(value); }
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
                set { _remarks = value.PadRight(24, ' ').ToCharArray(); }
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
            internal double MktReplay
            {
                get { return ArisApi_a.DoubleTwiddling(_mktReplay); }
                set { _mktReplay = ArisApi_a.DoubleTwiddling(value); }
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
            private int _tokenNo;
            internal ContractDescription ContractDesc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _counterPartyBrokerID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            private char[] _fillerOptions1;
            internal char CloseoutFlag;
            internal char Fillerx1;
            private short _orderType;
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            private char[] _remarks;
            internal char OpenClose;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _settlor;
            private short _proOrClientIndicator;
            private short _settlementPeriod;
            internal char CoverOrUncover;
            internal char GiveupFlag;
            internal int Filler;
            private double _nnfField;
            private double _mktReplay;

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
                OrderEntry = new OrderEntryRequest(NseFoMessageCode.OrderModifyRequest);
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
                OrderEntry = new OrderEntryRequest(NseFoMessageCode.OrderCancelRequest);
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

        #region Trade

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeModificationRequest
        {
            #region Constructor

            /// <summary>
            /// Constructor
            /// </summary>
            internal TradeModificationRequest()
            {
                MessageHeader = new MessageHeader(NseFoMessageCode.TradeModificationRequest, this);
            }

            /// <summary>
            /// Constructor
            /// </summary>
            internal TradeModificationRequest(NseFoMessageCode NseFoMessageCode)
            {
                MessageHeader = new MessageHeader(NseFoMessageCode, this);
            }

            #endregion

            #region Property

            internal int TokenNo
            {
                get { return IPAddress.NetworkToHostOrder(_tokenNo); }
                set { _tokenNo = IPAddress.HostToNetworkOrder(value); }
            }
            internal int FillNumber
            {
                get { return IPAddress.NetworkToHostOrder(_fillNumber); }
                set { _fillNumber = IPAddress.HostToNetworkOrder(value); }
            }
            internal int FillQuantity
            {
                get { return IPAddress.NetworkToHostOrder(_fillQuantity); }
                set { _fillQuantity = IPAddress.HostToNetworkOrder(value); }
            }
            internal int FillPrice
            {
                get { return IPAddress.NetworkToHostOrder(_fillPrice); }
                set { _fillPrice = IPAddress.HostToNetworkOrder(value); }
            }
            internal int NewVolume
            {
                get { return IPAddress.NetworkToHostOrder(_newVolume); }
                set { _newVolume = IPAddress.HostToNetworkOrder(value); }
            }
            internal string BuyBrokerID
            {
                get { return new string(_buyBrokerID); }
                set { _buyBrokerID = value.PadRight(5, ' ').ToCharArray(); }
            }
            internal string SellBrokerID
            {
                get { return new string(_sellBrokerID); }
                set { _sellBrokerID = value.PadRight(5, ' ').ToCharArray(); }
            }
            internal int TraderID
            {
                get { return IPAddress.NetworkToHostOrder(_traderID); }
                set { _traderID = IPAddress.HostToNetworkOrder(value); }
            }
            internal string BuyAccountNumber
            {
                get { return new string(_buyAccountNumber); }
                set { _buyAccountNumber = value.PadRight(10, ' ').ToCharArray(); }
            }
            internal string SellAccountNumber
            {
                get { return new string(_sellAccountNumber); }
                set { _sellAccountNumber = value.PadRight(10, ' ').ToCharArray(); }
            }
            internal string BuyParticipant
            {
                get { return new string(_buyParticipant); }
                set { _buyParticipant = value.PadRight(12, ' ').ToCharArray(); }
            }
            internal string SellParticipant
            {
                get { return new string(_sellParticipant); }
                set { _sellParticipant = value.PadRight(12, ' ').ToCharArray(); }
            }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private int _tokenNo;
            internal ContractDescription ContractDescription;
            private int _fillNumber;
            private int _fillQuantity;
            private int _fillPrice;
            internal char MktType;
            internal char BuyOpenClose;
            private int _newVolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _buyBrokerID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _sellBrokerID;
            private int _traderID;
            internal char RequestedBy;
            internal char SellOpenClose;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _buyAccountNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _sellAccountNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _buyParticipant;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _sellParticipant;
            internal char BuyCoverUncover;
            internal char SellCoverUncover;
            internal char BuyGiveupflag;
            internal char SellGiveupflag;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeModificationRequestResponse
        {
            internal TradeModificationRequest TradeModification;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeModificationError
        {
            internal TradeModificationRequest TradeModification;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeCancellationRequest
        {
            internal TradeCancellationRequest()
            {
                TradeModificationRequest = new TradeModificationRequest(NseFoMessageCode.TradeCancellationRequest);
            }
            internal TradeModificationRequest TradeModificationRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeCancellationResponse
        {
            internal TradeModificationRequest TradeModification;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeCancellationError
        {
            internal TradeModificationRequest TradeModification;
        }

        #endregion

        #endregion
    }

    namespace SpreadOrderAndTradeManagement
    {
        #region Spread Order and Trade management

        #region Nested Structure

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct SpreadOrderLegInfo
        {
            #region Property

            internal int Token2
            {
                get { return IPAddress.NetworkToHostOrder(_token2); }
                set { _token2 = IPAddress.HostToNetworkOrder(value); }
            }
            internal string OpBrokerId2
            {
                get { return new string(_opBrokerId2); }
                set { _opBrokerId2 = value.PadRight(5, ' ').ToCharArray(); }
            }
            internal short OrderType2
            {
                get { return IPAddress.NetworkToHostOrder(_orderType2); }
                set { _orderType2 = IPAddress.HostToNetworkOrder(value); }
            }
            internal short BuySell2
            {
                get { return IPAddress.NetworkToHostOrder(_buySell2); }
                set { _buySell2 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int DisclosedVol2
            {
                get { return IPAddress.NetworkToHostOrder(_disclosedVol2); }
                set { _disclosedVol2 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int DisclosedVolRemaining2
            {
                get { return IPAddress.NetworkToHostOrder(_disclosedVolRemaining2); }
                set { _disclosedVolRemaining2 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int TotalVolRemaining2
            {
                get { return IPAddress.NetworkToHostOrder(_totalVolRemaining2); }
                set { _totalVolRemaining2 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int Volume2
            {
                get { return IPAddress.NetworkToHostOrder(_volume2); }
                set { _volume2 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int VolumeFilledToday2
            {
                get { return IPAddress.NetworkToHostOrder(_volumeFilledToday2); }
                set { _volumeFilledToday2 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int Price2
            {
                get { return IPAddress.NetworkToHostOrder(_price2); }
                set { _price2 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int TriggerPrice2
            {
                get { return IPAddress.NetworkToHostOrder(_triggerPrice2); }
                set { _triggerPrice2 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int MinimumFillAON2
            {
                get { return IPAddress.NetworkToHostOrder(_minimumFillAON2); }
                set { _minimumFillAON2 = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            private int _token2;
            internal ContractDescription ContractDesc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _opBrokerId2;
            internal char Fillerx2;
            private short _orderType2;
            private short _buySell2;
            private int _disclosedVol2;
            private int _disclosedVolRemaining2;
            private int _totalVolRemaining2;
            private int _volume2;
            private int _volumeFilledToday2;
            private int _price2;
            private int _triggerPrice2;
            private int _minimumFillAON2;
            //internal OrderFlags OrderFlag;
            internal char OpenClose2;
            internal char CoverUncover2;
            internal char GiveupFlag2;
            internal char FillerY;

            #endregion
        }

        #endregion

        /// <summary>
        /// MS_SPD_OE_REQUEST
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SpreadOrderEntryRequest
        {
            #region Constructor

            /// <summary>
            /// Constructor
            /// </summary>
            internal SpreadOrderEntryRequest()
            {
                MessageHeader1 = new MessageHeader(NseFoMessageCode.SpreadOrderEntryRequest, this);
            }

            /// <summary>
            /// Constructor
            /// </summary>
            internal SpreadOrderEntryRequest(NseFoMessageCode NseFoMessageCode)
            {
                MessageHeader1 = new MessageHeader(NseFoMessageCode, this);
            }

            #endregion

            #region Property

            internal short CompetitorPeriod1
            {
                get { return IPAddress.NetworkToHostOrder(_competitorPeriod1); }
                set { _competitorPeriod1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal short SolicitorPeriod1
            {
                get { return IPAddress.NetworkToHostOrder(_solicitorPeriod1); }
                set { _solicitorPeriod1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal short ReasonCode1
            {
                get { return IPAddress.NetworkToHostOrder(_reasonCode1); }
                set { _reasonCode1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal string StartAlpha
            {
                get { return new string(_startAlpha); }
                set { _startAlpha = value.PadRight(2, ' ').ToCharArray(); }
            }
            internal string EndAlpha1
            {
                get { return new string(_endAlpha1); }
                set { _endAlpha1 = value.PadRight(2, ' ').ToCharArray(); }
            }
            internal int Token1
            {
                get { return IPAddress.NetworkToHostOrder(_token1); }
                set { _token1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal string OpBrokerID1
            {
                get { return new string(_opBrokerID1); }
                set { _opBrokerID1 = value.PadRight(5, ' ').ToCharArray(); }
            }
            internal string FillerOptions1
            {
                get { return new string(_fillerOptions1); }
                set { _fillerOptions1 = value.PadRight(3, ' ').ToCharArray(); }
            }
            internal short OrderType1
            {
                get { return IPAddress.NetworkToHostOrder(_orderType1); }
                set { _orderType1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal double OrderNumber1
            {
                get { return ArisApi_a.DoubleTwiddling(_orderNumber1); }
                set { _orderNumber1 = ArisApi_a.DoubleTwiddling(value); }
            }
            internal string AccountNumber1
            {
                get { return new string(_accountNumber1); }
                set { _accountNumber1 = value.PadRight(10, ' ').ToCharArray(); }
            }
            internal short BookType1
            {
                get { return IPAddress.NetworkToHostOrder(_bookType1); }
                set { _bookType1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal short BuySell1
            {
                get { return IPAddress.NetworkToHostOrder(_buySell1); }
                set { _buySell1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int DiscloseVol1
            {
                get { return IPAddress.NetworkToHostOrder(_discloseVol1); }
                set { _discloseVol1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int DisclosedVolRemaining1
            {
                get { return IPAddress.NetworkToHostOrder(_disclosedVolRemaining1); }
                set { _disclosedVolRemaining1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int TotalVolRemaining1
            {
                get { return IPAddress.NetworkToHostOrder(_totalVolRemaining1); }
                set { _totalVolRemaining1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int Volume1
            {
                get { return IPAddress.NetworkToHostOrder(_volume1); }
                set { _volume1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int VolumeFilledToday1
            {
                get { return IPAddress.NetworkToHostOrder(_volumeFilledToday1); }
                set { _volumeFilledToday1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int Price1
            {
                get { return IPAddress.NetworkToHostOrder(_price1); }
                set { _price1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int TriggerPrice1
            {
                get { return IPAddress.NetworkToHostOrder(_triggerPrice1); }
                set { _triggerPrice1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int GoodTillDate1
            {
                get { return IPAddress.NetworkToHostOrder(_goodTillDate1); }
                set { _goodTillDate1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int EntryDateTime1
            {
                get { return IPAddress.NetworkToHostOrder(_entryDateTime1); }
                set { _entryDateTime1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int MinFillAON1
            {
                get { return IPAddress.NetworkToHostOrder(_minFillAON1); }
                set { _minFillAON1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int LastModified1
            {
                get { return IPAddress.NetworkToHostOrder(_lastModified1); }
                set { _lastModified1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal short BranchID1
            {
                get { return IPAddress.NetworkToHostOrder(_branchID1); }
                set { _branchID1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal int TraderID1
            {
                get { return IPAddress.NetworkToHostOrder(_traderID1); }
                set { _traderID1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal string BrokerID1
            {
                get { return new string(_brokerID1); }
                set { _brokerID1 = value.PadRight(5, ' ').ToCharArray(); }
            }
            internal string OERemarks1
            {
                get { return new string(_oERemarks1); }
                set { _oERemarks1 = value.PadRight(24, ' ').ToCharArray(); }
            }
            internal string Settlor1
            {
                get { return new string(_settlor1); }
                set { _settlor1 = value.PadRight(12, ' ').ToCharArray(); }
            }
            internal short ProClient1
            {
                get { return IPAddress.NetworkToHostOrder(_proClient1); }
                set { _proClient1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal short SettlementPeriod1
            {
                get { return IPAddress.NetworkToHostOrder(_settlementPeriod1); }
                set { _settlementPeriod1 = IPAddress.HostToNetworkOrder(value); }
            }
            internal double NnfField
            {
                get { return ArisApi_a.DoubleTwiddling(_nnfField); }
                set { _nnfField = ArisApi_a.DoubleTwiddling(value); }
            }
            internal double MktReplay
            {
                get { return ArisApi_a.DoubleTwiddling(_mktReplay); }
                set { _mktReplay = ArisApi_a.DoubleTwiddling(value); }
            }
            internal int PriceDiff
            {
                get { return IPAddress.NetworkToHostOrder(_priceDiff); }
                set { _priceDiff = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader1;
            internal char ParticipantType1;
            internal char Filler1;
            private short _competitorPeriod1;
            private short _solicitorPeriod1;
            internal char ModCxlBy1;
            internal char Filler9;
            private short _reasonCode1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _startAlpha;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _endAlpha1;
            private int _token1;
            internal ContractDescription ContractDescription;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _opBrokerID1;
            internal char Fillerx1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            private char[] _fillerOptions1;
            internal char Fillery1;
            private short _orderType1;
            private double _orderNumber1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _accountNumber1;
            private short _bookType1;
            private short _buySell1;
            private int _discloseVol1;
            private int _disclosedVolRemaining1;
            private int _totalVolRemaining1;
            private int _volume1;
            private int _volumeFilledToday1;
            private int _price1;
            private int _triggerPrice1;
            private int _goodTillDate1;
            private int _entryDateTime1;
            private int _minFillAON1;
            private int _lastModified1;
            internal OrderFlags OrderFlag;
            private short _branchID1;
            private int _traderID1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _brokerID1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            private char[] _oERemarks1;
            internal char OpenClose1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _settlor1;
            private short _proClient1;
            private short _settlementPeriod1;
            internal char CoverUncover1;
            internal char GiveupFlag1;
            internal short Reserved16;
            internal char CfilerReserved1;
            internal char CfilerReserved2;
            private double _nnfField;
            private double _mktReplay;
            private int _priceDiff;
            internal SpreadOrderLegInfo Leg2;
            internal SpreadOrderLegInfo Leg3;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SpreadOrderEntryResponse
        {
            internal SpreadOrderEntryRequest SpreadOrder;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SpreadOrderConfirmationResponse
        {
            internal SpreadOrderEntryRequest SpreadOrder;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SpreadOrderErrorResponse
        {
            internal SpreadOrderEntryRequest SpreadOrder;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SpreadOrderCancelConfirmationResponse
        {
            internal SpreadOrderEntryRequest SpreadOrder;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SpreadOrderModificationRequest
        {
            internal SpreadOrderModificationRequest()
            {
                SpreadOrder = new SpreadOrderEntryRequest(NseFoMessageCode.SpreadOrderModificationRequest);
            }
            internal SpreadOrderEntryRequest SpreadOrder;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SpreadOrderModificationResponse
        {
            internal SpreadOrderEntryRequest SpreadOrder;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SpreadOrderModificationConfirmationResponse
        {
            internal SpreadOrderEntryRequest SpreadOrder;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SpreadOrderModificationErrorResponse
        {
            internal SpreadOrderEntryRequest SpreadOrder;
        }

        #endregion
    }

    namespace TwoLegAndThreeLegOrderEntry
    {
        #region 2L & 3L Order Entry

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderEntryRequest2l3l
        {
            internal OrderEntryRequest2l3l(NseFoMessageCode NseFoMessageCode)
            {
                OrderEntry = new SpreadOrderEntryRequest(NseFoMessageCode);
            }

            internal SpreadOrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderRequestedResponse2l3l
        {
            internal SpreadOrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class MarketOrderResponse2l3l
        {
            internal SpreadOrderEntryRequest OrderEntry;
        }

        /// <summary>
        ///  
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderErrorResponse2l3l
        {
            internal SpreadOrderEntryRequest OrderEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OrderCancelConfirmationResponse2l3l
        {
            internal SpreadOrderEntryRequest OrderEntry;
        }

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
            internal int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            internal int NewVolume { get { return IPAddress.NetworkToHostOrder(_newVolume); } }
            internal string OldAccountNumber { get { return new string(_oldAccountNumber); } }
            internal string Participant { get { return new string(_participant); } }
            internal string OldParticipant { get { return new string(_oldParticipant); } }

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
            internal OrderFlags OrderFlag;
            private int _goodTillDate;
            private int _fillNumber;
            private int _fillQuantity;
            private int _fillPrice;
            private int _volumeFilledToday;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _activityType;
            private int _activityTime;
            private double _counterTraderOrderNumber;
            private char temp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _counterBrokerId;
            private int _token;
            internal ContractDescription ContractDescription;
            internal char OpenClose;
            internal char OldOpenClose;
            internal char BookType;
            private char temp1;
            private int _newVolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _oldAccountNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _participant;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _oldParticipant;
            internal char CoverUncover;
            internal char OldCoverUncover;
            internal char GiveupTrade;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeModificationRequestedNotification
        {
            internal BroadMsgSendFromControl TradeModification;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeModificationConfirmationResponse
        {
            internal TradeConfirmation TradeModification;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeModificationRejectionResponse
        {
            internal TradeConfirmation TradeModificationRejection;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeCancellationRequestNotification
        {
            internal BroadMsgSendFromControl TradeCancellation;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeCancellationConfirmationResponse
        {
            internal TradeConfirmation TradeCancellationConfirmation;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradeCancellationRejection
        {
            internal TradeConfirmation TradeCancellation;
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

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private int _traderID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            internal byte[] Reserved1;
            internal char Reserved2;
            private short _broadMsgLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 239)]
            private char[] _broadcastMessage;

            #endregion
        }

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
        #region Bhav Copy

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MarketStatisticsData
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short MarketType
            {
                get { return IPAddress.NetworkToHostOrder(_marketType); }
                set { _marketType = IPAddress.HostToNetworkOrder(value); }
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
                set { _lowPrice = IPAddress.HostToNetworkOrder(value); }
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
            public int TotalQuantityTraded
            {
                get { return IPAddress.NetworkToHostOrder(_totalQuantityTraded); }
                set { _totalQuantityTraded = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public double TotalValueTraded
            {
                get { return ArisApi_a.DoubleTwiddling(_totalValueTraded); }
                set { _totalValueTraded = ArisApi_a.DoubleTwiddling(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int PreviousClosePrice
            {
                get { return IPAddress.NetworkToHostOrder(_previousClosePrice); }
                set { _previousClosePrice = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int OpenInterest
            {
                get { return IPAddress.NetworkToHostOrder(_openInterest); }
                set { _openInterest = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int ChangeOpenInterest
            {
                get { return IPAddress.NetworkToHostOrder(_changeOpenInterest); }
                set { _changeOpenInterest = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Indictor
            {
                get { return new string(_indictor); }
                set { _indictor = value.PadRight(4, ' ').ToCharArray(); }
            }

            #endregion

            #region Member variable

            /// <summary>
            /// 
            /// </summary>
            internal ContractDescription ContractDescription;
            /// <summary>
            /// 
            /// </summary>
            private short _marketType;
            /// <summary>
            /// 
            /// </summary>
            private int _openPrice;
            /// <summary>
            /// 
            /// </summary>
            private int _highPrice;
            /// <summary>
            /// 
            /// </summary>
            private int _lowPrice;
            /// <summary>
            /// 
            /// </summary>
            private int _closingPrice;
            /// <summary>
            /// 
            /// </summary>
            private int _totalQuantityTraded;
            /// <summary>
            /// 
            /// </summary>
            private double _totalValueTraded;
            /// <summary>
            /// 
            /// </summary>
            private int _previousClosePrice;
            /// <summary>
            /// 
            /// </summary>
            private int _openInterest;
            /// <summary>
            /// 
            /// </summary>
            private int _changeOpenInterest;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            private char[] _indictor;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HeaderOfReportOnMarketStatistics
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short UserType
            {
                get { return IPAddress.NetworkToHostOrder(_userType); }
                set { _userType = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public string BrokerId
            {
                get { return new string(_brokerId); }
                set { _brokerId = value.PadRight(5, ' ').ToCharArray(); }
            }
            /// <summary>
            /// 
            /// </summary>
            public string BrokerName
            {
                get { return new string(_brokerName); }
                set { _brokerName = value.PadRight(25, ' ').ToCharArray(); }
            }
            /// <summary>
            /// 
            /// </summary>
            public int TraderNumber
            {
                get { return IPAddress.NetworkToHostOrder(_traderNumber); }
                set { _traderNumber = IPAddress.HostToNetworkOrder(value); }
            }
            /// <summary>
            /// 
            /// </summary>
            public string TraderName
            {
                get { return new string(_traderName); }
                set { _traderName = value.PadRight(26, ' ').ToCharArray(); }
            }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            /// <summary>
            /// 
            /// </summary>
            public char MessageType;
            /// <summary>
            /// 
            /// </summary>
            private int _reportDate;
            /// <summary>
            /// 
            /// </summary>
            private short _userType;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _brokerId;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            private char[] _brokerName;
            /// <summary>
            /// 
            /// </summary>
            private int _traderNumber;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            private char[] _traderName;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ReportOnMarketStatistics
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
            /// <summary>
            /// 
            /// </summary>
            public char MessageType;
            /// <summary>
            /// 
            /// </summary>
            public byte Reserved1;
            /// <summary>
            /// 
            /// </summary>
            private short _numberOfRecords;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public MarketStatisticsData[] MarketStatisticsData;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TrailerOfReportOnMarketStatistics
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public int NoOfPackets
            {
                get { return IPAddress.NetworkToHostOrder(_noOfPackets); }
                set { _noOfPackets = IPAddress.HostToNetworkOrder(value); }
            }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            /// <summary>
            /// 
            /// </summary>
            public char MessageType;
            /// <summary>
            /// 
            /// </summary>
            private int _noOfPackets;
            /// <summary>
            /// 
            /// </summary>
            private char Reserved;

            #endregion
        }

        #endregion

        #region Report on Index

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class MarketIndex
        {
            #region Property

            internal short IdnexOpenining { get { return IPAddress.NetworkToHostOrder(_idnexOpenining); } }
            internal short IndexHigh { get { return IPAddress.NetworkToHostOrder(_indexHigh); } }
            internal short IndexLow { get { return IPAddress.NetworkToHostOrder(_indexLow); } }
            internal short IndexClosing { get { return IPAddress.NetworkToHostOrder(_indexClosing); } }
            internal short IndexStart { get { return IPAddress.NetworkToHostOrder(_indexStart); } }

            #endregion

            #region Member variables

            private short _idnexOpenining;
            private short _indexHigh;
            private short _indexLow;
            private short _indexClosing;
            private short _indexStart;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class ReportMarketIndex
        {
            #region Property

            internal string IndexName { get { return new string(_indexName); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char MessageType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            internal char[] _indexName;
            internal MarketIndex MarkeIndex;

            #endregion
        }

        #endregion

        #region Report on Industry Index

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class MarketIndustryIndex
        {
            #region Property

            internal short IdnexOpenining { get { return IPAddress.NetworkToHostOrder(_idnexOpenining); } }
            internal short IndexHigh { get { return IPAddress.NetworkToHostOrder(_indexHigh); } }
            internal short IndexLow { get { return IPAddress.NetworkToHostOrder(_indexLow); } }
            internal short IndexClosing { get { return IPAddress.NetworkToHostOrder(_indexClosing); } }
            internal short IndexStart { get { return IPAddress.NetworkToHostOrder(_indexStart); } }
            internal string IndustryName { get { return new string(_industryName); } }

            #endregion

            #region Member variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            private char[] _industryName;
            private short _idnexOpenining;
            private short _indexHigh;
            private short _indexLow;
            private short _indexClosing;
            private short _indexStart;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class ReportMarketIndustryIndex
        {
            #region Property

            internal short NoOfReport { get { return IPAddress.NetworkToHostOrder(_noOfReport); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char MessageType;
            internal byte Reserved;
            private short _noOfReport;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            internal MarketIndustryIndex[] MarketIndustryIndex;

            #endregion
        }

        #endregion

        #region Report on Sector Index

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class MarketSectorIndex
        {
            #region Property

            internal short IndexValue { get { return IPAddress.NetworkToHostOrder(_indexValue); } }
            internal string SectorName { get { return new string(_SectorName); } }

            #endregion

            #region Member variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            private char[] _SectorName;
            private short _indexValue;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class ReportMarketSectorIndex
        {
            #region Property

            internal short NoOfReport { get { return IPAddress.NetworkToHostOrder(_noOfReport); } }
            internal string IndustryName { get { return new string(_industryName); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char MessageType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            private char[] _industryName;
            private short _noOfReport;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            internal MarketSectorIndex[] MarketSectorIndex;

            #endregion
        }

        #endregion
    }

    namespace Reports
    {
        #region Reports

        #region Nested Structure

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct OpenOrdersReportData
        {
            #region Property

            internal short MarketType { get { return IPAddress.NetworkToHostOrder(_marketType); } }
            internal double OrderNumber { get { return ArisApi_a.DoubleTwiddling(_orderNumber); } }
            internal int TotalVolume { get { return IPAddress.NetworkToHostOrder(_totalVolume); } }
            internal int OutstadingVolume { get { return IPAddress.NetworkToHostOrder(_outstadingVolume); } }
            internal int DisclosedVolume { get { return IPAddress.NetworkToHostOrder(_disclosedVolume); } }
            internal int DisclosedRemainingVolume { get { return IPAddress.NetworkToHostOrder(_disclosedRemainingVolume); } }
            internal int LimitPrice { get { return IPAddress.NetworkToHostOrder(_limitPrice); } }
            internal int TriggerPrice { get { return IPAddress.NetworkToHostOrder(_triggerPrice); } }
            internal int MinFillAON { get { return IPAddress.NetworkToHostOrder(_minFillAON); } }
            internal int GoodTillDate { get { return IPAddress.NetworkToHostOrder(_goodTillDate); } }
            internal string AccountNumber { get { return new string(_accountNumber); } }
            internal string Remarks { get { return new string(_remarks); } }
            internal string Settlor { get { return new string(_settlor); } }
            internal int EntryDateTime { get { return IPAddress.NetworkToHostOrder(_entryDateTime); } }
            internal string TraderName { get { return new string(_traderName); } }
            internal int TraderNumber { get { return IPAddress.NetworkToHostOrder(_traderNumber); } }
            internal short BuyOrSell { get { return IPAddress.NetworkToHostOrder(_buyOrSell); } }
            internal short Activity { get { return IPAddress.NetworkToHostOrder(_activity); } }
            internal short ProCli { get { return IPAddress.NetworkToHostOrder(_proCli); } }
            internal int ModCxlTraderId { get { return IPAddress.NetworkToHostOrder(_modCxlTraderId); } }

            #endregion

            #region Member variables

            internal ContractDescription ContractDescription;
            private short _marketType;
            private double _orderNumber;
            private int _totalVolume;
            private int _outstadingVolume;
            private int _disclosedVolume;
            private int _disclosedRemainingVolume;
            private int _limitPrice;
            private int _triggerPrice;
            private int _minFillAON;
            private int _goodTillDate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _accountNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            private char[] _remarks;
            internal char OpenOrClose;
            internal char CoverOrUncover;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _settlor;
            private int _entryDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            private char[] _traderName;
            private int _traderNumber;
            private short _buyOrSell;
            private short _activity;
            private short _proCli;
            private int _modCxlTraderId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal byte[] Reserved;
            internal OrderFlags OrderFlags;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TradesTodayReportData
        {
            #region Property

            internal short MarketType { get { return IPAddress.NetworkToHostOrder(_marketType); } }
            internal int QuantityTraded { get { return IPAddress.NetworkToHostOrder(_quantityTraded); } }
            internal int Price { get { return IPAddress.NetworkToHostOrder(_price); } }
            internal double OrderNumber { get { return ArisApi_a.DoubleTwiddling(_orderNumber); } }
            internal int TraderId { get { return IPAddress.NetworkToHostOrder(_traderId); } }
            internal int BuyOrSell { get { return IPAddress.NetworkToHostOrder(_buyOrSell); } }
            internal string AccountNumber { get { return new string(_accountNumber); } }
            internal string Settlor { get { return new string(_settlor); } }
            internal string CounterBorker { get { return new string(_counterBorker); } }
            internal int ModCxlTraderId { get { return IPAddress.NetworkToHostOrder(_modCxlTraderId); } }
            internal short Activity { get { return IPAddress.NetworkToHostOrder(_activity); } }
            internal int TradeNumber { get { return IPAddress.NetworkToHostOrder(_tradeNumber); } }
            internal int TradeDateTime { get { return IPAddress.NetworkToHostOrder(_tradeDateTime); } }

            #endregion

            #region Member variables

            internal ContractDescription ContractDescription;
            private short _marketType;
            private int _quantityTraded;
            private int _price;
            private double _orderNumber;
            private int _traderId;
            private int _buyOrSell;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _accountNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _settlor;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _counterBorker;
            private int _modCxlTraderId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal byte[] Reserved;
            internal OrderFlags OrderFlags;
            private short _activity;
            private int _tradeNumber;
            private int _tradeDateTime;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct SpreadOrderReportData
        {
            #region Property

            internal short MarketType { get { return IPAddress.NetworkToHostOrder(_marketType); } }
            internal int TotalVolume { get { return IPAddress.NetworkToHostOrder(_totalVolume); } }
            internal int OutstadingVolume { get { return IPAddress.NetworkToHostOrder(_outstadingVolume); } }
            internal int DisclosedVolume { get { return IPAddress.NetworkToHostOrder(_disclosedVolume); } }
            internal int DisclosedRemainingVolume { get { return IPAddress.NetworkToHostOrder(_disclosedRemainingVolume); } }
            internal int LimitPrice { get { return IPAddress.NetworkToHostOrder(_limitPrice); } }
            internal int TriggerPrice { get { return IPAddress.NetworkToHostOrder(_triggerPrice); } }
            internal int MinFillAON { get { return IPAddress.NetworkToHostOrder(_minFillAON); } }
            internal int GoodTillDate { get { return IPAddress.NetworkToHostOrder(_goodTillDate); } }
            internal string AccountNumber { get { return new string(_accountNumber); } }
            internal string Remarks { get { return new string(_remarks); } }
            internal string Settlor { get { return new string(_settlor); } }
            internal int EntryDateTime { get { return IPAddress.NetworkToHostOrder(_entryDateTime); } }
            internal string TraderName { get { return new string(_traderName); } }
            internal int TraderNumber { get { return IPAddress.NetworkToHostOrder(_traderNumber); } }
            internal short BuyOrSell { get { return IPAddress.NetworkToHostOrder(_buyOrSell); } }
            internal short OrderActivity { get { return IPAddress.NetworkToHostOrder(_orderActivity); } }
            internal short ProCli { get { return IPAddress.NetworkToHostOrder(_proCli); } }
            internal int ModCxlTraderId { get { return IPAddress.NetworkToHostOrder(_modCxlTraderId); } }
            internal int PriceDifference { get { return IPAddress.NetworkToHostOrder(_priceDifference); } }

            #endregion

            #region Member variables

            internal ContractDescription ContractDescription;
            private short _marketType;
            private int _totalVolume;
            private int _outstadingVolume;
            private int _disclosedVolume;
            private int _disclosedRemainingVolume;
            private int _limitPrice;
            private int _triggerPrice;
            private int _minFillAON;
            private int _goodTillDate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _accountNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            private char[] _remarks;
            internal char OpenOrClose;
            internal char CoverOrUncover;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _settlor;
            private int _entryDateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            private char[] _traderName;
            private int _traderNumber;
            private short _buyOrSell;
            private short _orderActivity;
            private short _proCli;
            private int _modCxlTraderId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal byte[] Reserved;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal OrderFlags OrderFlags;
            internal ContractDescription ContractDescriptionLeg2;
            private int _priceDifference;
            internal char OpenOrCloseLeg2;
            internal char CoverOrUncoverLeg2;

            #endregion
        }

        #endregion

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class HeaderOfReports
        {
            #region Property

            internal int ReportDate { get { return IPAddress.NetworkToHostOrder(_reportDate); } }
            internal short UserType { get { return IPAddress.NetworkToHostOrder(_userType); } }
            internal string BrokerId { get { return new string(_brokerId); } }
            internal string FirmName { get { return new string(_firmName); } }
            internal int TraderNumber { get { return IPAddress.NetworkToHostOrder(_traderNumber); } }
            internal string TraderName { get { return new string(_traderName); } }

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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class OpenOrdersReport
        {
            #region Property

            internal short BranchNumber { get { return IPAddress.NetworkToHostOrder(_branchNumber); } }
            internal string BranchName { get { return new string(_branchName); } }
            internal short NumberOfRecords { get { return IPAddress.NetworkToHostOrder(_numberOfRecords); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char MessageType;
            internal char Reserved;
            private short _branchNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            private char[] _branchName;
            private short _numberOfRecords;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal OpenOrdersReportData[] OpenOrdersReportData;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class TradesReport
        {
            #region Property

            internal short BranchNumber { get { return IPAddress.NetworkToHostOrder(_branchNumber); } }
            internal string BranchName { get { return new string(_branchName); } }
            internal short NumberOfRecords { get { return IPAddress.NetworkToHostOrder(_numberOfRecords); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char MessageType;
            internal char Reserved;
            private short _branchNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            private char[] _branchName;
            private short _numberOfRecords;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            internal TradesTodayReportData[] TradesTodayReportData;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SpreadOrderLogReport
        {
            #region Property

            internal short BranchNumber { get { return IPAddress.NetworkToHostOrder(_branchNumber); } }
            internal string BranchName { get { return new string(_branchName); } }
            internal short NumberOfRecords { get { return IPAddress.NetworkToHostOrder(_numberOfRecords); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char MessageType;
            internal char Reserved;
            private short _branchNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            private char[] _branchName;
            private short _numberOfRecords;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal SpreadOrderReportData[] SpreadOrderReportData;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class SpreadTradeReport
        {
            #region Property

            internal short BranchNumber { get { return IPAddress.NetworkToHostOrder(_branchNumber); } }
            internal string BranchName { get { return new string(_branchName); } }
            internal short NumberOfRecords { get { return IPAddress.NetworkToHostOrder(_numberOfRecords); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            internal char MessageType;
            internal char Reserved;
            private short _branchNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            private char[] _branchName;
            private short _numberOfRecords;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            internal TradesTodayReportData[] TradesTodayReportData;

            #endregion
        }

        #endregion
    }

    namespace ExerciseOrDeliveryRequest
    {
        #region ExerciseOrDeliveryRequest

        #region Nested Structure

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct ExerciseInfo
        {
            #region Property

            internal int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            internal string InstrumentName { get { return new string(_instrumentName); } }
            internal string Symbol { get { return new string(_symbol); } }
            internal int ExpiryDate { get { return IPAddress.NetworkToHostOrder(_expiryDate); } }
            internal int StrikePrice { get { return IPAddress.NetworkToHostOrder(_strikePrice); } }
            internal string OptionType { get { return new string(_optionType); } }
            internal short CALevel { get { return IPAddress.NetworkToHostOrder(_cALevel); } }
            internal short ExplFlag { get { return IPAddress.NetworkToHostOrder(_explFlag); } }
            internal double ExplNumber { get { return ArisApi_a.DoubleTwiddling(_explNumber); } }
            internal short MarketType { get { return IPAddress.NetworkToHostOrder(_marketType); } }
            internal string AccountNumber { get { return new string(_accountNumber); } }
            internal int Quantity { get { return IPAddress.NetworkToHostOrder(_quantity); } }
            internal short ProCLi { get { return IPAddress.NetworkToHostOrder(_proCLi); } }
            internal short ExerciseType { get { return IPAddress.NetworkToHostOrder(_exerciseType); } }
            internal int EntryDateTime { get { return IPAddress.NetworkToHostOrder(_entryDateTime); } }
            internal short BranchId { get { return IPAddress.NetworkToHostOrder(_branchId); } }
            internal int TraderId { get { return IPAddress.NetworkToHostOrder(_traderId); } }
            internal string BrokerId { get { return new string(_brokerId); } }
            internal string Remarks { get { return new string(_remarks); } }
            internal string Participant { get { return new string(_participant); } }

            #endregion

            #region Member variables

            private int _token;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            private char[] _instrumentName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _symbol;
            private int _expiryDate;
            private int _strikePrice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _optionType;
            private short _cALevel;
            private short _explFlag;
            private double _explNumber;
            private short _marketType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _accountNumber;
            private int _quantity;
            private short _proCLi;
            private short _exerciseType;
            private int _entryDateTime;
            private short _branchId;
            private int _traderId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _brokerId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
            private char[] _remarks;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _participant;

            #endregion
        }

        #endregion

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class ExerciseAndDeliveryIntentionEntryRequest
        {
            #region Property

            internal short ReasonCode { get { return IPAddress.NetworkToHostOrder(_reasonCode); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private short _reasonCode;
            internal ExerciseInfo ExerciseInfo;

            #endregion
        }

        #endregion
    }

    namespace GiveUpTrades
    {
        #region GiveUpTrades

        #region Nested Structure

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct GiveUp
        {
            #region Property

            internal double OrderNumber { get { return ArisApi_a.DoubleTwiddling(_orderNumber); } }
            internal int FillNumber { get { return IPAddress.NetworkToHostOrder(_fillNumber); } }
            internal string InstrumentName { get { return new string(_instrumentName); } }
            internal string Symbol { get { return new string(_symbol); } }
            internal int ExpiryDate { get { return IPAddress.NetworkToHostOrder(_expiryDate); } }
            internal int StrikePrice { get { return IPAddress.NetworkToHostOrder(_strikePrice); } }
            internal string OptionType { get { return new string(_optionType); } }
            internal short CALevel { get { return IPAddress.NetworkToHostOrder(_cALevel); } }
            internal int FillVolume { get { return IPAddress.NetworkToHostOrder(_fillVolume); } }
            internal int FillPrice { get { return IPAddress.NetworkToHostOrder(_fillPrice); } }
            internal string BrokerId { get { return new string(_brokerId); } }
            internal short BuyOrSell { get { return IPAddress.NetworkToHostOrder(_buyOrSell); } }
            internal short BookType { get { return IPAddress.NetworkToHostOrder(_bookType); } }
            internal int LastModifiedDateTime { get { return IPAddress.NetworkToHostOrder(_lastModifiedDateTime); } }
            internal string Participant { get { return new string(_participant); } }

            #endregion

            #region Member Variables

            private double _orderNumber;
            private int _fillNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            private char[] _instrumentName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _symbol;
            private int _expiryDate;
            private int _strikePrice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _optionType;
            private short _cALevel;
            private int _fillVolume;
            private int _fillPrice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            private char[] _brokerId;
            internal char Filler;
            private short _buyOrSell;
            private short _bookType;
            private int _lastModifiedDateTime;
            internal char InitiatedByControl;
            internal char OpenOrClose;
            internal char CoverOrUnCover;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            private char[] _participant;
            internal char GiveupFlag;
            internal char Deleted;

            #endregion
        }

        #endregion

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class GiveUpApproveRequest
        {
            #region Property

            internal short ReasonCode { get { return IPAddress.NetworkToHostOrder(_reasonCode); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private short _reasonCode;
            internal GiveUp GiveUp;

            #endregion
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal class GiveUpApproveResponse
        {
            #region Property

            internal short ReasonCode { get { return IPAddress.NetworkToHostOrder(_reasonCode); } }

            #endregion

            #region Member variables

            internal MessageHeader MessageHeader;
            private short _reasonCode;
            internal GiveUp GiveUp;

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
        public struct ContractDesc
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public string InstrumentName { get { return new string(_instrumentName); } }
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
            public int ExpiryDate { get { return IPAddress.NetworkToHostOrder(_expiryDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int StrikePrice { get { return IPAddress.NetworkToHostOrder(_strikePrice); } }
            /// <summary>
            /// 
            /// </summary>
            public string OptionType { get { return new string(_optionType); } }
            /// <summary>
            /// 
            /// </summary>
            public int CaLevel { get { return IPAddress.NetworkToHostOrder(_caLevel); } }

            #endregion

            #region Member variables

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            private char[] _instrumentName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _symbol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _series;
            private int _expiryDate;
            private int _strikePrice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _optionType;
            private short _caLevel;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct MarketStatus
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

            #endregion

            #region Member variables

            private short _normal;
            private short _oddlot;
            private short _spot;
            private short _auction;

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
            public int Token { get { return IPAddress.NetworkToHostOrder(_token); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            public int _token;
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
            public int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
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

            private int _token;
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
            public int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
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
            public int lOpenInterest { get { return IPAddress.NetworkToHostOrder(_openInterest); } }
            /// <summary>
            /// 
            /// </summary>
            public int lDayHiOI { get { return IPAddress.NetworkToHostOrder(_dayHiOI); } }
            /// <summary>
            /// 
            /// </summary>
            public int lDayLoOI { get { return IPAddress.NetworkToHostOrder(_dayLoOI); } }

            #endregion

            #region Member variables

            private int _token;
            private short _marketType;
            private int _fillPrice;
            private int _fillVolume;
            private int _openInterest;
            private int _dayHiOI;
            private int _dayLoOI;

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
            public int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
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

            #region Member Variable

            private int _token;
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

            #endregion

            #region Member variables

            private int _quantity;
            private int _price;
            private short _noOfOrders;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct SpreadMBPInfo
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

            #endregion

            #region Member variables

            private short _noOfOrders;
            private int _quantity;
            private int _price;

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
            public int Token
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

            private int _token;
            private short _bookType;
            private short _tradingStatus;
            private int _volumeTradedToday;
            private int _lastTradedPrice;
            private char NetChangeIndicator;
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
            public int Quantity { get { return IPAddress.NetworkToHostOrder(_quantity); } }
            /// <summary>
            /// 
            /// </summary>
            public int Price { get { return IPAddress.NetworkToHostOrder(_price); } }
            /// <summary>
            /// 
            /// </summary>
            public short NumberOfOrders { get { return IPAddress.NetworkToHostOrder(_numberOfOrders); } }
            /// <summary>
            /// 
            /// </summary>
            public short BbBuySellFlag { get { return IPAddress.NetworkToHostOrder(_bbBuySellFlag); } }

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
            public int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            /// <summary>
            /// 
            /// </summary>
            public int OpenInterest { get { return IPAddress.NetworkToHostOrder(_openInterest); } }

            #endregion

            #region Member variables

            private int _token;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public MarketWiseInfo[] MarketWiseInfo;
            private int _openInterest;

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
            public byte Filler;

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

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
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
            private int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
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

            private int _token;
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

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct AssetOi
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            /// <summary>
            /// 
            /// </summary>
            public int OpenInterest { get { return IPAddress.NetworkToHostOrder(_openInterest); } }

            #endregion

            #region Member variable

            private int _token;
            private int _openInterest;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct IndexDetails
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            /// <summary>
            /// 
            /// </summary>
            public string Name { get { return new string(_name); } }
            /// <summary>
            /// 
            /// </summary>
            public int Open { get { return IPAddress.NetworkToHostOrder(_open); } }
            /// <summary>
            /// 
            /// </summary>
            public int High { get { return IPAddress.NetworkToHostOrder(_high); } }
            /// <summary>
            /// 
            /// </summary>
            public int Low { get { return IPAddress.NetworkToHostOrder(_low); } }
            /// <summary>
            /// 
            /// </summary>
            public int Last { get { return IPAddress.NetworkToHostOrder(_last); } }
            /// <summary>
            /// 
            /// </summary>
            public int Close { get { return IPAddress.NetworkToHostOrder(_close); } }
            /// <summary>
            /// 
            /// </summary>
            public int PrevClose { get { return IPAddress.NetworkToHostOrder(_prevClose); } }
            /// <summary>
            /// 
            /// </summary>
            public int LifeHigh { get { return IPAddress.NetworkToHostOrder(_lifeHigh); } }
            /// <summary>
            /// 
            /// </summary>
            public int LifeLow { get { return IPAddress.NetworkToHostOrder(_lifeLow); } }
            /// <summary>
            /// 
            /// </summary>
            public int Filler1 { get { return IPAddress.NetworkToHostOrder(_filler1); } }
            /// <summary>
            /// 
            /// </summary>
            public int Filler2 { get { return IPAddress.NetworkToHostOrder(_filler2); } }
            /// <summary>
            /// 
            /// </summary>
            public int Filler3 { get { return IPAddress.NetworkToHostOrder(_filler3); } }

            #endregion

            #region Member Variables

            private int _token;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            private char[] _name;
            private int _open;
            private int _high;
            private int _low;
            private int _last;
            private int _close;
            private int _prevClose;
            private int _lifeHigh;
            private int _lifeLow;
            private int _filler1;
            private int _filler2;
            private int _filler3;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct ContractDetails
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            /// <summary>
            /// 
            /// </summary>
            public string Symbol { get { return new string(_symbol); } }
            /// <summary>
            /// 
            /// </summary>
            public string InstrumentName { get { return new string(_instrumentName); } }
            /// <summary>
            /// 
            /// </summary>
            public short ExpiryDay { get { return IPAddress.NetworkToHostOrder(_expiryDay); } }
            /// <summary>
            /// 
            /// </summary>
            public short ExpiryMonth { get { return IPAddress.NetworkToHostOrder(_expiryMonth); } }
            /// <summary>
            /// 
            /// </summary>
            public short ExpiryYear { get { return IPAddress.NetworkToHostOrder(_expiryYear); } }
            /// <summary>
            /// 
            /// </summary>
            public string OptionType { get { return new string(_optionType); } }
            /// <summary>
            /// 
            /// </summary>
            public int StrikePrice { get { return IPAddress.NetworkToHostOrder(_strikePrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int BuyPrice { get { return IPAddress.NetworkToHostOrder(_buyPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int SellPrice { get { return IPAddress.NetworkToHostOrder(_sellPrice); } }
            /// <summary>
            /// 
            /// </summary>
            public double BuyQuantity { get { return ArisApi_a.DoubleBitReverse(_buyQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public double SellQuantity { get { return ArisApi_a.DoubleBitReverse(_sellQuantity); } }
            /// <summary>
            /// 
            /// </summary>
            public int Open { get { return IPAddress.NetworkToHostOrder(_open); } }
            /// <summary>
            /// 
            /// </summary>
            public int High { get { return IPAddress.NetworkToHostOrder(_high); } }
            /// <summary>
            /// 
            /// </summary>
            public int Low { get { return IPAddress.NetworkToHostOrder(_low); } }
            /// <summary>
            /// 
            /// </summary>
            public int Last { get { return IPAddress.NetworkToHostOrder(_last); } }
            /// <summary>
            /// 
            /// </summary>
            public int Close { get { return IPAddress.NetworkToHostOrder(_close); } }
            /// <summary>
            /// 
            /// </summary>
            public int PrevClose { get { return IPAddress.NetworkToHostOrder(_prevClose); } }
            /// <summary>
            /// 
            /// </summary>
            public int DprHigh { get { return IPAddress.NetworkToHostOrder(_dprHigh); } }
            /// <summary>
            /// 
            /// </summary>
            public int DprLow { get { return IPAddress.NetworkToHostOrder(_dprLow); } }
            /// <summary>
            /// 
            /// </summary>
            public double TotalTrades { get { return ArisApi_a.DoubleBitReverse(_totalTrades); } }
            /// <summary>
            /// 
            /// </summary>
            public double OpenInterest { get { return ArisApi_a.DoubleBitReverse(_openInterest); } }
            /// <summary>
            /// 
            /// </summary>
            public int Filler1 { get { return IPAddress.NetworkToHostOrder(_filler1); } }
            /// <summary>
            /// 
            /// </summary>
            public int Filler2 { get { return IPAddress.NetworkToHostOrder(_filler2); } }
            /// <summary>
            /// 
            /// </summary>
            public int Filler3 { get { return IPAddress.NetworkToHostOrder(_filler3); } }

            #endregion

            #region Member Variables
            private int _token;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            private char[] _symbol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            private char[] _instrumentName;
            private short _expiryDay;
            private short _expiryMonth;
            private short _expiryYear;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _optionType;
            private int _strikePrice;
            private int _buyPrice;
            private int _sellPrice;
            private double _buyQuantity;
            private double _sellQuantity;
            private int _open;
            private int _high;
            private int _low;
            private int _last;
            private int _close;
            private int _prevClose;
            private int _dprHigh;
            private int _dprLow;
            private double _totalTrades;
            private double _openInterest;
            private int _filler1;
            private int _filler2;
            private int _filler3;

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
            public BroadCastDestination BroadcastDestination;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            public byte[] Reserved;
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
            /// <summary>
            /// 
            /// </summary>
            public short SnapQuoteTime { get { return IPAddress.NetworkToHostOrder(_snapQuoteTime); } }
            /// <summary>
            /// 
            /// </summary>
            public int RiskFreeInterestRate { get { return IPAddress.NetworkToHostOrder(_riskFreeInterestRate); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            /// <summary>
            /// 
            /// </summary>
            public MarketStatus MarketStatus;
            /// <summary>
            /// 
            /// </summary>
            public MarketStatus ExMarketStatus;
            /// <summary>
            /// 
            /// </summary>
            public MarketStatus PlMarketStatus;
            /// <summary>
            /// 
            /// </summary>
            public char UpdatePortfolio;
            private int _marketIndex;
            private short _defaultSettlementPeriodNormal;
            private short _defaultSettlementPeriodSpot;
            private short _defaultSettlementPeriodAuction;
            private short _competitorPeriod;
            private short _solicitorPeriod;
            private short _warningPercent;
            private short _volumeFreezePercent;
            private short _snapQuoteTime;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] Reserved1;
            private int _boardLotQuantity;
            private int _tickSize;
            private short _maximumGtcDays;
            /// <summary>
            /// 
            /// </summary>
            public SecurityEligibleIndicators SecurityEligibleIndicators;
            private short _disclosedQuantityPercentAllowed;
            private int _riskFreeInterestRate;

            #endregion
        }

        #endregion

        #region ChangeInSecurityMaster

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct ChangeInSecurityMaster
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
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
            public int MarginPercentage { get { return IPAddress.NetworkToHostOrder(_marginPercentage); } }
            /// <summary>
            /// 
            /// </summary>
            public int MinimumLotQuantity { get { return IPAddress.NetworkToHostOrder(_minimumLotQuantity); } }
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
            public int LowPriceRange { get { return IPAddress.NetworkToHostOrder(_lowPriceRange); } }
            /// <summary>
            /// 
            /// </summary>
            public int HighPriceRange { get { return IPAddress.NetworkToHostOrder(_highPriceRange); } }
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
            public int ExerciseStartDate { get { return IPAddress.NetworkToHostOrder(_exerciseStartDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int ExerciseEndDate { get { return IPAddress.NetworkToHostOrder(_exerciseEndDate); } }
            /// <summary>
            /// 
            /// </summary>
            public int OldToken { get { return IPAddress.NetworkToHostOrder(_oldToken); } }
            /// <summary>
            /// 
            /// </summary>
            public string AssetInstrument { get { return new string(_assetInstrument); } }
            /// <summary>
            /// 
            /// </summary>
            public string AssetName { get { return new string(_assetName); } }
            /// <summary>
            /// 
            /// </summary>
            public int AssetToken { get { return IPAddress.NetworkToHostOrder(_assetToken); } }
            /// <summary>
            /// 
            /// </summary>
            public int IntrinsicValue { get { return IPAddress.NetworkToHostOrder(_intrinsicValue); } }
            /// <summary>
            /// 
            /// </summary>
            public int ExtrinsicValue { get { return IPAddress.NetworkToHostOrder(_extrinsicValue); } }
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
            public int BasePrice { get { return IPAddress.NetworkToHostOrder(_basePrice); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private int _token;
            /// <summary>
            /// 
            /// </summary>
            public ContractDesc ContractDesc;
            private short _permittedToTrade;
            private double _issuedCapital;
            private int _warningQuantity;
            private int _freezeQuantity;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
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
            private int _marginPercentage;
            private int _minimumLotQuantity;
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
            private int _lowPriceRange;
            private int _highPriceRange;
            private int _expiryDate;
            private int _noDeliveryStartDate;
            private int _noDeliveryEndDate;
            /// <summary>
            /// 
            /// </summary>
            public EligibilityIndicators EligibilityIndicators;
            private int _bookClosureStartDate;
            private int _bookClosureEndDate;
            private int _exerciseStartDate;
            private int _exerciseEndDate;
            private int _oldToken;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            private char[] _assetInstrument;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _assetName;
            private int _assetToken;
            private int _intrinsicValue;
            private int _extrinsicValue;
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
            private int _basePrice;

            #endregion
        }

        #endregion

        #region ChangeInInstrumentMaster

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct ChangeInInstrumentMaster
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public short InstrumentId { get { return IPAddress.NetworkToHostOrder(_instrumentId); } }
            /// <summary>
            /// 
            /// </summary>
            public string InstrumentName { get { return new string(_instrumentName); } }
            /// <summary>
            /// 
            /// </summary>
            public string InstrumentDescription { get { return new string(_instrumentDescription); } }
            /// <summary>
            /// 
            /// </summary>
            public int InstrumentUpdateDateTime { get { return IPAddress.NetworkToHostOrder(_instrumentUpdateDateTime); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private short _instrumentId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            private char[] _instrumentName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            private char[] _instrumentDescription;
            private int _instrumentUpdateDateTime;
            /// <summary>
            /// 
            /// </summary>
            public char DeleteFlag;

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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)]
            public TokenAndEligibility[] TokenAndEligibility;

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
            public int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
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
            private int _token;
            /// <summary>
            /// 
            /// </summary>
            public ContractDesc ContractDesc;
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
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
            public int Token { get { return IPAddress.NetworkToHostOrder(_token); } }
            /// <summary>
            /// 
            /// </summary>
            public int OpeningPrice { get { return IPAddress.NetworkToHostOrder(_openingPrice); } }
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

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _symbol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _series;
            private int _token;
            private int _openingPrice;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Reserved;

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

        #region SpreadCombinationMasterUpdate

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct SpreadCombinationMasterUpdate
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public int Token1 { get { return IPAddress.NetworkToHostOrder(_token1); } }
            /// <summary>
            /// 
            /// </summary>
            public int Token2 { get { return IPAddress.NetworkToHostOrder(_token2); } }
            /// <summary>
            /// 
            /// </summary>
            public int ReferencePrice { get { return IPAddress.NetworkToHostOrder(_referencePrice); } }
            /// <summary>
            /// 
            /// </summary>
            public int DayLowPriceDiffRange { get { return IPAddress.NetworkToHostOrder(_dayLowPriceDiffRange); } }
            /// <summary>
            /// 
            /// </summary>
            public int DayHighPriceDiffRange { get { return IPAddress.NetworkToHostOrder(_dayHighPriceDiffRange); } }
            /// <summary>
            /// 
            /// </summary>
            public int OpHighPriceDiffRange { get { return IPAddress.NetworkToHostOrder(_opHighPriceDiffRange); } }
            /// <summary>
            /// 
            /// </summary>
            public int OpLowPriceDiffRange { get { return IPAddress.NetworkToHostOrder(_opLowPriceDiffRange); } }

            #endregion

            #region Member variables

            private int _token1;
            private int _token2;
            /// <summary>
            /// 
            /// </summary>
            public ContractDesc ContractDesc1;
            /// <summary>
            /// 
            /// </summary>
            public ContractDesc ContractDesc2;
            private int _referencePrice;
            private int _dayLowPriceDiffRange;
            private int _dayHighPriceDiffRange;
            private int _opLowPriceDiffRange;
            private int _opHighPriceDiffRange;
            /// <summary>
            /// 
            /// </summary>
            public byte Eligibility;
            /// <summary>
            /// 
            /// </summary>
            public char Reserved;
            /// <summary>
            /// 
            /// </summary>
            public char DeleteFlag;
            /// <summary>
            /// 
            /// </summary>
            public char Reserved1;

            #endregion
        }

        #endregion

        #region SpreadMarketByPrice

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct SpreadMarketByPrice
        {
            #region Property

            /// <summary>
            /// 
            /// </summary>
            public int Token1 { get { return IPAddress.NetworkToHostOrder(_token1); } }
            /// <summary>
            /// 
            /// </summary>
            public int Token2 { get { return IPAddress.NetworkToHostOrder(_token2); } }
            /// <summary>
            /// 
            /// </summary>
            public short MbpBuy { get { return IPAddress.NetworkToHostOrder(_mbpBuy); } }
            /// <summary>
            /// 
            /// </summary>
            public short MbpSell { get { return IPAddress.NetworkToHostOrder(_mbpSell); } }
            /// <summary>
            /// 
            /// </summary>
            public int LastActiveTime { get { return IPAddress.NetworkToHostOrder(_lastActiveTime); } }
            /// <summary>
            /// 
            /// </summary>
            public int TradedVolume { get { return IPAddress.NetworkToHostOrder(_tradedVolume); } }
            /// <summary>
            /// 
            /// </summary>
            public double TotalTradedVolume { get { return ArisApi_a.DoubleBitReverse(_totalTradedVolume); } }
            /// <summary>
            /// 
            /// </summary>
            public double Buy { get { return ArisApi_a.DoubleBitReverse(_buy); } }
            /// <summary>
            /// 
            /// </summary>
            public double Sell { get { return ArisApi_a.DoubleBitReverse(_sell); } }
            /// <summary>
            /// 
            /// </summary>
            public int OpenPriceDifference { get { return IPAddress.NetworkToHostOrder(_openPriceDifference); } }
            /// <summary>
            /// 
            /// </summary>
            public int DayHighPriceDifference { get { return IPAddress.NetworkToHostOrder(_dayHighPriceDifference); } }
            /// <summary>
            /// 
            /// </summary>
            public int DayLowPriceDifference { get { return IPAddress.NetworkToHostOrder(_dayLowPriceDifference); } }
            /// <summary>
            /// 
            /// </summary>
            public int LastTradedPriceDifference { get { return IPAddress.NetworkToHostOrder(_lastTradedPriceDifference); } }
            /// <summary>
            /// 
            /// </summary>
            public int LastUpdateTime { get { return IPAddress.NetworkToHostOrder(_lastUpdateTime); } }

            #endregion

            #region Member variables

            /// <summary>
            /// 
            /// </summary>
            internal MessageHeader MessageHeader;
            private int _token1;
            private int _token2;
            private short _mbpBuy;
            private short _mbpSell;
            private int _lastActiveTime;
            private int _tradedVolume;
            private double _totalTradedVolume;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public SpreadMBPInfo[] BuyMBPInfo;
            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public SpreadMBPInfo[] SellMBPInfo;
            private double _buy;
            private double _sell;
            private int _openPriceDifference;
            private int _dayHighPriceDifference;
            private int _dayLowPriceDifference;
            private int _lastTradedPriceDifference;
            private int _lastUpdateTime;

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
            public string Symbol { get { return new string(_symbol); } }
            /// <summary>
            /// 
            /// </summary>
            public string Series { get { return new string(_series); } }
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            private char[] _symbol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            private char[] _series;
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
            public string Key { get { return new string(_key); } }
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            private char[] _key;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            private char[] _error;

            #endregion
        }

        #endregion
    }

}
