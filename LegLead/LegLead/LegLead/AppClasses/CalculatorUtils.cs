using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegLead
{
    class CalculatorUtils
    {
        #region Day Calculation
        public static double CalculateDay(DateTime expiry)
        {
            TimeSpan ts = expiry.Date.Subtract(DateTime.Now.Date);

            if (ts.Days <= 0)
                return 1;

            return (ts.Days);
        } 
        #endregion

        #region Call Region
        public static decimal CallVolatility(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicImpliedCallVolatility(greekVar);
        }

        private static double MuicImpliedCallVolatility(GreeksVariable objVar)
        {
            double high = 5;
            double low = 0;
            do
            {
                //CallOption(UnderlyingPrice, ExercisePrice, Time, Interest, (High + Low) / 2, Dividend)
                if (MuicCallOption(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, (high + low) / 2, objVar._DividentYield) > objVar.ActualValue)
                    high = (high + low) / 2;
                else
                    low = (high + low) / 2;
            }
            while ((high - low) > 0.0001);
            return ((high + low) / 2) * 100;
        }

        public static double MuicCallOption(double underlyingPrice, double exercisePrice, double time, double interest, double volatility, double dividend)
        {
            double CallOption = Math.Exp(-dividend * time) * underlyingPrice * Cnd(dOne(underlyingPrice, exercisePrice, time, interest, volatility, dividend)) -
                                exercisePrice * Math.Pow(Math.E, (-interest * time)) * Cnd(dOne(underlyingPrice, exercisePrice, time, interest, volatility, dividend) - volatility * Math.Sqrt(time));

            return CallOption;
        }

        private static double Cnd(double x)
        {
            const double a1 = 0.31938153;
            const double a2 = -0.356563782;
            const double a3 = 1.781477937;
            const double a4 = -1.821255978;
            const double a5 = 1.330274429;

            double l = Math.Abs(x);
            double k = 1.0 / (1.0 + 0.2316419 * l);
            double dCnd = 1.0 - 1.0 / Math.Sqrt(2 * Convert.ToDouble(Math.PI.ToString())) *
                                Math.Exp(-l * l / 2.0) * (a1 * k + a2 * k * k + a3 * Math.Pow(k, 3.0) +
                                                             a4 * Math.Pow(k, 4.0) + a5 * Math.Pow(k, 5.0));

            if (x < 0)
            {
                return (1.0 - dCnd);
            }

            return dCnd;
        }

        public static double dOne(double underlyingPrice, double exercisePrice, double time, double interest, double volatility, double dividend)
        {
            double dOne = (Math.Log(underlyingPrice / exercisePrice) + (interest - dividend + 0.5 * Math.Pow(volatility, 2)) * time) / (volatility * (Math.Sqrt(time)));
            return dOne;
        }
        #endregion

        #region Put Region
        public static decimal PutVolatility(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicImpliedPutVolatility(greekVar);
        }

        private static double MuicImpliedPutVolatility(GreeksVariable objVar)
        {
            double high = 5, low = 0;
            do
            {
                if (MuicPutOption(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, (high + low) / 2, objVar._DividentYield) > objVar.ActualValue)
                    high = (high + low) / 2;
                else
                    low = (high + low) / 2;
            }
            while ((high - low) > 0.0001);
            return ((high + low) / 2) * 100;
        }

        public static double MuicPutOption(double underlyingPrice, double exercisePrice, double time, double interest, double volatility, double dividend)
        {
            double PutOption = exercisePrice * Math.Exp(-interest * time) * Cnd(-dTwo(underlyingPrice, exercisePrice, time, interest, volatility, dividend)) - Math.Exp(-dividend * time) * underlyingPrice * Cnd(-dOne(underlyingPrice, exercisePrice, time, interest, volatility, dividend));

            return PutOption;
        }

        public static double dTwo(double underlyingPrice, double exercisePrice, double time, double interest, double volatility, double dividend)
        {
            double dTwo = dOne(underlyingPrice, exercisePrice, time, interest, volatility, dividend) - volatility * Math.Sqrt(time);
            return dTwo;
        }

        #endregion

        #region Call Price and MuicActualcall
        public static decimal CallPrice(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicActualCall(greekVar);
        }

        private static decimal MuicActualCall(GreeksVariable objVar)
        {
            //AppClasses.AppGlobal.Logger.ShowToMessageLog("Fut Price:" + objVar.SpotPrice + "  ,Exp:" + objVar._TimeToExpiry);
            double firstPart = (objVar.SpotPrice * (Math.Exp(-1 * (objVar._DividentYield) * (objVar._TimeToExpiry))) * Cnd(MuicFirstDist(objVar)));
            double secondPart = (objVar.StrikePrice * (Math.Exp(-1 * (objVar._IntrestRate) * (objVar._TimeToExpiry))) * Cnd(MuicSecondDist(objVar)));
            double actualValue = (firstPart - secondPart);
            return (decimal)actualValue;
        } 
        #endregion

        #region MuicFirstDict and MuicSecondDist
        public static double MuicFirstDist(GreeksVariable objVar)
        {
            double d1 = dOne(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield);
            return d1;
            
        }

        public static double MuicSecondDist(GreeksVariable objVar)
        {
            double d2 = dTwo(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield);
            return d2;
        } 
        #endregion

        #region Delta Calc
        public static decimal CallDelta(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicDeltaCall(greekVar);
        }

        private static double MuicDeltaCall(GreeksVariable objVar)
        {
            double CallDelta = Cnd(dOne(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield));

            return CallDelta;
        } 
        #endregion

        #region Gamma Calc
        public static decimal CallGamma(GreeksVariable greekVar)
        {
            decimal dPrice = 0.0000M;
            return dPrice = (decimal)MuicGamma(greekVar);
        }

        private static double MuicGamma(GreeksVariable objVar)
        {
            double Gamma = NdOne(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield) / (objVar.SpotPrice * (objVar._Volatility * Math.Sqrt(objVar._TimeToExpiry)));
            if (Gamma.ToString() == "NaN")
                Gamma = 0;
            return Gamma;
        }

        public static double NdOne(double underlyingPrice, double exercisePrice, double time, double interest, double volatility, double dividend)
        {
            double NdOne = Math.Exp(-(Math.Pow(dOne(underlyingPrice, exercisePrice, time, interest, volatility, dividend), 2)) / 2) / (Math.Sqrt(2 * 3.14159265358979));
            return NdOne;
        } 
        #endregion

        #region Theta Calc
        public static decimal CallTheta(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicThetaCall(greekVar);
        }

        private static double MuicThetaCall(GreeksVariable objVar)
        {
            double CT = -(objVar.SpotPrice * objVar._Volatility * NdOne(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield))
                        / (2 * Math.Sqrt(objVar._TimeToExpiry)) - objVar._IntrestRate * objVar.StrikePrice * Math.Exp(-objVar._IntrestRate * (objVar._TimeToExpiry)) * NdTwo(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield);

            double CallTheta = CT / 365;
            return CallTheta;
        }

        public static double NdTwo(double underlyingPrice, double exercisePrice, double time, double interest, double volatility, double dividend)
        {
            double NdTwo = Cnd(dTwo(underlyingPrice, exercisePrice, time, interest, volatility, dividend));
            return NdTwo;
        } 
        #endregion

        #region Vega Calc
        public static decimal CallVega(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicVega(greekVar);
        }

        private static double MuicVega(GreeksVariable objVar)
        {
            double Vega = 0.01 * objVar.SpotPrice * Math.Sqrt(objVar._TimeToExpiry) * NdOne(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield);
            return Vega;
        } 
        #endregion

        #region Rho Calc
        public static decimal CallRho(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicRhoCall(greekVar);
        }

        public static double MuicRhoCall(GreeksVariable objVar)
        {
            double CallRho = 0.01 * objVar.StrikePrice * objVar._TimeToExpiry * Math.Exp(-objVar._IntrestRate * objVar._TimeToExpiry) * Cnd(dTwo(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield));
            return CallRho;
        } 
        #endregion

        #region Put price and MuicActualprice
        public static decimal PutPrice(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicActualPut(greekVar);
        }

        private static decimal MuicActualPut(GreeksVariable objVar)
        {
            //AppClasses.AppGlobal.Logger.ShowToMessageLog("Fut Price:" + objVar.SpotPrice + "  ,Exp:" + objVar._TimeToExpiry);
            double firstPart = (objVar.SpotPrice * (Math.Exp(-1 * (objVar._DividentYield) * (objVar._TimeToExpiry))) * Cnd(-1 * MuicFirstDist(objVar)));
            double secondPart = (objVar.StrikePrice * (Math.Exp(-1 * (objVar._IntrestRate) * (objVar._TimeToExpiry))) * Cnd(-1 * MuicSecondDist(objVar)));
            double actualValue = (secondPart - firstPart);
           // AppClasses.AppGlobal.Logger.ShowToMessageLog("second-" + secondPart + ", first-" + firstPart);
            return (decimal)actualValue;
        } 
        #endregion

        #region Delta Put Calc
        public static decimal PutDelta(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicDeltaPut(greekVar);
        }

        private static double MuicDeltaPut(GreeksVariable objVar)
        {
            double PutDelta = Cnd(dOne(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield)) - 1;
            return PutDelta;
        } 
        #endregion

        #region Gamma Put Calc
        public static decimal PutGamma(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicGamma(greekVar);
        }
        #endregion

        #region Theta Put Calc
        public static decimal PutTheta(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicThetaPut(greekVar);
        }

        private static double MuicThetaPut(GreeksVariable objVar)
        {
            double PT = -(objVar.SpotPrice * objVar._Volatility * NdOne(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield))
                        / (2 * Math.Sqrt(objVar._TimeToExpiry)) + objVar._IntrestRate * objVar.StrikePrice * Math.Exp(-objVar._IntrestRate * (objVar._TimeToExpiry)) * (1 - NdTwo(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield));
            double PutTheta = PT / 365;
            return PutTheta;
        } 
        #endregion

        #region Vega Put Calc
        public static decimal PutVega(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicVega(greekVar);
        } 
        #endregion

        #region Rho Put Calc
        public static decimal PutRho(GreeksVariable greekVar)
        {
            decimal price = 0.0000M;
            return price = (decimal)MuicRhoPut(greekVar);
        }

        public static double MuicRhoPut(GreeksVariable objVar)
        {
            double PutRho = -0.01 * objVar.StrikePrice * objVar._TimeToExpiry * Math.Exp(-objVar._IntrestRate * objVar._TimeToExpiry) * (1 - Cnd(dTwo(objVar.SpotPrice, objVar.StrikePrice, objVar._TimeToExpiry, objVar._IntrestRate, objVar._Volatility, objVar._DividentYield)));
            return PutRho;
        } 
        #endregion

    }
    public class GreeksVariable
    {
        public double SpotPrice;
        public double _IntrestRate;
        public double IntrestRate
        {
            get { return _IntrestRate * 100; }
            set { _IntrestRate = value / 100; }

        }
        public double StrikePrice;
        public double _Volatility;
        public double Volatility
        {
            get { return _Volatility * 100; }
            set { _Volatility = value / 100; }

        }
        public double _TimeToExpiry;
        public double _DividentYield;
        public double DividentYield
        {
            get { return _DividentYield * 100; }
            set { _DividentYield = value / 100; }
        }
        public double ActualValue;
        public double TimeToExpiry
        {
            get { return _TimeToExpiry * 365; }
            set { _TimeToExpiry = value / 365; }
        }
    }
       
}
