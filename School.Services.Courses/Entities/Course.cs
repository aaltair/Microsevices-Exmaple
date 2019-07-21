using System;
using System.ComponentModel.DataAnnotations;
using School.Services.Courses.Entities.Interfaces;

namespace School.Services.Courses.Entities
{
    public class Course : IBaseEntity
    {
        public int CourseId { get; set; }
        [MaxLength(50)]
        public string CourseName { get; set; }
        [MaxLength(50)]
        public string CourseCategory { get; set; }
        public int AuthorId { get; set; }
        public Author Author { set; get; }
        [MaxLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(50)]
        public string UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public bool IsDelete { get; set; }
    }
}