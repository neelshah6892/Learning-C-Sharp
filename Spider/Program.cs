using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using System.Net;
using System.IO;
//using System.Windows.Forms;
using System.Security.Permissions;
using OpenQA.Selenium.Interactions;
using PuppeteerSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using OpenQA.Selenium.DevTools.V95.IndexedDB;
using System.Configuration;
using OpenQA.Selenium.DevTools.V95.Fetch;
using Microsoft.Playwright;
using System.Threading.Tasks;
using HtmlAgilityPack;

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
            options.SetLoggingPreference(LogType.Browser, LogLevel.Warning);
            options.AddArgument("disable-extension");
            options.AddArgument("disable-infobars");

            ChromeDriver driver = new ChromeDriver("./", options);

            driver.Manage().Window.Maximize();
            
            //Course link
            driver.Url = "https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3";
            Thread.Sleep(60000);

            //Close menu
            /*driver.FindElement(By.XPath("//*[@id=\"nav-tray-portal\"]/span/span/div/div/div/span/button")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//*[@id=\"step-1\"]/div[3]/button")).Click();*/

            //Start Lesson
            driver.FindElement(By.XPath("//*[@id=\"wiki_page_show\"]/div[3]/table/tbody/tr[5]/td[2]/a")).Click();
            Thread.Sleep(10000);

            //Close menu
            /*driver.FindElement(By.XPath("//*[@id=\"nav-tray-portal\"]/span/span/div/div/div/span/button")).Click();
            Thread.Sleep(2000);*/
            /*driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[2]/button[1]")).Click();*/

            /*var scraper = new HtmlWeb();
            var page = scraper.Load(driver.ToString());
            var nodes = page.DocumentNode.Descendants().SkipWhile(e => e.Id != "Techniques").Skip(1).TakeWhile(e => e.Name != "h2");

            foreach (var currNode in nodes)
            {
                if (currNode.GetClasses().Contains("mw-headline"))
                {
                    var headline = currNode.InnerText;
                    Console.WriteLine(headline);
                }
            }*/

            //Play Video
            driver.FindElement(By.XPath("//*[@id=\"kaltura1\"]")).Click();

            //Fetch vudeo link
            /*var entries = driver.Manage().Logs.GetLog(LogType.Browser);
            foreach (var entry in entries)
            {
                Console.WriteLine(entry.ToString());
            }*/

            IDevTools devTools = driver as IDevTools;
            DevToolsSession session = devTools.GetDevToolsSession();
            FetchAdapter fetchAdapter = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V95.DevToolsSessionDomains>().Fetch;

            var enableCommandSettings = new OpenQA.Selenium.DevTools.V95.Fetch.EnableCommandSettings();

            var requestPattern = new OpenQA.Selenium.DevTools.V95.Fetch.RequestPattern();
            requestPattern.RequestStage = RequestStage.Request;
            requestPattern.ResourceType = (OpenQA.Selenium.DevTools.V95.Network.ResourceType?)ResourceType.Xhr;

            enableCommandSettings.Patterns = new OpenQA.Selenium.DevTools.V95.Fetch.RequestPattern[] { requestPattern };

            fetchAdapter.Enable(enableCommandSettings);

            EventHandler<OpenQA.Selenium.DevTools.V95.Fetch.RequestPausedEventArgs> requestIntercepted = (sender, e) =>
            {
                Requests.Add(e.Request);
                fetchAdapter.ContinueRequest(new OpenQA.Selenium.DevTools.V95.Fetch.ContinueRequestCommandSettings()
                {
                    RequestId = e.RequestId
                });
            };

            fetchAdapter.RequestPaused += requestIntercepted;


            //Next Page
            driver.FindElement(By.XPath("//*[@id=\"module_navigation_target\"]/div/div[2]/div/span/a")).Click();
            Thread.Sleep(20000);

            //Play Video
            driver.FindElement(By.XPath("//*[@id=\"kaltura1\"]")).Click();

            //driver.FindElement(By.XPath("//*[@id=\"ecCards\"]/div[2]/div[1]/div[1]/a")).Click();
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