using System.Diagnostics;
using System.Runtime.InteropServices;
using LibVLCSharp;
using LibVLCSharp.Shared;

Core.Initialize();

using var libvlc = new LibVLC();
//using var media = new Media(libvlc, new Uri(@"D:\Video\Be Fit.mp4"));
using var media = new Media(libvlc, new Uri(@"C:\Users\Administrator\Desktop\BeFit.mp4"));
using var mediaplayer = new MediaPlayer(media);

mediaplayer.Fullscreen = true;
mediaplayer.Play();
Thread.Sleep(120000);
mediaplayer.Stop();


/*[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

Process vlc = new Process();

vlc.StartInfo.FileName = @"C:\Program Files\VideoLAN\VLC\vlc.exe";

//vlc.Start(@"C:\\Users\\Administrator\\Downloads\\Interactive Notebook.mp4");
vlc.Start();

//Thread.Sleep(70000);
Thread.Sleep(10000);

vlc.Kill();*/