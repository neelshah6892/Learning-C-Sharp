using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibVLCSharp;
using System.Threading;
using LibVLCSharp.Shared;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BeFit
{
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern bool SwitchToThisWindow(IntPtr handle);

        static void Main(string[] args)
        {
            Core.Initialize();

            using (var libvlc = new LibVLC())
            {
                //var media = new Media(libvlc, new Uri(@"C:\Users\Administrator\Desktop\Be Fit.mp4"));
                var media = new Media(libvlc, new Uri(@"D:\Video\Be Fit.mp4"));
                var mediaplayer = new MediaPlayer(media);
                mediaplayer.Fullscreen = true;
                mediaplayer.Play();
                Thread.Sleep(110000);
                mediaplayer.Stop();
                //Thread.Sleep(120000);
                var prc = Process.GetProcessesByName("vlc");
                if (prc.Length > 0)
                {
                    SwitchToThisWindow(prc[0].MainWindowHandle);
                    
                    //mediaplayer.EndReached += (s, e) => Environment.Exit(0);
                    
                }

            }
            
        }
    }
}
