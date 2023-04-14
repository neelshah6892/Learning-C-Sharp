from selenium.webdriver.common.by import By
from seleniumwire import webdriver  # Import from seleniumwire
import time
import re
import os

def findStartLesson():
   lnks=driver.find_elements(By.CLASS_NAME, "startLesson")
   for lnk in lnks:
   # get_attribute() to get all href
      file = open("D:/Ecornell/"+directory+"/"+directory+" Course Links.txt", "a+")
      print(lnk.get_attribute('href'))
      file.write(lnk.get_attribute('href')+ "\n")
      file.close()


options = webdriver.ChromeOptions()
driver = webdriver.Chrome(chrome_options=options)

url = "https://ondemand.ecornell.com/"

driver.get(url)
time.sleep(60)

driver.find_element(By.XPATH, '//*[@id="term_skill"]/a[2]').click()
skills = driver.find_elements(By.XPATH, '//*[@id="term_skill"]/ul/li')
#skillsNo = len(skills)

count = 1

for item in skills:
   print(item.text)
   skillsName = re.sub(r'[^a-zA-Z0-9\s]+', '', item.text)
   directory = skillsName
   parent_dir = "D:/Ecornell/"
   path = os.path.join(parent_dir, directory)
   os.mkdir(path)
   driver.find_element(By.ID, 'skill'+str(count)).click()
   print(count)
   time.sleep(20)
   if len(driver.find_elements(By.CLASS_NAME, 'paginatePage')) != 0:
   #pagesLen = len(pages)
      pages = driver.find_elements(By.CLASS_NAME, 'paginatePage')
      pagesLen = pages[-1:][0].text
      print(pagesLen)

      for page in range(int(pagesLen)):
         findStartLesson()
         time.sleep(5)
         if driver.find_element(By.CLASS_NAME, 'paginateNext'):
            driver.find_element(By.CLASS_NAME, 'paginateNext').click()
            time.sleep(30)
         else:
            pass
   else:
      findStartLesson()
      time.sleep(30)

     
   time.sleep(30)
   driver.find_element(By.ID, 'skill'+str(count)).click()
   time.sleep(30)
   count+=1
