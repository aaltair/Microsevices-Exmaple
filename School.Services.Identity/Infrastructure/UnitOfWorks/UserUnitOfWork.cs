using School.Services.Identity.Infrastructure.Contexts;
using School.Services.Identity.Infrastructure.Interfaces;
using School.Services.Identity.Infrastructure.Repositories;

namespace School.Services.Identity.Infrastructure.UnitOfWorks
{
    public class UserUnitOfWork : UnitOfWork<UserDbContext>, IUserUnitOfWork
    {
        #region filed
    
        private UserRepository _userRepository;


        #endregion

        public UserUnitOfWork(UserDbContext context)
        {
            _context = context;
        }

        #region initlitation
        public UserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_context));

        #endregion
    }
}