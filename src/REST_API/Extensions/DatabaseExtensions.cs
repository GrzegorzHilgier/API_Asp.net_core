using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using InfrastructureSP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace REST_API.Extensions
{
    public static class DatabaseExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static IServiceCollection AddEFInfrastructure(this IServiceCollection services, string connectionString)
        {
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<CatalogContext>(contextOptions =>
                {
                    contextOptions.UseSqlServer(
                        connectionString,
                        serverOptions => { serverOptions.MigrationsAssembly(typeof(Startup).Assembly.FullName); });
                });
            services.AddScoped<IItemRepository, ItemRepository>();
            return services;
        }

        public static IServiceCollection AddDapperInfrastructre(this IServiceCollection services,
            string connectionString)
        {
            return services.AddScoped<IItemRepository, ItemRepositorySP>();
        }
    }
}
