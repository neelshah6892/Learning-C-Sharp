using Microsoft.Playwright;
using System.Threading;

using var playwright = await Playwright.CreateAsync();

var opts = new BrowserTypeLaunchOptions() { Headless = false };
await using var browser = await playwright.Chromium.LaunchAsync(opts);

//await using var browser = await playwright.Chromium.LaunchAsync();
var page = await browser.NewPageAsync();
await page.GotoAsync("https://ondemand.ecornell.com/");
await page.Locator("#username").FillAsync("cornell@hipointservices.com");
await page.Locator("#fm1 > div:nth-child(4) > input").ClickAsync();
await page.Locator("#password").FillAsync("$anketShah81");
await page.Locator("#fm1 > div:nth-child(4) > input").ClickAsync();

//Course link
await page.GotoAsync("https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3");
await page.Locator("#wiki_page_show > div.show-content.user_content.clearfix.enhanced > table > tbody > tr:nth-child(5) > td.start-btn-wrapper > a").ClickAsync();

var heading = await page.Locator("#wiki_page_show > div > h1.watch.custom-heading").InnerTextAsync();
Console.WriteLine(heading);

await page.Locator("#nav-tray-portal > span > span > div > div > div > span > button").ClickAsync();

//Thread.Sleep(10000);

//await page.Locator("body > div.mwPlayerContainer.kdark.ua-mouse.ua-win.ua-chrome.size-large.start-state > div.controlBarContainer.hover.open > div.controlsContainer > button.btn.comp.playPauseBtn.display-high.icon-play").ClickAsync();

//page.Request += (_, req) => Console.WriteLine($">> {req.Method} {req.Url}");
//page.Response += (_, resp) => Console.WriteLine($"<< {resp.Status} {resp.Url}");


await page.Locator("#transcript-player-plugin-kaltura1 > div > div.transcript-menu > div.transcript-menu-item.toggleTranscriptBodyWrapper.close > a.transcript-body-open > span.transcript-body_transcript").ClickAsync();

var subtitles = await page.Locator("#transcript-player-plugin-kaltura1 > div > div.transcript-box").AllInnerTextsAsync();
Console.WriteLine(subtitles.ToString());
Thread.Sleep(10000);

await page.ScreenshotAsync(new()
{
    Path = "screenshot.png"
});