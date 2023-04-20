using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ArisDev.MTApi
{
    public struct ChangePassword
    {
        public MessageHeader Header;
        public string LoginId;
        public string NewPassword;
        public string Password;
        public ushort UniqueId;
    }

    [Serializable]
    public class OrderInfo
    {
        public ArisDev.MTApi.MTEnums.BookType BookType;
        public MTEnums.BuySell BuySell;
        public string ClientCode;
        public ContractDetails ContractDetails;
        public ContractInformation ContractInfo;
        public uint GatewayId;
        public DateTime GtdTime;
        public decimal Ltp;
        public decimal MarketProtection;
        public decimal OrderPrice;
        public int Qty;
        public int QtyDisclosed;
        public ulong StrategyId;
        public decimal TriggerPrice;
        public string UserRemarks;
        public MTEnums.Validity Validity;
        public OrderInfo() { }
    }

    [Serializable]
    public class ContractDetails
    {
        public decimal ClosePrice;
        public decimal DprHigh;
        public decimal DprLow;
        public bool IsAutoAllowed;
        public bool IsRBIViolation;
        public int LotSize;
        public int MaxQty;
        public decimal MaxValue;
        public string PriceFormat;
        public decimal PriceTick;

        public ContractDetails() { }
    }

    public struct ClientCode
    {
        public string Code;
        public uint GatewayId;
    }

    public struct ClientExchangeDetails
    {
        public ushort AlertOn;
        public int BrokerId;
        public string ClientId;
        public string ClientName;
        public decimal Commission;
        public decimal DepositAmt;
        public int MaxPosition;
        public MessageHeader MessageHeader;
        public decimal ThreasholdLimit1;
        public decimal ThreasholdLimit2;
        public decimal ThreasholdLimit3;
        public bool ThresholdBaseAmount;
    }

    public struct ContractInformation
    {
        public string Exchange;
        public int ExpiryDate;
        public string InstrumentName;
        public decimal Multiplier;
        public int PriceDivisor;
        public string Series;
        public int StrikePrice;
        public string Symbol;
        public string TokenNo;
    }

    public struct DownLoadData
    {
        public short CurrPart;
        public byte[] Data;
        public short DataSize;
        public ushort DataType;
        public string FileName;
        public MessageHeader Header;
        public short NoOfParts;
    }

    public struct DownLoadNotification
    {
        public string CtclId;
        public ushort DataType;
        public string Exchange;
        public byte Flag;
        public MessageHeader Header;
        public string MemberId;
        public ushort Remarks;
    }

    public struct ForgotPwdReq
    {
        public string AccountNumber;
        public int DateOfBirth;
        public MessageHeader Header;
        public string LoginId;
        public string PanNumber;
    }

    public struct ForgotPwdRes
    {
        public string EmailId;
        public MessageHeader Header;
        public string LoginId;
        public string Password;
    }

    public struct GatewayMessage
    {
        public string Exchange;
        public byte Flag;
        public MessageHeader Header;
        public string Message;
        public ushort UniqueId;
    }

    public struct GetAllQuestionReq
    {
        public MessageHeader Header;
    }

    public struct GetAllQuestionRes
    {
        public MessageHeader Header;
        public QueAnswer[] QueAns;
    }

    public struct HeartBeat
    {
        public MessageHeader Header;
    }

    public struct InfoMessages
    {
        public MessageHeader Header;
        public string Message;
        public short MessageType;
    }

    public struct LogoffRequest
    {
        public MessageHeader Header;
    }

    public struct LogoffResponse
    {
        public MessageHeader Header;
    }

    public struct LogonRequest
    {
        public MessageHeader Header;
        public bool IsContractDownload;
        public string LocalIp;
        public string LoginId;
        public string NewPassword;
        public string Password;
        public int Reserved;
        public short Version;
    }

    public struct LogonResponse
    {
        public short AccountType;
        public uint AllowedGateway;
        public MessageHeader Header;
        public int LastLogonTime;
        public ushort LastOrderNo;
        public string LoginId;
        public ushort MultiLegLastOrderNo;
        public string Name;
        public string NewPassword;
        public ushort UniqueId;
        public short Version;
    }

    public struct MappedClient
    {
        public ClientCode[] CodeArray;
        public MessageHeader Header;
        public byte NoOfRecords;
        public ushort UniqueId;
    }

    public struct MarginChange
    {
        public string Exchange;
        public short FloatIntial;
        public short FloatSpecial;
        public MessageHeader Header;
        public int InitialBuyMargin;
        public int InitialSellMargin;
        public int PriceDivisor;
        public int SpecialBuyMarginRate;
        public int SpecialSellMarginRate;
        public short SpreadBenefitFlag;
        public string TokenNo;
    }

    public struct MarketNotification
    {
        public string CtclId;
        public MessageHeader Header;
        public short MarketStatus;
        public short MarketType;
    }

    public struct MessageHeader
    {
        public short ErrorCode;
        public uint GatewayId;
        public int MessageTime;
        public short TransCode;
    }

    public struct MultiLegOrderRequest
    {
        public byte AccountType;
        public byte BookType;
        public string Clientcode;
        public string ClOrdNo;
        public string CtclId;
        public int EntryTime;
        public string ExchOrderNo;
        public string ExchRemarks;
        public int GtdTime;
        public MessageHeader Header;
        public ushort IntOrderNo;
        public byte IsDataMissing;
        public byte IsDownloading;
        public byte IsSychState;
        public OrderParameter Leg1;
        public OrderParameter Leg2;
        public OrderParameter Leg3;
        public long LocationId;
        public string LoginId;
        public string MemberId;
        public int ModifiedTime;
        public byte NoOFLegs;
        public short OrderStatus;
        public byte OrderType;
        public string OrgClOrdNo;
        public int SpreadPrice;
        public ulong StrategyId;
        public string StsNo;
        public ushort UniqueId;
        public string UserRemarks;
        public byte ValidityType;
    }

    public struct OrderParameter
    {
        public byte BuySell;
        public ContractInformation ContractInfo;
        public int OrderPrice;
        public int Qty;
        public int QtyRemaining;
        public int QtyTraded;
        public int QtyTradedTotal;
        public int TriggerPrice;
    }

    public struct OwnOrderResponse
    {
        public byte AccountType;
        public byte BookType;
        public byte BuySell;
        public string Clientcode;
        public string ClOrdNo;
        public ContractInformation ContractInfo;
        public string CtclId;
        public int EntryTime;
        public string ExchOrderNo;
        public string ExchRemarks;
        public int GtdTime;
        public MessageHeader Header;
        public ushort IntOrderNo;
        public byte IsDataMissing;
        public byte IsDownloading;
        public byte IsSychState;
        public long LocationId;
        public string LoginId;
        public int LtpOrClose;
        public short MarketProtection;
        public string MemberId;
        public int ModifiedTime;
        public byte NoOfLegs;
        public int OrderPrice;
        public byte OrderStatus;
        public string OrgClOrdNo;
        public int Qty;
        public int QtyDisclosed;
        public int QtyDiscRemaing;
        public int QtyMinFill;
        public int QtyRemaining;
        public int QtyTraded;
        public int QtyTradedTotal;
        public ulong StrategyId;
        public string StsNo;
        public string TradeNo;
        public int TradePrice;
        public int TradeTime;
        public int TriggerPrice;
        public int TriggerTime;
        public ushort UniqueId;
        public string UserRemarks;
        public byte ValidityType;
    }

    public struct OrderTypePair
    {
        public byte BookType;
        public MessageHeader Header;
        public byte ValidityType;
    }

    public struct QueAnswer
    {
        public string Question;
        public int QuestionId;
    }

    public struct QueIdAndAnswer
    {
        public string Answer;
        public int QuestionId;
    }

    public struct QuenstionAndAnswer
    {
        public string Answer;
        public string Question;
        public int QuestionId;
    }

    public struct RMSDetailInfo
    {
        public string ExpiryDate;
        public int GrossOrderQty;
        public MessageHeader Header;
        public string InstrumentName;
        public byte IsBranch;
        public byte IsUpdate;
        public int MaxBuyTradedQty;
        public int MaxSellTradedQty;
        public string Symbol;
        public string TokenNo;
        public ushort UniqueId;
    }

    public struct RMSInfo
    {
        public byte AdminRmsCheck;
        public byte AllowOtherScript;
        public byte AutoSquareOnLoss;
        public int BTPLimit;
        public double BuyTurnOver;
        public byte ClientRmsCheck;
        public byte DprChecking;
        public double GrossExposure;
        public MessageHeader Header;
        public int IndexCircuitLimit;
        public byte IsBranch;
        public byte IsFIIclient;
        public byte IsLoginIdWise;
        public double M2MGainLimit;
        public double M2MLossLimit;
        public double MarginLimit;
        public double MarginPercentage;
        public double MarketWidePL;
        public double OpenPositionValue;
        public byte RmsOnOff;
        public short ScriptsAllowed;
        public byte ScriptWiseLimit;
        public double SellTurnOver;
        public int SingleOrderQty;
        public double SingleOrderValue;
        public double TotalTurnOver;
        public decimal TradeRatio;
        public int TradingStopTime;
        public ushort UniqueId;
    }

    public struct RMSRequest
    {
        public MessageHeader Header;
        public string LoginId;
        public ushort UniqueId;
    }

    public struct Sevice2FADetails
    {
        public string EmailId;
        public MessageHeader Header;
        public string ImageVarId;
        public QuenstionAndAnswer[] QuestionAndAnswers;
        public ushort UniqueId;
    }

    public struct SeviceRegistrationReq
    {
        public string AccountNumber;
        public string AlternateEmailId;
        public string AlternateMobileNo;
        public string BankBranchName;
        public string City;
        public int DateOfBirth;
        public string EmailId;
        public string FirstName;
        public bool gender;
        public MessageHeader Header;
        public string ImageVarId;
        public string LastName;
        public string MiddleName;
        public string MobileNo;
        public string OfficeAddress;
        public string PanNumber;
        public int Pincode;
        public QueIdAndAnswer[] QueIdAndAnswer;
        public string ResidanceAddress;
        public string State;
    }

    public struct SeviceRegistrationRes
    {
        public MessageHeader Header;
    }

    public struct StopLossTriggerNotification
    {
        public string Exchange;
        public string ExchOrderNo;
        public MessageHeader Header;
        public ushort IntOrderNo;
        public byte IsDataMissing;
        public byte IsDownloading;
        public int TriggerTime;
        public ushort UniqueId;
        public string UserRemarks;
    }

    public struct StrategyMapping
    {
        public byte Delete;
        public MessageHeader Header;
        public ulong Strategy;
    }

    public struct SystemInfoComplete
    {
        public MessageHeader Header;
    }

    public struct SystemInfoRequest
    {
        public MessageHeader Header;
    }

    public struct SystemInformation
    {
        public short BranchId;
        public string CtclId;
        public int DisclosedQtyPer;
        public string Exchange;
        public string FMCCode;
        public MessageHeader Header;
        public long LocationId;
        public short MarketStatus;
        public short MarketType;
        public string MemberId;
        public string Reserved;
        public string SEBICode;
    }

    public struct TradeTransaction
    {
        public byte AccountType;
        public ushort AlertOn;
        public string BrokerCode;
        public int BrokerId;
        public short BuySell;
        public string ClientId;
        public string ClientName;
        public decimal Commission;
        public ContractInformation ContractInfo;
        public string DealerCode;
        public string DealerId;
        public string DealerName;
        public decimal DepositeAmount;
        public string ExchOrderNo;
        public decimal Expenses;
        public string FamilyCode;
        public bool IsInsert;
        public bool IsTWS;
        public MessageHeader MessageHeader;
        public int Qty;
        public string Reserved1;
        public string Reserved2;
        public string Reserved3;
        public decimal ThresholdLimit1;
        public decimal ThresholdLimit2;
        public decimal ThresholdLimit3;
        public string TradeNo;
        public decimal TradePrice;
        public int TradeTime;
        public string TradingMemberId;
        public string UserCode;
        public int UserId;
    }

    public struct UpdateNetPosition
    {
        public decimal BFAmount;
        public int BFQty;
        public string ClientId;
        public string DealerId;
        public int ExpiryDate;
        public MessageHeader MessageHeader;
        public string Symbol;
    }

    public struct UpdateSpanValue
    {
        public decimal AtMaxValue;
        public int ExpiryDate;
        public string InstrumentName;
        public MessageHeader MessageHeader;
        public decimal SpreadMargin;
        public string Symbol;
    }

    public class MTEnums
    {
        public MTEnums() { }

        public enum AccountType
        {
            NONE = 0,
            CLIENT = 1,
            PRO = 2,
            ADMIN = 9,
        }

        [Flags]
        public enum AlertType
        {
            NONE = 0,
            M2M = 1,
            SpanMargin = 2,
            Position = 4,
        }

        public enum BookType
        {
            RL = 1,
            ST = 2,
            SL = 3,
            MKT = 4,
            MIT = 5,
            AucBuyIn = 6,
            AucSellIn = 7,
            AucTradingBuyIn = 8,
            AucTradingSellIn = 9,
        }

        public enum BroadCastType
        {
            MarketPicture = 0,
            SanpQuote = 1,
            TouchLine = 2,
        }

        public enum BuySell
        {
            NONE = 0,
            BUY = 1,
            SELL = 2,
            BOTH = 3,
        }

        public enum ChangeIndicator
        {
            NoChange = 0,
            Increase = 1,
            Decrease = 2,
        }

        public enum ConnectionType
        {
            TCP = 0,
            UDP = 1,
        }

        public enum DownloadPreference
        {
            LastLogin = 1,
            LastTrade = 2,
            FullDay = 3,
        }

        public enum EnvironmentType
        {
            None = 0,
            Manager = 1,
            Client = 2,
            XceedZip = 3,
            Lzo1 = 4,
            Lzo2 = 5,
            ContractAndPositionManager = 6,
        }

        public enum FieldType
        {
            None = 0,
            Date = 1,
            Time = 2,
            DateTime = 3,
            Price = 4,
            Quantity = 5,
            Indicator = 6,
            Percentage = 7,
        }

        [Flags]
        public enum FileTrasnfer
        {
            None = 0,
            Order = 1,
            L2Order = 2,
            MultiLegOrder = 4,
            Trade = 8,
            NetPosition = 16,
            OpenPosition = 32,
            Bhavcopy = 64,
            ContractMaster = 128,
            IndexData = 256,
            OrderPair = 512,
            ClientInfo = 1024,
            ClientDetails = 2048,
            GroupDetails = 4096,
            ExchangeDetails = 8192,
            ClientConfig = 16384,
        }

        public enum GatewayGroup
        {
            NSE = 15,
            BSE = 48,
            CURRENCY = 164,
            OPTION = 181,
            FUTURE = 328,
            CASH = 514,
            MCX = 960,
            NATIONAL = 2047,
            CTS = 1048576,
            INTERNATIONAL = 7337984,
            ALL = 8388607,
        }

        [Flags]
        public enum GatewayId
        {
            NONE = 0,
            NSEFO = 1,
            NSECM = 2,
            NSECD = 4,
            NCDEX = 8,
            BSE = 16,
            USE = 32,
            MCX = 64,
            MCXSX = 128,
            MCXSXFO = 256,
            MCXSXCM = 512,
            ICEX = 1024,
            DGCX = 2048,
            BFX = 4096,
            SMX = 8192,
            GBOT = 16384,
            TTFIX = 32768,
            XTRADER = 65536,
            VERTEX = 131072,
            CME = 262144,
            SGX = 524288,
            MT = 1048576,
            FXCM = 2097152,
            RTS = 4194304,
            ALL = 8388607,
        }

        [Flags]
        public enum Instrument
        {
            NONE = 0,
            FUT = 1,
            OPT = 2,
            MLEG = 3,
            CS = 4,
            FOR = 5,
            FXNDF = 6,
            GOVT = 7,
            IDX = 8,
            NRG = 9,
            FUTURE = 10,
            OPTION = 11,
            FOREX = 12,
            ENERGY = 13,
        }

        public enum MarketStatus
        {
            Undefined = -1,
            Closed = 0,
            Open = 1,
            PreOpen = 2,
            PreOpenClosed = 3,
            PostClose = 4,
            Suspended = 5,
            DaySession = 6,
            LoginSession = 7,
            OpeningOrderEntry = 8,
            OpeningOrderConfirmation = 9,
            Tradingsession = 10,
            EndofDay = 11,
            MemberQuery = 12,
            StartofAuction = 13,
            OfferEntry = 14,
            AuctionMatching = 15,
            ReportDownload = 16,
            EndofAuction = 17,
            PositionTransferSession = 18,
            Reserved = 19,
            MargineSession = 20,
            QuerySession = 21,
            BreakSession = 22,
            TransferSession = 23,
        }

        public enum MarketType
        {
            Normal = 0,
            Auction = 1,
            Special = 2,
            OddLot = 3,
            Spot = 4,
            AuctionBuyIn = 5,
            AuctionSellOut = 6,
            TradingBuyIn = 7,
            TradingSellOut = 8,
        }

        [Flags]
        public enum MissingDataType
        {
            None = 0,
            OrderPara = 1,
            ContractInfo = 2,
            GeneralDetails = 4,
            UniqueData = 8,
            All = 15,
        }

        public enum MultiLegType
        {
            Normal = 0,
            Spread = 1,
            Leg2 = 2,
            Leg3 = 3,
        }

        public enum NoOfLegs
        {
            Normal = 0,
            Leg1 = 1,
            Leg2 = 2,
            Leg3 = 3,
        }

        public enum Notification
        {
            None = 0,
            Request = 1,
            Start = 1,
            Complete = 2,
            Terminated = 3,
        }

        public enum OrderStatus
        {
            None = 0,
            Submitted = 1,
            MPending = 2,
            EPending = 3,
            Freezed = 4,
            MRejected = 5,
            ERejected = 6,
            Cancelled = 7,
            ECancelled = 8,
            Executed = 9,
            ESOrder = 10,
            ESTrade = 11,
        }

        public enum RequestType
        {
            None = 0,
            Modify = 1,
            CancelAndFresh = 2,
            CancelBuy = 3,
            CancelSell = 4,
            CancelAll = 5,
        }

        [Flags]
        public enum StrategyType
        {
            None = 0,
            Manual = 1,
            Arbitrage = 2,
            Jobber = 4,
            Liquidity = 8,
            MTChart = 16,
            HighLow = 32,
            SArbitrage = 64,
            SemiJobber = 128,
            CalenderSpread = 256,
            SpreadRatio = 512,
            DeltaHedging = 1024,
            AdvancedJobber = 2048,
            MultiMarket = 4096,
            MarketMaking = 8192,
            Chain = 16384,
            Every = 32768,
            Closing = 65536,
            AverageTrading = 131072,
            AutoDecisionTrader = 262144,
            OtdTimer = 524288,
            EveryReverse = 1048576,
            OptionTrader = 2097152,
            Speculator = 4194304,
            JobberXL = 8388608,
            JobberChain = 16777216,
            All = 33554431,
        }

        [Flags]
        public enum Validity
        {
            AON = 1,
            IOC = 2,
            GTC = 4,
            DAY = 8,
            GTD = 16,
            EOS = 32,
            FOK = 64,
            GTT = 128,
        }
    }

    public struct BroadcastMessageHeader
    {
        public string Exchange;
        public int ExchangeTimeStamp;
        public uint GatewayId;
        public short TransCode;
    }

    public struct BestFive
    {
        public int OrderPrice;
        public int Quantity;
        public int TotalNumberOfOrders;
    }

    public struct MarketPicture
    {
        public int AverageTradedPrice;
        public BestFive[] Best5Buy;
        public BestFive[] Best5Sell;
        public int ClosePrice;
        public int CurrentOpenInterest;
        public BroadcastMessageHeader Header;
        public int HighPrice;
        public int LastTradedPrice;
        public int LastTradedQty;
        public int LastTradeTime;
        public int LowPrice;
        public char NetChangeIndicator;
        public int OpenPrice;
        public int PriceDivisor;
        public string TokenNo;
        public double TotalBuyQty;
        public double TotalQtyTraded;
        public double TotalSellQty;
        public double TotalTradedValue;
        public int TotalTrades;
        public int YearlyHigh;
        public int YearlyLow;
    }

}