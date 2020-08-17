using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Catalog;
using Fixtures;
using Infrastructure.Repositories;
using Xunit;

namespace Infrastructure.Tests
{
    public class ArtistRepositoryTests :
        IClassFixture<CatalogContextFactory>
    {
        private readonly CatalogContextFactory _factory;
        public ArtistRepositoryTests(CatalogContextFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_add_and_return_record_by_id()
        {
            var artist = new Artist
            {
                ArtistId = new Guid(), 
                ArtistName = "John"
            };
            var sut = new ArtistRepository(_factory.ContextInstance);
            var addedArtist = sut.Add(artist);
            var result = await sut.GetAsync(addedArtist.ArtistId);
            Assert.Equal(artist.ArtistId, result.ArtistId);
            Assert.Equal(artist.ArtistName, result.ArtistName);
        }

    }
}
