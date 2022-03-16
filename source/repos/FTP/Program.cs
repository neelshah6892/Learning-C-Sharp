using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Linq;

namespace FTP
{
    class Program
    {
        static void Main(string[] args)
        {

            string sourceDirectory = @"//172.16.2.49/Scriptmaster/Vaibhav/08102021/GETSClient";
            string targetDirectory = @"d:\GETSClient";
            DirectoryInfo sourceDircetory = new DirectoryInfo(sourceDirectory);
            DirectoryInfo targetDircetory = new DirectoryInfo(targetDirectory);
            CopyAll(sourceDircetory, targetDircetory);
            Console.WriteLine("All File copied, press enter to close this window");
            Console.ReadLine();


        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }

}