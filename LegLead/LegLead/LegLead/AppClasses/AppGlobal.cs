using System.Collections.Generic;
using LogWriter;
using MTCommon;
using System;
using System.Data;
using ClientCommon;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Drawing;


namespace LegLead
{
    public class AppGlobal
    {
        public static List<MarketWatch> MarketWatch;
        public static List<int> monthint;
        public const string ReadContract = "D:\\nseContractFile\\";
        public const string Watch = "LegLead";
        #region Form Object;

        #endregion
        public SystemConfiguration SystemConfig;
        public static List<string> SymList = new List<string>();
        public static Dictionary<UInt64, List<int>> TokenList = new Dictionary<UInt64, List<int>>();
        public static Dictionary<UInt64, List<int>> MapList = new Dictionary<UInt64, List<int>>();
        public static Dictionary<string, string> SectorList = new Dictionary<string, string>();
        public static bool Should_procced = true;


    }
    public class AtmDetail 
    {
       public int TokenNo;
       public double strike;
       public int FutureToken;
    }
    
}
