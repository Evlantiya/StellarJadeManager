using System.Web;
using Newtonsoft.Json;

public class WarpService : IWarpService
{
    private HttpClient _httpClient;

    public WarpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> TryParseWarpHistoryAsync(string warpUrl){
        if (string.IsNullOrEmpty(warpUrl)){
            return false;
        }

        var warpUri = new Uri(warpUrl);
        if (warpUri.Host!="api-os-takumi.mihoyo.com"){
            return false;
        }


        GetGachaLogResponse? warpPage;
        var warpsData = new List<WarpDTO>();
        foreach (int banner in Enum.GetValues(typeof(BannerEnum)))
        {
            var endId = "0";
            do{
                var query = $"&page=1&size=20&gacha_type=11&end_id={endId}";
                var hren = warpUrl + query;
                var test = await _httpClient.GetStringAsync(hren);
                warpPage = JsonConvert.DeserializeObject<GetGachaLogResponse>(test);

                // warpPage = await _httpClient.GetFromJsonAsync<GetGachaLogResponse>(hren);
                List<WarpDTO> warps = warpPage?.data?.list ?? new List<WarpDTO>();
                warpsData.AddRange(warps);
                endId = warps.LastOrDefault()?.id ?? "0";
            } 
            while (warpPage != null && warpPage.data.list.Count > 0);
        }
        string jsonString = JsonConvert.SerializeObject(warpsData, Formatting.Indented);
        File.WriteAllText($"Data/Warps/warps_{warpsData?.FirstOrDefault()?.uid ?? "0"}", jsonString);
        return true;
    }
}
