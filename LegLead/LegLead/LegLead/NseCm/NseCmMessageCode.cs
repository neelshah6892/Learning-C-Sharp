
namespace LegLead.NseCmApi
{
    /// <summary>
    /// Message code
    /// </summary>
    public enum NseCmMessageCode
    {
        #region Logon Process

        /// <summary>
        /// 
        /// </summary>
        LogonRequest = 2300,

        /// <summary>
        /// 
        /// </summary>
        LogOnResponse = 2301,

        /// <summary>
        /// 
        /// </summary>
        LogoffRequest = 2320,

        /// <summary>
        /// 
        /// </summary>
        LogoffRespose = 2321,

        /// <summary>
        /// Invitation Message
        /// </summary>
        InvitationMessage = 15000,

        #endregion

        #region Download Process

        /// <summary>
        /// 
        /// </summary>
        SystemInformationRequest = 1600,

        /// <summary>
        /// 
        /// </summary>
        SystemInformationResponse = 1601,

        /// <summary>
        /// 
        /// </summary>
        UpdateLocalDatabaseRequest = 7300,

        /// <summary>
        /// 
        /// </summary>
        PartialSystemInformation = 7321,

        /// <summary>
        /// 
        /// </summary>
        UpdateLocalDatabaseHeader = 7307,

        /// <summary>
        /// 
        /// </summary>
        UpdateLocalDatabaseData = 7304,

        /// <summary>
        /// 
        /// </summary>
        UpdateLoacalDataBaseTrailer = 7308,

        /// <summary>
        /// 
        /// </summary>
        MessageDownloadRequest = 7000,

        /// <summary>
        /// 
        /// </summary>
        MessageDownloadHeaderResponse = 7011,

        /// <summary>
        /// 
        /// </summary>
        MessageDownloadDataResponse = 7021,

        /// <summary>
        /// 
        /// </summary>
        MessageDownloadTrailerResponse = 7031,

        #endregion

        #region Order and Trade Management

        /// <summary>
        /// 
        /// </summary>
        OrderEntryRequest = 2000,

        /// <summary>
        /// 
        /// </summary>
        OrderRequestResponse = 2001,

        /// <summary>
        /// 
        /// </summary>
        MarketOrderResponse = 2012,

        /// <summary>
        /// 
        /// </summary>
        OrderEntryConfirm = 2073,

        /// <summary>
        /// 
        /// </summary>
        OrderFreeze = 2170,

        /// <summary>
        /// 
        /// </summary>
        OrderEntryError = 2231,

        /// <summary>
        /// 
        /// </summary>
        OrderModifyRequest = 2040,

        /// <summary>
        /// 
        /// </summary>
        OrderModifyResponse = 2041,

        /// <summary>
        /// 
        /// </summary>
        OrderModifyConfirm = 2074,

        /// <summary>
        /// 
        /// </summary>
        OrderModifyError = 2042,

        /// <summary>
        /// 
        /// </summary>
        OrderCancelRequest = 2070,

        /// <summary>
        /// 
        /// </summary>
        OrderCancelResponse = 2071,

        /// <summary>
        /// 
        /// </summary>
        OrderCancelConfirm = 2075,

        /// <summary>
        /// 
        /// </summary>
        OrderCancelError = 2072,

        /// <summary>
        /// 
        /// </summary>
        TradeModificationRequest = 5445,

        /// <summary>
        /// 
        /// </summary>
        TradeModificationRequestResponse = 5446,

        /// <summary>
        /// 
        /// </summary>
        TradeCancellationRequest = 5440,

        /// <summary>
        /// 
        /// </summary>
        TradeCancellationResponse = 5441,

        /// <summary>
        /// 
        /// </summary>
        TradeError = 2223,

        /// <summary>
        /// 
        /// </summary>
        BatchOrderCancel = 9002,

        #endregion

        #region Spread Order and Trade Management

        /// <summary>
        /// 
        /// </summary>
        SpreadOrderEntryRequest = 2100,

        /// <summary>
        /// 
        /// </summary>
        SpreadOrderEntryResponse = 2101,

        /// <summary>
        /// 
        /// </summary>
        SpreadOrderConfirmationResponse = 2124,

