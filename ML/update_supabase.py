
import json
import os
from supabase import create_client, Client

url = "https://nyeujbavubnlpyjssdsa.supabase.co"
key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im55ZXVqYmF2dWJubHB5anNzZHNhIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTU0Mzg0MDAsImV4cCI6MjAzMTAxNDQwMH0.6uy9QulcyOFAWqC3qc4IOXPuoBeOT9GXJIx4DsrM-X0"
supabase: Client = create_client(url, key)


# with open(f'ML/ITEMS_HREN.json', 'r') as f:
#         data = json.load(f)
#         for item in data:
#                 item["id"] = int(item["id"])
#         response = supabase.table('items').insert(data).execute()
banners = []
patches = []


# patchesSupabase = supabase.table("patch").select("*").execute().data

for bannerFileId in os.listdir('ML/data/banners'):
    with open(f"ML/data/banners/{bannerFileId}", 'r') as f:
        banners.append(json.load(f))

hren = []
id = 1
for banner in banners:
    for item in banner["items"]:
        hren.append(
            {
                "id":id,
                "item_id":item["id"],
                "banner_id":banner["id"]
            }
        )
        id+=1
# for patchFileName in os.listdir('Server/Data/Patches'):
#     with open(f"Server/Data/Patches/{patchFileName}", 'r') as f:
#         patches.append(json.load(f))

# banner_hren = []
# for patch in patches:
#     hren = patch["patchInfo"]["banners"]
#     hren.extend([i+1000 for i in patch["patchInfo"]["banners"]])
#     for banner_id in hren:
#         b = {
#             "id": banner_id,
#             "patch_id": list(filter(lambda p_s: p_s["version"] == patch["patchInfo"]["version"], patchesSupabase))[0]["id"],
#             "type_id": 12 if banner_id >= 3000 else 11
#         }
#         banner_hren.append(b)

response = supabase.table('banner_item').insert(hren).execute()
print(1)