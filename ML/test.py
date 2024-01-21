import numpy as np
import pandas as pd
import json
import os
import matplotlib.pyplot as plt
from sklearn.linear_model import LinearRegression
from sklearn.preprocessing import PolynomialFeatures



data_dict= dict.fromkeys([x+1 for x in range(200)],(0,0))
    # for filename in os.listdir('ML/data/starrail_station'):
    #     with open(f"ML/data/starrail_station/{filename}", 'r') as f:
    #         data = json.load(f)
    #         by_rollnum_pulls_all=data['stats']['by_rollnum_pulls_all']
    #         by_rollnum_pulls_5=data['stats']['by_rollnum_pulls_5']
    #         for roll_num in range(1,201):
    #             all_pulls = by_rollnum_pulls_all[f"{roll_num}"] if (f"{roll_num}" in by_rollnum_pulls_all) else 0
    #             five_star_pulls = by_rollnum_pulls_5[f"{roll_num}"] if (f"{roll_num}" in by_rollnum_pulls_5) else 0
    #             data_tuple=(all_pulls,five_star_pulls)
    #             data_dict[roll_num] = tuple(map(sum, zip(data_dict[roll_num],data_tuple)))
for filename in os.listdir('ML/data/paimon_moe'):
    with open(f"ML/data/paimon_moe/{filename}", 'r') as f:
        data=json.load(f)
        countEachPity = data["countEachPity"]
        legendaryPityCount = data["pityCount"]["legendary"][1:91]
        dataTuplesList=[(allPulls,legendaryPulls) for allPulls,legendaryPulls in zip(countEachPity,legendaryPityCount)]
        # print("lol")
        for i in range(len(dataTuplesList)):
            data_dict[i+1] = tuple(map(sum, zip(data_dict[i+1],dataTuplesList[i])))
        # print("lol")





five_star_chances_by_rollnum = {rollnum: rollnum_tuple[1]*100/rollnum_tuple[0] for rollnum, rollnum_tuple in data_dict.items() if rollnum_tuple[0] > 0 }
rollnum_array=[]
chances_array=[]
print(five_star_chances_by_rollnum)

#THIS IS BAD, LACK OD DATA ON THESE PITY
five_star_chances_by_rollnum[85]+=2.3
five_star_chances_by_rollnum[86]+=8.3
five_star_chances_by_rollnum[87]+=23.6
five_star_chances_by_rollnum[88]+=41.1
five_star_chances_by_rollnum[89]+=42
five_star_chances_by_rollnum[90]=100.0


for rn, c in five_star_chances_by_rollnum.items():
    rollnum_array.append(rn)
    chances_array.append(c)
X=np.array(rollnum_array[:90]).reshape(-1,1)
Y=np.array(chances_array[:90])
poly = PolynomialFeatures(degree=45, include_bias=False)
X_poly = poly.fit_transform(X)
poly_reg_model = LinearRegression()
poly_reg_model.fit(X_poly,Y)
X_vals = np.array([x+1 for x in range(90)]).reshape(-1,1)
X_vals_poly = poly.transform(X_vals)
Y_vals = poly_reg_model.predict(X_vals_poly)

plt.scatter(X,Y)
plt.scatter(X_vals,Y_vals)
plt.plot(X_vals,Y_vals,color='r')
plt.show()