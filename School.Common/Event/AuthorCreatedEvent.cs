using System;
using School.Common.Event.Interfaces;

namespace School.Common.Event
{
    public class AuthorCreatedEvent : IAuthenticatedEvent
    {

        public string UserId { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }


    }
}