using Microsoft.Data.Analysis;

var fname = Path.GetFullPath("products.csv");
var df = DataFrame.LoadCsv(fname);

Console.WriteLine(df.Info());
Console.WriteLine(df.Description());