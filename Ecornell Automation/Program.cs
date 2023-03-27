using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Selenium_Automation
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver("C:\\Users\\dhwan\\Desktop\\chromedriver_win32");     
            driver.Url = "https://ondemand.ecornell.com/";

            var name = "New Folder";
            string root = @"C:\Users\dhwan\Desktop\"+name;
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

        }
    }
}