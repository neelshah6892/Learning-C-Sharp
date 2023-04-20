using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using LegLead.NseCmApi.Broadcast;

namespace LegLead
{
    public class BTPacket
    {
        public struct MessageHeader
        {
            public UInt64 TransCode;
        }

        #region All Structures
        // GUI update 1 
        // Reset update 2
        // L4_LC_LP_UC_UP_SpreadBiddingUpdate 3
        // FUT ltp update 4
        // TrdUpdate3L 5
        // Save_EOD 6
        // EOD trade Retransfer 7
        // Manual Trade entry 8
        // RMS Error Message 9
        // Trade Massage 10
        // VOL_update 11
        // TWO_Leg_Trade_Update 12 // TradePrice = Leg1 Price and Transcost  = Leg2 Price
        // request_new_strike 13
        // new_strike_request 9
        // Intermediate_order_send 10
        // winden_offset 19
        // Rule_Not_Found 21
        // Immidiate_send_Order 10
        // Upadate_Greeks = 22


        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct HeartBeat
        {
            public UInt64 TransCode;
            public UInt64 counter;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct volupdate
        {
            public UInt64 TransCode;
            public double PRICE;
            public double Qty;
            public int token;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct GUIUpdate
        {
            public UInt64 TransCode;
            public double Wind;
            public double Unwind;
            public double AvgSpread;
            public double Netting;
            public double TradePrice;
            public double TransactionCost;
            public UInt64 gui_id;
            public UInt64 UniqueID;
            public UInt64 StrategyId;
            public int Token;
            public int Open;
            public int Round;
            public int NonIOCStrike;
            public int WindPos;
            public int UnWindPos;
            public int OverNightWindPos;
            public int OverNightUnWindPos;
            public bool NonIOCSeries;
            public bool IsbeenTraded;
            public bool isWind;
            public string toString()
            {
                string ss = "";                
                switch (TransCode)
                {
                    case 1:
                        ss = ss + "GUI|Enter" + UniqueID + "|strategyID:" + StrategyId + "|ask_wind:" + Wind + "|bid_unwind:" + Unwind + "|open:" + Open + "|round:" + Round + "|wind:" + WindPos + "|unwind:" + UnWindPos + "|av. spread:" + AvgSpread;
                        break;
                    case 2:
                        ss = ss + "GUI|Reset" + UniqueID + "|strategyID:" + StrategyId + "|ask_wind:" + Wind + "|bid_unwind:" + Unwind + "|open:" + Open + "|round:" + Round + "|wind:" + WindPos + "|unwind:" + UnWindPos + "|av. spread:" + AvgSpread;
                        break;
                    case 5:
                        ss = ss + "TRADE|Fut_Token" + Token + "|Uniq_id:" + UniqueID + "|strategyID:" + StrategyId + "|gui_id:" + gui_id + "|tradePrice:" + TradePrice + "|txncost:" + TransactionCost + "|open:" + Open + "|round:" + Round + "|wind:" + WindPos + "|unwind:" + UnWindPos + "|av. spread:" + AvgSpread;
                        break;
                    case 10:
                        break;
                    case 16:
                        break;
                    case 17:
                        break;
                    case 9:
                        break;
                    case 19:
                        ss = ss + "GUI|" + gui_id + "|TransCode |" + TransCode;
                        break;
                    case 20:
                        ss = ss + "GUI|" + gui_id + "|TransCode |" + TransCode + "|Limit Hit|" + Token;
                        break;
                    case 21:
                        ss = ss + "GUI|" + gui_id + "|TransCode |" + TransCode + "|Rule_Found|" + UniqueID;
                        break;
                    default:
                        break;
                }
                return ss;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct FUTLtp
        {
            //MessageHeader header;
            public UInt64 TransCode;
            public double tradePrice;
            public int Token;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct L4_LC_LP_UC_UP_SpreadBiddingUpdate
        {
            //  MessageHeader header; // 0-31
            public UInt64 TransCode;
            public UInt64 SequenceNo;//32
            public double BidPrice; // Conversion // 32 - 
            public double AskPrice;
            public double Ltp;
            public double Volume;
            public UInt64 UniqueID;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct SpreadMarketUpdate
        {
            public UInt64 TransCode;
            public UInt64 SequenceNo;
            public double BidPrice; // Conversion // 32 - 
            public double AskPrice;
            public double Ltp;
            public UInt64 UniqueId1;
            public UInt64 UniqueId2;
            public int BidQty;
            public int AskQty;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct OI_Ticker
        {
            public UInt64 TransCode;
            public int Current_OI;
            public int Token;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct TrdUpdate3L
        {
            MessageHeader header;
            public UInt64 UniqueID;
            public double TradePrice;
            public double TransactionCost;
            public bool isWind;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct TradeMessage
        {
            public UInt64 TransCode;
            public UInt64 Token;
            public UInt64 ExchangeSequence;
            public UInt64 Sequence;
            public double Price;
            public int Qty;
            public int Side; // 66 buy 83 sell
        }
        #endregion


        public struct IndexBroadcast
        {
            public UInt64 TransCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
            public char[] _symbol;
            public double _Price;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct OneThreeThreeOneSpreadTradeMsg
        {
            public UInt64 TransCode;
            public UInt64 Sequence;
            public UInt64 Expiry;
            public double Spread;
            public UInt32 Strike1;
            public UInt32 Strike2;
            public UInt32 Strike3;
            public UInt32 Strike4;
            public UInt32 IsWind;
            public UInt32 IsCallPut;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct OneTwoOneTradeMsg
        {
            public UInt64 TransCode;
            public UInt64 Sequence;
            public UInt64 Expiry;
            public double Spread;
            public UInt32 Strike1;
            public UInt32 Strike2;
            public UInt32 Strike3;
            public UInt32 IsWind;
            public UInt32 IsCallPut;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct BoxTradeMsg
        {
            public UInt64 TransCode;
            public UInt64 Sequence;
            public UInt64 Expiry;
            public double Spread;
            public UInt32 Strike1;
            public UInt32 Strike2;
            public UInt32 IsWind;

        }


        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct ConRev
        {
            public UInt64 TransCode;
            public UInt64 Sequence;
            public UInt64 FUTExpiry;
            public UInt64 Expiry;
            public double Spread;
            public UInt32 Strike1;
            public UInt32 IsWind;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct SpreadMarketPicture
        {
            public BestFive[] Best5Buy;
            public BestFive[] Best5Sell;
            public int TokenNo1;
            public int TokenNo2;
            public int LastTradeTime;
            public int LastTradedPrice;
            public int LastTradedQty;
            public short _mbpBuy;
            public short _mbpSell;
            public UInt64 seqNo;


        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct BestFive
        {
            public int OrderPrice;
            public int Quantity;
            public int TotalNumberOfOrders;
        }




        
    }
}
