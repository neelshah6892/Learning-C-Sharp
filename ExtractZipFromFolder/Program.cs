using System;
using System.IO.Compression;

namespace unzip_file
{
    class Program
    {
        static void Main(string[] args)
        {
            string zipFilePath = @"C:\Users\dhwan\Downloads\BhavCopy_NSE_FO_0_0_0_20240809_F_0000.csv.zip";
            string extractionPath = @"D:\bsefiles\";
            ZipFile.ExtractToDirectory(zipFilePath, extractionPath);
            Console.WriteLine("Extracted Successfully");

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