using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Requests.Artist;
using Domain.Responses.Item;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using REST_API.Filters;

namespace REST_API.Controllers
{
    [Route("api/artist")]
    [ApiController]
    [JsonException]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _artistService.GetArtistsAsync();
            var totalItems = result.ToList().Count;
            var itemsOnPage = result
                .OrderBy(c => c.ArtistName)
                .Skip(pageSize * pageIndex)
                .Take(pageSize);
            var model = new PaginatedEntity<ArtistResponse>(
                pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _artistService.GetArtistAsync(new
                GetArtistRequest {Id = id});
            return Ok(result);
        }

        [HttpGet("{id:guid}/items")]
        public async Task<IActionResult> GetItemsById(Guid id)
        {
            var result = await _artistService.GetItemByArtistIdAsync(new
                GetArtistRequest { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddArtistRequest request)
        {
            var result = await _artistService.AddArtistAsync(request);
            return CreatedAtAction(nameof(GetById), new { id =
                result.ArtistId }, null);
        }
    }
}
    

