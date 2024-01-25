using Microsoft.Data.Analysis;
using Spectre.Console;

var fname = Path.GetFullPath("products.csv");
var df = DataFrame.LoadCsv(fname);

Console.WriteLine(df.Info());
Console.WriteLine(df.Description());


//Explicitly specify column data types
var df1 = DataFrame.LoadCsv(fname,
    dataTypes: new Type[] { typeof(int), typeof(string), typeof(string),
        typeof(decimal), typeof(decimal) });

Console.WriteLine(df1.Info());

Console.WriteLine(df.Head(6));
Console.WriteLine(df1.Tail(6));

//Filter Data
PrimitiveDataFrameColumn<bool> fil = df["unit_price"].ElementwiseGreaterThan(100);
Console.WriteLine(df.Filter(fil));

Console.WriteLine(df.Filter(df.Columns[4].ElementwiseLessThan(10)));

//Spectre Console to show data nicely
/*var table = new Table()
    .Border(TableBorder.Ascii)
    .BorderColor(Color.SteelBlue)
    .AddColumn(new TableColumn("Id").RightAligned())
    .AddColumn(new TableColumn("Product name"))
    .AddColumn(new TableColumn("Category").LeftAligned())
    .AddColumn(new TableColumn("Unit price").RightAligned())
    .AddColumn(new TableColumn("Units in stock").RightAligned());*/

var table = new Table()
    .Border(TableBorder.Ascii)
    .BorderColor(Color.SteelBlue)
    .AddColumn(new TableColumn("Category"))
    .AddColumn(new TableColumn("Max price").RightAligned());

/*foreach( var e in df.Rows)
{
    string[] row = { $"{e[0]}", $"{e[1]}", $"{e[2]}", $"{e[3]:0.00}", $"{e[4]:0.00}" };
    table.AddRow(row);
}

AnsiConsole.Write(table);

//Sort Dataframe using orderby
foreach (var e in df.OrderBy("unit_price").Rows)
{
    string[] row = { $"{e[0]}", $"{e[1]}", $"{e[2]}", $"{e[3]:0.00}", $"{e[4]:0.00}" };
    table.AddRow(row);
}

AnsiConsole.Write(table);*/

//Grouped Data using groupby
var g = df.GroupBy("category");

/*foreach (var e in g.Head(100).Rows)
{
    string[] row = { $"{e[1]}", $"{e[2]}", $"{e[0]}", $"{e[3]:0.00}", $"{e[4]:0.00}" };
    table.AddRow(row);
}

AnsiConsole.Write(table);*/

//Min, max, sum on grouped data
var df2 = g.Max("unit_price");

foreach (var e in df2.Rows)
{
    string[] row = { $"{e[0]}", $"{e[1]:0.00}" };
    table.AddRow(row);
}

AnsiConsole.Write(table);