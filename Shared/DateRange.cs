public struct EventDateRange
{
    public DateTime RangeStart {get; set;}
    public DateTime RangeEnd {get; set;}
    public EventDateRange (DateTime start, DateTime end){
        this.RangeStart=start;
        this.RangeEnd=end;
    }
    public bool IsDateInRange(DateTime date){
        return (date >= RangeStart && date <= RangeEnd);
    }
    public bool IsDate(DateTime date){
        return (date >= RangeStart && date <= RangeEnd);
    }

}