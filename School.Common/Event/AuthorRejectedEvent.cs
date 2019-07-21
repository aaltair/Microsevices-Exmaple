using School.Common.Event.Interfaces;

namespace School.Common.Event
{
    public class AuthorRejectedEvent :IRejectedEvent
    {
        public string Reason { get; set; }
        public string Code { get; set; }
    }
}