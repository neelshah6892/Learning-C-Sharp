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
    licount = 1
    for i in driver.find_elements(By.XPATH, '//*[@id="sticky-container"]/nav/ul[2]/li/ul/li['+str(licount)+']/span/div/a'):
        print(str(i.get_attribute('href')))
        print(i.get_attribute('href'))
        licount+=1
    
    menu = driver.find_elements(By.XPATH, '//*[@id="sticky-container"]/nav/ul[2]/li/ul')
    for item in menu:
        print(item.text)

    if driver.find_element(By.CLASS_NAME, 'module-sequence-footer-button--next'):
        RunAll()
    else:
        RunAll()
        driver.close()
        driver.switch_to.window(driver.window_handles[0])
