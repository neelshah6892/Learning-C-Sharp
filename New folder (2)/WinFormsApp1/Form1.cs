using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            [DllImport("user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
            var date = DateTime.Now.ToString("ddMMyyyy");
            var dat = DateTime.Now.ToString("MMdd");
            Process notepad = new Process();
            //notepad.StartInfo.FileName = @"C:\Windows\Notepad.exe";
            //notepad.Start();

            // Need to wait for notepad to start
            //notepad.WaitForInputIdle();

            //IntPtr p = notepad.MainWindowHandle;
            //ShowWindow(p, 1);
            //SendKeys.SendWait("{F5}");
            //SendKeys.SendWait("%{F6}");
            //SendKeys.SendWait("^V");


            //Process notepad = new Process();
            notepad.StartInfo.FileName = @"D:\GETSClient\CTCLClient.exe";
            notepad.Start();

            // Need to wait for notepad to start
            notepad.WaitForInputIdle();

            IntPtr p = notepad.MainWindowHandle;
            ShowWindow(p, 1);
            SendKeys.SendWait("{F10}");
            Thread.Sleep(1000);
            SendKeys.SendWait("Enter");
            SendKeys.SendWait("%{F6}");
            Thread.Sleep(1000);
            SendKeys.SendWait("Enter");
            SendKeys.SendWait("%{F3}");
            Thread.Sleep(1000);
            SendKeys.SendWait("Enter");
            //ADMIN_MMDDMESSAGELIST.CSV
            File.Copy(@"d:\MESSAGE LOG\ADMIN_"+dat+"MESSAGELIST.csv", @"C:\Users\Administrator\Desktop\Daily\ADMIN_"+dat+"MESSAGELIST.csv");
            //FOLDER COPY PASTE (deSKTOP->GET_FILES GET_FILES -> GETS_EXCEL->DDMMM)
            File.Copy(@"C:\Users\Administrator\Desktop\GETS_FILES\GETS_FILES\GETS_EXCEL\"+dat+".csv", @"C:\Users\Administrator\Desktop\Daily\"+date+".csv");
            //GETS_FILE -> GETS_EXCEL
            File.Copy(@"C:\Users\Administrator\Desktop\GETS_FILES\GETS_EXCEL\"+dat+"PendingOrders.xls" + date+".csv", @"C:\Users\Administrator\Desktop\Daily\PendingOrders.xls");
            //notepad.CloseMainWindow();
            //notepad.Close();
            MessageBox.Show("Tasks Completed");
        }
    }
}