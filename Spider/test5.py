import os
import pyperclip

file = open('C:\\Users\\dhwan\\Desktop\\Not Working.txt', 'r+')

content = file.readlines()

copyline = content[0]

pyperclip.copy(copyline)

file2 = open('C:\\Users\\dhwan\\Desktop\\test.txt', 'a')

pasteline = pyperclip.paste()

content2 = file2.write(pasteline)

