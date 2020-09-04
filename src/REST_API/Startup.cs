using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using Infrastructure;
using AutoMapper;
using Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using REST_API.Extensions;
using REST_API.Middleware;
using Microsoft.OpenApi.Models;

namespace REST_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddEFInfrastructure(Configuration.GetSection("DataSource:ConnectionStringCatalogDatabase").Value, 
                                     Configuration.GetSection("DataSource:ConnectionStringCartDatabase").Value)
                .AddAutoMapper()
                .AddDomainServices()
                .AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateCartCommand).Assembly)
                .AddSwaggerGen(configuration =>
                {
                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    configuration.IncludeXmlComments(xmlPath);
                })
                .AddControllers().AddValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                })
                .UseMiddleware<ResponseTimeMiddlewareAsync>()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
