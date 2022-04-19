using Microsoft.Office.Interop.Excel;

Application excel = new Application();
Workbook wb = excel.Workbooks.Open("C:\\Users\\Administrator\\Desktop\\GFDLNFO_BACKADJUSTED_18042022.csv");
Worksheet ws = wb.Worksheets["GFDLNFO_BACKADJUSTED_18042022"];
excel.Visible = true;
excel.DisplayAlerts = false;

Thread.Sleep(10000);

excel.Quit();