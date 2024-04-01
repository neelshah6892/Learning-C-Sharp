using System;
using System.IO.Compression;

class Program
{
    static void Main(string[] args)
    {
        string dirName = "New";
        string zipname = "28032024.zip";

        using var archive = ZipFile.OpenRead(zipname);

        foreach (var entry in archive.Entries)
        {
            Console.WriteLine(entry.Name);
        }

        if (Directory.Exists(dirName))
        {
            Directory.Delete(dirName, true);
        }

        ZipFile.ExtractToDirectory(zipname, dirName, true);
    }
}