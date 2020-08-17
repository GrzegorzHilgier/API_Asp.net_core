using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Cart;
using Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SchemaDefinitions
{
    internal class CartSessionSchemaDefinition : IEntityTypeConfiguration<CartSession>
    {
        public void Configure(EntityTypeBuilder<CartSession> builder)
        {
            builder.ToTable("CartSession", CatalogContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.Id);
            builder.Property(p => p.User)
                .IsRequired();
            builder.Property(p => p.ValidityDate)
                .IsRequired();
        }
    }
}
