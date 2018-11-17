from selenium import webdriver
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from bs4 import BeautifulSoup
import urllib.request
import pandas as pd
import re
import matplotlib.pyplot as plt
import sys


filepath = "../filedata/"
year = 2017

driverUS = webdriver.Chrome()
urlUS = "https://understat.com/league/EPL/" + str(year)  
driverUS.get(urlUS)
#driverUS.maximize_window()

content = driverUS.find_element_by_id('league-chemp').text




content = content.replace(" ", ",")
content.replace("Manchester,City", "Manchester City")
content.replace("Manchester,United", "Manchester United")
content.replace("Newcastle,United", "Newcastle United")
content.replace("Crystal,Palace", "Crystal Palace")
content.replace("West,Ham", "West Ham")
content.replace("West,Bromwich,Albion", "West Bromwich Albion")
print(content)

driverUS.quit()
