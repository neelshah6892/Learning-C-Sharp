using PuppeteerSharp;

using var browser = await Puppeteer.LaunchAsync(new() { Headless = true });
var page = await browser.NewPageAsync();
page.Request += (sender, e) =>
{
    Console.WriteLine($"Request: {e.Request.Method} {e.Request.Url}");
    foreach (var header in e.Request.Headers)
    {
        Console.WriteLine($"{header.Key}: {header.Value}");
    }
};
await page.GoToAsync("https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3");

//Link Crwaler

//driver.Url = "https://ondemand.ecornell.com/";

//driver.FindElement(By.XPath("//*[@id=\"term_skill\"]/ul/li[1]/div/label")).Click();

