/*using System.IO.Compression;

string zipName = "archive.zip";

if (File.Exists(zipName))
{
    File.Delete(zipName);
}

var files = Directory.GetFiles(@"data", "*.jpg");

using var archive = ZipFile.Open(zipName, ZipArchiveMode.Create);

foreach (var file in files)
{
    archive.CreateEntryFromFile(file, Path.GetFileName(file));
}*/