        /// <summary>
        /// 
        /// </summary>
        SpreadOrderErrorResponse = 2154,

        /// <summary>
        /// 
        /// </summary>
        SpreadOrderCancelConfirmationResponse = 2130,

        /// <summary>
        /// 
        /// </summary>
        SpreadOrderModificationRequest = 2118,

        /// <summary>
        /// 
        /// </summary>
        SpreadOrderModificationResponse = 2119,

        /// <summary>
        /// 
        /// </summary>
        SpreadOrderModificationConfirmationResponse = 2136,

        /// <summary>
        /// 
        /// </summary>
        SpreadOrderModificationErrorResponse = 2133,

        #endregion

        #region 2L & 3L order entry

        /// <summary>
        /// 
        /// </summary>
        OrderRequest2l3lLeg2L = 2102,

        /// <summary>
        /// 
        /// </summary>
        OrderRequest2l3lLeg3L = 2104,

        /// <summary>
        /// 
        /// </summary>
        OrderRequestedResponse2l3lLeg2L = 2103,

        /// <summary>
        /// 
        /// </summary>
        OrderRequestedResponse2l3lleg3L = 2105,

        /// <summary>
        /// 
        /// </summary>
        MarketOrderResponse2l3lLeg2L = 2125,

        /// <summary>
        /// 
        /// </summary>
        MarketOrderResponse2l3lLeg3L = 2126,

        /// <summary>
        /// 
        /// </summary>
        OrderErrorResponse2l3lLeg2L = 2155,

        /// <summary>
        /// 
        /// </summary>
        OrderErrorResponse2l3lLeg3L = 2156,

        /// <summary>
        /// 
        /// </summary>
        OrderCancelConfirmationResponse2l3lLeg2L = 2131,

        /// <summary>
        /// 
        /// </summary>
        OrderCancelConfirmationResponse2l3lLeg3L = 2132,

        #endregion

        #region Unsolicited Messages

        /// <summary>
        /// 
        /// </summary>
        StopLossTrigger = 2212,

        /// <summary>
        /// 
        /// </summary>
        TradeConfirmation = 2222,

        /// <summary>
        /// 
        /// </summary>
        TradeModificationConfirmationResponse = 2287,

        /// <summary>
        /// 
        /// </summary>
        TradeModificationRejectionResponse = 2288,

        /// <summary>
        /// 
        /// </summary>
        TradeCancellationConfirmationResponse = 2282,

        /// <summary>
        /// 
        /// </summary>
        TradeCancellationRejection = 2286,

        /// <summary>
        /// 
        /// </summary>
        BroadMsgSendFromControlInt = 5295,

        /// <summary>
        /// 
        /// </summary>
        HeartBeatMessage = 8999,

        /// <summary>
        /// 
        /// </summary>
        HeartBeatMessageResponse = 9000,

        /// <summary>
        /// 
        /// </summary>
        NegotiatedOrderEnteredByCounterParty = 2009,

        /// <summary>
        /// 
        /// </summary>
        NegotiatedOrderToBl = 2008,

        /// <summary>
        /// 
        /// </summary>
        CancelNegotiatedOrder = 2076,

        #endregion

        #region Bhavcopy

        /// <summary>
        /// 
        /// </summary>
        ReportOnMarketStatistics = 1833,

        /// <summary>
        /// 
        /// </summary>
        ReportOnIndex = 1836,

        #endregion

        #region Reports

        /// <summary>
        /// 
        /// </summary>
        ReportRequest = 1900,

        /// <summary>
        /// 
        /// </summary>
        ReportResponse = 1901,

        /// <summary>
        /// 
        /// </summary>
        OpenOrdersReport = 1821,

        /// <summary>
        /// 
        /// </summary>
        OrderLogReport = 1824,

        /// <summary>
        /// 
        /// </summary>
        TradesReport = 1827,

        /// <summary>
        /// 
        /// </summary>
        SpreadOrderLogReport = 1992,

        /// <summary>
        /// 
        /// </summary>
        SpreadTradeReport = 1993,

        #endregion

        #region Broadcast

        /// <summary>
        /// 
        /// </summary>
        GeneralMessageBroadcast = 6501,

