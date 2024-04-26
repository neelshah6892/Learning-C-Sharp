using System.IO.Compression;

var zipFilePath = "C:\\Users\\dhwan\\Downloads\\24042024.zip";

// Open the zip file for reading
using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Read))
{
    foreach (ZipArchiveEntry entry in zip.Entries)
    {
        // Access individual entry properties
        Console.WriteLine($"File Name: {entry.Name}");
        // You can read the entry's content here if needed
    }
       
}

string extractPath = "C:\\Users\\dhwan\\Downloads";

using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Read))
    foreach (ZipArchiveEntry entry in zip.Entries)
        if (entry.Name == "GFDLNFO_BACKADJUSTED_24042024.csv")
            entry.ExtractToFile("GFDLNFO_BACKADJUSTED_24042024.csv");

//ZipFile.ExtractToDirectory(zipFilePath, extractPath);
