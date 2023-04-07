using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;

namespace Spider
{
    internal class PageDownload
    {
        string pageSource = driver.PageSource;
        Console.WriteLine(pageSource);
        var path2 = "data.html";
        File.WriteAllText(path2, pageSource);
    }
}
