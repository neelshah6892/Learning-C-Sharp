﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Net;

namespace Selenium_Automation
{
    class Program
    {
        static void Main(string[] args)
        {
            //var url = "https://cfvod.kaltura.com/hls/p/1284141/sp/128414100/serveFlavor/entryId/1_kf1hqqj3/v/31/ev/25/flavorId/1_17frlhdr/name/a.mp4/index.m3u8"

            using (var client = new WebClient())
            {
                client.DownloadFile("https://cfvod.kaltura.com/pd/p/1284141/sp/128414100/serveFlavor/entryId/1_kf1hqqj3/v/31/ev/25/flavorId/1_17frlhdr/name/a.mp4/index.m3u8", "D:/a.mp4");
            }

            IWebDriver driver = new ChromeDriver("D:\\chromedriver_win32");     
            driver.Url = "https://ondemand.ecornell.com/";

            var name = "New Folder";
            string root = @"D:\"+name;
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

        }
    }
}