using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReadExcel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWB = excelApp.Workbooks.Open(@"C:\Users\Administrator\Desktop\Sample.xlsx");
            //Excel.Worksheet excelWS = excelWB.Sheets[1];
            Excel._Worksheet excelWS = (Excel._Worksheet)excelWB.Sheets[1];
            Excel.Range excelRange = excelWS.UsedRange;

            int rowCount = excelRange.Rows.Count;
            int columnCount = excelRange.Columns.Count;

            for(int i = 0; i < rowCount; i++)
            {
                for(int j= 0; j < columnCount; j++)
                {
                    if (excelRange.Cells[i,j] != null)
                    {
                        Console.Write(excelRange.Cells[i,j].Text.ToString()+" ");
                        //Console.Write(excelRange.Cells[i, j].Value2.ToString() + " ");
                    }
                }
            }
            Console.ReadKey();
            Marshal.ReleaseComObject(excelWS);
            Marshal.ReleaseComObject(excelRange);
            excelWB.Close();
            Marshal.ReleaseComObject(excelWB);
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);
        }
    }
}