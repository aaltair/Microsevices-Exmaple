﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Common.Event;
using School.Common.Handler.Interfaces;
using School.Common.RabbitMq;
using School.Common.ServiceDiscovery;
using School.Services.Identity.Extensions;
using School.Services.Identity.Handler;

namespace School.Services.Identity
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
            services.AddIdentityConfiguration(Configuration);
            services.AutoServiceRegister(Configuration);
            services.AddSwaggerConfiguration(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddAutoMapper();
            

            services.AddTransient<IEventHandler<AuthorCreatedEvent>, AuthorCreatedHandler>();

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
      
            app.UseSwaggerConfig();
        }
        private void ConfigureConsul(IServiceCollection services)
        {
            var serviceConfig = Configuration.GetServiceConfig();

            services.RegisterConsulServices(serviceConfig);
        }

    }


}
