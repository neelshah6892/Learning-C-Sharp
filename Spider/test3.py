from seleniumwire import webdriver
import time
from selenium.webdriver.common.by import By
import re
import wget
import codecs
import os
import shutil


subpath = "D:/Ecornell/Building Teams 33/"

options = webdriver.ChromeOptions()
options.add_argument('ignore-certificate-errors')

# Create a new instance of the Chrome driver
driver = webdriver.Chrome(chrome_options=options)


#https://stackoverflow.com/questions/69215070/response-403-with-selenium-web-scraper-how-to-fix
#https://stackoverflow.com/questions/34692009/download-image-from-url-using-python-urllib-but-receiving-http-error-403-forbid
#https://stackoverflow.com/questions/62955392/how-do-you-correctly-parse-web-links-to-avoid-a-403-error-when-using-wget
#https://lms.ecornell.com/courses/1706761/pages/watch-interviewing-practices-and-processes?module_item_id=26344321#

#Login
driver.get('https://lms.ecornell.com/courses/1478421/pages/ask-the-expert-anna-kozlova-2?module_item_id=16826295')
time.sleep(45)

#//*[@id="pid_myVideoTarget5"]
if driver.find_elements(By.CLASS_NAME, 'icon-play'):
    driver.implicitly_wait(10)
    video = driver.find_elements(By.CLASS_NAME, 'icon-play')
    for vid in video:
         driver.find_element(By.CLASS_NAME, 'icon-play').click()
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
                    
                    file = (subpath+"/"+dir+".mp4")
                    wget.download(videolink, file)
                    time.sleep(10)
                    
                   
            else:
                pass
        
    del driver.requests
else:
    pass
    time.sleep(10)