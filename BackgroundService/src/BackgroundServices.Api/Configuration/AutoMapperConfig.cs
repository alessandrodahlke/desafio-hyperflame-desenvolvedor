using BackgroundServices.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundServices.Api.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AutoMapperServiceConfig(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var mappginConfig = AutoMapperSetup.RegisterMappings();
            var mapper = mappginConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
