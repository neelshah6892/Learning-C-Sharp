from selenium.webdriver.common.by import By
from seleniumwire import webdriver  # Import from seleniumwire
import time
import re

options = webdriver.ChromeOptions()
driver = webdriver.Chrome(chrome_options=options)

# Go to the Course : Adopting Inclusive Hiring Practices- Create Inclusive Interview Practices
driver.get('https://ondemand.ecornell.com/')
time.sleep(30)

#element = driver.find_element(By.NAME, 'uk-card-title')
#element.click()

#//*[@id="term_skill"]/ul/li[1]/div/label
#//*[@id="ecCards"]/div[26]/div[4]
#class="uk-card-title"

skills = driver.find_elements(By.XPATH, '//*[@id="term_skill"]/ul/li')

for item in skills:
   print(item)

lnks=driver.find_elements(By.CLASS_NAME, "startLesson")
for lnk in lnks:
   # get_attribute() to get all href
   print(lnk.get_attribute('href'))