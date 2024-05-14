using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

namespace StellarJadeManager.Server.Controllers;

[Route("/api/warp")]
public class WarpController: ControllerBase
{
    private ILogger<WarpController> logger;

    private IWarpService _warpService;

    public WarpController(IWarpService warpService ,ILogger<WarpController> logger)
    {
        this.logger=logger;
        this._warpService=warpService;
    }

    // [Route("/parse")]
    [HttpPost("parse")]
    public async Task<IActionResult> ParseGachaLog([FromBody] string warpUrl)
    {
        var result = await _warpService.TryParseWarpHistoryAsync(warpUrl);
        return result ? Ok() : Conflict();

    }

    [HttpGet("test")]
    [Authorize]
    public async Task<IActionResult> TestAuth()
    {
        return Ok("Authorized");
    }
}
