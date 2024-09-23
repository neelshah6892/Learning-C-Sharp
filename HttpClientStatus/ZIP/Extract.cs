using System.IO.Compression;

string dirName = "data2";
string zipName = "archive.zip";

if (Directory.Exists(dirName))
{
    Directory.Delete(dirName, true);
}

ZipFile.ExtractToDirectory(zipName, dirName);