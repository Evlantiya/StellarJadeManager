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
        var warpsData = new List<Warp>();
        var endId = "0";
        do{
            var query = $"&page=1&size=20&gacha_type=11&end_id={endId}";
            var hren = warpUrl + query;
            var test = await _httpClient.GetStringAsync(hren);
            warpPage = JsonConvert.DeserializeObject<GetGachaLogResponse>(test);

            // warpPage = await _httpClient.GetFromJsonAsync<GetGachaLogResponse>(hren);
            List<Warp> warps = warpPage?.data?.list ?? new List<Warp>();
            warpsData.AddRange(warps);
            endId = warps.LastOrDefault()?.id ?? "0";
        } 
        while (warpPage != null && warpPage.data.list.Count > 0);
        return true;
    }
}
