using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SchemaDefinitions
{
    internal class CartUserSchemaDefinition : IEntityTypeConfiguration<CartUser>
    {
        public void Configure(EntityTypeBuilder<CartUser> builder)
        {
            builder.ToTable("CartSession", CatalogContext.DEFAULT_SCHEMA);
            builder.Property(k => k.Email)
                .IsRequired()
                .HasMaxLength(70);
        }
    }
}
