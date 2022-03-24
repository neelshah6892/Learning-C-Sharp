using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Runtime.InteropServices;

namespace ExcelSingleCellValue
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //string path = "C:\\Users\\Administrator\\Desktop\\GreekExcel\\GreekExcel.xlsb";
            string path = @"C:\\Users\\Administrator\\Desktop\\GreekExcel\\GreekExcel.xls";

            Application excel = new Application();
            //excel.Visible = true;
            //Workbook wb = excel.Workbooks.Open(path);
            Workbook wb = (Workbook)Marshal.BindToMoniker(path);
            Worksheet ws = wb.Worksheets["MarketWatch"];


            //float test = float.Parse(ws.Cells[3, 27].Value.ToString());
            float test = float.Parse(ws.Cells[3, 13].Value.ToString());

            double max = test;
            double min = test;

            Console.WriteLine("Min: " + min + "\tMax: " + max);

            //wb.Close();

            while (true)
            {

                //test = float.Parse(ws.Cells[3, 27].Value.ToString());
                test = float.Parse(ws.Cells[3, 13].Value.ToString());

                double num1 = test;


                if (num1 > max)
                {
                    max = num1;
                    Console.WriteLine("New max: " + max);
                    Console.WriteLine(num1 + ": No new min max. " + "Min: " + min + "\tMax: " + max);
                }
                else if (num1 < min)
                {
                    min = num1;
                    Console.WriteLine("New min: " + min);
                    Console.WriteLine(num1 + ": No new min max. " + "Min: " + min + "\tMax: " + max);
                }
                else
                {
                    Console.WriteLine(num1 + ": No new min max. " + "Min: " + min + "\tMax: " + max);
                }
                //Thread.Sleep(60000 * 5);
                Thread.Sleep(5000);
            }

            
        }
    }
}
