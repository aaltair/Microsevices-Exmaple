using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace School.Services.Courses.Controllers
{
    [Authorize(AuthenticationSchemes = "TestKey")] 
    
    public class BaseController : Controller
    {

        [NonAction]
        protected string CurrentUserId()
        {
            return HttpContext.User.FindFirstValue("Id");
        }
    }
}
