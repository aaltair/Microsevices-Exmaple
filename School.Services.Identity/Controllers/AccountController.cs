using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using School.Services.Identity.Dtos;
using School.Services.Identity.Entities.User;
using School.Services.Identity.Services.Interfaces;

namespace School.Services.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController :ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
            _configuration = configuration;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, lockoutOnFailure: false);
            if (signInResult.Succeeded) return await GenerateToken(loginDto.Username, loginDto.Password);
            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] SignUpDto signUpDto)
        {
            return Ok(await _userService.SignUp(signUpDto));
        }


        private async Task<IActionResult> GenerateToken(string userName, string password)
        {

            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);
            var now = DateTime.Now;

            var userClaims = await _userManager.GetClaimsAsync(user);
            userClaims.Add(new Claim("Id", user.Id));
            userClaims.Add(new Claim("Name", user.UserName));
            userClaims.Add(new Claim("Roles", Newtonsoft.Json.JsonConvert.SerializeObject(roles)));
            //var claims = new[]
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, userName),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim(JwtRegisteredClaimNames.Iat, now.Ticks.ToString(), ClaimValueTypes.Integer64)
            //}.Union(userClaims);


            //var signingCredentials = new SigningCredentials(
            //    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("Audience")["Secret"])),
            //    SecurityAlgorithms.HmacSha256);

            //var jwt = new JwtSecurityToken(
            //    issuer: _configuration.GetSection("Audience")["Iss"],
            //    audience: _configuration.GetSection("Audience")["Aud"],
            //    claims: claims,
            //    notBefore: now,
            //    expires: now.Add(TimeSpan.FromDays(1)),
            //    signingCredentials: signingCredentials);



            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            }.Union(userClaims);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("Audience")["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = _configuration.GetSection("Audience")["Iss"],
                ValidateAudience = true,
                ValidAudience = _configuration.GetSection("Audience")["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };

            var jwt = new JwtSecurityToken(
                issuer: _configuration.GetSection("Audience")["Iss"],
                audience: _configuration.GetSection("Audience")["Aud"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(1)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new
            {

                Id = user.Id,
                FullName = user.FirstName + " " + user.SecondName + " " + user.LastName,
                FullNameEn = user.FirstNameEn + " " + user.SecondNameEn + " " + user.LastNameEn,
                ProfileImg = user.ProfileImg,
                token = encodedJwt,
                expiration = jwt.ValidTo,
                Roles = roles,

            });

        }

    }

}
