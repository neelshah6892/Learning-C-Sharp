using Microsoft.Office.Interop.Excel;

namespace VarAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            Application excel = new Application();
            Workbook workbook = excel.Workbooks.Open("C:\\Users\\Administrator\\Desktop\\RMS Reports\\VAR FILE_14.02.2022.xlsx");
            excel.Visible = true;
            //excel.DisplayAlerts = false;
            Worksheet ws = workbook.Worksheets["03.02.2022"];

            /*Application excelApp = new Application();
            Workbook ExcelWorkBook = null;
            Worksheet ExcelWorkSheet = null;
            excelApp.Visible = true;
            ExcelWorkSheet = ExcelWorkBook.Worksheets[1];*/



            for (int i = 2; i < 281; i++)
            {
                if(ws.Cells[i, 15].Value > 0 && ws.Cells[i, 16].Value > 0)
                {
                    ws.Cells[i, 26] = 0;
                    ws.Cells[i, 27] = 0;
                    ws.Cells[i, 28] = ws.Cells[i, 5].Value;

                }
                else if (ws.Cells[i, 16].Value > 0 && ws.Cells[i, 15].Value < 0)
                {
                    ws.Cells[i, 26] = ((Math.Abs(ws.Cells[i, 15].Value) * 100) / ws.Cells[i, 23].Value);
                    ws.Cells[i, 27] = 0;
                    ws.Cells[i, 28] = ws.Cells[i, 5].Value;
                }
                else if(ws.Cells[i, 15].Value > 0 && ws.Cells[i, 16].Value < 0)
                {
                    ws.Cells[i, 26] = 0;
                    ws.Cells[i, 27] = ((Math.Abs(ws.Cells[i, 16].Value) * 100) / ws.Cells[i, 23].Value);
                    ws.Cells[i, 28] = ws.Cells[i, 5].Value;
                }
                else
                {
                    ws.Cells[i, 26] = ((Math.Abs(ws.Cells[i, 15].Value) * 100) / ws.Cells[i, 23].Value);
                    ws.Cells[i, 27] = ((Math.Abs(ws.Cells[i, 16].Value) * 100) / ws.Cells[i, 23].Value);
                    ws.Cells[i, 28] = ws.Cells[i, 5].Value;
                    //ExcelWorkSheet.Cells[i, 2] = ws.Cells[i, 26];
                    //ExcelWorkSheet .Cells[i, 3]= ws.Cells[i, 27];
                }
            }

            for (int i = 2; i < 281; i++)
            {
                if (ws.Cells[i, 28].Value < 0)
                {
                    if(ws.Cells[i, 26].Value > ws.Cells[i, 27].Value)
                    {
                        ws.Cells[i, 30] = ws.Cells[i, 26].Value;
                        //ws.Cells[i, 32] = ((ws.Cells[i, 30].Value * 100) / ws.Cells[i, 23].Value);
                    }
                    else if(ws.Cells[i, 27].Value > ws.Cells[i, 26].Value)
                    {
                        ws.Cells[i, 30] = ws.Cells[i, 27].Value;
                        //ws.Cells[i, 32] = ((ws.Cells[i, 30].Value * 100) / ws.Cells[i, 23].Value);
                    }
                }
                else
                {
                    ws.Cells[i, 30] = (ws.Cells[i, 28].Value * 5) / 100000;
                    /*ws.Cells[i, 31] = (ws.Cells[i, 28].Value * 5);
                    ws.Cells[i, 32] = ((ws.Cells[i, 30].Value * 100) / ws.Cells[i, 23].Value);*/
                }
            }
                


            /*for (int i = 2; i<281; i++)
            {
                if ((ws.Cells[i,28].Value > ws.Cells[i,27].Value) && (ws.Cells[i, 28].Value > ws.Cells[i, 26].Value))
                {
                    ws.Cells[i, 30] = (ws.Cells[i, 28].Value * 5)/100000;
                }
                else if((ws.Cells[i,27].Value > ws.Cells[i, 28].Value) && (ws.Cells[i, 27].Value > ws.Cells[i, 26].Value))
                {
                    ws.Cells[i, 30] = ws.Cells[i, 27].Value;
                }
                else if ((ws.Cells[i, 26].Value > ws.Cells[i, 28].Value) && (ws.Cells[i, 26].Value > ws.Cells[i, 27].Value))
                {
                    ws.Cells[i, 30] = ws.Cells[i, 26].Value;
                }
            }*/



            //workbook.Save();
            excel.Quit();
            //excelApp.Quit();
        }
    }
}