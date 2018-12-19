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
import time

yearMap = {"2018":210,"2017":79,"2016":54,"2015":42,"2014":27}
year = sys.argv[1]

driverUS = webdriver.Chrome()
urlFpl = "https://www.premierleague.com/stats/top/clubs/saves?se="  + str(yearMap[year])

driverUS.get(urlFpl)
time.sleep(10)

content = driverUS.find_element_by_xpath('//*[@id="mainContent"]/div/div/div[2]/div[1]/div[2]').text

content = content.replace(" ", ",")
content = content.replace("Manchester,City", "Manchester City")
content = content.replace("Tottenham,Hotspur", "Tottenham")
content = content.replace("Manchester,United", "Manchester United")
content = content.replace("Newcastle,United", "Newcastle United")
content = content.replace("Crystal,Palace", "Crystal Palace")
content = content.replace("West,Ham", "West Ham")
content = content.replace("West,Bromwich,Albion", "West Bromwich Albion")
content = content.replace("Wolverhampton,Wanderers", "Wolverhampton Wanderers")
content = content.replace("Aston,Villa", "Aston Villa")
content = content.replace("AFC,Bournemouth", "Bournemouth")
content = content.replace("Brighton,and,Hove,Albion", "Brighton")
content = content.replace("Cardiff,City", "Cardiff")
content = content.replace("West Ham,United", "West Ham")
content = content.replace("Huddersfield,Town", "Huddersfield")
content = content.replace("Leicester,City", "Leicester")
content = content.replace("Swansea,City", "Swansea")
content = content.replace("Stoke,City", "Stoke")
content = content.replace("Hull,City", "Hull")
content = content.replace("Norwich,City", "Norwich")
content = content.replace("Queens,Park,Rangers", "Queens Park Rangers")
content = content.replace(".","")

fname = "../filedata/saves_fpl.csv"
print(content)
with open(fname, "w", encoding="utf-8") as file:
	file.write(content)

driverUS.quit()
