public class Event
{

    public string Title {get; set;} = "undefined";
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public int StellarJadesAmount {get; set;}
    public Event(string title, DateTime start, DateTime end, int stellarJadesAmount)
    {
        this.Title=title;
        this.StartDate=StartDate;
        this.EndDate=EndDate;
        this.StellarJadesAmount=stellarJadesAmount;
    }

    public Event()
    {
    }
}