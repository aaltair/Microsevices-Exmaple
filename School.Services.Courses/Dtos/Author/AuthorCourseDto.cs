using System.Collections.Generic;
using School.Services.Courses.Dtos.Course;

namespace School.Services.Courses.Dtos.Author
{
    public class AuthorCourseDto
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public ICollection<CourseDto> Courses { set; get; }
    }
}