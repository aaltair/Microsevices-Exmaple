using School.Services.Identity.Entities.User;
using School.Services.Identity.Infrastructure.Abstracts;
using School.Services.Identity.Infrastructure.Contexts;

namespace School.Services.Identity.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context) : base(context)
        {
            _context = context;
        }
    }
}