using System;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.ChromeDriver;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver("C:\\Users\\Administrator\\Desktop\\chromedriver.exe");

            // This will open up the URL
            driver.Url = "https://www1.nseindia.com/products/content/derivatives/equities/archieve_fo.htm";



            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //Needed for .net framework 4 to download files 
            DateTime today = DateTime.Today.AddDays(-1);
            //Console.WriteLine(today.ToString("MMM"));
            //string strURL = "https://www1.nseindia.com/content/historical/DERIVATIVES/2022/JAN/fo24JAN2022bhav.csv.zip";
            //https://www1.nseindia.com/content/historical/DERIVATIVES/2022/JAN/fo27JAN2022bhav.csv.zip
            string strURL = "https://www1.nseindia.com/content/historical/DERIVATIVES/"+today.Year+"/"+today.ToString("MMM")+"/fo"+today.Day+""+ today.ToString("MMM") + ""+today.Year+"bhav.csv.zip";
            Console.WriteLine(strURL);
            WebClient client = new WebClient();
            client.DownloadFile(strURL, "C:\\Users\\Administrator\\Desktop\\fo" + today.Day + "" + today.ToString("MMM") + "" + today.Year + "bhav.csv.zip");
        }
    }
}
