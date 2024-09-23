using System.IO.Compression;

string zipFile = "data.zip";

using var archive = ZipFile.OpenRead(zipFile);

foreach (var entry in archive.Entries)
{
    Console.WriteLine(entry.Name);
}