using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LegLead
{
    /// <summary>
    /// System Configuration
    /// </summary>
    [Serializable]
    public class SystemConfiguration
    {
        [XmlElement]
        public string ApplicationName { get; set; }

        [XmlElement]
        public string Version { get; set; }
      
        [XmlElement]
        public string NseCmBroadcastIp { get; set; }
        [XmlElement]
        public int NseCmBroadcastPort { get; set; }
       
        [XmlElement]
        public string NseFoBroadcastIp { get; set; }
        [XmlElement]
        public int NseFoBroadcastPort { get; set; }

        [XmlElement]
        public string Gateway { get; set; }

        [XmlElement]
        public double IndexIntervalAboveTwenty { get; set; }

        [XmlElement]
        public double IndexIntervalTenToTwenty { get; set; }

        [XmlElement]
        public double IndexIntervalFiveToTen { get; set; }

        [XmlElement]
        public double IndexIntervalBelowFive { get; set; }

        [XmlElement]
        public double StockIntervalAboveTwenty { get; set; }

        [XmlElement]
        public double StockIntervalTenToTwenty { get; set; }

        [XmlElement]
        public double StockIntervalFiveToTen { get; set; }

        [XmlElement]
        public double StockIntervalBelowFive { get; set; }

        [XmlElement]
        public string SymbolPairFilePath { get; set; }
        
        [XmlElement]
        public string SymboldiffFilePath { get; set; }

        [XmlElement]
        public string Iv_PercentileFilePath { get; set; }

        [XmlElement]
        public double IVPHigh { get; set; }

        [XmlElement]
        public double IVPLow { get; set; }

        [XmlElement]
        public string LogFilePath { get; set; }

        [XmlElement]
        public string IvPastData { get; set; }
    }
}
