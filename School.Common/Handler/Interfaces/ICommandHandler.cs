using System.Threading.Tasks;
using School.Common.Command.Interfaces;

namespace School.Common.Handler.Interfaces
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}