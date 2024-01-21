import requests
import json


print('start')
# The API endpoint
url = "https://api.paimon.moe/wish?banner="

firstBannerId=300009
lastBannerId=300059
bannersInfo=[]
# response = requests.get(f'{url}{firstBannerId}')
# A GET request to the API
for id in range(firstBannerId,lastBannerId+1):
    response = requests.get(f'{url}{id}')
    bannersInfo.append(response.json())

for i in range(len(bannersInfo)):
    with open(f'data_{i+9}.json', 'w') as f:
        json.dump(bannersInfo[i],f)
