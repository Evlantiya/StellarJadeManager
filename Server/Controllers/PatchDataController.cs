using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AutoMapper;
using StellarJadeManager.Shared;

namespace StellarJadeManager.Server.Controllers;

[Route("/api/patches")]
public class PatchController: ControllerBase
{
    ILogger<PatchController> logger;
    private IPatchRepository patchRepository;
    private readonly PostgresContext _context;
    private readonly IMapper _mapper;
    public PatchController(IPatchRepository repa, ILogger<PatchController> logger, IMapper mapper, PostgresContext context)
    {
        this.logger = logger;
        this.patchRepository = repa;
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IEnumerable<PatchDTO> Get()
    {
        return patchRepository.GetRelevant().ToList(); 
    }

    [HttpPost]
    public async Task<IActionResult> Add()
    {
        try
        {
            await hren();
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [NonAction]
    public async Task hren()
    {
        var patchesDTO = patchRepository.GetAll().Where(p => p.Version != "2.3" && p.Version != "2.4").ToList();
        var patches = _mapper.Map<List<PatchDTO>,List<Patch>>(patchesDTO);

        await _context.Patches.AddRangeAsync(patches);
        await _context.SaveChangesAsync();

    }

    //[HttpPost]
    //public void post_test(int rollsAmount){
    //    logger.LogInformation(rollsAmount.ToString()); 
    //} 
}
