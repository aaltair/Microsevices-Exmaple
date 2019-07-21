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
                new Author {AuthorId = 1, AuthorName = "Alaa Altair", CreatedOn = DateTime.Now},
                new Author {AuthorId = 2, AuthorName = "Ali Altair", CreatedOn = DateTime.Now},
                new Author {AuthorId = 3, AuthorName = "Ahmad Altair", CreatedOn = DateTime.Now},
            });

            builder.Entity<Course>().HasData(new Course[]
                {
                    new Course
                    {
                        CourseId = 1, CourseCategory = "FullStack", AuthorId = 1, CourseName = ".Net Core With React",
                        CreatedOn = DateTime.Now
                    },
                    new Course
                    {
                        CourseId = 2, CourseCategory = "FrontEnd", AuthorId = 2, CourseName = "React With Redux",
                        CreatedOn = DateTime.Now
                    },
                    new Course
                    {
                        CourseId = 3, CourseCategory = "BackEnd", AuthorId = 3, CourseName = ".Net Core WebApi",
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