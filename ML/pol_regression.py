import numpy as np
import pandas as pd
import json
import os
import matplotlib.pyplot as plt
from sklearn.linear_model import LinearRegression
from sklearn.preprocessing import PolynomialFeatures
from sklearn.model_selection import train_test_split
from sklearn.metrics import mean_squared_error
import math

data_dict= dict.fromkeys([x+1 for x in range(200)],(0,0))
for filename in os.listdir('ML/data'):
    with open(f"ML/data/{filename}", 'r') as f:
        data = json.load(f)
        by_rollnum_pulls_all=data['stats']['by_rollnum_pulls_all']
        by_rollnum_pulls_5=data['stats']['by_rollnum_pulls_5']
        for roll_num in range(1,201):
            all_pulls = by_rollnum_pulls_all[f"{roll_num}"] if (f"{roll_num}" in by_rollnum_pulls_all) else 0
            five_star_pulls = by_rollnum_pulls_5[f"{roll_num}"] if (f"{roll_num}" in by_rollnum_pulls_5) else 0
            data_tuple=(all_pulls,five_star_pulls)
            data_dict[roll_num] = tuple(map(sum, zip(data_dict[roll_num],data_tuple)))

five_star_chances_by_rollnum = {rollnum: rollnum_tuple[1]*100/rollnum_tuple[0] for rollnum, rollnum_tuple in data_dict.items() if rollnum_tuple[0] > 0 }
rollnum_array=[]
chances_array=[]
for rn, c in five_star_chances_by_rollnum.items():
    rollnum_array.append(rn)
    chances_array.append(c)
X=np.array(rollnum_array[:90]).reshape(-1,1)
Y=np.array(chances_array[:90])
print(X)
# X_train, X_test, y_train, y_test = train_test_split(X, Y, test_size=0.2, random_state=67)
poly = PolynomialFeatures(degree=20, include_bias=False)

X_poly = poly.fit_transform(X)
# X_train_poly = poly.fit_transform(X_train)
# X_test_poly = poly.transform(X_test)
poly_reg_model = LinearRegression()
poly_reg_model.fit(X_poly,Y)
# poly_reg_model.fit(X_train_poly, y_train)
# y_pred = poly_reg_model.predict(X_test_poly)
# rmse = np.sqrt(mean_squared_error(y_test, y_pred))
# print('Root Mean Squared Error:', rmse)
X_vals = np.array([x+1 for x in range(90)]).reshape(-1,1)
print(X_vals)
X_vals_poly = poly.transform(X_vals)
Y_vals = poly_reg_model.predict(X_vals_poly)
print(Y_vals)
# data = pd.read_csv('data.csv')
X_test = 80
X_test_poly = poly.transform(np.array([X_test]).reshape(-1,1))
Y_test = poly_reg_model.predict(X_test_poly)
plt.scatter(X,Y)
plt.plot(X_vals,Y_vals, color="r")
plt.scatter(np.array([X_test]).reshape(-1,1),Y_test, color="g")
plt.show()