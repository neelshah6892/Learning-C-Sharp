using Microsoft.Playwright;

namespace PlaywrightDemo
{
    public class Tests
    {
        [SetUp]
        public void setup()
        {
        }
        [Test]
        public async Task Test1()
        {
            //Playwright
            using var playwright = await Playwright.CreateAsync();
            //Browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            //Page
            var page = await browser.NewPageAsync();
            await page.GotoAsync(url:"https://imneelshah.github.io");
            await page.ClickAsync(selector: "text=blog");
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "Demo.jpg"
            });
            Console.WriteLine("Done");
        }
    }
}
