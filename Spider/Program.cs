using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new ChromeOptions();
            options.AddArgument("user-data-dir=C:\\Users\\dhs71\\AppData\\Local\\Google\\Chrome\\User Data\\Profile 5");
            options.AddArgument("disable-infobars");

            ChromeDriver driver = new ChromeDriver("./", options);

            driver.Manage().Window.Maximize();
            driver.Url = "https://ondemand.ecornell.com/";
            driver.FindElement(By.XPath("//*[@id=\"fm1\"]/div[2]/input")).Click();
        }
    }
}