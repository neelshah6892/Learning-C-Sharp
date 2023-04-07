using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Security.Permissions;
using OpenQA.Selenium.Interactions;
using PuppeteerSharp;

namespace Spider
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var options = new ChromeOptions();

            //Laptop
            //options.AddArgument("user-data-dir=C:\\Users\\dhwan\\AppData\\Local\\Google\\Chrome\\User Data");

            //dhwani System
            options.AddArgument("user-data-dir=C:\\Users\\dhs71\\AppData\\Local\\Google\\Chrome\\User Data\\Profile 5");
            options.AddArgument("--profile_ddirectory=Profile 5");

            //Main laptop
            //options.AddArgument("--profile_directory=Arjun");
            //options.AddArgument("user-data-dir=C:\\Users\\geete\\AppData\\Local\\Google\\Chrome\\User Data\\Arjun");

            options.AddArgument("disable-extension");
            options.AddArgument("disable-infobars");

            ChromeDriver driver = new ChromeDriver("./", options);

            driver.Manage().Window.Maximize();
            //driver.Url = "https://ondemand.ecornell.com/";

            //driver.FindElement(By.XPath("//*[@id=\"term_skill\"]/ul/li[1]/div/label")).Click();
            driver.Url = "https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3";
            

            Thread.Sleep(60000);
            /*driver.FindElement(By.XPath("//*[@id=\"nav-tray-portal\"]/span/span/div/div/div/span/button")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//*[@id=\"step-1\"]/div[3]/button")).Click();*/

            
            driver.FindElement(By.XPath("//*[@id=\"wiki_page_show\"]/div[3]/table/tbody/tr[5]/td[2]/a")).Click();
            Thread.Sleep(10000);
            /*driver.FindElement(By.XPath("//*[@id=\"nav-tray-portal\"]/span/span/div/div/div/span/button")).Click();
            
            Thread.Sleep(2000);*/

            var option = new LaunchOptions
            {
                Headless = false,
                ExecutablePath = "./chromedriver.exe"
            };


            using var browser = await Puppeteer.LaunchAsync(option);
            var page = await browser.NewPageAsync();
            page.Request += (sender, e) =>
            {
                Console.WriteLine($"Request: {e.Request.Method} {e.Request.Url}");
                foreach (var header in e.Request.Headers)
                {
                    Console.WriteLine($"{header.Key}: {header.Value}");
                }
            };
            await page.GoToAsync("https://lms.ecornell.com/courses/1706761/modules/items/26344322");

            driver.FindElement(By.XPath("//*[@id=\"module_navigation_target\"]/div/div[2]/div/span/a")).Click();
            Thread.Sleep(5000);
            SendKeys.SendWait("^(s)");
            Thread.Sleep(2000);
            SendKeys.SendWait("~");

            /*Actions rc = new Actions(driver);
            WebElement link = driver.FindElement(By.XPath("//*[@id=\"application\"]"));
            rc.ContextClick(link).Perform();*/



            Thread.Sleep(20000);
            //driver.SwitchTo().NewWindow(WindowType.Tab);
            driver.FindElement(By.XPath("//*[@id=\"ecCards\"]/div[2]/div[1]/div[1]/a")).Click();
            Thread.Sleep(10000);
            //string coureLink = driver.FindElement(By.XPath("//*[@id=\"ecCards\"]/div[2]/div[2]/div/div/ul[1]/li[6]/a")).ToString();
            //driver.FindElement(By.ClassName("uk-card-body uk-link-reset uk-position-relative ec-details-link")).Click();
            Thread.Sleep(100000);

            driver.FindElement(By.ClassName("moving-btn")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//*[@id=\"module_navigation_target\"]/div/div[2]/div/span/a")).Click();

            
            //*[@id="term_skill"]/ul/li[1]

            

            //cornell@hipointservices.com
            //
            Thread.Sleep(30000);
            driver.Close();
        }
    }
}