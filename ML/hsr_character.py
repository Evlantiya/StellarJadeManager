from enum import Enum
import json

class Gender(Enum):
    MALE = 1
    FEMALE = 2

class Element(Enum):
    PHYSICAL = 1
    FIRE = 2
    ICE = 3
    LIGHTNING = 4
    WIND = 5
    QUANTUM = 6
    IMAGINARY = 7 

class Path(Enum):
    DESTRUCTION = 1
    HUNT = 2
    ERUDITION = 3
    HARMONY = 4
    NIHILITY = 5
    PRESERVATION = 6
    ABUNDANCE = 7 

class Faction(Enum):
    Herta_Space_Station = 1
    Belobog = 2
    Xianzhou_Luofu = 3
    Stellaron_Hunters = 4
    IPC = 5
    Knights_of_Beauty = 6
    Intelligentsia_Guild = 7
    Garden_of_Recollection = 8
    Masked_Fools = 9
    Self_Annihilators = 10

class Character:
    def __init__(self, name, gender, faction, element, path, *banners):
        self.name = name
        self.gender = gender
        self.faction = faction
        self.element = element
        self.path = path
        self.banners = banners
    
    def set_stats(self, stats):
        self.stats=stats

    def set_user_rating(self, rating):
        self.rating=rating

class PrimaryStats:
    def __init__(self, hp, atk, defence, speed):
        self.hp = hp
        self.atk=atk
        self.defence=defence
        self.speed=speed
class UserRating:
    def __init__(self, PFRating, MoCRating):
        self.PF = PFRating
        self.MoC = MoCRating
        

def get_characters():
    chars = [
        Character("Seele", Gender.FEMALE, Faction.Belobog, Element.QUANTUM, Path.HUNT, 2003, 2013),
        Character("Jing Yuan", Gender.MALE, Faction.Xianzhou_Luofu, Element.LIGHTNING, Path.ERUDITION, 2004, 2024),
        Character("Silver Wolf", Gender.FEMALE, Faction.Stellaron_Hunters, Element.QUANTUM, Path.NIHILITY, 2005, 2016),
        Character("Luocha", Gender.MALE, Faction.Xianzhou_Luofu, Element.IMAGINARY, Path.ABUNDANCE, 2006, 2026),
        Character("Blade", Gender.MALE, Faction.Stellaron_Hunters, Element.WIND, Path.DESTRUCTION, 2007, 2018),
        Character("Kafka", Gender.FEMALE, Faction.Stellaron_Hunters, Element.LIGHTNING, Path.NIHILITY, 2008, 2020),
        Character("Dan Heng • Imbibitor Lunae", Gender.MALE, Faction.Xianzhou_Luofu, Element.IMAGINARY, Path.DESTRUCTION, 2009, 2022),
        Character("Fu Xuan", Gender.FEMALE, Faction.Xianzhou_Luofu, Element.QUANTUM, Path.PRESERVATION, 2010),
        Character("Jingliu", Gender.FEMALE, Faction.Xianzhou_Luofu, Element.ICE, Path.DESTRUCTION, 2011),
        Character("Topaz and Numby", Gender.FEMALE, Faction.IPC, Element.FIRE, Path.HUNT, 2012),
        Character("Huohuo", Gender.FEMALE, Faction.Xianzhou_Luofu, Element.WIND, Path.ABUNDANCE, 2014),
        Character("Argenti", Gender.MALE, Faction.Knights_of_Beauty, Element.PHYSICAL, Path.ERUDITION, 2015),
        Character("Ruan Mei", Gender.FEMALE, Faction.Herta_Space_Station, Element.ICE, Path.HARMONY, 2017),
        Character("Dr. Ratio", Gender.MALE, Faction.Intelligentsia_Guild, Element.IMAGINARY, Path.HUNT, 2019),
        Character("Black Swan", Gender.FEMALE, Faction.Garden_of_Recollection, Element.WIND, Path.NIHILITY, 2021),
        Character("Sparkle", Gender.FEMALE, Faction.Masked_Fools, Element.QUANTUM, Path.HARMONY, 2023),
        Character("Acheron", Gender.FEMALE, Faction.Self_Annihilators, Element.LIGHTNING, Path.NIHILITY, 2025),
    ]

    for char in chars:
        name = char.name.lower().replace('.','').replace(' ','-')
        if char.name == "Dan Heng • Imbibitor Lunae":
            name = "imbibitor-lunae"
        if char.name == "Topaz and Numby":
            name = "topaz"
        with open(f"ML/data/prydwen/{name}.json", 'r') as f:
            data=json.load(f)["result"]["data"]["currentUnit"]["nodes"][0]
            stats = data["stats"]
            rating = data["ratings"]
            p_stats = PrimaryStats(stats["hp_base"], stats["atk_base"], stats["def_base"], stats["speed_base"])
            user_rating = UserRating(rating["pure"], rating["moc"])
            char.set_stats(p_stats)
            char.set_user_rating(user_rating)

    return chars  


