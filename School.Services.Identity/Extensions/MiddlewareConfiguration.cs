using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using School.Services.Identity.Entities.User;
using School.Services.Identity.Infrastructure.Contexts;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
namespace School.Services.Identity.Extensions
{
    public static class MiddlewareConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            return services;
        }

        public static IServiceCollection AutoServiceRegister(this IServiceCollection services, IConfiguration configuration)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains($"{nameof(School.Services.Identity.Infrastructure)}") |
                            a.FullName.Contains($"{nameof(School.Services.Identity.Services)}"));

            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses()
                    .AsMatchingInterface()
                    .WithScopedLifetime());


            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Version = "v1", Title = "Course Apis" });

                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } } };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation version 1");
                c.DefaultModelRendering(ModelRendering.Model);
                c.EnableDeepLinking();
                c.DocumentTitle = "Course Apis";

            });
            app.UseSwagger();
            return app;
        }

        public static IApplicationBuilder UseExceptionConfig(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(ReqDelegate);
            });
            return app;
        }

        private static async Task ReqDelegate(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var error = context.Features.Get<IExceptionHandlerFeature>();
            if (error != null)
            {


                var ex = error.Error;


                var errorId = Activity.Current?.Id ?? context.TraceIdentifier;
                var jsonResponse = JsonConvert.SerializeObject(new
                {
                    ErrorId = errorId,
                    Message = error.Error.Message ?? "error happened."
                });

                await context.Response.WriteAsync(jsonResponse, Encoding.UTF8);
            }

        }


        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = Boolean.Parse(configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequireDigit").Value);
                options.Password.RequireLowercase = Boolean.Parse(configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequireLowercase").Value);
                options.Password.RequireUppercase = Boolean.Parse(configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequireUppercase").Value);
                options.Password.RequireNonAlphanumeric = Boolean.Parse(configuration
                    .GetSection("IdentityConfiguration").GetSection("RequireNonAlphanumeric").Value);
                options.Password.RequiredLength = int.Parse(configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequiredLength").Value);
            })
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }


        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.ClaimsIssuer = configuration.GetSection("Audience")["Iss"];
                options.Audience = configuration.GetSection("Audience")["Aud"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration.GetSection("Audience")["Iss"],
                    ValidAudience = configuration.GetSection("Audience")["Aud"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Audience")["Secret"])),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            return services;
        }


    }
}