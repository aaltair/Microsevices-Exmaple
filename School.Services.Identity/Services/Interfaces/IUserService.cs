using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Services.Identity.Dtos;

namespace School.Services.Identity.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> SignUp(SignUpDto signUpDto);
    }
}