        /// <summary>
        /// 
        /// </summary>
        ChangeInSystemStatus = 7206,

        /// <summary>
        /// 
        /// </summary>
        SecurityMasterUpdate = 7305,

        /// <summary>
        /// 
        /// </summary>
        ChangeInInstrumentMaster = 7324,

        /// <summary>
        /// 
        /// </summary>
        ChangeParticipantStatus = 7306,

        /// <summary>
        /// 
        /// </summary>
        ChangeofSecurityStatus = 7320,

        /// <summary>
        /// 
        /// </summary>
        ChangeofSecurityStatusPreopen = 7210,

        /// <summary>
        /// 
        /// </summary>
        TurnoverLimitExceededOrBrokerReactivated = 9010,

        /// <summary>
        /// 
        /// </summary>
        TurnoverLimitExceededOrBrokerReactivatedBrokerReactivated = 9011,

        /// <summary>
        /// 
        /// </summary>
        AuctionActivityMessage = 6582,

        /// <summary>
        /// 
        /// </summary>
        ChangeofAuctionStatus = 6581,

        /// <summary>
        /// 
        /// </summary>
        MarketStatusOpen = 6511,

        /// <summary>
        /// 
        /// </summary>
        MarketStatusClose = 6521,
        /// <summary>
        /// 
        /// </summary>
        MarketStatusNormalMarketPreOpenEnded = 6571,
        /// <summary>
        /// 
        /// </summary>
        MarketStatusPreOrPostDay = 6531,
        /// <summary>
        /// 
        /// </summary>
        MarketStatusPreOpenShutdown = 6531,
        /// <summary>
        /// 
        /// </summary>
        MarketStatusPreClose = 6571,

        /// <summary>
        /// 
        /// </summary>
        MarketStatusPostClose = 6522,

        /// <summary>
        /// 
        /// </summary>
        TickerAndMarketIndex = 7202,

        /// <summary>
        /// 
        /// </summary>
        MarketByOrderOrMarketByPriceUpdate = 7200,

        /// <summary>
        /// 
        /// </summary>
        MarketByPriceUpdate = 7208,

        /// <summary>
        /// 
        /// </summary>
        MarketWatchUpdate = 7201,

        /// <summary>
        /// 
        /// </summary>
        ContractOpenMessage = 6013,

        /// <summary>
        /// 
        /// </summary>
        BroadCastCircuitCheck = 6541,

        /// <summary>
        /// 
        /// </summary>
        SpreadMarketByPrice = 7211,

        /// <summary>
        /// 
        /// </summary>
        IndexBroadcast = 7207,

        /// <summary>
        /// 
        /// </summary>
        IndustryIndexBroadcast = 7203,

        /// <summary>
        /// 
        /// </summary>
        SuspendedSymbolTrigger = 6573,

        /// <summary>
        /// 
        /// </summary>
        SpotNonGlobalSuspended = 6574,

        /// <summary>
        /// 
        /// </summary>
        SpotGlobalSuspended = 6575,

        /// <summary>
        /// 
        /// </summary>
        UnderlyingOpenInterest = 7130,

        #endregion

        #region Inquiry

        /// <summary>
        /// 
        /// </summary>
        AuctionInquiryRequest = 1090,

        /// <summary>
        /// 
        /// </summary>
        AuctionInquiryResponse = 1091,

        /// <summary>
        /// 
        /// </summary>
        MBOInquiryforOddLotMarket = 1010,

        /// <summary>
        /// 
        /// </summary>
        MBOInquiryResponsetoOddLotMarket = 1011,

        #endregion

        #region Exercise / Delivery Request

        /// <summary>
        /// 
        /// </summary>
        ExerciseAndDeliveryIntentionEntryRequest = 4000,

        /// <summary>
        /// 
        /// </summary>
        ExerciseAndDeliveryResponse = 4001,

        /// <summary>
        /// 
        /// </summary>
        ExerciseAndDeliveryConfirmation = 4002,

        /// <summary>
        /// 
        /// </summary>
        ExerciseAndDeliveryModification = 4005,

        /// <summary>
        /// 
        /// </summary>
        ExerciseAndDeliveryModificationConfirm = 4007,

