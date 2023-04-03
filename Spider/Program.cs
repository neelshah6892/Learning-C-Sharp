using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;
using System.IO;

namespace Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new ChromeOptions();

            //Laptop
            options.AddArgument("user-data-dir=C:\\Users\\dhwan\\AppData\\Local\\Google\\Chrome\\User Data");

            //dhwani System
            //options.AddArgument("user-data-dir=C:\\Users\\dhs71\\AppData\\Local\\Google\\Chrome\\User Data\\Profile 5");
            //options.AddArgument("--profile_ddirectory=Profile 5");

            //Main laptop
            //options.AddArgument("--profile_directory=Arjun");
            //options.AddArgument("user-data-dir=C:\\Users\\geete\\AppData\\Local\\Google\\Chrome\\User Data\\Arjun");

            options.AddArgument("disable-extension");
            options.AddArgument("disable-infobars");

            ChromeDriver driver = new ChromeDriver("./", options);

            driver.Manage().Window.Maximize();
            driver.Url = "https://ondemand.ecornell.com/";

            /*Thread.Sleep(5000);
            driver.FindElement(By.XPath("//*[@id=\"username\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"username\"]")).SendKeys(Keys.ArrowDown);
            driver.FindElement(By.XPath("//*[@id=\"username\"]")).SendKeys(Keys.Enter);
            driver.FindElement(By.XPath("//*[@id=\"fm1\"]/div[2]/input")).Click();
            //driver.FindElement(By.XPath("//*[@id=\"fm1\"]/div[3]/input")).Click();
            /hread.Sleep(10000);*/
            //Console.ReadKey();
            Thread.Sleep(100000);
            //driver.FindElement(By.XPath("//*[@id=\"term_skill\"]/a[2]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"term_skill\"]/ul/li[1]/div/label")).Click();
            Thread.Sleep(10000);
            //driver.SwitchTo().NewWindow(WindowType.Tab);
            driver.FindElement(By.ClassName("uk-card-body uk-link-reset uk-position-relative ec-details-link")).Click();
            string name = driver.FindElement(By.ClassName("uk-card-body uk-link-reset uk-position-relative ec-details-link")).ToString();
            Console.WriteLine(name);
            Thread.Sleep(3000);

            driver.FindElement(By.ClassName("moving-btn")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//*[@id=\"module_navigation_target\"]/div/div[2]/div/span/a")).Click();

            //driver.Url = "https://google.com/";
            driver.ExecuteScript("return window.performance.getEntries();");
            //*[@id="term_skill"]/ul/li[1]

            

            //cornell@hipointservices.com
            //
            Thread.Sleep(30000);
            driver.Close();
        }
    }
}