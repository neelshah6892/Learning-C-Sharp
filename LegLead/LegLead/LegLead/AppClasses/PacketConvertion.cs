using System;
using System.Reflection;
using System.Data;
using System.Collections;
using MTApi;
using System.Linq;
using System.Threading;
using LegLead.NseFoApi;
using LegLead.AppClasses;


namespace LegLead
{
    /// <summary>
    /// Packet convertion
    /// </summary>
    public static class PacketConvertion
    {
        
        #region MarketDepth

        public static void GetOwnMarketPictureNseCm(NseCmApi.Broadcast.InteractiveMBPData marketPicture)
        {
            try
            {
                MTBCastPackets.MarketPicture _marketPic = new MTBCastPackets.MarketPicture();
                _marketPic.Header.GatewayId = 2;
                _marketPic.AverageTradedPrice = marketPicture.AverageTradePrice;

                _marketPic.Best5Buy = new MTBCastPackets.BestFive[5] { new MTBCastPackets.BestFive() { OrderPrice=marketPicture.BuyMBPInformation[0].Price , Quantity =marketPicture.BuyMBPInformation[0].Quantity, TotalNumberOfOrders = marketPicture.BuyMBPInformation[0].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.BuyMBPInformation[1].Price , Quantity =marketPicture.BuyMBPInformation[1].Quantity, TotalNumberOfOrders = marketPicture.BuyMBPInformation[1].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.BuyMBPInformation[2].Price , Quantity =marketPicture.BuyMBPInformation[2].Quantity, TotalNumberOfOrders = marketPicture.BuyMBPInformation[2].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.BuyMBPInformation[3].Price , Quantity =marketPicture.BuyMBPInformation[3].Quantity, TotalNumberOfOrders = marketPicture.BuyMBPInformation[3].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.BuyMBPInformation[4].Price , Quantity =marketPicture.BuyMBPInformation[4].Quantity, TotalNumberOfOrders = marketPicture.BuyMBPInformation[4].NumberOfOrders}};
                _marketPic.Best5Sell = new MTBCastPackets.BestFive[5] { new MTBCastPackets.BestFive() { OrderPrice=marketPicture.SellMBPInformation[0].Price , Quantity =marketPicture.SellMBPInformation[0].Quantity, TotalNumberOfOrders = marketPicture.SellMBPInformation[0].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.SellMBPInformation[1].Price , Quantity =marketPicture.SellMBPInformation[1].Quantity, TotalNumberOfOrders = marketPicture.SellMBPInformation[1].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.SellMBPInformation[2].Price , Quantity =marketPicture.SellMBPInformation[2].Quantity, TotalNumberOfOrders = marketPicture.SellMBPInformation[2].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.SellMBPInformation[3].Price , Quantity =marketPicture.SellMBPInformation[3].Quantity, TotalNumberOfOrders = marketPicture.SellMBPInformation[3].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.SellMBPInformation[4].Price , Quantity =marketPicture.SellMBPInformation[4].Quantity, TotalNumberOfOrders = marketPicture.SellMBPInformation[4].NumberOfOrders}};

                _marketPic.ClosePrice = marketPicture.ClosingPrice;
                _marketPic.HighPrice = marketPicture.HighPrice;
                _marketPic.LastTradedPrice = marketPicture.LastTradedPrice;
                _marketPic.LastTradedQty = marketPicture.LastTradeQuantity;
                _marketPic.LastTradeTime = marketPicture.LastTradeTime;
                _marketPic.LowPrice = marketPicture.LowPrice;
                _marketPic.OpenPrice = marketPicture.OpenPrice;
                _marketPic.PriceDivisor = 100;
                _marketPic.TokenNo = marketPicture.Token.ToString();
                _marketPic.TotalBuyQty = marketPicture.TotalBuyQuantity;
                _marketPic.TotalSellQty = marketPicture.TotalSellQuantity;
                _marketPic.TotalQtyTraded = marketPicture.VolumeTradedToday;
                _marketPic.TotalTrades = 0;
                int updateTime = 0;
                if (ArisApi_a._arisApi.MarketPictureCollection.ContainsKey(marketPicture.Token))
                    ArisApi_a._arisApi.MarketPictureCollection[marketPicture.Token] = _marketPic;
                else
                    ArisApi_a._arisApi.MarketPictureCollection.Add(marketPicture.Token, _marketPic);

                ArisApi_a._arisApi.OnMarketDepthUpdateProcess(_marketPic,updateTime);
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }

        public static void GetOwnMarketPictureNseFo(NseFoApi.Broadcast.InteractiveMBPData marketPicture, NseFoApi.Header.MessageHeader messageHeader)
        {
            try
            {
                MTBCastPackets.MarketPicture _marketPic = new MTBCastPackets.MarketPicture();
                _marketPic.Header.GatewayId = 1;
                _marketPic.AverageTradedPrice = marketPicture.AverageTradePrice;

                _marketPic.Best5Buy = new MTBCastPackets.BestFive[5] { new MTBCastPackets.BestFive() { OrderPrice=marketPicture.BuyMBPInformation[0].Price , Quantity =marketPicture.BuyMBPInformation[0].Quantity, TotalNumberOfOrders = marketPicture.BuyMBPInformation[0].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.BuyMBPInformation[1].Price , Quantity =marketPicture.BuyMBPInformation[1].Quantity, TotalNumberOfOrders = marketPicture.BuyMBPInformation[1].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.BuyMBPInformation[2].Price , Quantity =marketPicture.BuyMBPInformation[2].Quantity, TotalNumberOfOrders = marketPicture.BuyMBPInformation[2].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.BuyMBPInformation[3].Price , Quantity =marketPicture.BuyMBPInformation[3].Quantity, TotalNumberOfOrders = marketPicture.BuyMBPInformation[3].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.BuyMBPInformation[4].Price , Quantity =marketPicture.BuyMBPInformation[4].Quantity, TotalNumberOfOrders = marketPicture.BuyMBPInformation[4].NumberOfOrders}};
                _marketPic.Best5Sell = new MTBCastPackets.BestFive[5] { new MTBCastPackets.BestFive() { OrderPrice=marketPicture.SellMBPInformation[0].Price , Quantity =marketPicture.SellMBPInformation[0].Quantity, TotalNumberOfOrders = marketPicture.SellMBPInformation[0].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.SellMBPInformation[1].Price , Quantity =marketPicture.SellMBPInformation[1].Quantity, TotalNumberOfOrders = marketPicture.SellMBPInformation[1].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.SellMBPInformation[2].Price , Quantity =marketPicture.SellMBPInformation[2].Quantity, TotalNumberOfOrders = marketPicture.SellMBPInformation[2].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.SellMBPInformation[3].Price , Quantity =marketPicture.SellMBPInformation[3].Quantity, TotalNumberOfOrders = marketPicture.SellMBPInformation[3].NumberOfOrders}, 
                          new MTBCastPackets.BestFive() { OrderPrice=marketPicture.SellMBPInformation[4].Price , Quantity =marketPicture.SellMBPInformation[4].Quantity, TotalNumberOfOrders = marketPicture.SellMBPInformation[4].NumberOfOrders}};

                _marketPic.ClosePrice = marketPicture.ClosingPrice;
                _marketPic.HighPrice = marketPicture.HighPrice;
                _marketPic.LastTradedPrice = marketPicture.LastTradedPrice;
                _marketPic.LastTradedQty = marketPicture.LastTradeQuantity;
                _marketPic.LastTradeTime = marketPicture.LastTradeTime;
                _marketPic.LowPrice = marketPicture.LowPrice;
                _marketPic.OpenPrice = marketPicture.OpenPrice;
                _marketPic.PriceDivisor = 100;
                _marketPic.TokenNo = marketPicture.Token.ToString();
                _marketPic.TotalBuyQty = marketPicture.TotalBuyQuantity;
                _marketPic.TotalSellQty = marketPicture.TotalSellQuantity;
                _marketPic.TotalQtyTraded = marketPicture.VolumeTradedToday;
                _marketPic.TotalTrades = 0;


                int updateTime = messageHeader.LogTime;

                string nxtexpiry1 = ArisApi_a._arisApi.SecondToDateTime(Market.NseCm, updateTime).ToString();


                double updateTime1 = messageHeader.TimeStamp1;
                double updateTime2 = messageHeader.TimeStamp2;

                if (ArisApi_a._arisApi.MarketPictureCollection.ContainsKey(marketPicture.Token))
                    ArisApi_a._arisApi.MarketPictureCollection[marketPicture.Token] = _marketPic;
                else
                    ArisApi_a._arisApi.MarketPictureCollection.Add(marketPicture.Token, _marketPic);

                ArisApi_a._arisApi.OnMarketDepthUpdateProcess(_marketPic,updateTime);
            }
            catch (Exception ex)
            {
                ArisApi_a._arisApi.WriteToErrorLog(
                    MethodBase.GetCurrentMethod().DeclaringType.Name + " : " + MethodBase.GetCurrentMethod().Name + " : " + ex.Message);
            }
        }


        public static void GetOwnBroadCast(NseCmApi.Broadcast.Indices broadcastPicture)
        {
            //BTPacket.IndexBroadcast _broadcast = new BTPacket.IndexBroadcast();
            //_broadcast._symbol = broadcastPicture.IndexName.ToCharArray();
            //_broadcast._Price = Convert.ToDouble(broadcastPicture.IndexValue) / 100;
            ArisApi_a._arisApi.OnIndexBroadCastProcess(broadcastPicture);
        }

        #endregion

        
    }
}