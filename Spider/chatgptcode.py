from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
import time

# Replace 'path_to_chromedriver' with the path to your ChromeDriver executable
driver = webdriver.Chrome(executable_path="D:/Neel/Github/Learning-C-Sharp/Spider/chromedriver/chromedriver.exe")

url_login = "https://ondemand.ecornell.com"
driver.get(url_login)

# Assuming there are input fields with 'username' and 'password' ids for login
#Login
email = driver.find_element(By.CSS_SELECTOR, "#username")
email.send_keys("cornell@hipointservices.com")
driver.find_element(By.CSS_SELECTOR, "#fm1 > div:nth-child(4) > input").click()
password = driver.find_element(By.CSS_SELECTOR, "#password")
password.send_keys("$anketShah81")
driver.find_element(By.CSS_SELECTOR, "#fm1 > div:nth-child(4) > input").click()
time.sleep(15)

url_target_page = "https://lms.ecornell.com/courses/1664091/pages/ask-the-activists-aaron-bartley-and-harper-bishop?module_item_id=23989764"
driver.get(url_target_page)

# Assuming video links are identified by the 'a' tag and have 'href' attribute
video_links = driver.find_elements(By.TAG_NAME, 'a')
video_urls = [link.get_attribute('href') for link in video_links]

# Filter out links that are not video URLs (optional)
video_urls = [url for url in video_urls if url.endswith('.mp4') or url.endswith('.webm')]

for video_url in video_urls:
    print(video_url)

