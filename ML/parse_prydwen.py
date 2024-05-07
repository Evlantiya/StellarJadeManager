import requests
import json
import hsr_character

characters = hsr_character.get_characters()

for char in characters:
    name = char.name.lower().replace('.','').replace(' ','-')
    if char.name == "Dan Heng • Imbibitor Lunae":
        name = "imbibitor-lunae"
    if char.name == "Topaz and Numby":
        name = "topaz"
    print(name)
    response = requests.get(f"https://www.prydwen.gg/page-data/star-rail/characters/{name}/page-data.json")
    with open(f'ML/data/prydwen/{name}.json', 'w') as f:
        json.dump(response.json() ,f, indent=2)

# The API endpoint
# url = "https://starrailstation.com/api/v1/warp_fetch/"

# firstBannerId=2003
# lastBannerId=2026
# # bannersInfo=[]
# # response = requests.get(f'{url}{firstBannerId}')
# # A GET request to the API
# for id in range(firstBannerId,lastBannerId+1):
#     # bannersInfo.append(response.json())
