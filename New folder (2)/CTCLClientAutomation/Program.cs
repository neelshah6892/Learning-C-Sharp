using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace CTCLClientAutomation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var date1 = DateTime.Now.ToString("ddMMyyyy");
            var date2 = DateTime.Now.ToString("MMdd");
            var date3 = DateTime.Now.ToString("ddMMM");

            //var prc = Process.GetProcessesByName("notepad");
            var prc = Process.GetProcessesByName("CTCLClient");
            if (prc.Length > 0)
            {
                //SetForegroundWindow(prc[0].MainWindowHandle);
                SwitchToThisWindow(prc[0].MainWindowHandle);

                Thread.Sleep(3000);
                SendKeys.SendWait("{F10}");
                Thread.Sleep(100);
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(100);

                SendKeys.SendWait("%{F6}");
                Thread.Sleep(100);
                //Context Menu
                //SendKeys.Send("+({F10})");
                RightMouseClick(250, 250);
                Thread.Sleep(1000);
                //SendKeys.SendWait("{DOWN 4}");
                SendKeys.SendWait("{DOWN}");
                Thread.Sleep(100);
                SendKeys.SendWait("{DOWN}");
                Thread.Sleep(100);
                SendKeys.SendWait("{DOWN}");
                Thread.Sleep(100);
                SendKeys.SendWait("{DOWN}");
                Thread.Sleep(100);
                SendKeys.SendWait("{RIGHT}");
                Thread.Sleep(100);
                SendKeys.SendWait("{RIGHT}");
                Thread.Sleep(100);
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(100);
                SendKeys.SendWait("{ESC}");
                Thread.Sleep(100);


                
                SendKeys.SendWait("{F3}");
                Thread.Sleep(100);
                
                /*SendKeys.SendWait("{TAB} 12");
                Thread.Sleep(100);*/
                SendKeys.SendWait("{F3}");
                Thread.Sleep(100);
                //Context Menu
                //SendKeys.Send("+({F10})");
                RightMouseClick(250, 250);
                Thread.Sleep(1000);
                SendKeys.SendWait("{DOWN}");
                Thread.Sleep(100);
                SendKeys.SendWait("{DOWN}");
                Thread.Sleep(100);
                SendKeys.SendWait("{DOWN}");
                Thread.Sleep(100);
                SendKeys.SendWait("{DOWN}");
                Thread.Sleep(100);
                SendKeys.SendWait("{DOWN}");
                Thread.Sleep(100);
                SendKeys.SendWait("{DOWN}");
                Thread.Sleep(100);
                SendKeys.SendWait("{RIGHT}");
                Thread.Sleep(100);
                SendKeys.SendWait("{RIGHT}");
                Thread.Sleep(100);
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(100);
                SendKeys.SendWait("{ESC}");
                Thread.Sleep(100);


                /*string dailyfolder = @"C:\Users\Administrator\Desktop\Daily";
                string todayfolder = @"C:\Users\Administrator\Desktop\Daily\" + date1;

                if (!Directory.Exists(dailyfolder))
                {
                    Directory.CreateDirectory(dailyfolder);
                    if (!Directory.Exists(todayfolder))
                    {
                        Directory.CreateDirectory(todayfolder);
                    }
                }*/

                //ADMIN_MMDDMESSAGELIST.CSV
                //File.Copy(@"d:\MESSAGE LOG\ADMIN_" + date2 + "MESSAGELIST.csv", @"C:\Users\Administrator\Desktop\Daily\ADMIN_" + date2 + "MESSAGELIST.csv");
                File.Copy(@"d:\MESSAGE LOG\ADMIN_" + date2 + "MESSAGELIST.csv", @"C:\Users\Administrator\Desktop\GETS_FILES\GETS_FILES\GETS_EXCEL\" + date3 + @"\MESSAGELIST.csv");
                //GETS_FILE -> GETS_EXCEL
                //File.Copy(@"C:\Users\Administrator\Desktop\GETS_FILES\GETS_FILES\GETS_EXCEL\" + date3 + @"\PendingOrders", @"C:\Users\Administrator\Desktop\Daily\PendingOrders");

                //FOLDER COPY PASTE (deSKTOP->GET_FILES GET_FILES -> GETS_EXCEL->DDMMM)
                //File.Copy(@"C:\Users\Administrator\Desktop\GETS_FILES\GETS_FILES\GETS_EXCEL\" + date3 + @"\NetPosition", @"C:\Users\Administrator\Desktop\Daily\NetPosition");
                Console.WriteLine("All Tasks Completed Succesfully");

            }
            else
            {
                Console.WriteLine("Process is not running");
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SwitchToThisWindow(IntPtr handle);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        public const int MOUSEEVENTF_RIGHTUP = 0x0010; /* right button up */

        public static void RightMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, xpos, ypos, 0, 0);
        }
    }
}
