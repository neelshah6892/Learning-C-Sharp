using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using sysreadern;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Project1
{
    class Class1
    {
        [DllImport("sysreadern.dll")]
        //public static extern 
        public static extern int ProcessMBR(char* streamBuffer, char* unCompbuffer, int filterTrCode, TKTABLE* tktable, long filterTkn, short saveFlg);

        //int z = ProcessMBR();
        Debug.WriteLine("Send to debug output.");
    }

}
