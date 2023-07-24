import re
from playwright.sync_api import Page, expect, sync_playwright
import time

parent_dir = "D:/Ecornell/Career Services 8/"

with sync_playwright() as p:
    browser = p.chromium.launch(headless=False)
    page = browser.new_page(java_script_enabled=True)
    #Login
    page.goto("https://ondemand.ecornell.com")
    page.fill("#username", "cornell@hipointservices.com")
    page.click("#fm1 > div:nth-child(4) > input")
    page.fill("#password", "$anketShah81")
    page.click("#fm1 > div:nth-child(4) > input")
    #Read txt file for course links
    file1 = open(parent_dir+'Career Services 8 Course Links.txt', 'r')
    Lines = file1.readlines()
    count = 0
    page.goto("https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3")
    page.click("#wiki_page_show > div.show-content.user_content.clearfix.enhanced > table > tbody > tr:nth-child(5) > td.start-btn-wrapper > a")
    heading = page.inner_text("#wiki_page_show > div > h1.watch.custom-heading")
    print(heading)
    time.sleep(5)
    page.locator("#nav-tray-portal > span > span > div > div > div > span > button").click()
    #page.locator("body > div.mwPlayerContainer.kdark.ua-mouse.ua-win.ua-chrome.size-large.start-state > div.controlBarContainer.hover.open > div.controlsContainer > button.btn.comp.playPauseBtn.display-high.icon-play").click()
    page.frame_locator("iframe[name=\"kaltura1_ifp\"]").locator("div").filter(has_text=re.compile(r"^0:00Off Air / 1:161x2x1\.5x1\.25x1x0\.75x0\.5xOffEnglish$")).get_by_label("Play clip").click()
    
    
    time.sleep(10)
    page.click("#transcript-player-plugin-kaltura1 > div > div.transcript-menu > div.transcript-menu-item.toggleTranscriptBodyWrapper.close > a.transcript-body-open > span.transcript-body_transcript")
    time.sleep(10)
    page.click("#module_navigation_target > div > div.module-sequence-footer > div > span > a")
    time.sleep(10)
    page.go_back()
    time.sleep(10)