using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ArisDev;
using System.Diagnostics;

namespace ArisDev.Api.ApiOwn
{
    #region OrderRequest

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class OrderDetail
    {
        public OrderDetail()
        {
            RejectionReason = string.Empty;
            ClientCode = string.Empty;
            Remarks = string.Empty;
            ParticipantId = string.Empty;
            InstrumentInformation.InstrumentCode = string.Empty;
            InstrumentInformation.InstrumentName = string.Empty;
            InstrumentInformation.OptionType = string.Empty;
        }

        //public ushort Exchange;
        public string Exchange;
        public ushort ErrorCode;
        public ushort ReasonCode;
        public InstrumentInformation InstrumentInformation = new InstrumentInformation();
        public ushort TypeOfOrder;
        public ushort SessionId;
        public double OrderNumber;
        public ushort BuyOrSell;
        public uint DisclosedQuantity;
        public uint DisclosedQuantityRemaining;
        public uint TotalQuantityRemaining;
        public uint OrderQuantity;
        public uint QuantityTradedToday;
        public int OrderPrice;
        public int TriggerPrice;
        public uint GoodTillDate;
        public uint OrderEntryTime;
        public uint OrderTimeOrLastUpdateTime;
        public byte OrderAttribute;
        public byte OrderAttribute1;
        public uint CtclId;
        public uint CPBrokerId;
        public uint TradingMemberId;
        public uint ClearingMemberId;
        public ushort Account;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string Remarks;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string RejectionReason;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string ParticipantId;
        public double TerminalInfo;
        public ushort Status;
        public short MarketProtectionPercentage;
        public int fillerId;
        public bool IsCancelSend;
        public bool IsModifySend;
    }
    #endregion

    #region Nested Structures

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class InstrumentInformation
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string InstrumentName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string InstrumentCode;
        public uint InstrumentIdentifier;
        public uint ExpiryDate;
        public int StrikePrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string OptionType;
        public ushort CaLevel;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string InstrumentSeries;
    }

    #endregion

    //public class MarketPictue
    //{
    //    public int AverageTradedPrice;
    //    public BestFive[] Best5Buy;
    //    public BestFive[] Best5Sell;
    //    public int ClosePrice;
    //    public int HighPrice;
    //    public int LastTradedPrice;
    //    public int LastTradedQty;
    //    public int LastTradeTime;
    //    public int LowPrice;
    //    public int OpenPrice;
    //    public int PriceDivisor;
    //    public string TokenNo;
    //    public double TotalBuyQty;
    //    public double TotalQtyTraded;
    //    public double TotalSellQty;
    //    public double TotalTradedValue;
    //    public int TotalTrades;
    //}

    //public struct BestFive
    //{
    //    public int OrderPrice;
    //    public int Quantity;
    //    public int TotalNumberOfOrders;
    //}


    #region OrderInformation
    //public class Orderinformation
    //{
    //    public string  RuleNo;
    //    public int StrikePrice;       
    //    public double Wind;
    //    public double Unwind;
    //    public int Over;
    //    public int Round;
    //    static readonly StringBuilder sMessage = new StringBuilder();
    //    static readonly StringBuilder sMessageSpread = new StringBuilder();
    //    public static string getinfo()
    //    {
    //        sMessage.Clear();

    //    }
    //}

    #endregion


}

