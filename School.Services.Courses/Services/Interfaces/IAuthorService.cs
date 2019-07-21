using School.Services.Courses.Dtos;
using School.Services.Courses.Dtos.Author;

namespace School.Services.Courses.Services.Interfaces
{
    public interface IAuthorService
    {
        PagedResultsDto<AuthorDto> GetAllAuthors(int pageIndex, int pageSize);
        PagedResultsDto<AuthorCourseDto> GetAllAuthorsWithCourses(int pageIndex, int pageSize);
        AuthorCourseDto GetAuthorById(int id);
        AuthorDto CreateAuthor(CreateAuthorDto createAuthor, string currentUserId);
        AuthorDto UpdateAuthor(AuthorDto author, string currentUserId);
        AuthorDto DeleteAuthor(int id, string currentUserId);
    }
}