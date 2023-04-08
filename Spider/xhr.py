from seleniumwire import webdriver  # Import from seleniumwire
import time
from selenium.webdriver.common.by import By
import re
import wget
#from bs4 import BeautifulSoup
import codecs

# Create a new instance of the Chrome driver
driver = webdriver.Chrome()

# Go to the Google home page
driver.get('https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3')
time.sleep(45)

#n=os.path.join("D:\Neel", "Page.html")
#f = codecs.open(n, "w", "utf−8")
#html = driver.page_source
#file.write(html)
#time.sleep(2)
#print(html)

element = driver.find_element(By.XPATH, '//*[@id=\"wiki_page_show\"]/div[3]/table/tbody/tr[5]/td[2]/a')
element.click()

#Play video
video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
video.click()

time.sleep(20)

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
            file = ("test.mp4")
            wget.download(videolink, file)