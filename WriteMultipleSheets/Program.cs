using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Bytescout.Spreadsheet;

namespace WriteMultiSheets{
    internal class Program{
        static void Main(string[] args){
            Spreadsheet Document = new Spreadsheet();
            Worksheet SheetOne = Document.Workbook.Worksheets.Add("My First Sheet");
            Worksheet SheetTwo = Document.Workbook.Worksheets.Add("My Second Sheet");

            SheetOne.Cell("A1").Value = "First Sheet";
            SheetTwo.Cell("A1").Value = "Second Sheet";

            if(File.Exists(@"multiSheet.xlsx")){
                File.Delete(@"multiSheet.xlsx");
            }
            Document.SaveAs(@"multiSheet.xlsx");
            Document.Close();

            //Process.Start(@"multiSheet.xlsx");


            //Read data from Excel
            Document.LoadFromFile(@"multiSheet.xlsx");
            Worksheet worksheet = Document.Workbook.Worksheets.ByName("My First Sheet");
            Console.WriteLine(worksheet.Cell("A1").Value);
            Document.Close();
            Console.ReadKey();

            //Read data from csv
            String filepath = @"multisheet.csv";
            try{
                using (StreamReader reader = new StreamReader(filepath)){
                    string line;
                    while((line = reader.ReadLine()) != null){
                        Console.WriteLine(line);
                    }
                }
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}