using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Requests.Item;
using Domain.Responses.Item;
using Fixtures;
using Newtonsoft.Json;
using REST_API;
using Xunit;

namespace API.Tests
{
    public class ItemControllerTests : IClassFixture<InMemoryApplicationFactory<Startup>>
    {
        private readonly InMemoryApplicationFactory<Startup> _factory;

        public ItemControllerTests(InMemoryApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/items/")]
        public async Task Get_should_return_success(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("86bff4f7-05a7-46b6-ba73-d43e2c45840f")]
        public async Task Get_by_id_should_return_item_data(string id)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/items/{id}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<Item>(responseContent);
            Assert.NotNull(responseEntity);
            Assert.Equal(id, responseEntity.Id.ToString());
        }

        [Theory]
        [InlineData("/api/items/?pageSize=1&pageIndex=0", 1,0)]
        [InlineData("/api/items/?pageSize=2&pageIndex=0", 2,0)]
        [InlineData("/api/items/?pageSize=1&pageIndex=1", 1,1)]
        public async Task get_should_return_paginated_data(string url, int
            pageSize, int pageIndex)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await
                response.Content.ReadAsStringAsync();
            var responseEntity =
                JsonConvert.DeserializeObject<PaginatedEntity<ItemResponse>>(responseContent);
            Assert.Equal(pageIndex, responseEntity.PageIndex);
            Assert.Equal(pageSize, responseEntity.PageSize);
        }

        [Fact]
        public async Task Add_should_create_new_record()
        {
            var request = new AddItemRequest
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
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/api/items", httpContent);
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response.Headers.Location);
        }

        [Fact]
        public async Task Update_should_modify_existing_item()
        {
            var client = _factory.CreateClient();
            var request = new EditItemRequest
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
            var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/items/{request.Id}", httpContent);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<Item>(responseContent);
            Assert.Equal(request.Name, responseEntity.Name);
            Assert.Equal(request.Description, responseEntity.Description);
            Assert.Equal(request.GenreId, responseEntity.GenreId);
            Assert.Equal(request.ArtistId, responseEntity.ArtistId);
        }
    }
}
