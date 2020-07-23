using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Requests.Item;
using Domain.Services;
using Fixtures;
using Infrastructure.Repositories;
using Xunit;

namespace Domain.Tests
{
    public class ItemServiceTests : IClassFixture<CatalogContextFactory>
    {
        private readonly ItemService _itemService;

        public ItemServiceTests(CatalogContextFactory catalogContextFactory)
        {
            var itemRepository = new ItemRepository(catalogContextFactory.ContextInstance);
            var mapper = catalogContextFactory.MapperInstance;
            _itemService = new ItemService(itemRepository, mapper);
        }

        [Fact]
        public async Task GetItems_should_return_right_data()
        {
            var result = await _itemService.GetItemsAsync();
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("b5b05534-9263-448c-a69e-0bbd8b3eb90e")]
        public async Task GetItem_should_return_right_data(string guid)
        {
            var result = await _itemService.GetItemAsync(new GetItemRequest { Id =
                new Guid(guid) });
            Assert.Equal(new Guid(guid), result.Id);
        }

        [Fact]
        public void GetItem_should_thrown_exception_with_null_id()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _itemService.GetItemAsync(null));
        }

        [Fact]
        public async Task AddItem_should_add_right_entity()
        {
            var testItem = new AddItemRequest
            {
                Name = "Test album",
                GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
                ArtistId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab"),
                Price = new Price {Amount = 13, Currency = "EUR"},
                AvailableStock = 2,
                Description = "test",
                PictureUri = "//uri",
                Format = "format",
                LabelName = "name",
                ReleaseDate = DateTimeOffset.Now
            };
            var result = await _itemService.AddItemAsync(testItem);
            Assert.Equal(testItem.Name, result.Name);
            Assert.Equal(testItem.Description, result.Description);
            Assert.Equal(testItem.GenreId, result.GenreId);
            Assert.Equal(testItem.ArtistId, result.ArtistId);
            Assert.Equal(testItem.Price.Amount, result.Price.Amount);
            Assert.Equal(testItem.Price.Currency, result.Price.Currency);
        }
    }
}
