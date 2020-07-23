using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Infrastructure;
using Infrastructure.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using REST_API.Extensions;

namespace Fixtures
{
    public class InMemoryApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseEnvironment("Testing")
                .ConfigureTestServices(services =>
                {
                    var options = new DbContextOptionsBuilder<CatalogContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;
                    services
                        .AddScoped<CatalogContext>(provider => new TestCatalogContext(options))
                        .AddAutoMapper()
                        .AddDomainServices()
                        .AddControllers().AddValidation();
                    var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<CatalogContext>();
                    db.Database.EnsureCreated();
                });
        }
    }
}
