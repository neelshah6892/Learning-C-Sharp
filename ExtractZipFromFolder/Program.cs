using System;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace unzip_file
{
    class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);

        static void Main(string[] args)
        {
            
               
            SYSTEMTIME st = new SYSTEMTIME();
            st.wYear = 2024; // must be short
            st.wMonth = 8;
            st.wDay = 20;
            st.wHour = 0;
            st.wMinute = 0;
            st.wSecond = 0;

            SetSystemTime(ref st); // invoke this method.

            Console.WriteLine(st);

            /*string zipFilePath = @"C:\Users\dhwan\Downloads\BhavCopy_NSE_FO_0_0_0_20240809_F_0000.csv.zip";
            string extractionPath = @"D:\bsefiles\";
            ZipFile.ExtractToDirectory(zipFilePath, extractionPath);
            Console.WriteLine("Extracted Successfully");*/

            /*string zipFilePath1 = @"E:\25072024.zip";
            string extractionPath1 = @"E:\";
            ZipFile.ExtractToDirectory(zipFilePath1, extractionPath1);
            Console.WriteLine("Extracted Successfully");

            string zipFilePath2 = @"E:\GFDLNFO_BACKADJUSTED_25072024.zip";
            string extractionPath2 = @"E:\";
            ZipFile.ExtractToDirectory(zipFilePath2, extractionPath2);
            Console.WriteLine("Extracted Successfully");

            File.Delete("E:\\BhavCopy_NSE_FO_0_0_0_20240725_F_0000.csv");
            File.Delete("E:\\GFDLNFO_BACKADJUSTED_25072024.zip");
            File.Delete("E:\\GFDLNFO_CONTRACT_25072024.zip");

            File.Move(@"E:\GFDLNFO_BACKADJUSTED_25072024.csv", @"G:\GFDLNFO_BACKADJUSTED_25072024.csv");
            Console.WriteLine("File Moved");*/


        }
    }
}

/*using System.IO.Compression;

DirectoryInfo d = new DirectoryInfo(@"E:\Global Datafeeds\");
FileInfo[] files = d.GetFiles();

foreach (var file in files)
{
    Console.WriteLine(file.Name);
    ZipFile.ExtractToDirectory("E:\\Global Datafeeds\\" + file.Name, @"E:\\", true);
    File.Delete(@"E:\\GFDLNFO_CONTRACT_" + file.Name);
    ZipFile.ExtractToDirectory(@"E:\\GFDLNFO_BACKADJUSTED_" + file.Name, @"E:\\", true);
    File.Delete(@"E:\\GFDLNFO_BACKADJUSTED_" + file.Name);
    //File.Copy(@"E:\\GFDLNFO_BACKADJUSTED_"+file.Name, @"E:\\Data\\"+file.Name, true);
}*/