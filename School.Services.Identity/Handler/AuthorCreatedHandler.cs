using System;
using System.Threading.Tasks;
using School.Common.Event;
using School.Common.Handler.Interfaces;

namespace School.Services.Identity.Handler
{
    public class AuthorCreatedHandler : IEventHandler<AuthorCreatedEvent>
    {
        public async Task HandleAsync(AuthorCreatedEvent @event)
        {
            Console.WriteLine("Server Create Author : " + @event.AuthorName);
            await Task.CompletedTask;

        }
    }
}