using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Requests.Item;
using Domain.Responses.Item;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST_API.Filters;

namespace REST_API.Controllers
{
    [Route("api/Items")]
    [ApiController]
    [JsonException]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _itemService.GetItemsAsync(pageSize, pageIndex);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ItemExists]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _itemService.GetItemAsync(new GetItemRequest {Id = id});
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddItemRequest request)
        {
            var result = await _itemService.AddItemAsync(request);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, null);
        }

        [HttpPut("{id:guid}")]
        [ItemExists]
        public async Task<IActionResult> Put(Guid id, EditItemRequest request)
        {
            request.Id = id;
            var result = await _itemService.EditItemAsync(request);
            return Ok(result);
        }
    }
}
