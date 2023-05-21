using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading.Tasks;

namespace MergeTwoFiles{
    internal class Program{
        static void Main(string[] args){

            string path = "C://Users//dhs71//OneDrive//Documents//New folder//Files";
            string[] txtfiles = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly);
            using(var txtOutput = File.Create(path+"mergedTxtFile.txt")){
                foreach(var txt in txtfiles){
                    using(var csvData = File.OpenRead(txt)){
                        csvData.CopyTo(txtOutput);
                    }
                }
            }


            /*string path = "C://Users//dhs71//OneDrive//Documents//New folder//Files";
            string[] csvfiles = Directory.GetFiles(path, "*.csv", SearchOption.TopDirectoryOnly);
            using(var csvOutput = File.Create(path+"mergedCsvFile.csv")){
                foreach(var csv in csvfiles){
                    using(var csvData = File.OpenRead(csv)){
                        csvData.CopyTo(csvOutput);
                    }
                }
            }*/
        }
    }
}