        /// <summary>
        /// 
        /// </summary>
        ExerciseAndDeliveryCancellation = 4008,

        /// <summary>
        /// 
        /// </summary>
        ExerciseAndDeliveryCancellationResponse = 4009,

        /// <summary>
        /// 
        /// </summary>
        ExerciseAndDeliveryCancellationConfirmation = 4010,

        #endregion

        #region Give Up Trades

        /// <summary>
        /// 
        /// </summary>
        GiveUpApproveRequest = 4500,

        /// <summary>
        /// 
        /// </summary>
        GiveUpApproveResponse = 4501,

        /// <summary>
        /// 
        /// </summary>
        GiveUpApproveConfirm = 4502,

        /// <summary>
        /// 
        /// </summary>
        GiveUpRejectRequest = 4503,

        /// <summary>
        /// 
        /// </summary>
        GiveUpRejectResponse = 4504,

        /// <summary>
        /// 
        /// </summary>
        GiveUpRejectConfirm = 4505,

        /// <summary>
        /// 
        /// </summary>
        GiveUpApproveAll = 4513

        #endregion
    }

    #region Market Status
    /// <summary>
    /// 
    /// </summary>
    public enum MarketStatus
    {
        /// <summary>
        /// 
        /// </summary>
        Pre_Open = 0,
        /// <summary>
        /// 
        /// </summary>
        Open = 1,
        /// <summary>
        /// 
        /// </summary>
        Closed = 2,
        /// <summary>
        /// 
        /// </summary>
        Pre_Open_Ended = 3,
    }
    #endregion

    #region Market Type
    /// <summary>
    /// 
    /// </summary>
    public enum MarketType
    {
        /// <summary>
        /// 
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 
        /// </summary>
        Odd_Lot = 2,
        /// <summary>
        /// 
        /// </summary>
        Spot = 3,
        /// <summary>
        /// 
        /// </summary>
        Auction = 4,
        /// <summary>
        /// 
        /// </summary>
        Call_Auction1 = 5,
        /// <summary>
        /// 
        /// </summary>
        Call_Auction2 = 6,
    }
    #endregion

    #region OrderAttributes
    /// <summary>
    /// 
    /// </summary>
    public enum OrderAttributes
    {
        /// <summary>
        /// 
        /// </summary>
        Mf = 0,
        /// <summary>
        /// 
        /// </summary>
        Aon = 1,
        /// <summary>
        /// 
        /// </summary>
        Eos = -1,
        /// <summary>
        /// 
        /// </summary>
        Ioc = 2,
        /// <summary>
        /// 
        /// </summary>
        Gtc = 3,
        /// <summary>
        /// 
        /// </summary>
        Day = 4,
        /// <summary>
        /// 
        /// </summary>
        Rl = -5,
        /// <summary>
        /// 
        /// </summary>
        Sl = 5,
        /// <summary>
        /// 
        /// </summary>
        Limit = -6,
        /// <summary>
        /// 
        /// </summary>
        Market = 6,
        /// <summary>
        /// 
        /// </summary>
        Ato = 7,
        /// <summary>
        /// 
        /// </summary>
        Mit = 11,
        /// <summary>
        /// 
        /// </summary>
        Frozen = 12,
        /// <summary>
        /// 
        /// </summary>
        NewOrder = -13,
        /// <summary>
        /// 
        /// </summary>
        OrderModified = 13,
        /// <summary>
        /// 
        /// </summary>
        OrderTraded = 14,
        /// <summary>
        /// 
        /// </summary>
        MatchedInd = 15,
    }
    #endregion
     
    #region Permit Trading
    /// <summary>
    /// 
    /// </summary>
    public enum PermitTrading
    {
        /// <summary>
        /// Trading Not Allowed
        /// </summary>
        Trading_Not_Allowed = 0,

        /// <summary>
        /// Trading Allowed
        /// </summary>
        Trading_Allowed = 1
    }
    #endregion

    #region User Type
    /// <summary>
    /// 
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 
        /// </summary>
        Dealer = 0,
        /// <summary>
        /// 
        /// </summary>
        Corporate_Manager = 4,
        /// <summary>
        /// 
        /// </summary>
        Branch_Manager = 5,
    }
    #endregion
}
