namespace School.Common.Event.Interfaces
{
    public interface IRejectedEvent
    {
        string Reason { get; set; }
        string Code { get; set; }
    }
}