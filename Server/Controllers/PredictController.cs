using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StellarJadeManager.Server.Controllers;

[Route("/predict")]
public class PredictController : ControllerBase{
    ILogger<PatchController> logger;

    public PredictController(ILogger<PatchController> logger)
    {
        this.logger=logger;

    }

    [HttpGet]
    public async Task<Dictionary<string,double>> Get()
    {
        HttpClient client = new HttpClient();
        // logger.LogError(HttpContext.Request.Query["pulls"].ToString());
        client.BaseAddress = new Uri("http://localhost:8080/");
        var response = await client.GetAsync($"predict");
        var hren = await response.Content.ReadFromJsonAsync<Dictionary<string, double>>();
        return hren;
    }
}
[Route("/pulls")]
public class PullsController : ControllerBase{
    ILogger<PatchController> logger;

    public PullsController(ILogger<PatchController> logger)
    {
        this.logger=logger;

    }

    [HttpGet]
    public async Task<Dictionary<string,List<int>>> Get()
    {
        HttpClient client = new HttpClient();
        // logger.LogError(HttpContext.Request.Query["pulls"].ToString());
        client.BaseAddress = new Uri("http://localhost:8080/");
        var response = await client.GetAsync($"data");
        var hren = await response.Content.ReadFromJsonAsync<Dictionary<string, List<int>>>();
        return hren;
    }
}