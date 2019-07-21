namespace School.Common.Event.Interfaces
{
    public interface IAuthenticatedEvent : IEvent
    {
        string UserId { get; set; }
      

    }
}