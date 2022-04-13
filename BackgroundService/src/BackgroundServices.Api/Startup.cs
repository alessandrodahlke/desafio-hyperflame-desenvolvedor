using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackgroundServices.Api.Configuration;
using BackgroundServices.Api.Workers;
using BackgroundServices.Application.Configurations;
using BackgroundServices.CrossCutting.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace BackgroundServices.Api
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ApiServiceConfig();
            services.AutoMapperServiceConfig();
            services.HangFireServiceConfig(_configuration);
            services.AddDependencies(_configuration);
            services.Configure<RabbitMQConfig>(_configuration.GetSection("RabbitMQ"));
            services.AddHostedService<ProcessaRelatorioWorker>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.HangFireApplicationConfig(env);
            app.ApiApplicationConfig(env);
        }
    }
}
