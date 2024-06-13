using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

public class LocalPatchRepositroy : IPatchRepository
{
    private List<PatchDTO> patches = new();
    private IMemoryCache cache;
    ILogger<LocalPatchRepositroy> logger;

    public LocalPatchRepositroy(IMemoryCache memCache , ILogger<LocalPatchRepositroy> logger){
        this.cache=memCache;
        this.logger= logger;

        var patchesJSONFileNames = Directory.GetFiles("Data/Patches");

        foreach(var patchJSONFileName in patchesJSONFileNames){
            var patchJSONString = File.ReadAllText(patchJSONFileName);
            dynamic? patchJSON = JsonConvert.DeserializeObject(patchJSONString);

            var patchInfo = patchJSON.patchInfo.Value;
            // string name = patchJSON?.name?.Value ?? "undefined";
            string version = patchJSON.patchInfo.version.Value;
            string title = patchJSON.patchInfo.title.Value;
            DateTime releaseDate = DateTime.ParseExact(patchJSON.patchInfo.releaseDate.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            int weeksCount = (int) patchJSON.patchInfo.weeksCount.Value;
            List<EventDTO> events = new();
            var eventsInfo=patchJSON.patchInfo.events;
            foreach(var e in eventsInfo){
                string eventTitle = e.eventTitle.Value;
                DateTime startDate = DateTime.ParseExact(e.startDate.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(e.endDate.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                int stellarJadesAmount = (int) e.stellarJadesAmount.Value;
                events.Add(new EventDTO(eventTitle, startDate, endDate, stellarJadesAmount));
            }
            patches.Add(new PatchDTO(version,title,releaseDate, weeksCount, events));
        }
    }

    public PatchDTO Get(string version)
    {
        //validate for patch version later
        return patches.First(p => p.Version == version);
    }

    public IEnumerable<PatchDTO> GetAll()
    {
        return patches;
    }

    public IEnumerable<PatchDTO> GetRelevant()
    {
        return patches.Where(p => !(p.ReleaseDate.AddDays(7*p.WeeksCount) < DateTime.Today));
    }
}