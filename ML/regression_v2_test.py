import json
import os
import hsr_character
import matplotlib.pyplot as plt
from pandas import read_csv, DataFrame, concat
from sklearn.neighbors import KNeighborsRegressor
from sklearn.linear_model import LinearRegression, LogisticRegression
from sklearn.svm import SVR
from sklearn.ensemble import RandomForestRegressor
from sklearn.metrics import r2_score
from sklearn.model_selection import train_test_split

# data = DataFrame(columns=['Pull number','Gender', 'Faction', 'Element','Path', 'Banner count', 'Legendary chance', 'Win percent'])
characters = hsr_character.get_characters()

rows=[]
hren = []
for character in characters:
    banner_count = 1
    for banner_id in character.banners:
        with open(f"ML/data/srstation/data_{banner_id}.json", 'r') as f:
            banner_data=json.load(f)["stats"]
            h_row = {'Users': banner_data["users"],
                       'Total pulls': banner_data["total_pulls"],
                       'MoC':character.rating.MoC,
                       'PF':character.rating.PF,
                       'Female': 1 if character.gender == hsr_character.Gender.FEMALE else 0,
                        'HP':character.stats.hp,
                        'ATK':character.stats.atk,
                        'DEF':character.stats.defence,
                        'SPEED':character.stats.speed,
                        'Banner count': banner_count,
                        'Win percent': banner_data["count_win_5"]/(banner_data["count_win_5"]+banner_data["count_lose_5"])
                    }
            element_variable = {elem.name: (1 if character.element == elem else 0) for elem in hsr_character.Element }
            path_variable = {path.name: (1 if character.path == path else 0) for path in hsr_character.Path }
            #для дамми переменных в модели регрессии, нужно на 1 переменную меньше. Иначе будет строгая коллинеарность
            element_variable.pop(hsr_character.Element.IMAGINARY.name) 
            path_variable.pop(hsr_character.Path.ABUNDANCE.name) 
            for pull_num in range(1,84):
                row = {'Users': banner_data["users"],
                       'Total pulls': banner_data["total_pulls"],
                       'Pull number': pull_num,
                       'MoC':character.rating.MoC,
                       'PF':character.rating.PF,
                       'Female': 1 if character.gender == hsr_character.Gender.FEMALE else 0,
                        'HP':character.stats.hp,
                        'ATK':character.stats.atk,
                        'DEF':character.stats.defence,
                        'SPEED':character.stats.speed,
                        'Banner count': banner_count,
                        'Legendary chance': banner_data["by_rollnum_chance_5"].get(str(pull_num),0) ,
                        'Win percent': banner_data["count_win_5"]/(banner_data["count_win_5"]+banner_data["count_lose_5"])
                    }
                rows.append(row | element_variable | path_variable)
            row_90 = {'Users': banner_data["users"],
                       'Total pulls': banner_data["total_pulls"],
                       'Pull number': 90,
                       'MoC':character.rating.MoC,
                       'PF':character.rating.PF,
                       'Female': 1 if character.gender == hsr_character.Gender.FEMALE else 0,
                        'HP':character.stats.hp,
                        'ATK':character.stats.atk,
                        'DEF':character.stats.defence,
                        'SPEED':character.stats.speed,
                        'Banner count': banner_count,
                        'Legendary chance':1,
                        'Win percent': banner_data["count_win_5"]/(banner_data["count_win_5"]+banner_data["count_lose_5"])
                    }
            rows.append(row_90 | element_variable | path_variable)
            hren.append(h_row | element_variable | path_variable)
        banner_count+=1

data = DataFrame(rows)
data_winrate = DataFrame(hren)

plt.scatter(data['Pull number'],data['Legendary chance'])
plt.plot(range(1,74),[0.0067 for i in range(1,74)], color='red')
plt.plot(range(74,91),[-4.15 + 0.057*i for i in range(74,91)], color='red')
plt.xlabel(r'$Номер$', fontsize=16)
plt.ylabel(r'$Вероятность$', fontsize=16)
plt.show()

# data.to_csv('ML/data_after_73.csv', index=False)
# data_winrate.to_csv('ML/data_winrate.csv', index=False)

                



# for filename in os.listdir('ML/data/srstation'):
#         with open(f"ML/data/srstation/{filename}", 'r') as f:
#             data=json.load(f)
#             print(1)
#         break