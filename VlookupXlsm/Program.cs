//using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;

Console.WriteLine("Performing Vlookup....");

Application excelApp = new Application();
Workbook workbook = excelApp.Workbooks.Open("D:\\bsefiles\\Demo.xlsx");
excelApp.Visible = true;
excelApp.DisplayAlerts = false;
Worksheet ws = workbook.Worksheets["Sheet1"];
Thread.Sleep(10000);
for (int i = 2; i < 204; i++)
{
    //ws.Cells[i, 3].Formula = String.Format("=VLOOKUP(A" + i + ", 'D:\\Github\\Parth\\[IV PRINT.xlsx]Sheet1'!$A$2:$H$203, 8,0)");
    //ws.Cells[i, 8].Formula = String.Format("=VLOOKUP(A"+ i + ", 'D:\\Github\\Parth\\[New Daily Movement.xlsm]Sheet2'!$A$4:$H$203, 10,0)");
    excelApp.WorksheetFunction.VLookup("A" + i, "D:\\Github\\Parth\\[New Daily Movement.xlsm]Sheet2'!$A$4:$H$203", 8, false);
}

//Console.WriteLine("Performing Paste Special Value");
//ws.Range["C2:C203"].Copy();
//ws.Range["C2:C203"].PasteSpecial(XlPasteType.xlPasteValues, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);

/*using ClosedXML.Excel;

class Program
{
    static void Main(string[] args)
    {
        // Load the .xlsm file
        using (var workbook = new XLWorkbook("D:\\Github\\Parth\\New Daily Movement.xlsm"))
        {
            var worksheet = workbook.Worksheet("Sheet2");
            var lookupValue = "NIFTY";
            var table = worksheet.Range("$A$4:$U$203"); // Adjust the range as needed

            // Perform VLOOKUP
            var result = worksheet.Evaluate($"VLOOKUP(\"{lookupValue}\", {table.RangeAddress.ToString()}, 9, FALSE)");

            // Output the result
            Console.WriteLine(result);
        }
    }
}*/

/*using Excel = Microsoft.Office.Interop.Excel;

class Program
{
    static void Main(string[] args)
    {
        // Create an instance of Excel application
        Excel.Application excelApp = new Excel.Application();

        // Open the source workbook
        Excel.Workbook sourceWorkbook = excelApp.Workbooks.Open(@"D:\\Github\\Parth\\New Daily Movement.xlsm");
        Excel.Worksheet sourceWorksheet = sourceWorkbook.Sheets[2];

        // Open the target workbook
        Excel.Workbook targetWorkbook = excelApp.Workbooks.Open(@"D:\\bsefiles\\Demo.xlsx");
        Excel.Worksheet targetWorksheet = targetWorkbook.Sheets[1];

        for (int i = 2; i < 204; i++)
        {
            // Perform VLOOKUP
            Excel.Range lookupRange = sourceWorksheet.Range["$A$4:$U$203"]; // Adjust the range as needed
            object lookupValue = "A" + i; // The value you are looking for
            int colIndex = 9; // Column index to return the value from
            bool exactMatch = false; // Set to true for exact match

            try
            {
                object result = excelApp.WorksheetFunction.VLookup(lookupValue, lookupRange, colIndex, exactMatch);
                Console.WriteLine("VLOOKUP Result: " + result.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            // Close the workbooks
            sourceWorkbook.Close(false);
            targetWorkbook.Close(false);

            // Quit the Excel application
            excelApp.Quit();
        }
    }
}*/

