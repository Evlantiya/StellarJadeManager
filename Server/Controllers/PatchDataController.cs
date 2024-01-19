using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StellarJadeManager.Server.Controllers;

[Route("/api/patches")]
public class PatchController: ControllerBase
{
    ILogger<PatchController> logger;
    private IPatchRepository patchRepository;
    public PatchController(IPatchRepository repa, ILogger<PatchController> logger)
    {
        this.logger=logger;
        this.patchRepository = repa;
    }

    [HttpGet]
    public IEnumerable<Patch> Get()
    {
        return patchRepository.GetAll().ToList(); 
    }

    

    [HttpPost]
    public void post_test(int rollsAmount){
        logger.LogInformation(rollsAmount.ToString()); 
    } 
}

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