/*using System.Net;

DateTime yesterday = DateTime.Now.AddDays(-1);

using (var client = new WebClient())
{
    client.DownloadFile("https://nsearchives.nseindia.com/content/fo/BhavCopy_NSE_FO_0_0_0_20240920_F_0000.csv.zip", "D:/bsefiles/BhavCopy_NSE_FO_0_0_0_20240920_F_0000.csv.zip");
}*/

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://nsearchives.nseindia.com/content/fo/BhavCopy_NSE_FO_0_0_0_20240920_F_0000.csv.zip";  // URL of the file to download
        string outputPath = @"D:/bsefiles/BhavCopy_NSE_FO_0_0_0_20240920_F_0000.csv.zip";  // Path where you want to save the file

        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Send a request to download the file
                HttpResponseMessage response = await client.GetAsync(url);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the file's content into a byte array
                byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                // Write the file to the local file system
                await File.WriteAllBytesAsync(outputPath, fileBytes);

                Console.WriteLine("File downloaded successfully!");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
    }
}
