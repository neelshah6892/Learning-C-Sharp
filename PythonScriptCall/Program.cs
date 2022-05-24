using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;


namespace PythonScriptCall
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            //Change over here and 2.py file too, need to do once per month
            DateTime expiry = new DateTime(2022, 06, 30);

            //Wednesday, Thursday, Friday
            DateTime yesterday = DateTime.Now.AddDays(-1);
            DateTime daybeforeyesterday = DateTime.Now.AddDays(-2);

            //Monday or 2 day holiday
            //DateTime yesterday = DateTime.Now.AddDays(-3);
            //DateTime daybeforeyesterday = DateTime.Now.AddDays(-4);

            //Tuesday
            //DateTime yesterday = DateTime.Now.AddDays(-1);
            //DateTime daybeforeyesterday = DateTime.Now.AddDays(-4);

            //1 day holiday
            //DateTime yesterday = DateTime.Now.AddDays(-2);
            //DateTime daybeforeyesterday = DateTime.Now.AddDays(-3);

            //yesterday = yesterday.AddDays(-2);
            //daybeforeyesterday = daybeforeyesterday.AddDays(-3);

            string text = File.ReadAllText(@"G:\\Yash_1\\iv_daily\\2.py");
            text = text.Replace(daybeforeyesterday.ToString("ddMMyyyy"), yesterday.ToString("ddMMyyyy"));
            File.WriteAllText(@"G:\\Yash_1\\iv_daily\\2.py", text);

            if (text.Contains(daybeforeyesterday.ToString("ddMMMyyyy").ToUpper()))
            {
                //Console.WriteLine(daybeforeyesterday.ToString("ddMMMyyyy").ToUpper());
                text = text.Replace(daybeforeyesterday.ToString("ddMMMyyyy").ToUpper(), yesterday.ToString("ddMMMyyyy").ToUpper());
                File.WriteAllText(@"G:\\Yash_1\\iv_daily\\2.py", text);
                Console.WriteLine(text);

            }
            else
            {
                //Console.WriteLine("False");
                Console.WriteLine(daybeforeyesterday.ToString("ddMMMyyyy").ToUpper());
                Console.WriteLine(yesterday.ToString("ddMMMyyyy").ToUpper());
            }


            WebClient client = new WebClient();
            Console.WriteLine("https://archives.nseindia.com/content/historical/DERIVATIVES/" + yesterday.ToString("yyyy") + "/" + yesterday.ToString("MMM").ToUpper() + "/fo" + yesterday.ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip", "C:\\Users\\Administrator\\Desktop\\demo.zip");
            client.DownloadFile("https://archives.nseindia.com/content/historical/DERIVATIVES/" + yesterday.ToString("yyyy") + "/" + yesterday.ToString("MMM").ToUpper() + "/fo" + yesterday.ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip", "C:\\Users\\Administrator\\Desktop\\demo.zip");

            ZipFile.ExtractToDirectory("C:\\Users\\Administrator\\Desktop\\demo.zip", @"C:\\Users\\Administrator\\Desktop\\");
            File.Delete("C:\\Users\\Administrator\\Desktop\\demo.zip");

            Console.WriteLine("Started Process 1....");
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
            Console.WriteLine("Ended Process 1....");

            if (!File.Exists("C:\\Users\\Administrator\\Desktop\\" + yesterday.ToString("ddMMyyyy") + ".xlsx"))
            {
                File.Copy("E:\\Github\\Learning-C-Sharp\\PythonScriptCall\\bin\\Debug\\net6.0\\" + yesterday.ToString("ddMMyyyy") + ".xlsx", "C:\\Users\\Administrator\\Desktop\\" + yesterday.ToString("ddMMyyyy") + ".xlsx");
            }
            File.Delete("E:\\Github\\Learning-C-Sharp\\PythonScriptCall\\bin\\Debug\\net6.0\\" + yesterday.ToString("ddMMyyyy") + ".xlsx");

            Thread.Sleep(5000);

            Console.WriteLine("Started Process 2....");
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
            Console.WriteLine("Ended Process 2....");

            File.Delete("C:\\Users\\Administrator\\Desktop\\newfile1.xlsx");
            File.Delete("C:\\Users\\Administrator\\Desktop\\fo" + yesterday.ToString("ddMMMMyyyy") + "bhav.csv");

            Thread.Sleep(2000);
            

            Console.WriteLine("Performing Vlookup....");

            Application excelApp = new Application();
            Workbook workbook = excelApp.Workbooks.Open("C:\\Users\\Administrator\\Desktop\\Vol 2021.xlsb");
            excelApp.Visible = true;
            excelApp.DisplayAlerts = false;
            Worksheet ws = workbook.Worksheets["REPORT"];
            Thread.Sleep(10000);
            for (int i = 2; i < 204; i++)
            {
                ws.Cells[i, 3].Formula = String.Format("=VLOOKUP(A"+ i + ", 'C:\\Users\\Administrator\\Desktop\\[IV PRINT.xlsx]Sheet1'!$A$2:$H$203, 8,0)");
            }

            Console.WriteLine("Performing Paste Special Value");
            ws.Range["C2:C203"].Copy();
            ws.Range["C2:C203"].PasteSpecial(XlPasteType.xlPasteValues, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);


            Console.WriteLine("Change of date and value");
            ws.Cells[2, 22] = expiry;
            Thread.Sleep(1000);
            ws.Cells[1, 22] = yesterday.Date;
            Thread.Sleep(1000);
            ws.Cells[1, 13] = ws.Cells[1, 23];
            Thread.Sleep(1000);
            ws.Cells[1, 14] = ws.Cells[1, 15].Value;
            Thread.Sleep(1000);
            workbook.Save();
            excelApp.Quit();


            Console.WriteLine("Macro Process");
            Application excel = new Application();
            Workbook wbook = excel.Workbooks.Open("C:\\Users\\Administrator\\Desktop\\Vol 2021.xlsb");
            excel.Visible = true;
            excel.DisplayAlerts = false;
            Worksheet ws2 = wbook.Worksheets["REPORT"];
            Thread.Sleep(5000);
            excel.Run("COPYDATA");
            
            wbook.Save();
            excel.Quit();

            File.Copy("C:\\Users\\Administrator\\Desktop\\" + yesterday.ToString("ddMMyyyy") + ".xlsx", "C:\\Users\\Administrator\\Desktop\\One Minute Data\\" + yesterday.ToString("ddMMyyyy") + ".xlsx");
            File.Delete("C:\\Users\\Administrator\\Desktop\\" + yesterday.ToString("ddMMyyyy") + ".xlsx");
            Console.WriteLine("Completed");
        }

        static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        }
}