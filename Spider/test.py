import os
import re
from seleniumwire import webdriver
from selenium.webdriver.common.by import By
import codecs

parent_dir = "D:/Ecornell/Building Teams 33/"

starttitle = "Start Page"
path = os.path.join(parent_dir, starttitle)
os.mkdir(path)
print(path)
coursedir = os.path.dirname(path)
print(coursedir)

def CreateSubFolder(a, b):
    subpath = os.path.join(b, a)
    print(subpath)
    os.mkdir(subpath)

def SavePage(a):
    n = os.path.join(a, a+".html")
    f = codecs.open(n, "w", "utf−8")
    pageSource = driver.execute_script("return document.body.innerHTML;")
    f.write(pageSource)


list = ["apple", "banana", "cherry"]
options = webdriver.ChromeOptions()
driver = webdriver.Chrome(chrome_options=options)

driver.get('https://ondemand.ecornell.com')


for item in list:
    print(item)
    CreateSubFolder(item, path)
    SavePage(item)

print(path)