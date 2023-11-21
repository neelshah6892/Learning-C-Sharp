using System.IO.Compression;

string dirName = @"C:\Users\dhwan\Dowmloads\20112023";
string zipName = @"C:\Users\dhwan\Downloads\20112023.zip";

ZipFile.ExtractToDirectory(zipName, dirName, true);