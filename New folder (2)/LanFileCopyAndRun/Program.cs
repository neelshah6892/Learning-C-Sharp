using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace LanFileCopyAndRun
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> values = new List<string> {"172.16.3.21", "172.16.3.22"};

            for (int i = 0; i < values.Count; i++)
            {
                NetworkCredential theNetworkCredential = new NetworkCredential("Administrator", "9", values[i]);
                CredentialCache theNetcache = new CredentialCache();
                string source = "C:\\Users\\Administrator\\Desktop\\CTCLClientAutomation.exe";
                //string destination = "\\\\192.168.1.90\\C$\\Users\\Administrator\\Desktop\\CTCLClientAutomation.exe";
                string destination = "\\\\172.16.3.21\\C$\\Users\\Administrator\\Desktop\\CTCLClientAutomation.exe";
                if (File.Exists(destination))
                {
                    Process.Start(destination);
                }
                else
                {
                    File.Copy(source, destination);
                    Thread.Sleep(5000);
                    Process.Start(destination);
                }
            }

            /*NetworkCredential theNetworkCredential = new NetworkCredential("Administrator", "9", "172.16.3.21");
            CredentialCache theNetcache = new CredentialCache();
            string source = "C:\\Users\\Administrator\\Desktop\\CTCLClientAutomation.exe";
            //string destination = "\\\\192.168.1.90\\C$\\Users\\Administrator\\Desktop\\CTCLClientAutomation.exe";
            string destination = "\\\\172.16.3.21\\C$\\Users\\Administrator\\Desktop\\CTCLClientAutomation.exe";
            if (File.Exists(destination))
            {
                Process.Start(destination);
            }
            else
            {
                File.Copy(source, destination);
                Thread.Sleep(5000);
                Process.Start(destination);
            }*/
        }
    }
}
