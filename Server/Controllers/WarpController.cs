using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using StellarJadeManager.Shared;
using Microsoft.EntityFrameworkCore;

namespace StellarJadeManager.Server.Controllers;

[Route("/api/warp")]
public class WarpController: ControllerBase
{
    private ILogger<WarpController> logger;

    private IWarpService _warpService;
    private PostgresContext _db;

    public WarpController(IWarpService warpService ,ILogger<WarpController> logger, PostgresContext db)
    {
        this.logger=logger;
        this._warpService=warpService;
        _db = db;
    }

    [HttpGet("userBannerInfo/list")]
    public async Task<IActionResult> GetUserBannerInfoListByClaimsIdOrDefault()
    {
        var bannerInfos = UserBannerInfo.CreateDefaultBannerInfos();
        if ( !User?.Identity?.IsAuthenticated ?? true){
            return Ok(bannerInfos);
        }
        var userId = Convert.ToInt32(User.FindFirstValue("Id"));
        var user = await _db.Users.Include(u=>u.Profiles).FirstOrDefaultAsync(u=>u.Id == userId);
        var profile = user?.Profiles.FirstOrDefault();
        if (profile.UserBannerInfos == null || profile.UserBannerInfos.Count == 0){
            foreach(var b in bannerInfos){
                b.ProfileId = profile.Id;
            }
            return Ok(bannerInfos);
        }
        return Ok(profile.UserBannerInfos);
        

    }

    // [Route("/parse")]
    [HttpPost("parse")]
    public async Task<IActionResult> ParseGachaLog([FromBody] string warpUrl)
    {
        Profile? profile = null;
        if (User?.Identity?.IsAuthenticated ?? false){
            var userId = Convert.ToInt32(User.FindFirstValue("Id"));
            var user = await _db.Users.Include(u=>u.Profiles).FirstOrDefaultAsync(u=>u.Id == userId);
            profile = user?.Profiles.FirstOrDefault() ?? null;
        }

        try{
            var result = await _warpService.GetWarpHistoryAsync(warpUrl, profile);
        }
        catch(Exception ex){
            logger.LogError(ex.Message);
            return BadRequest();
        }

        return Ok();
        // return result ? Ok() : Conflict();

    }

    [HttpGet("test")]
    [Authorize]
    public async Task<IActionResult> TestAuth()
    {
        return Ok("Authorized");
    }
}
