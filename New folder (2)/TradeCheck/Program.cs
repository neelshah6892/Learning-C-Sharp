using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToExcel;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Threading;

namespace TradeCheck
{
    internal class Program
    {
        //static private string fileName = "C:\\Users\\Administrator\\Desktop\\Python\\Trade.csv";
        static void Main(string[] args)
        {
            //var lines = File.ReadAllLines(@"C:\\Users\\Administrator\\Desktop\\Python\\TRADEFO_20202207.txt");


            

            string path = "C:\\Users\\Administrator\\Desktop\\Python\\TRADEFO.txt";

            //File.Copy(path, "TRADEFO.csv");
            
            string newpath = "C:\\Users\\Administrator\\source\\repos\\TradeCheck\\bin\\Debug\\TRADEFO.csv";

            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(newpath);
            Worksheet ws = wb.Worksheets[1];

            foreach(string line in ws.Cells)
            {
                Console.WriteLine(line);
            }

            string test = ws.Cells[10000, 26].Value.ToString();
            Console.WriteLine(test);

            //var res = strs1.Except(strs2).Union(strs2.Except(strs1));
            string temp = "";

            /*foreach (string line in lines)
            {
                if (temp == "")
                {
                    temp = line;
                }
                else if (temp == line)
                {
                    Console.WriteLine(line);
                }
                    
                //Console.WriteLine(line[177] + "" +line[178] + "" +line[179] + "" + line[180] + "" + line[181] + "" + line[182] + "" + line[183] + "" + line[184]);
            }*/
            wb.Close();
            excel.Quit();
        }
    }
}

