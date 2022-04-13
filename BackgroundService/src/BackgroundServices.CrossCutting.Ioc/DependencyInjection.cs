using BackgroundServices.Domain.Interfaces;
using BackgroundServices.Infrastructure.Context;
using BackgroundServices.Infrastructure.Mappings.Mongo;
using BackgroundServices.Infrastructure.Repositories;
using BackgroundServices.Services.Interfaces;
using BackgroundServices.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundServices.CrossCutting.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddScoped<IGerenciarLotesArquivos, GerenciarLotesArquivos>();
            services.AddScoped<IGerenciarArquivos, GerenciarArquivos>();
            services.AddScoped<ILoteService, LoteService>();
            services.AddScoped<IArquivoService, ArquivoService>();
            services.AddScoped<IApplicationErrorService, ApplicationErrorService>();
            services.AddScoped<IRelatorioService, RelatorioService>();
            #endregion

            #region Repositories
            services.AddScoped<ILoteRepository, LoteRepository>();
            services.AddScoped<IArquivoRepository, ArquivoRepository>();
            services.AddScoped<IApplicationErrorRepository, ApplicationErrorRepository>();
            services.AddScoped<IRelatorioRepository, RelatorioRepository>();
            #endregion

            services.AddScoped<MongoContext>();
            PersistMappings.Configure();

            return services;
        }
    }
}
