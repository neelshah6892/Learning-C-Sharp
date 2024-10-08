using System.IO;
using System.Linq;
using Microsoft.Data.Analysis;

// Define data path
var dataPath = Path.GetFullPath(@"housing-prices.csv");

// Load the data into the data frame
var dataFrame = DataFrame.LoadCsv(dataPath);

dataFrame.Info();

dataFrame.Description();