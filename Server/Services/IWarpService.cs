public interface IWarpService
{
    public Task<bool> TryParseWarpHistoryAsync(string warpUrl);
}