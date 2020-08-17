using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SchemaDefinitions
{
    internal class ArtistEntitySchemaDefinition : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artists", CatalogContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.ArtistId);
            builder.Property(p => p.ArtistId);
            builder.Property(p => p.ArtistName)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
