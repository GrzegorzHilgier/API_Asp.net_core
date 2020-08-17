using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Catalog;
using Fixtures;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.Tests
{
    public class ItemRepositoryTests : IClassFixture<CatalogContextFactory>
    {
        private readonly ItemRepository _sut;

        public ItemRepositoryTests(CatalogContextFactory catalogContextFactory)
        {
            var context = catalogContextFactory.ContextInstance;
            _sut = new ItemRepository(context);
        }

        [Fact]
        public async Task should_get_data()
        {
            var result = await _sut.GetAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task should_returns_null_with_id_not_present()
        {
            var result = await _sut.GetAsync(Guid.NewGuid());
            Assert.Null(result);
        }

        [Theory]
        [InlineData("b5b05534-9263-448c-a69e-0bbd8b3eb90e")]
        public async Task should_return_record_by_id(string guid)
        {
            var result = await _sut.GetAsync(new Guid(guid));
            Assert.Equal<Guid>(new Guid(guid),result.Id);
        }

        [Fact]
        public async Task should_add_new_item()
        {
            var testItem = new Item
            {
                Name = "Test album",
                Description = "Description",
                LabelName = "Label name",
                Price = new Price {Amount = 13, Currency = "EUR"},
                PictureUri = "https://mycdn.com/pictures/32423423",
                ReleaseDate = DateTimeOffset.Now,
                AvailableStock = 6,
                GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
                ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
            };

            _sut.Add(testItem);
            await _sut.UnitOfWork.SaveEntitiesAsync();
            var result = await _sut.GetAsync(testItem.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task should_update_item()
        {
            var testItem = new Item
            {
                Id = new Guid("b5b05534-9263-448c-a69e-0bbd8b3eb90e"),
                Name = "Test album",
                Description = "Description updated",
                LabelName = "Label name",
                Price = new Price { Amount = 50, Currency = "EUR" },
                PictureUri = "https://mycdn.com/pictures/32423423",
                ReleaseDate = DateTimeOffset.Now,
                AvailableStock = 6,
                GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
                ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
            };

            _sut.Update(testItem);
            await _sut.UnitOfWork.SaveEntitiesAsync();
            var result = await _sut.GetAsync(testItem.Id);
            Assert.NotNull(result);
            Assert.Equal("Description updated", result.Description);
        }
    }
}
