using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Cart;
using Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SchemaDefinitions
{
    internal class CartItemSchemaDefinition : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("Items", CatalogContext.DEFAULT_SCHEMA);

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Quantity)
                .IsRequired();

            builder.HasOne(e => e.CartSession)
                .WithMany(c => c.Items)
                .HasForeignKey(k => k.CartSessionId);
        }

    }
}
