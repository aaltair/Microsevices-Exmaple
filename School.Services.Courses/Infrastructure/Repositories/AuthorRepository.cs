using School.Services.Courses.Entities;
using School.Services.Courses.Infrastructure.Abstracts;
using School.Services.Courses.Infrastructure.Contexts;

namespace School.Services.Courses.Infrastructure.Repositories
{
    public class AuthorRepository : BaseRepository<Author>
    {
        private readonly CoursesDbContext _context;

        public AuthorRepository(CoursesDbContext context) : base(context)
        {
            _context = context;
        }

    }
}