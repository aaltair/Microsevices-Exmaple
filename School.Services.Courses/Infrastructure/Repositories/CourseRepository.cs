using School.Services.Courses.Entities;
using School.Services.Courses.Infrastructure.Abstracts;
using School.Services.Courses.Infrastructure.Contexts;

namespace School.Services.Courses.Infrastructure.Repositories
{
    public class CourseRepository : BaseRepository<Course>
    {
        private readonly CoursesDbContext _context;

        public CourseRepository(CoursesDbContext context) : base(context)
        {
            _context = context;
        }
    }
}