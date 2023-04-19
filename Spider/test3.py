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

#https://lms.ecornell.com/courses/1706761/pages/watch-interviewing-practices-and-processes?module_item_id=26344321#

#Login
driver.get('https://lms.ecornell.com/courses/1706779/quizzes/3037060?module_item_id=26343968')
time.sleep(45)

if driver.find_element(By.XPATH, '/html/head/title') == "Lesson Quiz":
    allLinks = driver.find_elements(By.TAG_NAME, 'a')
    for link in allLinks:
        if link.get_attribute('download'):
            driver.execute_script("window.open('');")
            driver.switch_to.window(driver.window_handles[1])
            print(link.get_attribute('href'))
            download = link.get_attribute('href')
            driver.get(download)
            time.sleep(30)


        driver.close()
        driver.switch_to.window(driver.window_handles[0])
    else:
        pass


    #Quiz Part
        if driver.find_elements(By.XPATH, '/html/head/title') == "Lesson Quiz":
            quizlink = driver.find_element(By.CLASS_NAME, 'btn').get_attribute('href')
            print(str(quizlink))
            driver.execute_script("window.open('');")
            driver.switch_to.window(driver.window_handles[2])
            driver.get(str(quizlink))
            time.sleep(10)

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

            driver.close()
            driver.switch_to.window(driver.window_handles[1])
        else:
            pass


        #Last Page Transcript Download