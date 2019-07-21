using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using School.Common.Service.Interfaces;

namespace School.Common.Service
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            this._webHost = webHost;
        }

        public void Run() => this._webHost.Run();

        public static HostBuilder Create<TStartup>(string[] args,bool isOclot=false) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;
            IConfigurationRoot config = null;
            if (isOclot)
                config = new ConfigurationBuilder()
                 .AddEnvironmentVariables()
                 .AddJsonFile("ocelot.json")
                .AddCommandLine(args)
                .Build();
            else
                config = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .AddCommandLine(args)
                    .Build();
            
     
             
                var webHostBuilder = WebHost.CreateDefaultBuilder()
                
                .UseConfiguration(config)
                .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        }
    }
}