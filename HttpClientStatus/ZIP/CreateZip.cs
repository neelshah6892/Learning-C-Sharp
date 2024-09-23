using System.IO.Compression;
  
string dirName = "data";
string zipName = "archive.zip";

if(File.Exists(zipName))
   {
   File.Delete(zipName);
   }

ZipFile.CreateFromDirectory(dirName, zipName);
