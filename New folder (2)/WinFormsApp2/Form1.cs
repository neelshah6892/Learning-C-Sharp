using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //[DllImport("user32.dll")]
            //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
            var date = DateTime.Now.ToString("ddMMyyyy");
            var dat = DateTime.Now.ToString("MMdd");
            //Process notepad = new Process();
            //notepad.StartInfo.FileName = @"C:\Windows\Notepad.exe";
            //notepad.Start();

            //string path = @"C:\Windows\Notepad.exe";
            //string fileName = Path.GetFileName(path);
            // Get the precess that already running as per the exe file name.
            //Process[] processName = Process.GetProcessesByName(fileName.Substring(0, fileName.LastIndexOf('.')));
            /*if (processName.Length > 0)
            {
                //Response.Write("Process already running");
                Console.WriteLine("Process Already running");
                IntPtr p = notepad.MainWindowHandle;
                ShowWindow(p, 1);
            }
            else
            {
                notepad.StartInfo.FileName = path;
                notepad.Start();
            }*/

            var prc = Process.GetProcessesByName("CTCLClient");
            if (prc.Length > 0)
            {
                SetForegroundWindow(prc[0].MainWindowHandle);
                //SendKeys.SendWait("{F10}");
                SendKeys.SendWait("{F5}");
            }
            else
            {
                MessageBox.Show("No Process Found Open");
            }
        }
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        // Need to wait for notepad to start
        //notepad.WaitForInputIdle();

        //IntPtr p = notepad.MainWindowHandle;
        //ShowWindow(p, 1);
        
            //SendKeys.SendWait("%{F6}");
            //SendKeys.SendWait("^V");

            /*[DllImport("user32.dll")]
            static extern bool IsIconic(IntPtr hWnd);
            [DllImport("user32.dll")]
            static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
            [DllImport("user32.dll")]
            static extern bool SetForegroundWindow(IntPtr hWnd);

            static void GotoProcess(Process notepad)
            {
                if (IsIconic(notepad.MainWindowHandle))
                {
                    ShowWindow(notepad.MainWindowHandle, 1);
                }
                else
                {
                    SetForegroundWindow(notepad.MainWindowHandle);
                }
            }*/
            //Process notepad = new Process();
            //notepad.StartInfo.FileName = @"D:\GETSClient\CTCLClient.exe";
            //notepad.Start();

            // Need to wait for notepad to start
            //notepad.WaitForInputIdle();

            //IntPtr p = notepad.MainWindowHandle;
            
            //ShowWindow(p, 1);
            //Thread.Sleep(10000);
            //SendKeys.SendWait("{F10}");
            //SendKeys.SendWait("{F5}");
            //Thread.Sleep(10000);
            //SendKeys.SendWait("{Enter}");
            //SendKeys.SendWait("%{F6}");
            //SendKeys.SendWait("{DOWN}");
            //SendKeys.SendWait("{RIGHT}");
            //SendKeys.SendWait("{ENTER}");
            //Thread.Sleep(10000);
            //SendKeys.SendWait("Enter");
            //SendKeys.SendWait("%{F3}");
            //Thread.Sleep(10000);
            //SendKeys.SendWait("Enter");
            //ADMIN_MMDDMESSAGELIST.CSV
            //File.Copy(@"d:\MESSAGE LOG\ADMIN_" + dat + "MESSAGELIST.csv", @"C:\Users\Administrator\Desktop\Daily\ADMIN_" + dat + "MESSAGELIST.csv");
            //FOLDER COPY PASTE (deSKTOP->GET_FILES GET_FILES -> GETS_EXCEL->DDMMM)
            //File.Copy(@"C:\Users\Administrator\Desktop\GETS_FILES\GETS_FILES\GETS_EXCEL\" + dat + ".csv", @"C:\Users\Administrator\Desktop\Daily\" + date + ".csv");
            //GETS_FILE -> GETS_EXCEL
            //File.Copy(@"C:\Users\Administrator\Desktop\GETS_FILES\GETS_EXCEL\" + dat + "PendingOrders.xls" + date + ".csv", @"C:\Users\Administrator\Desktop\Daily\PendingOrders.xls");
            //notepad.CloseMainWindow();
            //notepad.Close();
            //MessageBox.Show("Tasks Completed");
        }
    }