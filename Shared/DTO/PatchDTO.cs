using System.Collections.Generic;

public class PatchDTO{
    public string Version {get; private set;}
    public string Title {get; private set;}
    public DateTime ReleaseDate {get; private set;}
    public int WeeksCount {get; private set;}
    public List<EventDTO> Events {get; private set;}
    // public List<Banner> Banners {get; private set;} = new();

    public PatchDTO(string version, string title, DateTime releaseDate, int weeksCount, List<EventDTO> events)
    {
        this.Version=version;
        this.Title=title;
        this.ReleaseDate=releaseDate;
        this.WeeksCount=weeksCount;
        this.Events = events;
    }

    // public Patch(string version, string title, DateTime releaseDate, int weeksCount, List<Event> events, List<Banner> banners)
    // {
    //     this.Version=version;
    //     this.Title=title;
    //     this.ReleaseDate=releaseDate;
    //     this.WeeksCount=weeksCount;
    //     this.Events=events;
    //     this.Banners=banners;
    // }
}
