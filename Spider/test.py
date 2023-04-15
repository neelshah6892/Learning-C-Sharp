import os
import re

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

list = ["apple", "banana", "cherry"]

for item in list:
    print(item)
    CreateSubFolder(item, path)

print(path)