using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace School.Services.Courses.Infrastructure.Contexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CoursesDbContext>
    {
        public CoursesDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<CoursesDbContext>();
            var connectionString = configuration.GetConnectionString("CoursesConnection");
            builder.UseSqlServer(connectionString);
            return new CoursesDbContext(builder.Options);
        }
    }
}