using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Mappers;
using Infrastructure;
using Infrastructure.Tests;
using Microsoft.EntityFrameworkCore;

namespace Fixtures
{
    public class CatalogContextFactory
    {
        public readonly TestCatalogContext ContextInstance;
        public readonly IMapper MapperInstance;

        public CatalogContextFactory()
        {
            var contextOptions = new
                    DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            EnsureCreation(contextOptions);
            ContextInstance = new TestCatalogContext(contextOptions);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CatalogProfile());
            });
            MapperInstance = mockMapper.CreateMapper();
        }

        private void EnsureCreation(DbContextOptions<CatalogContext>
            contextOptions)
        {
            using var context = new TestCatalogContext(contextOptions);
            context.Database.EnsureCreated();
        }
    }
}
