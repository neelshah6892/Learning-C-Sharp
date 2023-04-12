from seleniumwire import webdriver  # Import from seleniumwire
import time
from selenium.webdriver.common.by import By
import re
import wget
import codecs
import os
import pyautogui
from PIL import Image
from Screenshot import Screenshot
#from Screenshot import Screenshot_Clipping


options = webdriver.ChromeOptions()
options.add_argument('ignore-certificate-errors')

#Default Download
#prefs = {'download.default_directory' : 'C:\Tutorial\down'}
#options.add_experimental_option("prefs",prefs)

options.add_argument("download.default_directory=D:/Downloads")

# Create a new instance of the Chrome driver
driver = webdriver.Chrome(chrome_options=options)

#NEW TAB

# Go to the Course
driver.get('https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3')
time.sleep(45)

title = driver.title
folder_title = re.sub(r'[^a-zA-Z0-9\s]+', '', title)
print(folder_title)

directory = folder_title
parent_dir = "D:/Ecornell"
path = os.path.join(parent_dir, directory)
os.mkdir(path)

#Save Page
# pyautogui.hotkey('ctrl', 's')
# time.sleep(1)
# pyautogui.typewrite(folder_title + '.html')
# pyautogui.hotkey('enter')
# time.sleep(60)

# pageSource = driver.page_source
# fileToWrite = open("page_source.html", "w")
# fileToWrite.write(pageSource)
# fileToWrite.close()

pageSource = driver.execute_script("return document.body.innerHTML;")
fileToWrite = open("inner.html", "w", encoding='utf8')
fileToWrite.write(pageSource)
fileToWrite.close()

pageSource = driver.execute_script("return document.documentElement.outerHTML;")
fileToWrite = open("outer.html", "w", encoding='utf8')
fileToWrite.write(pageSource)
fileToWrite.close()

#Save SS
#ss = Screenshot_Clipping.Screenshot()
#ss.full_Screenshot(driver, save_path=r'.' , image_name='name.png')

#Start Click
element = driver.find_element(By.XPATH, '//*[@id=\"wiki_page_show\"]/div[3]/table/tbody/tr[5]/td[2]/a')
element.click()

time.sleep(10)

#Download Transcript
if driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a'):
    driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a').click()
else:
    pass

#Play video
if driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]'):
    driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]').click()
else:
    pass

#Rename and move transcript


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
            title = driver.title
            folder_title = "".join(ch for ch in title if ch.isalnum())
            print(folder_title)

            directory = folder_title
            #parent_dir = "D:/"
            #path = os.path.join(parent_dir, directory)
            os.mkdir(path)
            file = (folder_title+".mp4")
            wget.download(videolink, file)
    else:
        pass

time.sleep(5)

#time.sleep(10)

#Next click
#Go to Confirmation Bias
if driver.find_element(By.CLASS_NAME, 'Button'):
    driver.find_element(By.CLASS_NAME, 'Button').click()
else:
    #Close tab
    pass

time.sleep(10)

driver.close()

# # Download Transcript
# element = driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a')
# element.click()

# #Play video
# video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
# video.click()

# time.sleep(10)

# for request in driver.requests:
#     if request.response:
#         #print(
#         #    request.url,
#         #    request.response.status_code,
#         #    request.response.headers['Content-Type']
#         #)
#         if(re.search("https://cfvod.kaltura.com" and "index.m3u8", request.url)):
#             print(request.url)
#             videolink = request.url
#             videolink = videolink.replace("https://cfvod.kaltura.com/hls" or "https://cfvod.kaltura.com/scf", "https://cfvod.kaltura.com/pd")
#             print(videolink)
#             file = ("Confirmation_Bias.mp4")
#             wget.download(videolink, file)

# #Next click
# #Go to How Confirmation Bias Could Influence You
# element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
# element.click()

# time.sleep(5)

# # Download How Confirmation Bias Could Influence You
# element = driver.find_element(By.XPATH, '//*[@id="wiki_page_show"]/div/div/p[1]/a')
# element.click()

# time.sleep(5)

# #Next click
# #Go to Overconfidence and Interviewing
# element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
# element.click()

# time.sleep(10)

# # Download Transcript
# element = driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a')
# element.click()

# #Play video
# video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
# video.click()

# time.sleep(10)

