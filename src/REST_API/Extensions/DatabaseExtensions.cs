﻿using System;
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
        public static IServiceCollection AddEFInfrastructure(this IServiceCollection services, string connectionStringCatalogDatabase, string connectionStringCartDatabase)
        {
            services
                .AddDbContext<CatalogContext>(contextOptions =>
                {
                    contextOptions.UseSqlServer(
                        connectionStringCatalogDatabase,
                        serverOptions => { serverOptions.MigrationsAssembly(typeof(Startup).Assembly.FullName); });
                })
                .AddDbContext<CartContext>(contextOptions =>
                {
                    contextOptions.UseSqlServer(
                        connectionStringCartDatabase,
                        serverOptions => { serverOptions.MigrationsAssembly(typeof(Startup).Assembly.FullName); });
                });
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            return services;
        }

        public static IServiceCollection AddDapperInfrastructre(this IServiceCollection services,
            string connectionString)
        {
            return services.AddScoped<IItemRepository, ItemRepositorySP>();
        }
    }
}
