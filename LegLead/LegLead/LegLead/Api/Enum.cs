using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegLead
{
    public enum Account
    {
        /// <summary>
        /// Order Placed For Client
        /// </summary>
        CLIENT = 1,

        /// <summary>
        /// Order Placed For House Or Proprietary Account
        /// </summary>
        PRO = 2,

        /// <summary>
        /// Order Placed For Institutions
        /// </summary>
        OrderPlacedForInstitutions = 3,
    }

    /// <summary>
    /// buy or sell
    /// </summary>
    public enum BuyOrSell
    {
        /// <summary>
        /// Buy
        /// </summary>
        Buy = 1,

        /// <summary>
        /// Sell
        /// </summary>
        Sell = 2
    }

    /// <summary>
    /// Exchange Multipler
    /// </summary>
    public enum ExchangeMultipler
    {
        /// <summary>
        /// 
        /// </summary>
        NcdexMcx = 100,
        /// <summary>
        /// 
        /// </summary>
        Mcxsx = 10000,
    }

    /// <summary>
    /// leg
    /// </summary>
    public enum Legs
    {
        /// <summary>
        /// First Leg
        /// </summary>
        FirstLeg = 1,

        /// <summary>
        /// Second Leg 
        /// </summary>
        SecondLeg = 2,
    }

    /// <summary>
    /// Market type
    /// </summary>
    public enum Market
    {
        /// <summary>
        /// Own
        /// </summary>
        Own = 0,

        /// <summary>
        /// Ncdex market
        /// </summary>
        Ncdex = 1,

        /// <summary>
        /// Mcx
        /// </summary>
        Mcx = 2,

        /// <summary>
        /// Mcx'sx
        /// </summary>
        Mcxsx = 3,

        /// <summary>
        /// 
        /// </summary>
        NseCm = 4,

        /// <summary>
        /// 
        /// </summary>
        NseFO = 5,
    }

    /// <summary>
    /// leg
    /// </summary>
    public enum Message_View
    {
        /// <summary>
        /// 
        /// </summary>
        All = 0,
        /// <summary>
        /// 
        /// </summary>
        All_Transaction_Messages = 1,
        /// <summary>
        /// 
        /// </summary>
        Order_Messages = 2,
        /// <summary>
        /// 
        /// </summary>
        Trade_Confirmation_Messages = 3,
        /// <summary>
        /// 
        /// </summary>
        Market_Messages = 4,
        /// <summary>
        /// 
        /// </summary>
        System_Messages = 5,
        /// <summary>
        /// 
        /// </summary>
        Surveilance_Messages = 6,
        /// <summary>
        /// 
        /// </summary>
        Other_Messages = 7,
        /// <summary>
        /// Strategy messages
        /// </summary>
        Strategy_Messages = 8,
    }

    /// <summary>
    /// Order Status
    /// </summary>
    public enum OrderStatus
    {
        Pending = 1,
        Freezed = 2,
        Modified = 3,
        ModifyError = 4,
        PartiallyExecuted = 5,
        Executed = 6,
        Cancelled = 7,
        CancelError = 8,
        CancelledByExchange = 9,
    }

}
