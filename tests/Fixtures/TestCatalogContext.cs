using Domain.Entities;
using Domain.Entities.Catalog;
using Infrastructure;
using Infrastructure.Tests.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Fixtures
{
    public class TestCatalogContext : CatalogContext
    {
        public TestCatalogContext(DbContextOptions<CatalogContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed<Artist>("./MockData/artist.json");
            modelBuilder.Seed<Genre>("./MockData/genre.json");
            modelBuilder.Seed<Item>("./MockData/item.json");
        }
    }
}
