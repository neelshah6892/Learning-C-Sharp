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
driver.get('https://lms.ecornell.com/courses/1706761/files/119335121?module_item_id=26344333')
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