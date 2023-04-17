from seleniumwire import webdriver
import time
from selenium.webdriver.common.by import By
import re
import wget
import codecs
import os


parent_dir = "D:/Ecornell/Building Teams 33/"

def NextClick():
    if driver.find_element(By.CLASS_NAME, 'module-sequence-footer-button--next'):
        driver.find_element(By.CLASS_NAME, 'module-sequence-footer-button--next').click()
    else:
        #Close tab
        pass

def RunAll():
    #Create course folder
    #CreateSubFolder(driver.title, path)

    #Next Click
    NextClick()
    time.sleep(10)

def StartClick():
    if driver.find_element(By.XPATH, '//*[@id=\"wiki_page_show\"]/div[3]/table/tbody/tr[5]/td[2]/a'):
        element = driver.find_element(By.XPATH, '//*[@id=\"wiki_page_show\"]/div[3]/table/tbody/tr[5]/td[2]/a')
        element.click()

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

    #Click Start
    StartClick()
    time.sleep(30)
    #licount = 1

    #//*[@id="sticky-container"]/nav/ul[2]/li/ul/li[2]/span/div/a
    length = len(driver.find_elements(By.XPATH, '//*[@id="sticky-container"]/nav/ul[2]/li/ul/li/span/div/a'))
    print(length)
    #for i in driver.find_elements(By.XPATH, '//*[@id="sticky-container"]/nav/ul[2]/li/ul/li['+str(licount)+']/span/div/a'):
    for i in range(length):
        i+=1
        el = driver.find_element(By.XPATH, '//*[@id="sticky-container"]/nav/ul[2]/li/ul/li['+str(i)+']/span/div/a')
        time.sleep(30)
        print(el.get_attribute("innerText"))
        print(el.get_attribute('href'))
        
        pageName = str(el.get_attribute("innerText"))
        #Create Sub folder
        title = pageName
        folder = re.sub(r'[^a-zA-Z0-9\s]+', '', title)
        print(folder)
        dir = folder
        subpath = os.path.join(path, dir)
        os.mkdir(subpath)

        n = os.path.join(subpath, dir+".html")
        f = codecs.open(n, "w", "utf−8")
        pageSource = driver.execute_script("return document.body.innerHTML;")
        f.write(pageSource)

        time.sleep(10)
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
                    
                    file = (subpath+"/"+dir+".mp4")
                    wget.download(videolink, file)
                    time.sleep(30)
                    
                    if driver.find_element(By.CLASS_NAME, 'module-sequence-footer-button--next'):
                        driver.find_element(By.CLASS_NAME, 'module-sequence-footer-button--next').click()
                        time.sleep(30)
                    else:
                        pass

            else:
                pass
        
    #licount+=1

    driver.close()
    driver.switch_to.window(driver.window_handles[0])
        

