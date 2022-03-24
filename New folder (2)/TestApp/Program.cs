using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string source = @"C:\Users\Administrator\Desktop\Python\TradeFO_20202207.txt";
        string readText = File.ReadAllText(source);
        //Console.WriteLine(readText);
        string destination = @"C:\Users\Administrator\Desktop\Python\TradeFO.csv";
        File.WriteAllText(destination, readText);
        Console.WriteLine(File.ReadAllText(destination));



        Process ctcl = new Process();
        ctcl.StartInfo.FileName = @"CTCL.exe";
        ctcl.Start();

        var prc = Process.GetProcessesByName("notepad");
        if (prc.Length > 0)
        {
            SetForegroundWindow(prc[0].MainWindowHandle);
            //SendKeys.SendWait("{F5}");
        }
        else
        {
            Console.WriteLine("Window Not Open");
        }
    }
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hand);
    [DllImport("user32.dll")]
    private static extern bool SwitchToThisWindow(IntPtr handle, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern bool IsIconic(IntPtr handle);
    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr handle);
}