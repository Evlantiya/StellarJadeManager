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
    [Route("rolls")]
    public void post_test(int rollsAmount){
        logger.LogInformation(rollsAmount.ToString()); 
    } 
}