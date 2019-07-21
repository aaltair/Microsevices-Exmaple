namespace School.Services.Courses.Dtos.Course
{
    public class CreateCourseDto
    {
        public string CourseName { get; set; }
        public string CourseCategory { get; set; }
        public int AuthorId { get; set; }
    }
}