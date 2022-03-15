using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;

namespace CreateExcel
{
    class Class1

    {
        public static void Main(string[] args)
        {
            Application excel = new Application();
            Workbook workbook = excel.Workbooks.Open("C:\\Users\\Administrator\\Desktop\\RMS Reports\\VAR FILE_14.02.2022.xlsx");
            excel.Visible = true;

            Worksheet ws = workbook.Worksheets["03.02.2022"];

            Application ExcelApp = new Application();
            
            Workbook? ExcelWorkBook = null;

            Worksheet? ExcelWorkSheet = null;

            ExcelApp.Visible = true;

            ExcelWorkBook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            //ExcelWorkBook.Worksheets.Add(); //Adding New Sheet in Excel Workbook

            try
            {
                ExcelWorkSheet = ExcelWorkBook.Worksheets[1]; //Compulsory Line in which sheet you want to write data

                ExcelWorkBook.Worksheets[1].Name = "MySheet"; //Renaming the Sheet1 to MySheet

                ws.Range["A1:A283"].Copy();
                ExcelWorkSheet.Paste();

                ws.Range["W1:W283"].Copy();
                ExcelWorkSheet.Range["B1:B283"].Select();
                ExcelWorkSheet.Paste();

                ExcelWorkBook.SaveAs("E:\\Traders Limit.xlsx");

                //ExcelWorkBook.Close();

                ExcelApp.Quit();
                excel.Quit();
                Marshal.ReleaseComObject(ExcelWorkSheet);

                Marshal.ReleaseComObject(ExcelWorkBook);

                Marshal.ReleaseComObject(ExcelApp);
            }
            catch (Exception exHandle)
            {
                Console.WriteLine("Exception: " + exHandle);
                Console.ReadLine();
            }
            finally
            {
                foreach (Process process in Process.GetProcessesByName("Excel"))
                    process.Kill();
            }
        }
    }
}