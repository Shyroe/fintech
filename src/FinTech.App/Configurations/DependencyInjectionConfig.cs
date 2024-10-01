using FinTech.App.Extensions;
using FinTech.Business.Intefaces;
using FinTech.Business.Notificacoes;
using FinTech.Business.Services;
using FinTech.Data.Context;
using FinTech.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace FinTech.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
            services.AddScoped<IStatusNotaFiscalRepository, StatusNotaFiscalRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<INotaFiscalService, NotaFiscalService>();

            return services;
        }
    }
}