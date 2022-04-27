using Microsoft.Office.Interop.Excel;
using System.Xml.Linq;

Application excel = new Application();
/*Workbook wb = excel.Workbooks.Open("C:\\Users\\Administrator\\Desktop\\GFDLNFO_BACKADJUSTED_18042022.csv");
Worksheet ws = wb.Worksheets["GFDLNFO_BACKADJUSTED_18042022"];
excel.Visible = true;
excel.DisplayAlerts = false;

Thread.Sleep(10000);*/

LoadOptions loadOptions = new LoadOptions();
//loadOptions.Password = password;

Workbook workbook = excel.Workbooks.Open("C:\\Users\\Administrator\\Desktop\\IV file.xlsx", loadOptions);
workbook.Unprotect();
// or workbook.Settings.Password = "";
workbook.Save(filePath);

excel.Quit();