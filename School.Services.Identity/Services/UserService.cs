using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RawRabbit;
using School.Common.Command;
using School.Services.Identity.Dtos;
using School.Services.Identity.Entities.User;
using School.Services.Identity.Services.Interfaces;

namespace School.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBusClient _busClient;

        public UserService(UserManager<ApplicationUser> userManager,IBusClient busClient)
        {
            _userManager = userManager;
            _busClient = busClient;
        }

        public async Task<UserDto> SignUp(SignUpDto signUpDto)
        {
            if (_userManager.FindByNameAsync(signUpDto.Username).Result != null) throw  new Exception("UserName_Exsist");
            ApplicationUser user = new ApplicationUser
            {
         
                UserName = signUpDto.Username,
                Email = signUpDto.Email,
                FirstName = signUpDto.FirstName,
                SecondName = signUpDto.SecondName,
                LastName = signUpDto.LastName,
                CreatedOn = DateTime.Now,
                IsDelete = false
            };

            IdentityResult result = _userManager.CreateAsync(user, signUpDto.Password).Result;
            if(!result.Succeeded) throw new Exception("SignUp_Error");
           await _busClient.PublishAsync(new CreateAuthorCommand
            {
                AuthorName = signUpDto.FirstName + " " + signUpDto.SecondName + " " + signUpDto.LastName
            });

            return new UserDto
            {
                Username = signUpDto.Username,
                Email = signUpDto.Email,
                FirstName = signUpDto.FirstName,
                SecondName = signUpDto.SecondName,
                LastName = signUpDto.LastName,
            } ;
        }
    

    }
} 