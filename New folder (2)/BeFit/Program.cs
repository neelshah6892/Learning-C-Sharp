using System;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LibVLCSharp.Shared;

namespace BeFit
{
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern bool SwitchToThisWindow(IntPtr handle);

        static void Main(string[] args)
        {
            //Core.Initialize();

            Core.Initialize();

            using (var libvlc = new LibVLC())
            {
                var media = new Media(libvlc, new Uri(@"D:\Video\Be Fit.mp4"));
                //var media = new Media(libvlc, new Uri(@"C:\Users\Administrator\Desktop\BeFit.mp4"));
                var mediaplayer = new MediaPlayer(media);
                mediaplayer.Fullscreen = true;
                mediaplayer.Play();
                /*ProcessStartInfo mininfo = new ProcessStartInfo("F7.exe");
                mininfo.WindowStyle = ProcessWindowStyle.Minimized;
                Process.Start(mininfo);*/
                
                Thread.Sleep(120000);
                mediaplayer.Stop();
                //mininfo.WindowStyle = ProcessWindowStyle.Maximized;
            }
            

            /*ProcessStartInfo startInfo  = new ProcessStartInfo("C:\\Program Files\\VideoLAN\\VLC\\vlc.exe", @"C:\\Users\\Administrator\\Desktop\\BeFit.mp4");

            //ProcessStartInfo mininfo = new ProcessStartInfo("D:\\Falcon7\\F7.exe");

            ProcessStartInfo mininfo = new ProcessStartInfo("D:\\Falcon7\\F7.exe");
            mininfo.WindowStyle = ProcessWindowStyle.Minimized;

            //var prctwo = Process.GetProcessesByName("F7.exe");
            //SwitchToThisWindow(prctwo[0].MainWindowHandle);
            

            //ProcessStartInfo startInfo = new ProcessStartInfo("C:\\Program Files\\VideoLAN\\VLC\\vlc.exe", @"D:\\Video\\BeFit.mp4");
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            //startInfo.CreateNoWindow = true;
            //startInfo.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(startInfo);
            Thread.Sleep(100);
            var prc = Process.GetProcessesByName("vlc");
            SwitchToThisWindow(prc[0].MainWindowHandle);
            //SendKeys.SendWait("{F}");
            Thread.Sleep(110000);
            mininfo.WindowStyle = ProcessWindowStyle.Maximized;
            prc[0].Kill();*/
        }
    }
}
