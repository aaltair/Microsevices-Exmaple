using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Services.Courses.Entities;

namespace School.Services.Courses.Infrastructure.Contexts
{
    public class CoursesDbContext : DbContext
    {

        public DbSet<Author> Authors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            FluentApi(builder);
            SeedData(builder);
            GlobalFilters(builder);

        }

        private void GlobalFilters(ModelBuilder builder)
        {
            builder.Entity<Author>().HasQueryFilter(w => !w.IsDelete);
            builder.Entity<Course>().HasQueryFilter(w => !w.IsDelete);
          
        }

        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<Author>().HasData(new Author[]
            {
                new Author {AuthorId = 1,AuthorName = "علاء عباس الطير", AuthorNameEn = "Alaa Abbas Altair", CreatedOn = DateTime.Now},

            });

            builder.Entity<Course>().HasData(new Course[]
                {
                    new Course
                    {
                        CourseId = 1, CourseCategory = "شامل" ,
                        CourseCategoryEn = "FullStack",
                        AuthorId = 1,
                        CourseNameEn = ".Net Core With React",
                        CourseName = "دوره (.Net Core With React) ",
                        CreatedOn = DateTime.Now
                    },
                    new Course
                    {
                        CourseId = 2,
                        CourseCategory = "تصمم وجهات",
                        CourseCategoryEn = "FrontEnd",
                        AuthorId = 1,
                        CourseNameEn = "React With Redux",
                        CourseName = "دوره (React With Redux)",
                        CreatedOn = DateTime.Now
                    },
                    new Course
                    {
                        CourseId = 3,
                        CourseCategory = "برمجه",
                        CourseCategoryEn = "BackEnd",
                        AuthorId = 1,
                        CourseNameEn = ".Net Core WebApi",
                        CourseName = "دوره (.Net Core WebApi)",
                        CreatedOn = DateTime.Now
                    },
                }
            );

        

        }

        private void FluentApi(ModelBuilder builder)
        {

        }




    }
}