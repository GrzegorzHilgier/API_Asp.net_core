using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.SchemaDefinitions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "catalog";
        public DbSet<Item> Items { get; set; }
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemEntityScheamDefinition());
            modelBuilder.ApplyConfiguration(new GenreEntitySchemaConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistEntitySchemaDefinition());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
