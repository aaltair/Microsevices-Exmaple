using School.Services.Courses.Infrastructure.Contexts;
using School.Services.Courses.Infrastructure.Repositories;

namespace School.Services.Courses.Infrastructure.Interfaces
{
    public interface ICourseUnitOfWork : IUnitOfWork<CoursesDbContext>
    {
        AuthorRepository AuthorRepository { get; }
        CourseRepository CourseRepository { get; }
    }
}