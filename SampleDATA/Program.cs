using System.IO;
using System.Linq;
using Microsoft.Data.Analysis;

// Define data path
var dataPath = Path.GetFullPath(@"housing-prices.csv");

// Load the data into the data frame
var dataFrame = DataFrame.LoadCsv(dataPath);

// Preview of column datatypes
Console.WriteLine(dataFrame.Info());

// Summary of the data
Console.WriteLine(dataFrame.Description());

// Transform data
dataFrame["ComputedPrices"] = dataFrame["HistoricalPrice"].Multiply(2);
Console.WriteLine(dataFrame["ComputedPrices"]);

// Sort data in groups
var sortedDataFrame = dataFrame.GroupBy("Size");
Console.WriteLine(sortedDataFrame.Count());

PrimitiveDataFrameColumn<bool> boolFilter = dataFrame["CurrentPrice"].ElementwiseGreaterThan(200000);
DataFrame filteredDataFrame = dataFrame.Filter(boolFilter);
Console.WriteLine(filteredDataFrame);

var ids = new List<Single>() { 1, 2, 3, 4, 5, 6 };
var bedrooms = new List<Single>() { 1, 2, 3, 2, 3, 1 };

var idColumn = new SingleDataFrameColumn("Id", ids);
var bedroomColumn = new SingleDataFrameColumn("BedroomNumber", bedrooms);
var dataFrame2 = new DataFrame(idColumn, bedroomColumn);

// Merge two dataframes
dataFrame = dataFrame.Merge(dataFrame2, new string[] { "Id" }, new string[] { "Id" });

// Save to csv
DataFrame.SaveCsv(dataFrame, "result.csv", ',');