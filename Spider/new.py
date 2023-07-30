import os
import re
import time
import codecs
import shutil
import requests
from selenium.webdriver.common.by import By
from selenium import webdriver
from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from seleniumwire import webdriver
import wget


parent_dir = "D:/Ecornell/Managing Diversity 23/"
file1 = open(parent_dir+'Managing Diversity 23 Course Links.txt', 'r')
Lines = file1.readlines()

options = webdriver.ChromeOptions()
options.add_argument('ignore-certificate-errors')
custom_user_agent = "Mozilla/5.0 (Linux; Android 11; 100011886A Build/RP1A.200720.011) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.69 Safari/537.36"
options.add_argument(f'user-agent={custom_user_agent}')
download_path = "D:\\Neel\\"
downloads = {"download.default_directory": download_path}
options.add_experimental_option("prefs", downloads)
# Create a new instance of the Chrome driver
driver = webdriver.Chrome("D:/Neel/Github/Learning-C-Sharp/Spider/chromedriver/chromedriver.exe", chrome_options=options)

driver.get("https://ondemand.ecornell.com")

#Login
email = driver.find_element(By.CSS_SELECTOR, "#username")
email.send_keys("cornell@hipointservices.com")
driver.find_element(By.CSS_SELECTOR, "#fm1 > div:nth-child(4) > input").click()
password = driver.find_element(By.CSS_SELECTOR, "#password")
password.send_keys("$anketShah81")
driver.find_element(By.CSS_SELECTOR, "#fm1 > div:nth-child(4) > input").click()
time.sleep(15)

#Explicit wait
wait = WebDriverWait(driver, 10)

for line in Lines:
    print("{}".format(line.strip()))
    new_url = line
    #driver.execute_script("window.open('');")
    #driver.switch_to.window(driver.window_handles[1])
    driver.get(new_url)
    #time.sleep(10)

    #Create course folder
    title = driver.title
    folder_title = re.sub(r'[^a-zA-Z0-9\s]+', '', title)
    print(folder_title)
    directory = folder_title
    path = os.path.join(parent_dir, directory)
    os.mkdir(path)

    n = os.path.join(path, directory+".html")
    f = codecs.open(n, "w", "utf−8")
    pageSource = driver.execute_script("return document.body.innerHTML;")
    f.write(pageSource)
    #time.sleep(15)

    #wait.until(EC.element_to_be_clickable((By.CSS_SELECTOR, "#wiki_page_show > div.show-content.user_content.clearfix.enhanced > table > tbody > tr:nth-child(5) > td.start-btn-wrapper > a")))
    driver.find_element(By.CSS_SELECTOR, "#wiki_page_show > div.show-content.user_content.clearfix.enhanced > table > tbody > tr:nth-child(5) > td.start-btn-wrapper > a").click()
    
    #driver.get("https://lms.ecornell.com/courses/1664091/files/110344291?module_item_id=23989790")
    #time.sleep(60)
    #Lesson Transcript
    #element = driver.find_element(By.XPATH, "/html/body/div[3]/div[2]/div[2]/div[3]/div[1]/div/div[2]/span/a")
    #driver.execute_script("arguments[0].click();", element)
    #time.sleep(60)

    #Single Video
    if driver.find_elements(By.XPATH, '//*[@id=\"kaltura1\"]'):
        driver.implicitly_wait(10)
        driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]').click()
    else:
        pass
    time.sleep(10)

    for request in driver.requests:
        if request.response:
            #print(
            #    request.url,
            #    request.response.status_code,
            #    request.response.headers['Content-Type']
            #)
            if(re.search("https://cfvod.kaltura.com" and "index.m3u8", request.url)):
                print(request.url)
                videolink = request.url
                videolink = videolink.replace("https://cfvod.kaltura.com/hls" or "https://cfvod.kaltura.com/scf", "https://cfvod.kaltura.com/pd")
                print(videolink)      

                      
            else:
                pass

    #Save Page

    #Attachment Downloads


    #Lesson Quiz

    #Wrap-up

    #Transcript

    #Next

    #back
    


    #Multiple Video
    driver.get("https://lms.ecornell.com/courses/1664091/pages/ask-the-activists-aaron-bartley-and-harper-bishop?module_item_id=23989764")
    #wait.until(EC.presence_of_all_elements_located((By.CSS_SELECTOR, "body > div.mwPlayerContainer.kdark.ua-mouse.ua-win.ua-chrome.size-small.start-state > div.videoHolder.hover > a")))
    #driver.find_element(By.XPATH, "/html/body/div[1]/div[2]/a").click()
    time.sleep(15)
    number = driver.find_elements(By.CLASS_NAME, "kaltura_video")
    print(len(number))
    if len(number) > 0:
        for video in driver.find_elements(By.CLASS_NAME, "kaltura_video"):
            driver.execute_script("arguments[0].click();", video)
            for request in driver.requests:
                if request.response:
                    #print(
                    #    request.url,
                    #    request.response.status_code,
                    #    request.response.headers['Content-Type']
                    #)
                    if(re.search("https://cfvod.kaltura.com" and "index.m3u8", request.url)):
                        print(request.url)
                        videolink = request.url
                        videolink = videolink.replace("https://cfvod.kaltura.com/hls" or "https://cfvod.kaltura.com/scf", "https://cfvod.kaltura.com/pd")
                        print(videolink)

driver.get("https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3")
driver.find_element(By.CSS_SELECTOR, "#wiki_page_show > div.show-content.user_content.clearfix.enhanced > table > tbody > tr:nth-child(5) > td.start-btn-wrapper > a").click()
wait = WebDriverWait(driver, 10).until(EC.element_to_be_clickable((By.CSS_SELECTOR, "#transcript-player-plugin-kaltura1 > div > div.transcript-menu > div.transcript-menu-item.downloadWrapper > a")))
driver.find_element(By.CSS_SELECTOR, "#transcript-player-plugin-kaltura1 > div > div.transcript-menu > div.transcript-menu-item.downloadWrapper > a").click()
time.sleep(15)