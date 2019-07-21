using School.Services.Courses.Infrastructure.Contexts;
using School.Services.Courses.Infrastructure.Interfaces;
using School.Services.Courses.Infrastructure.Repositories;

namespace School.Services.Courses.Infrastructure.UnitOfWorks
{
    public class CourseUnitOfWork : UnitOfWork<CoursesDbContext>, ICourseUnitOfWork
    {
        #region filed
        private AuthorRepository _authorRepository;
        private CourseRepository _courseRepository;
     


        #endregion

        public CourseUnitOfWork(CoursesDbContext context)
        {
            _context = context;
        }

        #region initlitation
        public AuthorRepository AuthorRepository => _authorRepository ?? (_authorRepository = new AuthorRepository(_context));
        public CourseRepository CourseRepository => _courseRepository ?? (_courseRepository = new CourseRepository(_context));


        #endregion
    }
}