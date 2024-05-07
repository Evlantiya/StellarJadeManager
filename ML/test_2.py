# data_dict= dict.fromkeys([x+1 for x in range(200)],(0,0))
import numpy as np
import pandas as pd
import json
import os
import matplotlib.pyplot as plt


# df = pd.DataFrame(
#     {
#         "pull_num": list(range(1,91)),
#         "legendary_pull_count":list(range(90)),
#         "relative_freq": list(range(90))
#     }
# )

pull_num = list(range(1,91))

legendary_pull_count = [0 for i in range(90)]


for filename in os.listdir('ML/data/starrail_station'):
    with open(f"ML/data/starrail_station/{filename}", 'r') as f:

        data=json.load(f)
        rollnum_5 = data["stats"]["by_rollnum_pulls_5"]
        
        for pull in pull_num:
            legendary_pull_count[pull-1]+= rollnum_5.get(str(pull), 0)
        
            
        # countEachPity = data["countEachPity"]
        # legendaryPityCount = data["pityCount"]["legendary"][1:91]
        # dataTuplesList=[(allPulls,legendaryPulls) for allPulls,legendaryPulls in zip(countEachPity,legendaryPityCount)]
        # # print("lol")
        # for i in range(len(dataTuplesList)):
        #     data_dict[i+1] = tuple(map(sum, zip(data_dict[i+1],dataTuplesList[i])))vvvv
            
s = sum(legendary_pull_count)

relative_freq = [legendary_pull_count[i]*100/s for i in range(90)]

cumulative_freq = [relative_freq[0] for i in range(90)]

for i in range(1,90):
    cumulative_freq[i]=cumulative_freq[i-1]+relative_freq[i]

plt.plot(pull_num,cumulative_freq)
plt.scatter(pull_num,cumulative_freq)
plt.axvline(x=73, color='r', linestyle='--')
plt.annotate('Номер крутки 73', xy=(73, 40), xytext=(75, 42),
             arrowprops=dict(facecolor='red', shrink=0.05))
plt.xlabel(r'$Номер$', fontsize=16)
plt.ylabel(r'$Вероятность$', fontsize=16)
plt.title('Эмпирическая функция распределения')
plt.show()
# print("lol") 