using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST_API.Conventions;
using REST_API.Filters;

namespace REST_API.Controllers
{
    [Produces("application/json")]
    [Route("api/cart")]
    [ApiController]
    [JsonException]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(CartAPIConvention), nameof(CartAPIConvention.Get))]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetCartsCommand { PageSize = 10, PageIndex = 0 });
            if (result.Data.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetCartCommand{Id = id});
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCartCommand request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new {id = result.Id});
        }

        [HttpPut("{cartId:guid}/items/{id:guid}")]
        public async Task<IActionResult> Put(Guid cartId, Guid id)
        {
            var result = await _mediator.Send(new
                UpdateCartItemQuantityCommand
                {
                    CartId = cartId,
                    CartItemId = id,
                    IsAddOperation = true
                });
            return Ok(result);
        }
        [HttpDelete("{cartId:guid}/items/{id:guid}")]
        public async Task<IActionResult> Delete(Guid cartId, Guid id)
        {
            var result = await _mediator.Send(new
                UpdateCartItemQuantityCommand
                {
                    CartId = cartId,
                    CartItemId = id,
                    IsAddOperation = false
                });
            return Ok(result);
        }
    }
}
