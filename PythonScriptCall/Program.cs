using System.Diagnostics;
using System.IO;
using System.Net;
using System.IO.Compression;

namespace PythonScriptCall
{
    class Program
    {
        static void Main(string[] args)
        {
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
            //Console.Read();
            /*if (!File.Exists("C:\\Users\\Administrator\\Desktop\\09022022.xlsx"))
            {
                File.Copy("E:\\Github\\Learning-C-Sharp\\PythonScriptCall\\bin\\Debug\\net6.0\\09022022.xlsx", "C:\\Users\\Administrator\\Desktop\\09022022.xlsx");
            }
            File.Delete("E:\\Github\\Learning-C-Sharp\\PythonScriptCall\\bin\\Debug\\net6.0\\09022022.xlsx");*/

            
            WebClient client = new WebClient();
            client.DownloadFile("https://archives.nseindia.com/content/historical/DERIVATIVES/2022/FEB/fo08FEB2022bhav.csv.zip", "C:\\Users\\Administrator\\Desktop\\demo.zip");

            ZipFile.ExtractToDirectory("C:\\Users\\Administrator\\Desktop\\demo.zip", @"C:\\Users\\Administrator\\Desktop\\");


            var cmd2 = "G:\\Yash_1\\iv_daily\\2.py";
            var process2 = new Process
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
            process2.ErrorDataReceived += Process_OutputDataReceived;
            process2.OutputDataReceived += Process_OutputDataReceived;

            process2.Start();
            process2.BeginErrorReadLine();
            process2.BeginOutputReadLine();
            process2.WaitForExit();
        }

        static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}