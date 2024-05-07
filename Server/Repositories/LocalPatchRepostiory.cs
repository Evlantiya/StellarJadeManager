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

        var patchesJSONFileNames = Directory.GetFiles("Data/Patches");

        foreach(var patchJSONFileName in patchesJSONFileNames){
            var patchJSONString = File.ReadAllText(patchJSONFileName);
            dynamic? patchJSON = JsonConvert.DeserializeObject(patchJSONString);

            var patchInfo = patchJSON.patchInfo.Value;
            // string name = patchJSON?.name?.Value ?? "undefined";
            string version = patchJSON.patchInfo.version.Value;
            string title = patchJSON.patchInfo.title.Value;
            DateTime releaseDate = DateTime.Parse(patchJSON.patchInfo.releaseDate.Value);
            int weeksCount = (int) patchJSON.patchInfo.weeksCount.Value;
            List<Event> events = new();
            var eventsInfo=patchJSON.patchInfo.events;
            foreach(var e in eventsInfo){
                string eventTitle = e.eventTitle.Value;
                DateTime startDate = DateTime.Parse(e.startDate.Value);
                DateTime endDate = DateTime.Parse(e.endDate.Value);
                int stellarJadesAmount = (int) e.stellarJadesAmount.Value;
                events.Add(new Event(eventTitle, startDate, endDate, stellarJadesAmount));
            }
            patches.Add(new Patch(version,title,releaseDate, weeksCount, events));
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

    public IEnumerable<Patch> GetRelevant()
    {
        return patches.Where(p => !(p.ReleaseDate.AddDays(7*p.WeeksCount) < DateTime.Today));
    }
}