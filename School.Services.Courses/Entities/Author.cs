using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using School.Services.Courses.Entities.Interfaces;

namespace School.Services.Courses.Entities
{
    public class Author : IBaseEntity
    {
        public int AuthorId { get; set; }
        [MaxLength(100)]
        public string AuthorName { get; set; }
        public ICollection<Course> Courses { set; get; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(100)]
        public string UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public bool IsDelete { get; set; }
    }
}