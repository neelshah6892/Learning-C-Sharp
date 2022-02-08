using System.Diagnostics;
using System.IO;

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
            File.Copy("E:\\Github\\Learning-C-Sharp\\PythonScriptCall\\bin\\Debug\\net6.0\\02022022.xlsx", "C:\\Users\\Administrator\\Desktop\\02022022.xlsx");
            //File.Delete("E:\\Github\\Learning-C-Sharp\\PythonScriptCall\\bin\\Debug\\net6.0\\02022022.xlsx");

        }

        static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}