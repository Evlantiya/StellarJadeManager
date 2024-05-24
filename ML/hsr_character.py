from enum import Enum
import json

# class Gender(Enum):
#     MALE = 1
#     FEMALE = 2

# class Element(Enum):
#     PHYSICAL = 1
#     FIRE = 2
#     ICE = 3
#     LIGHTNING = 4
#     WIND = 5
#     QUANTUM = 6
#     IMAGINARY = 7 

# class Path(Enum):
#     DESTRUCTION = 1
#     HUNT = 2
#     ERUDITION = 3
#     HARMONY = 4
#     NIHILITY = 5
#     PRESERVATION = 6
#     ABUNDANCE = 7 

# class Faction(Enum):
#     Herta_Space_Station = 1
#     Belobog = 2
#     Xianzhou_Luofu = 3
#     Stellaron_Hunters = 4
#     IPC = 5
#     Knights_of_Beauty = 6
#     Intelligentsia_Guild = 7
#     Garden_of_Recollection = 8
#     Masked_Fools = 9
#     Self_Annihilators = 10

        

class Item:
    def __init__(self, id, name, type, rarity, banners):
        self.id = id
        self.name = name
        self.type = type
        self.rarity = rarity
        self.banners = banners
    
    # def set_stats(self, stats):
    #     self.stats=stats

    # def set_user_rating(self, rating):
    #     self.rating=rating

# class PrimaryStats:
#     def __init__(self, hp, atk, defence, speed):
#         self.hp = hp
#         self.atk=atk
#         self.defence=defence
#         self.speed=speed
# class UserRating:
#     def __init__(self, PFRating, MoCRating):
#         self.PF = PFRating
#         self.MoC = MoCRating
        

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

    # for char in chars:
    #     name = char.name.lower().replace('.','').replace(' ','-')
    #     if char.name == "Dan Heng • Imbibitor Lunae":
    #         name = "imbibitor-lunae"
    #     if char.name == "Topaz and Numby":
    #         name = "topaz"
    #     with open(f"data/prydwen/{name}.json", 'r') as f:
    #         data=json.load(f)["result"]["data"]["currentUnit"]["nodes"][0]
    #         stats = data["stats"]
    #         rating = data["ratings"]
    #         p_stats = PrimaryStats(stats["hp_base"], stats["atk_base"], stats["def_base"], stats["speed_base"])
    #         user_rating = UserRating(rating["pure"], rating["moc"])
    #         char.set_stats(p_stats)
    #         char.set_user_rating(user_rating)



