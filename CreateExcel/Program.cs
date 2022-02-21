using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Diagnostics;

using System.Runtime.InteropServices;

using Microsoft.Office.Interop;

using Microsoft.Office.Interop.Excel;



namespace ConsoleApplication1

{

    class Class1

    {

        public static void Main(string[] ar)

        {

            Application ExcelApp = new Application();

            Workbook ExcelWorkBook = null;

            Worksheet ExcelWorkSheet = null;



            ExcelApp.Visible = true;

            ExcelWorkBook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            // ExcelWorkBook.Worksheets.Add(); //Adding New Sheet in Excel Workbook

            try

            {

                ExcelWorkSheet = ExcelWorkBook.Worksheets[1]; // Compulsory Line in which sheet you want to write data

                //Writing data into excel of 100 rows with 10 column 

                for (int r = 1; r < 101; r++) //r stands for ExcelRow and c for ExcelColumn

                {

                    // Excel row and column start positions for writing Row=1 and Col=1

                    for (int c = 1; c < 11; c++)

                        ExcelWorkSheet.Cells[r, c] = "R" + r + "C" + c;

                }

                ExcelWorkBook.Worksheets[1].Name = "MySheet";//Renaming the Sheet1 to MySheet

                ExcelWorkBook.SaveAs("e:\\Testing.xlsx");

                ExcelWorkBook.Close();

                ExcelApp.Quit();

                Marshal.ReleaseComObject(ExcelWorkSheet);

                Marshal.ReleaseComObject(ExcelWorkBook);

                Marshal.ReleaseComObject(ExcelApp);

            }

            catch (Exception exHandle)

            {

                Console.WriteLine("Exception: " + exHandle.Message);

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