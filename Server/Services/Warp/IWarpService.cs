using StellarJadeManager.Shared;

public interface IWarpService
{
    public Task<List<UserBannerInfo>> GetWarpHistoryAsync(string warpUrl, Profile? profile = null);
}