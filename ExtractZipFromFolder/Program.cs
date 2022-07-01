using System.IO.Compression;

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
}