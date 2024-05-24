import json
import pyperclip
import hsr_character


class Banner:
    def __init__(self, id, bannerItems):
        self.id = id
        self.Items=bannerItems

    def to_dict(self):
        return {
            f'id': self.id,
            'items': [item.to_dict() for item in self.Items],
        }

class Item:
    def __init__(self, id, name, type, rarity, banners):
        self.id = id
        self.name = name
        self.type = type
        self.rarity = rarity
        self.banners = banners
    def to_dict(self):
        return {
            'id': self.id,
            'name': self.name, 
            'type': self.type ,
            'rarity':self.rarity
        }
        

def get_characters():
    chars = [
        Item(1102,"Seele", "Character", 5, [2003, 2013]),
        Item(1204,"Jing Yuan", "Character", 5, [2004, 2024]),
        Item(1006,"Silver Wolf", "Character", 5, [2005, 2016]),
        Item(1203,"Luocha", "Character", 5, [2006, 2026]),
        Item(1205,"Blade", "Character", 5, [2007, 2018]),
        Item(1005,"Kafka", "Character", 5, [2008, 2020]),
        Item(1213,"Dan Heng: Imbibitor Lunae", "Character", 5, [2009, 2022]),
        Item(1208,"Fu Xuan", "Character", 5, [2010]),
        Item(1212,"Jingliu", "Character", 5, [2011, 2028]),
        Item(1112,"Topaz and Numby", "Character", 5, [2012, 2030]),
        Item(1217,"Huohuo", "Character", 5, [2014]),
        Item(1302,"Argenti", "Character", 5, [2015]),
        Item(1303,"Ruan Mei", "Character", 5, [2017]),
        Item(1305,"Dr. Ratio", "Character", 5, [2019]),
        Item(1307,"Black Swan", "Character", 5, [2021]),
        Item(1306,"Sparkle", "Character", 5, [2023]),
        Item(1308,"Acheron", "Character", 5, [2025]),
        Item(1304,"Aventurine", "Character", 5, [2027]),
        Item(1309,"Robin", "Character", 5, [2029]),
    ]
    return chars  



characters = get_characters()

banner_ids = []

Banners = []

for item in characters:
    banner_ids.extend(item.banners)

all_items = []

for id in banner_ids:
    with open(f'data/srstation/data_{id}.json', 'r') as f:
        data = json.load(f)

        for id_5 in data["stats"]["pulls_5"]:
            hren = [char for char in characters if id in char.banners and int(id_5) == data["stats"]["rateup"]]
            item_5 = Item(id_5, hren[0].name if hren else "" , "Character", 5, [])
            all_items.append(item_5)
        for id_4 in data["stats"]["pulls_4"]:
            item_4 = Item(id_4, "", "Character" if len(id_4) == 4 else "Light Cone", 4, [])
            all_items.append(item_4)

        rateup_5_itemId = data["stats"]["rateup"]
        rateup_4_itemsId = sorted(data["stats"]["pulls_4"].items(), key=lambda item: item[1], reverse=True)[:3]
        bannerItems = []
        bannerItems.append(Item(rateup_5_itemId, [char for char in characters if id in char.banners][0].name, "Character", 5, []))

        for rateup_4_id,value in rateup_4_itemsId:
            bannerItems.append(Item(int(rateup_4_id),"", "Character" , 4, []))

        Banners.append(Banner(id, bannerItems))

for id in [id + 1000 for id in banner_ids]:
    with open(f'data/srstation/lc/data_{id}.json', 'r') as f:
        data = json.load(f)

        for id_5 in data["stats"]["pulls_5"]:
            # hren = [char for char in characters if id in char.banners and int(id_5) == data["stats"]["rateup"]]
            item_5 = Item(id_5,"" , "Light Cone", 5, [])
            all_items.append(item_5)
        for id_4 in data["stats"]["pulls_4"]:
            item_4 = Item(id_4, "", "Character" if len(id_4) == 4 else "Light Cone", 4, [])
            all_items.append(item_4)


        rateup_5_itemId = data["stats"]["rateup"]
        rateup_4_itemsId = sorted(data["stats"]["pulls_4"].items(), key=lambda item: item[1], reverse=True)[:3]
        bannerItems = []
        bannerItems.append(Item(rateup_5_itemId, "", "Light cone", 5, []))

        for rateup_4_id,value in rateup_4_itemsId:
            bannerItems.append(Item(int(rateup_4_id),"", "Light cone" , 4, []))

        Banners.append(Banner(id, bannerItems))



for banner in Banners:
    # all_items.extend(banner.Items)
    with open(f'data/banners/{banner.id}.json', 'w') as file:
        json.dump(banner.to_dict(), file, indent=4)

used_id = []
unique_items = []

for item in all_items:
    if item.id in used_id:
        continue
    used_id.append(item.id)
    unique_items.append(item.to_dict())

with open(f'data/items/items.json', 'w') as file:
        json.dump(unique_items, file, indent=4)



# for character in characters:
#     bannerId = character.banners[0]
#     with open(f'data/srstation/data_{bannerId}.json', 'r') as f:
#         data = json.load(f)
#         characterId = data["stats"]["rateup"]
#     result.append(f"{character.name} = {characterId}")


# pyperclip.copy(hren)