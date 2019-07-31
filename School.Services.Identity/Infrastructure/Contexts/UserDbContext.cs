using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School.Services.Identity.Entities.User;

namespace School.Services.Identity.Infrastructure.Contexts
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {

      
        public UserDbContext(DbContextOptions<UserDbContext> options)
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
         
            builder.Entity<ApplicationUser>().HasQueryFilter(w => !w.IsDelete);
        }

        private void SeedData(ModelBuilder builder)
        {
     

            var hash = new PasswordHasher<ApplicationUser>();
            var ADMIN_ID = Guid.NewGuid().ToString();
            var ROLE_ID = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            { Id = ROLE_ID, Name = "SuperAdmin", NormalizedName = "SuperAdmin".ToUpper() });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = "aaltair.developer@gmail.com",
                NormalizedEmail = "aaltair.developer@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+962788260020",
                PhoneNumberConfirmed = true,
                FirstName = "علاء",
                SecondName = "عباس",
                LastName = "الطير",
                FirstNameEn = "Alaa",
                SecondNameEn = "Abbas",
                LastNameEn = "Altair",
                BirthDate = new DateTime(1993, 1, 27),
                Address = "Amman",
                City = "Amman",
                PasswordHash = hash.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = String.Empty,
                CreatedOn = DateTime.Now
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

        }

        private void FluentApi(ModelBuilder builder)
        {

        }




    }
}