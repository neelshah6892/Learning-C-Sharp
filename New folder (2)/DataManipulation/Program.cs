using Microsoft.Data.Analysis;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Form;


Console.WriteLine("Hello, World!");
//var df1 = DataFrame.LoadCsv("C:\\Users\\Administrator\\Desktop\\TEST.xlsb");
//Console.WriteLine(df1);

//Process.Start("notepad");

[DllImport("User32.dll")]
static extern int SetForegroundWindow(IntPtr point);

var p = Process.GetProcessesByName("CTCLClient")[0];
var pointer = p.Handle;

SetForegroundWindow(pointer);
SendKeys.Send("{F10}");
//System.Windows.Forms.SendKeys.Send("F10"); ;
