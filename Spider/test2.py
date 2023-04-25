from seleniumwire import webdriver
import time
from selenium.webdriver.common.by import By
import re
import wget
import codecs
import os
import shutil

#https://stackoverflow.com/questions/69215070/response-403-with-selenium-web-scraper-how-to-fix
#https://stackoverflow.com/questions/34692009/download-image-from-url-using-python-urllib-but-receiving-http-error-403-forbid
#https://stackoverflow.com/questions/62955392/how-do-you-correctly-parse-web-links-to-avoid-a-403-error-when-using-wget

parent_dir = "D:/Ecornell/Sales 25/"


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
custom_user_agent = "Mozilla/5.0 (Linux; Android 11; 100011886A Build/RP1A.200720.011) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.69 Safari/537.36"
options.add_argument(f'user-agent={custom_user_agent}')
options.add_argument('ignore-certificate-errors')
options = {
    'request_storage': 'memory'  # Store requests and responses in memory only
}
# Create a new instance of the Chrome driver
driver = webdriver.Chrome(seleniumwire_options=options)

#Login
driver.get('https://ondemand.ecornell.com/')
time.sleep(45)

#Read txt file for course links
file1 = open(parent_dir+'Sales 25 Course Links.txt', 'r')
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
    time.sleep(10)

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
    time.sleep(20)
    #licount = 1

    #//*[@id="sticky-container"]/nav/ul[2]/li/ul/li[2]/span/div/a
    length = len(driver.find_elements(By.XPATH, '//*[@id="sticky-container"]/nav/ul[2]/li/ul/li/span/div/a'))
    print(length)
    #for i in driver.find_elements(By.XPATH, '//*[@id="sticky-container"]/nav/ul[2]/li/ul/li['+str(licount)+']/span/div/a'):
    for i in range(length):
        i+=1
        el = driver.find_element(By.XPATH, '//*[@id="sticky-container"]/nav/ul[2]/li/ul/li['+str(i)+']/span/div/a')
        time.sleep(10)
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
                    
                    file = (subpath+"/"+dir+".mp4")
                    wget.download(videolink, file)
                    time.sleep(10)
                    

                    
            else:
                pass
        
        del driver.requests

        #Transript
        if driver.find_elements(By.CLASS_NAME, 'active-elements'):
            driver.find_element(By.CLASS_NAME, 'active-elements').click()
            time.sleep(5)
            shutil.move("C:/Users/dhs71/Downloads/transcript.txt", subpath+"/transcript.txt")
        else:
            pass
        
        #Attachment Download
        allLinks = driver.find_elements(By.TAG_NAME, 'a')
        for link in allLinks:
            if re.findall("https://ecornell.s3.amazonaws.com/", link.get_attribute('href')):
                print(link.get_attribute('href'))
                file = (subpath+"/")
                wget.download(link.get_attribute('href'), file)
                time.sleep(10)
            else:
                pass
        
        



        if driver.find_elements(By.CLASS_NAME, 'module-sequence-footer-button--next'):
            driver.find_element(By.CLASS_NAME, 'module-sequence-footer-button--next').click()
            time.sleep(10)
        else:
            pass
    #licount+=1

    driver.close()
    driver.switch_to.window(driver.window_handles[0])
        

