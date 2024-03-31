using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Threading;

namespace EdgeDriverSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = new EdgeDriver();
            try
            {
                driver.Url = "https://www.bseindia.com/members/index.aspx?expandable=6";

                Thread.Sleep(5000);

                 var element = driver.FindElement(By.XPath("//*[@id=\"divID\"]/div[5]/div[2]/p[1]/strong/span[2]/span/a"));

                element.Click();

                Thread.Sleep(5000);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}