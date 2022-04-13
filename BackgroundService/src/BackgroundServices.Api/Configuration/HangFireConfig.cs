using BackgroundServices.Application.Settings;
using BackgroundServices.Services.Interfaces;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundServices.Api.Configuration
{
    public static class HangFireConfig
    {
        public static void HangFireServiceConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new HangfireSetting();
            configuration.GetSection("Hangfire").Bind(config);

            if (config.UseMemoryStorage)
            {
                services.AddHangfire(op => { op.UseMemoryStorage(); });
            }
            else
            {
                var migrationsOptiopns = new MongoMigrationOptions
                {
                    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                    BackupStrategy = new CollectionMongoBackupStrategy()
                };
                var storageOptions = new MongoStorageOptions
                {
                    MigrationOptions = migrationsOptiopns
                };
                services.AddHangfire(c => c
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseMongoStorage(config.StringConexao, config.Database, storageOptions)
                );
            }

            services.AddHangfireServer();
        }

        public static void HangFireApplicationConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuthorizationFilter() }
            });

            RecurringJob.AddOrUpdate<IGerenciarLotesArquivos>("CARREGAR-LOTE-ARQUIVOS", x => x.Gerenciar(),
                  Cron.Daily(6));

            RecurringJob.AddOrUpdate<IGerenciarArquivos>("CARREGAR-ARQUIVOS-INDIVIDUAIS", x => x.Gerenciar(),
                Cron.Daily(6)); 
        }

        public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                return true;
            }
        }
    }
}
