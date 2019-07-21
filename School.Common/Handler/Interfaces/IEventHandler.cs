using System.Threading.Tasks;
using School.Common.Event.Interfaces;

namespace School.Common.Handler.Interfaces
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event); // We have to user an arobase in the parameter name because 'event' is a reserved word by C#
    }
}