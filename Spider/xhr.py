from seleniumwire import webdriver  # Import from seleniumwire
import time
from selenium.webdriver.common.by import By
import re
import wget
#from bs4 import BeautifulSoup
import codecs
from ahk import AHK

ahk = AHK()

options = webdriver.ChromeOptions()
options.add_argument('ignore-certificate-errors')
# Create a new instance of the Chrome driver
driver = webdriver.Chrome(chrome_options=options)

# Go to the Course : Adopting Inclusive Hiring Practices- Create Inclusive Interview Practices
driver.get('https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3')
time.sleep(30)

#Start click
# Go to Interviewing Practices and Processes
element = driver.find_element(By.XPATH, '//*[@id=\"wiki_page_show\"]/div[3]/table/tbody/tr[5]/td[2]/a')
element.click()

time.sleep(10)

# Download Transcript
element = driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a')
element.click()

#Play video
video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
video.click()

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
            file = ("Interviewing_Practices_and_Processes.mp4")
            wget.download(videolink, file)

time.sleep(5)

#ahk.key_down("Control")
#ahk.key_press("s")
#ahk.key_up('Control')
#ahk.right_click()
#ahk.key_press("Down")
#ahk.key_press("Down")
#ahk.key_press("Down")
#ahk.key_press("Down")
#ahk.key_press("Enter")

#time.sleep(10)

#Next click
#Go to Confirmation Bias
element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span/a')
element.click()

time.sleep(10)

# Download Transcript
element = driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a')
element.click()

#Play video
video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
video.click()

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
            file = ("Confirmation_Bias.mp4")
            wget.download(videolink, file)

#Next click
#Go to How Confirmation Bias Could Influence You
element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
element.click()

time.sleep(5)

# Download How Confirmation Bias Could Influence You
element = driver.find_element(By.XPATH, '//*[@id="wiki_page_show"]/div/div/p[1]/a')
element.click()

time.sleep(5)

#Next click
#Go to Overconfidence and Interviewing
element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
element.click()

time.sleep(10)

# Download Transcript
element = driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a')
element.click()

#Play video
video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
video.click()

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
            file = ("Overconfidence_and_Interviewing.mp4")
            wget.download(videolink, file)

            #Next click
#Go to Interviewing for Fit
element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
element.click()

time.sleep(10)

# Download Transcript
element = driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a')
element.click()

#Play video
video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
video.click()

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
            file = ("Interviewing_for_Fit.mp4")
            wget.download(videolink, file)

#Next click
#Go to Case Study on Disability and Bias in Interviewing
element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
element.click()

time.sleep(10)

# Download Transcript
element = driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a')
element.click()

#Play video
video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
video.click()

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
            file = ("Case_Study_on_Disability_and_Bias_in_Interviewing.mp4")
            wget.download(videolink, file)

#Next click
#Go to Framing Disability in a DEI-Centered Approach
element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
element.click()

time.sleep(10)

#Next click
#Go to Solutions to Improve the Inclusivity of the Interview Process
element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
element.click()

time.sleep(10)

# Download Transcript
element = driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a')
element.click()

#Play video
video = driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]')
video.click()

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
            file = ("Solutions_to_Improve_the_Inclusivity_of_the_Interview_Process.mp4")
            wget.download(videolink, file)  

#Next click
#Go to Checklist for Inclusive Interviewing Practices
element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
element.click()

time.sleep(10)

# Download Transcript
element = driver.find_element(By.XPATH, '//*[@id="wiki_page_show"]/div/div/p[1]/a')
element.click()

time.sleep(5)

#Next click
#Go to Questions to Ask and to Avoid in an Interview
element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
element.click()

time.sleep(10)

#Next click
#Go to Lesson Quiz
element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
element.click()

time.sleep(10)

#Next click
#Go to Lesson Wrap-up
element = driver.find_element(By.XPATH, '//*[@id="module_sequence_footer"]/div[2]/div/span[2]/a')
element.click()

time.sleep(10)

#Next click
#Go to Lesson Transcript
element = driver.find_element(By.XPATH, '//*[@id="module_navigation_target"]/div/div[2]/div/span[2]/a')
element.click()

#Download transcript Pdf
time.sleep(10)
element = driver.find_element(By.XPATH, '//*[@id="content"]/div[1]/span/a')
element.click()
time.sleep(10)