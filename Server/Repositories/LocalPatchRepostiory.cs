using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Caching.Memory;

public class LocalPatchRepositroy : IPatchRepository
{
    private List<Patch> patches = new();
    private IMemoryCache cache;
    ILogger<LocalPatchRepositroy> logger;

    public LocalPatchRepositroy(IMemoryCache memCache , ILogger<LocalPatchRepositroy> logger){
        this.cache=memCache;
        this.logger= logger;
        logger.LogInformation("start local repo construct");

        var patchesJSONFileNames = Directory.GetFiles("Data/Patches");

        foreach(var patchJSONFileName in patchesJSONFileNames){
            var patchJSONString = File.ReadAllText(patchJSONFileName);
            dynamic? patchJSON = JsonConvert.DeserializeObject(patchJSONString);

            var patchInfo = patchJSON?.patchInfo.Value ?? "undefined";
            // string name = patchJSON?.name?.Value ?? "undefined";
            string version = patchJSON.patchInfo.version.Value;
            string title = patchJSON.patchInfo.title.Value;
            DateTime releaseDate = DateTime.Parse(patchJSON.patchInfo.releaseDate.Value);
            int weeksCount = (int) patchJSON.patchInfo.weeksCount.Value;

            patches.Add(new Patch(version,title,releaseDate, weeksCount));
            logger.LogInformation("end local repo construct");
        }
    }

    public Patch Get(string version)
    {
        //validate for patch version later
        return patches.First(p => p.Version == version);
    }

    public IEnumerable<Patch> GetAll()
    {
        return patches;
    }
}