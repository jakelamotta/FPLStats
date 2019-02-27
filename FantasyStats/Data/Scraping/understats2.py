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

year = sys.argv[1]
leagueName = sys.argv[2]

filepath = "../filedata/"

driverUS = webdriver.Chrome()
urlUS = "https://understat.com/league/" + leagueName + "/" + str(year)
driverUS.get(urlUS)
#driverUS.maximize_window()

file = open('scraped.txt', 'w', encoding='utf-8')
i = 2

prevContent = ""

while i < 50:
	content = driverUS.find_element_by_id('league-players').text

	if (prevContent != content):
		file.write(content)

	prevContent = content
	
	element = driverUS.find_element_by_xpath('//*[@data-page=' + str(i) + ']')
	element.click()
	i+=1

fname= filepath + "stats_" + str(year) + ".csv"

file = open(fname, 'w', encoding='utf-8')
fileManualMatch = open(filepath + "notscraped" + str(year) + ".csv", "w+",encoding="utf-8")
file.write("PID,Player,Team,Apps,Minutes,Goals,Assists,xG,xA,xG90,xA90\n")

for line in open("scraped.txt", encoding='utf-8'):
	#line = line.replace("\+\d.\d\d\s"," ")
	line = re.sub(r"\+\d.\d\d\s", " ", line)
	line = re.sub(r"\-\d.\d\d\s", " ", line)
	if len(line)>43:
		
		#This is cause Understat puts Team1,Team2 in Team column when player has switched teams in January transfer window
		line = line.replace(", ", "--")
		line = line.replace(" ", ",", 1)

		line = line.replace(" Arsenal",",Arsenal")
		line = line.replace(" Bournemouth",",Bournemouth")
		line = line.replace(" Brighton",",Brighton")
		line = line.replace(" Burnley",",Burnley")
		line = line.replace(" Chelsea",",Chelsea")
		line = line.replace(" Crystal Palace",",Crystal Palace")
		line = line.replace(" Everton",",Everton")
		line = line.replace(" Huddersfield",",Huddersfield")
		line = line.replace(" Leicester",",Leicester")
		line = line.replace(" Liverpool",",Liverpool")
		line = line.replace(" Manchester City",",Manchester City")
		line = line.replace(" Manchester United",",Manchester United")
		line = line.replace(" Newcastle United",",Newcastle United")
		line = line.replace(" Southampton",",Southampton")
		line = line.replace(" Cardiff",",Cardiff")
		line = line.replace(" Swansea",",Swansea")
		line = line.replace(" Tottenham",",Tottenham")
		line = line.replace(" Watford",",Watford")
		line = line.replace(" Wolverhampton Wanderers",",Wolverhampton Wanderers")
		line = line.replace(" West Ham",",West Ham")
		line = line.replace(" Fulham",",Fulham")
		line = line.replace(" Aston Villa", "Aston Villa")
		line = line.replace(" West Bromwich Albion", "West Bromwich Albion")
		line = line.replace("Queens,Park,Rangers", "Queens Park Rangers")
		line = line.replace("Bernardo Silva", "Bernardo Mota Veiga de Carvalho e Silva")
		line = line.replace("Richarlison", "Richarlison de Andrade")
		line = line.replace("Felipe Anderson", "Felipe Anderson Pereira Gomes")
		line = line.replace("Lucas Moura", "Lucas Rodrigues Moura da Silva")
		line = re.sub(r"Patr.*cio", "Patricio", line)
		
		line = line.replace("Rui Patricio", "Rui Pedro Patricio")
		line = line.replace("de Gea", "De Gea")
		line = line.replace("Ederson", "Ederson Santana de Moraes")
		line = line.replace("Alisson", "Alisson Ramses Becker")
		line = line.replace("Son Heung-Min", "Heung-Min Son")
		line = line.replace("David Luiz", "David Luiz Moreira Marinho")
		line = line.replace("Ricardo Pereira", "Ricardo Domingos Barbosa Pereira")
		line = line.replace("Ben Chilwell", "Benjamin Chilwell")

		

		line = line.replace(" 0",",0")
		line = line.replace(" 1",",1")
		line = line.replace(" 2",",2")
		line = line.replace(" 3",",3")
		line = line.replace(" 4",",4")
		line = line.replace(" 5",",5")
		line = line.replace(" 6",",6")
		line = line.replace(" 7",",7")
		line = line.replace(" 8",",8")
		line = line.replace(" 9",",9")		



		if line.count(",") == 10:
			file.write(line)
		else:
			fileManualMatch.write(line)

driverUS.quit()

r = urllib.request.urlopen('https://fantasy.premierleague.com/drf/elements/').read()
soup = BeautifulSoup(r, features="html.parser")

with open("playerdata.json", "w", encoding="iso-8859-1") as file:
	for line in soup:
		
		line = re.sub(r"dos Santos Patr.*cio", "Patricio", line)
		file.write(line)

df_us = pd.read_csv(fname)
df = pd.read_json('playerdata.json')
df['Player'] = df[['first_name', 'second_name']].apply(lambda x: ' '.join(x), axis=1)
df.drop(df.columns[[0, 1, 3, 4, 6, 7, 8, 9, 10, 11, 12, 13, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 36, 40, 42, 43, 44, 45, 46, 47, 49, 50, 51, 52, 53, 54, 55]], axis=1, inplace=True)

result = pd.merge(df, df_us, on='Player')
result['Position'] = 'Unfilled'

finalfname= filepath + 'attackingreturn_' + str(year) +  '.csv'
result.to_csv(finalfname)

df = pd.read_pickle('atkreturn.pkl')

df = df[df.Minutes > 90]

df['xAR90pm'] = 0
df['xAR90pm'] = df['xAR90'] / (df['now_cost'] / 10.0)

df.to_csv('over90.csv')

df = df[df.Minutes > 90]

df_gks = df[df.element_type == 1]
print('attackingreturn_' + str(year) +  '.csv')
#plt.scatter((df_gks['now_cost']/10.0),df_gks['total_points'])
#plt.show()
