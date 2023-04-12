from selenium.webdriver.common.by import By
from seleniumwire import webdriver  # Import from seleniumwire
import time
import re

options = webdriver.ChromeOptions()
driver = webdriver.Chrome(chrome_options=options)

url = "https://ondemand.ecornell.com/"
# Go to the Course : Adopting Inclusive Hiring Practices- Create Inclusive Interview Practices
driver.get(url)
time.sleep(60)

#NEW TAB
new_url = "https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3"
driver.execute_script("window.open('');")
driver.switch_to.window(driver.window_handles[1])
driver.get(new_url)
time.sleep(60)
driver.close()
driver.switch_to.window(driver.window_handles[0])
time.sleep(30)

#element = driver.find_element(By.NAME, 'uk-card-title')
#element.click()

# resultSet = driver.find_element(By.XPATH, '//*[@id="term_skill"]/ul/')
# options = resultSet.find_elements(By.TAG_NAME, 'li')

# for option in options:
#     print(option.text)


#//*[@id="term_skill"]/ul/li[1]/div/label
#//*[@id="ecCards"]/div[26]/div[4]
#class="uk-card-title"

#F/On Demand/Skill/Courses/Content
#https://www.geeksforgeeks.org/opening-and-closing-tabs-using-selenium/

skills = driver.find_elements(By.XPATH, '//*[@id="term_skill"]/ul/li')

for item in skills:
   print(item)

lnks=driver.find_elements(By.CLASS_NAME, "startLesson")
for lnk in lnks:
   # get_attribute() to get all href
   file = open("D:/links.txt", "a")
   print(lnk.get_attribute('href'))
   file.write(lnk.get_attribute('href')+ "\n")
   file.close()

if driver.find_element(By.CLASS_NAME, 'paginateNext'):
   driver.find_element(By.CLASS_NAME, 'paginateNext').click()

lnks=driver.find_elements(By.CLASS_NAME, "startLesson")
for lnk in lnks:
   # get_attribute() to get all href
   file = open("D:/links.txt", "a")
   print(lnk.get_attribute('href'))
   file.write(lnk.get_attribute('href')+ "\n")
   file.close()