from seleniumwire import webdriver
import time
from selenium.webdriver.common.by import By
import re
import wget
import codecs
import os

parent_dir = "D:/Ecornell/Building Teams 33/"
sub_dir = ""
directory = ""

def CreateCourseFolder():
    title = driver.title
    folder_title = re.sub(r'[^a-zA-Z0-9\s]+', '', title)
    print(folder_title)
    directory = folder_title
    path = os.path.join(parent_dir, directory)
    sub_dir = path
    os.mkdir(path)

def SaveInnerHTML():
    pageSource = driver.execute_script("return document.body.innerHTML;")
    fileToWrite = open(directory+"inner.html", "w", encoding='utf8')
    fileToWrite.write(pageSource)
    fileToWrite.close()

def SaveOuterHTML():
    pageSource = driver.execute_script("return document.documentElement.outerHTML;")
    fileToWrite = open(directory+"outer.html", "w", encoding='utf8')
    fileToWrite.write(pageSource)
    fileToWrite.close()

def CreateSubFolder():
    title = driver.title
    subfolder_title = re.sub(r'[^a-zA-Z0-9\s]+', '', title)
    print(subfolder_title)
    directory = subfolder_title
    path = os.path.join(sub_dir, directory)
    os.mkdir(path)

def StartClick():
    if driver.find_element(By.XPATH, '//*[@id=\"wiki_page_show\"]/div[3]/table/tbody/tr[5]/td[2]/a'):
        element = driver.find_element(By.XPATH, '//*[@id=\"wiki_page_show\"]/div[3]/table/tbody/tr[5]/td[2]/a')
        element.click()

def DownloadTranscript():
    if driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a'):
        driver.find_element(By.XPATH, '//*[@id="transcript-player-plugin-kaltura1"]/div/div[1]/div[2]/a').click()
        time.sleep(30)
        #Rename and move transcript
        #C:\Users\geete\Downloads
        src = "C:/Users/dhs71/Downloads/transcript.txt"
        #src = "C:/User/geete/Downloads/transcript.txt"
        dst = directory+".txt"
        os.rename(src, dst)
    else:
        pass

def DownloadVideo():
    if driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]'):
        driver.find_element(By.XPATH, '//*[@id=\"kaltura1\"]').click()
    else:
        pass
    time.sleep(30)
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
                folder_title = re.sub(r'[^a-zA-Z0-9\s]+', '', title)
                print(folder_title)

                directory = folder_title
                parent_dir = "D:/Ecornell/"
                path = os.path.join(parent_dir, directory)
                os.mkdir(path)
                file = (folder_title+".mp4")
                wget.download(videolink, file)
            else:
                pass

def NextClick():
    if driver.find_element(By.CLASS_NAME, 'Button'):
        driver.find_element(By.CLASS_NAME, 'Button').click()
    else:
        #Close tab
        pass

def ExtraDownload():
    pass

def PDFDownload():
    if driver.find_element(By.XPATH, '//*[@id="content"]/div[2]/span/a'):
        driver.find_element(By.XPATH, '//*[@id="content"]/div[2]/span/a').click()

def Quiz():
    pass

def RunAll():
    #Create course folder
    CreateSubFolder()

    #Save Page
    SaveInnerHTML()
    time.sleep(30)
    SaveOuterHTML()
    time.sleep(30)

    #Download Transcript
    DownloadTranscript()
    time.sleep(10)

    #Play & Download Video
    DownloadVideo()

    #Next Click
    NextClick()
    time.sleep(30)

    #Download PDF
    PDFDownload()
    time.sleep(30)

    #Extra Downloads
    ExtraDownload()


options = webdriver.ChromeOptions()
options.add_argument('ignore-certificate-errors')

# Create a new instance of the Chrome driver
driver = webdriver.Chrome(chrome_options=options)

#Login
driver.get('https://ondemand.ecornell.com/')
time.sleep(45)

#Read txt file for course links
file1 = open(parent_dir+'Building Teams 33 Course Links.txt', 'r')
Lines = file1.readlines()
count = 0
# Strips the newline character
for line in Lines:
    count += 1
    print("Course {}: {}".format(count, line.strip()))
    new_url = line
    driver.execute_script("window.open('');")
    driver.switch_to.window(driver.window_handles[1])
    driver.get(new_url)
    time.sleep(30)

    #Create course folder
    CreateCourseFolder()

    #Save Start Page
    SaveInnerHTML()
    time.sleep(30)
    SaveOuterHTML()
    time.sleep(30)

    #Start Click
    StartClick()
    time.sleep(30)

    if driver.find_element(By.CLASS_NAME, 'Button'):
        RunAll()
    else:
        RunAll()
        driver.close()
        driver.switch_to.window(driver.window_handles[0])
   

driver.close()