# for request in driver.requests:
#     if request.response:
#         #print(
#         #    request.url,
#         #    request.response.status_code,
#         #    request.response.headers['Content-Type']
#         #)
#         if(re.search("https://cfvod.kaltura.com" and "index.m3u8", request.url)):
#             print(request.url)
#             videolink = request.url
#             videolink = videolink.replace("https://cfvod.kaltura.com/hls" or "https://cfvod.kaltura.com/scf", "https://cfvod.kaltura.com/pd")
#             print(videolink)
#             file = ("Overconfidence_and_Interviewing.mp4")
#             wget.download(videolink, file)

#             #Next click
# #Go to Interviewing for Fit
# element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
# element.click()

# time.sleep(10)

# # Download Transcript
# element = driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a')
# element.click()

# #Play video
# video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
# video.click()

# time.sleep(10)

# for request in driver.requests:
#     if request.response:
#         #print(
#         #    request.url,
#         #    request.response.status_code,
#         #    request.response.headers['Content-Type']
#         #)
#         if(re.search("https://cfvod.kaltura.com" and "index.m3u8", request.url)):
#             print(request.url)
#             videolink = request.url
#             videolink = videolink.replace("https://cfvod.kaltura.com/hls" or "https://cfvod.kaltura.com/scf", "https://cfvod.kaltura.com/pd")
#             print(videolink)
#             file = ("Interviewing_for_Fit.mp4")
#             wget.download(videolink, file)

# #Next click
# #Go to Case Study on Disability and Bias in Interviewing
# element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
# element.click()

# time.sleep(10)

# # Download Transcript
# element = driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a')
# element.click()

# #Play video
# video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
# video.click()

# time.sleep(10)

# for request in driver.requests:
#     if request.response:
#         #print(
#         #    request.url,
#         #    request.response.status_code,
#         #    request.response.headers['Content-Type']
#         #)
#         if(re.search("https://cfvod.kaltura.com" and "index.m3u8", request.url)):
#             print(request.url)
#             videolink = request.url
#             videolink = videolink.replace("https://cfvod.kaltura.com/hls" or "https://cfvod.kaltura.com/scf", "https://cfvod.kaltura.com/pd")
#             print(videolink)
#             file = ("Case_Study_on_Disability_and_Bias_in_Interviewing.mp4")
#             wget.download(videolink, file)

# #Next click
# #Go to Framing Disability in a DEI-Centered Approach
# element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
# element.click()

# time.sleep(10)

# #Next click
# #Go to Solutions to Improve the Inclusivity of the Interview Process
# element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
# element.click()

# time.sleep(10)

# # Download Transcript
# element = driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a')
# element.click()

# #Play video
# video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
# video.click()

# time.sleep(10)

# for request in driver.requests:
#     if request.response:
#         #print(
#         #    request.url,
#         #    request.response.status_code,
#         #    request.response.headers['Content-Type']
#         #)
#         if(re.search("https://cfvod.kaltura.com" and "index.m3u8", request.url)):
#             print(request.url)
#             videolink = request.url
#             videolink = videolink.replace("https://cfvod.kaltura.com/hls" or "https://cfvod.kaltura.com/scf", "https://cfvod.kaltura.com/pd")
#             print(videolink)
#             file = ("Solutions_to_Improve_the_Inclusivity_of_the_Interview_Process.mp4")
#             wget.download(videolink, file)  

# #Next click
# #Go to Checklist for Inclusive Interviewing Practices
# element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
# element.click()

# time.sleep(10)

# # Download Transcript
# element = driver.find_element(By.XPATH, '//*[@id="wiki_page_show"]/div/div/p[1]/a')
# element.click()

# time.sleep(5)

# #Next click
# #Go to Questions to Ask and to Avoid in an Interview
# element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
# element.click()

# time.sleep(10)

# #Next click
# #Go to Lesson Quiz
# element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
# element.click()

# time.sleep(10)

# #Next click
# #Go to Lesson Wrap-up
# element = driver.find_element(By.XPATH, '//*[@id="module_sequence_footer"]/div[2]/div/span[2]/a')
# element.click()

# time.sleep(10)

# #Next click
# #Go to Lesson Transcript
# element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
# element.click()

# #Download transcript Pdf
# time.sleep(10)
# element = driver.find_element(By.XPATH, '//*[@id="content"]/div[1]/span/a')
# element.click()
# time.sleep(10)