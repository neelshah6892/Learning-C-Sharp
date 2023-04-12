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
# Go to the Course : Adopting Inclusive Hiring Practices- Create Inclusive Interview Practices
driver.get(url)
time.sleep(60)

#NEW TAB
# new_url = "https://ondemand.ecornell.com/lesson.do?lessonCode=ILR562OD3"
# driver.execute_script("window.open('');")
# driver.switch_to.window(driver.window_handles[1])
# driver.get(new_url)
# time.sleep(30)
# driver.close()
# driver.switch_to.window(driver.window_handles[0])
# time.sleep(30)

# resultSet = driver.find_element(By.XPATH, '//*[@id="term_skill"]/ul/')
# options = resultSet.find_elements(By.TAG_NAME, 'li')

# for option in options:
#     print(option.text)


#//*[@id="term_skill"]/ul/li[1]/div/label
#//*[@id="ecCards"]/div[26]/div[4]
#class="uk-card-title"

#F/On Demand/Skill/Courses/Content
#https://www.geeksforgeeks.org/opening-and-closing-tabs-using-selenium/

driver.find_element(By.XPATH, '//*[@id="term_skill"]/a[2]').click()
skills = driver.find_elements(By.XPATH, '//*[@id="term_skill"]/ul/li')

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
   findStartLesson()
   if driver.find_element(By.CLASS_NAME, 'paginateNext'):
      driver.find_element(By.CLASS_NAME, 'paginateNext').click()
      time.sleep(30)
      findStartLesson()
   else:
      driver.find_element(By.ID, 'skill'+str(count)).click()
      pass

   count+=1
