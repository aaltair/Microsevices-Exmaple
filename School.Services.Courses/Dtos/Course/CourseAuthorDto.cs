using School.Services.Courses.Dtos.Author;

namespace School.Services.Courses.Dtos.Course
{
    public class CourseAuthorDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCategory { get; set; }
        public int AuthorId { get; set; }
        public AuthorDto Author { set; get; }
    }
}