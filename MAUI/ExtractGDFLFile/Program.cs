using System;
using System.IO.Compression;
class Program
{
    static void Main(string[] args)
    {

        DateTime today = DateTime.Now;
        Console.WriteLine(today.ToString("ddMMyyy"));
        /*string startPath = @".\start";
        string zipPath = @".\result.zip";
        string extractPath = @".\extract";

        ZipFile.CreateFromDirectory(startPath, zipPath);

        ZipFile.ExtractToDirectory(zipPath, extractPath);*/
    }
}