using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace VWAP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Administrator\\Desktop\\GreekExcel\\GreekExcel_sanga.xlsb";
            //string path = "C:\\Users\\Administrator\\Desktop\\GreekExcel\\GreekExcel.xls";

            Application excel = new Application
            {
                Visible = true,
            };
            //Workbook wb = excel.Workbooks.Open(path);
            Workbook wb = System.Runtime.InteropServices.Marshal.BindToMoniker(path) as Workbook;
            Worksheet ws = wb.Worksheets["MarketWatch"];


            object[,] myObjects;

            Range range = ws.Range[ws.Cells[3, 7], ws.Cells[202, 8]];
            //Range range = ws.Range[ws.Cells[3, 8], ws.Cells[8, 9]];
            myObjects = range.Value;

            foreach (object obj in myObjects)
            {
                Console.WriteLine(obj);
            }

            //wb.Close();
            //excel.Quit();
        }
    }
}
