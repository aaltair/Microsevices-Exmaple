using School.Common.Event.Interfaces;

namespace School.Common.Event
{
    public class UserAuthenticatedEvent : IEvent
    {
        public string Username { get; set; }
    }
}