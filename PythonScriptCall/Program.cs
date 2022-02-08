using System.Diagnostics;

/*string strCmdText;
string file;
file = "G:\\Yash_1\\iv_daily\\1.py";
strCmdText = "python3" + file;
//Process.Start("CMD.exe", strCmdText);

void run_cmd(string cmd, string args)
{
    ProcessStartInfo start = new ProcessStartInfo();
    start.FileName = "C:\\Users\\Administrator\\AppData\\Local\\Programs\\Python\\Python310\\python.exe";
    start.Arguments = string.Format("{0} {1}", cmd, args);
    start.UseShellExecute = false;
    start.RedirectStandardOutput = true;
    using (Process process = Process.Start(start))
    {
        using (StreamReader reader = process.StandardOutput)
        {
            string result = reader.ReadToEnd();
            Console.Write(result);
        }
    }
}*/
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
                    FileName = "C:\\ProgramData\\Anaconda3\\python.exe",
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
            Console.Read();
        }

        static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}