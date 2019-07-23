using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using School.Common.Command;
using School.Common.Event;
using School.Common.Handler.Interfaces;
using School.Common.RabbitMq;
using School.Common.ServiceDiscovery;
using School.Services.Courses.Extensions;
using School.Services.Courses.Handler;

namespace School.Services.Courses
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureConsul(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDatabaseConfiguration(Configuration);
            services.AutoServiceRegister(Configuration);
            services.AddSwaggerConfiguration(Configuration);
            services.AddAuthenticationConfiguration(Configuration);
            services.AddAutoMapper();
            services.AddRabbitMq(Configuration);
            services.AddTransient<ICommandHandler<CreateAuthorCommand>, CreateAuthorCommandHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseExceptionConfig();
            }

            app.UseMvc();
            app.UseAuthentication();
            app.UseSwaggerConfig();
            app.UseCors(b => b
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );
        }


        private void ConfigureConsul(IServiceCollection services)
        {
            var serviceConfig = Configuration.GetServiceConfig();

            services.RegisterConsulServices(serviceConfig);
        }

    }
}
