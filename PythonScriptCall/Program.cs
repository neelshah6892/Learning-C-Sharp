using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;

namespace PythonScriptCall
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime expiry = new DateTime(2022, 02, 24);
            DateTime yesterday = DateTime.Now.AddDays(-1);
            DateTime daybeforeyesterday = DateTime.Now.AddDays(-2);

            /*string text = File.ReadAllText(@"G:\\Yash_1\\iv_daily\\2.py");
            //text = text.Replace(daybeforeyesterday.ToString("ddMMyyyy"), yesterday.ToString("ddMMyyyy"));
            text = text.Replace("fo" + daybeforeyesterday.ToString("ddMMMMyyyy") + "bhav.csv", "fo" + yesterday.ToString("ddMMMMyyyy") + "bhav.csv");
            //Console.WriteLine(text);
            File.WriteAllText(@"G:\\Yash_1\\iv_daily\\2.py", text);*/

            var cmd = "G:\\Yash_1\\iv_daily\\1.py";
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "G:\\Yash_1\\venv\\Scripts\\python.exe",
                    Arguments = cmd,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };
            process.ErrorDataReceived += Process_OutputDataReceived;
            process.OutputDataReceived += Process_OutputDataReceived;

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();
            if (!File.Exists("C:\\Users\\Administrator\\Desktop\\" + yesterday.ToString("ddMMyyyy") + ".xlsx"))
            {
                File.Copy("E:\\Github\\Learning-C-Sharp\\PythonScriptCall\\bin\\Debug\\net6.0\\" + yesterday.ToString("ddMMyyyy") + ".xlsx", "C:\\Users\\Administrator\\Desktop\\" + yesterday.ToString("ddMMyyyy") + ".xlsx");
            }
            File.Delete("E:\\Github\\Learning-C-Sharp\\PythonScriptCall\\bin\\Debug\\net6.0\\" + yesterday.ToString("ddMMyyyy") + ".xlsx");

            
            WebClient client = new WebClient();
            client.DownloadFile("https://archives.nseindia.com/content/historical/DERIVATIVES/" + yesterday.ToString("yyyy") + "/" + yesterday.ToString("MMMM") + "/fo" + yesterday.ToString("ddMMMMyyyy") + "bhav.csv.zip", "C:\\Users\\Administrator\\Desktop\\demo.zip");

            ZipFile.ExtractToDirectory("C:\\Users\\Administrator\\Desktop\\demo.zip", @"C:\\Users\\Administrator\\Desktop\\");
            File.Delete("C:\\Users\\Administrator\\Desktop\\demo.zip");


            var cmd2 = "G:\\Yash_1\\iv_daily\\2.py";
            var process2 = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "G:\\Yash_1\\venv\\Scripts\\python.exe",
                    Arguments = cmd2,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };
            process2.ErrorDataReceived += Process_OutputDataReceived;
            process2.OutputDataReceived += Process_OutputDataReceived;

            process2.Start();
            process2.BeginErrorReadLine();
            process2.BeginOutputReadLine();
            process2.WaitForExit();

            File.Delete("C:\\Users\\Administrator\\Desktop\\newfile1.xlsx");
            File.Delete("C:\\Users\\Administrator\\Desktop\\fo" + yesterday.ToString("ddMMMMyyyy") + "bhav.csv");

            Application excelApp = new Application();
            Workbook workbook = excelApp.Workbooks.Open("C:\\Users\\Administrator\\Desktop\\Vol 2021.xlsb");
            excelApp.Visible = true;
            Worksheet ws = workbook.Worksheets["REPORT"];
            
            for (int i = 2; i < 204; i++)
            {
                string str = ws.Cells[i, 3].Formula = String.Format("=VLOOKUP(A"+ i + ", 'C:\\Users\\Administrator\\Desktop\\[IV PRINT.xlsx]Sheet1'!$A$2:$H$203, 8,0)");
            }
            ws.Range["C2:C203"].Copy();
            ws.Range["C2:C203"].PasteSpecial(XlPasteType.xlPasteValues, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);

            ws.Cells[2, 22] = expiry;
            ws.Cells[1, 22] = yesterday.Date;
            ws.Cells[1, 13] = ws.Cells[1, 23];
            ws.Cells[1, 14] = ws.Cells[1, 15];
            workbook.Save();
            excelApp.Quit();
            



            //Application excel = new Application();
            //Workbook wbook = excelApp.Workbooks.Open("C:\\Users\\Administrator\\Desktop\\Vol 2021.xlsb");
            //excelApp.Visible = true;
            //Worksheet ws2 = wbook.Worksheets["REPORT"];

            //workbook.Save();
            //excelApp.Quit();
        }

        static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
        
    }
}