using School.Services.Identity.Infrastructure.Contexts;
using School.Services.Identity.Infrastructure.Repositories;

namespace School.Services.Identity.Infrastructure.Interfaces
{
    public interface IUserUnitOfWork : IUnitOfWork<UserDbContext>
    {
        UserRepository UserRepository { get; }
    }
}