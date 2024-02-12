using System;
using System.IO.Compression;

class Program
{
    static void Main(string[] args)
    {
        
        //string startPath = @"C:\Users\dhwan\Downloads\";
        string zipPath = @"C:\Users\dhwan\Downloads\09022024.zip";
        string extractPath = @"C:\Users\dhwan\Downloads\";

        Console.WriteLine(Directory.GetParent(extractPath));

        //ZipFile.CreateFromDirectory(startPath, zipPath);

        ZipFile.ExtractToDirectory(zipPath, extractPath);
    }
}