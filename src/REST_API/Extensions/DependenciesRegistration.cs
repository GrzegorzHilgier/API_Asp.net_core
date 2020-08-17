using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Mappers;
using Domain.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace REST_API.Extensions
{
    public static class DependenciesRegistration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IGenreService, GenreService>();
            return services;
        }

        public static IMvcBuilder AddValidation(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(configuration => 
                configuration.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            return builder;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CatalogProfile());
                cfg.AddProfile(new CartProfile());
            });
            var mapperInstance = mockMapper.CreateMapper();
            services.AddSingleton(mapperInstance);
            return services;
        }
    }
}
