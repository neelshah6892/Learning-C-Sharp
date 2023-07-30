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

driver.get("https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3")
wait.until(EC.element_to_be_clickable((By.CSS_SELECTOR, "#wiki_page_show > div.show-content.user_content.clearfix.enhanced > table > tbody > tr:nth-child(5) > td.start-btn-wrapper > a")))
driver.find_element(By.CSS_SELECTOR, "#wiki_page_show > div.show-content.user_content.clearfix.enhanced > table > tbody > tr:nth-child(5) > td.start-btn-wrapper > a").click()


time.sleep(30)
#Single Video
if driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]'):
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