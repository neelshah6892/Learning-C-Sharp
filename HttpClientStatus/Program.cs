using System.Net;

DateTime yesterday = DateTime.Now.AddDays(-1);

using (var client = new WebClient())
{
    client.DownloadFile("https://nsearchives.nseindia.com/content/fo/BhavCopy_NSE_FO_0_0_0_20240920_F_0000.csv.zip", "D:/bsefiles/BhavCopy_NSE_FO_0_0_0_20240920_F_0000.csv.zip");
}