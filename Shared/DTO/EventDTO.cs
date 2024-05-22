public class EventDTO
{

    public string EventTitle {get; set;} = "undefined";
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public int StellarJadesAmount {get; set;}
    public EventDTO(string eventTitle, DateTime startDate, DateTime endDate, int stellarJadesAmount)
    {
        this.EventTitle = eventTitle;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.StellarJadesAmount = stellarJadesAmount;
    }
}