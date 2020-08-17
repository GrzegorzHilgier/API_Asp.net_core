using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Cart;
using Domain.Repositories;
using Infrastructure.SchemaDefinitions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class CartContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "cart";
        public DbSet<CartSession> CartSessions { get; set; }
        public DbSet<CartUser> CartUsers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public CartContext(DbContextOptions<CartContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CartItemSchemaDefinition());
            modelBuilder.ApplyConfiguration(new CartSessionSchemaDefinition());
            modelBuilder.ApplyConfiguration(new CartUserSchemaDefinition());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
