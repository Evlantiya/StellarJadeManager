
import json
import os
from supabase import create_client, Client

url = "https://nyeujbavubnlpyjssdsa.supabase.co"
key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im55ZXVqYmF2dWJubHB5anNzZHNhIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTU0Mzg0MDAsImV4cCI6MjAzMTAxNDQwMH0.6uy9QulcyOFAWqC3qc4IOXPuoBeOT9GXJIx4DsrM-X0"
supabase: Client = create_client(url, key)


# banners=[2031,2032,3031,3032]


# boothil = 1315
# f=1208
# b_character_items = [1315, 1106,1109,1111]
# f_character_items = [1208, 1106,1109,1111]

# boothil_signa = 23027
# fu = 23011
# b_lc_items = [23027,21009,21012,21020]
# f_lc_items = [23011, 21009,21012,21020]


# hren = []

# id=225
# for bannerId in banners:
#     if(bannerId==2031):
#         for charId in b_character_items:
#             banner_item = {
#                 "id":id,
#                 "item_id":charId,
#                 "banner_id":bannerId
#             }
#             hren.append(banner_item)
#             id+=1
#     if(bannerId==2032):
#         for charId in f_character_items:
#             banner_item = {
#                 "id":id,
#                 "item_id":charId,
#                 "banner_id":bannerId
#             }
#             hren.append(banner_item)
#             id+=1
#     if(bannerId==3031):
#         for lcId in b_lc_items:
#             banner_item = {
#                 "id":id,
#                 "item_id":lcId,
#                 "banner_id":bannerId
#             }
#             hren.append(banner_item)
#             id+=1
#     if(bannerId==3032):
#         for lcId in f_lc_items:
#             banner_item = {
#                 "id":id,
#                 "item_id":lcId,
#                 "banner_id":bannerId
#             }
#             hren.append(banner_item)
#             id+=1

# import csv

# with open('hren.csv', 'w', newline='') as csvfile:
#     writer = csv.DictWriter(csvfile, fieldnames=['item_id', 'banner_id'])
#     writer.writeheader()
#     for item in hren:
#         writer.writerow(item)


# resp = supabase.table('banner_item').insert(hren).execute()
# print(1)

# resp = supabase.table('items').select('*').execute()
# items = resp.data


# for item in items:

#     current_directory = "Client/wwwroot/Images"
#     name_formatted = item['name'].lower().replace('.', '').replace(',', '').replace(':', '').replace('!', '').replace(' ', '-').replace('\'','')
#     name_with_id =str(item['id']) + '.png'
#     if item['type'] == 'Character':
#         for folder in ['/characters/', '/characters-full/', '/characters-mini/']:
#             file_path = current_directory + folder + name_formatted + ".png"
#             new_file_path = current_directory + folder + name_with_id
#             if os.path.exists(file_path):
#                 os.rename(file_path, new_file_path)
#     elif item['type'] == 'Light Cone':

#         file_path = current_directory + "/lightcones/" + name_formatted + ".png"
#         new_file_path = current_directory + "/lightcones/" + name_with_id
#         if os.path.exists(file_path):
#             os.rename(file_path, new_file_path)

current_directory = "Client/wwwroot/Images/banner-ticket"
for filename in os.listdir(current_directory):
    os.rename(current_directory + '/' + filename, current_directory + '/' + filename[-8:])