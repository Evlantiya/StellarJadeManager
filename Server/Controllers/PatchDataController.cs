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
        logger.LogInformation("Get controller call");
        return patchRepository.GetAll().ToList();
    }
}