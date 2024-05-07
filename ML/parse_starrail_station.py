import requests
import json


print('start')
# The API endpoint
url = "https://starrailstation.com/api/v1/warp_fetch/"

firstBannerId=2003
lastBannerId=2026
# bannersInfo=[]
# response = requests.get(f'{url}{firstBannerId}')
# A GET request to the API
for id in range(firstBannerId,lastBannerId+1):
    response = requests.get(f'{url}{id}')
    # bannersInfo.append(response.json())
    with open(f'ML/data/data_{id}.json', 'w') as f:
        json.dump(response.json() ,f, indent=2)

# for i in range(len(bannersInfo)):
#     with open(f'data_{i+3}.json', 'w') as f:
#         json.dump(bannersInfo[i],f, indent=2)
