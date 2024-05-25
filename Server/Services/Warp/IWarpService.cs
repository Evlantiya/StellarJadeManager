using StellarJadeManager.Shared;

public interface IWarpService
{
    public Task<List<Warp>> GetWarpHistoryAsync(string warpUrl, Profile? profile = null);
}