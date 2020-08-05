using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Requests.Item;
using Domain.Responses.Item;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST_API.Conventions;
using REST_API.Filters;

namespace REST_API.Controllers
{
    [Produces("application/json")]
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

        /// <summary>
        /// Gets paginated Items.
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns>Returns found Items.</returns>
        [HttpGet]
        [ApiConventionMethod(typeof(ItemAPIConvention), nameof(ItemAPIConvention.Get))]
        public async Task<IActionResult> Get([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _itemService.GetItemsAsync(pageSize, pageIndex);
            if (result.Data.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("{id:guid}")]
        [ItemExists]
        [ApiConventionMethod(typeof(ItemAPIConvention), nameof(ItemAPIConvention.GetById))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _itemService.GetItemAsync(new GetItemRequest {Id = id});
            return Ok(result);
        }

        [HttpPost]
        [ApiConventionMethod(typeof(ItemAPIConvention), nameof(ItemAPIConvention.GetById))]
        public async Task<IActionResult> Post(AddItemRequest request)
        {
            var result = await _itemService.AddItemAsync(request);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, null);
        }

        [HttpPut("{id:guid}")]
        [ItemExists]
        [ApiConventionMethod(typeof(ItemAPIConvention), nameof(ItemAPIConvention.GetById))]
        public async Task<IActionResult> Put(Guid id, EditItemRequest request)
        {
            request.Id = id;
            var result = await _itemService.EditItemAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [ItemExists]
        [ApiConventionMethod(typeof(ItemAPIConvention), nameof(ItemAPIConvention.Delete))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteItemRequest{ Id = id};
            await _itemService.DeleteItemAsync(request);
            return NoContent();
        }
    }
}
