using System;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace WriteExcel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWb = excelApp.Workbooks.Add("");
            Excel._Worksheet excelWS = excelWb.ActiveSheet;
            excelWS.Cells[1, 1] = "First Name";
            excelWS.Cells[1, 2] = "Last Name";
            excelWS.Cells[2, 1] = "Neel";
            excelWS.Cells[2, 2] = "Shah";
            //excelWb.SaveCopyAs(@"C:\Users\dhwan\Desktop\Demo.xlsx");

            //XLsx to Csv
            excelWb.SaveAs(@"C:\Users\dhwan\Desktop\Demo.csv", Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV);
            
            
            excelWb.Close();
            excelApp.Quit();
        }
    }
}