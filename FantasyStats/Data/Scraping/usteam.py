from selenium import webdriver
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
import urllib.request
import re
import sys
import time


yearMap = {"2019":274,"2018":210,"2017":79,"2016":54,"2015":42,"2014":27}
filepath = "../filedata/"
year = sys.argv[1]
leagueName = sys.argv[2]

driverUS = webdriver.Chrome()
urlUS = "https://understat.com/league/" + leagueName + "/" + str(year)  
driverUS.get(urlUS)
#driverUS.maximize_window()

content = driverUS.find_element_by_id('league-chemp').text

content = content.replace(" ", ",")
content = content.replace("Manchester,City", "Manchester City")
content = content.replace("Manchester,United", "Manchester United")
content = content.replace("Newcastle,United", "Newcastle United")
content = content.replace("Sheffield,United", "Sheffield United")
content = content.replace("Crystal,Palace", "Crystal Palace")
content = content.replace("West,Ham", "West Ham")
content = content.replace("West,Bromwich,Albion", "West Bromwich Albion")
content = content.replace("Wolverhampton,Wanderers", "Wolverhampton Wanderers")
content = content.replace("Aston,Villa", "Aston Villa")
content = content.replace("Queens,Park,Rangers", "Queens Park Rangers")

fname = "../filedata/team_us.csv"

with open(fname, "w", encoding="utf-8") as file:
	file.write(content)
		
print("team_us.csv")

urlFpl = "https://www.premierleague.com/stats/top/clubs/clean_sheet?se=" + str(yearMap[year])

driverUS.get(urlFpl)
time.sleep(10)

content = driverUS.find_element_by_xpath('//*[@id="mainContent"]/div/div/div[2]/div[1]/div[2]/table/tbody').text

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
content = content.replace("Sheffield,United", "Sheffield United")
content = content.replace(".","")

fname = "../filedata/team_fpl.csv"

with open(fname, "w", encoding="utf-8") as file:
	file.write(content)

driverUS.quit()
