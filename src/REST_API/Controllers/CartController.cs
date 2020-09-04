using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Entities;
using Domain.Responses.Cart;
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

        /// <summary>
        /// Gets paginated Carts.
        /// </summary>
        /// <returns><see cref="PaginatedEntity{TEntity}"/> of found Card.</returns>
        [HttpGet]
        [ApiConventionMethod(typeof(CartAPIConvention), nameof(CartAPIConvention.Get))]
        public async Task<IActionResult> Get([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _mediator.Send(new GetCartsCommand { PageSize = pageSize, PageIndex = pageIndex });
            if (result.Data.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Gets Cart by Id.
        /// </summary>
        /// <param name="id">Requested Id.</param>
        /// <returns><see cref="CartSessionResponse"/> of requested cart.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetCartCommand{Id = id});
            return Ok(result);
        }

        /// <summary>
        /// Posts a Cart.
        /// </summary>
        /// <param name="request">Create cart command data.</param>
        /// <returns><see cref="Guid"/> of created cart.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateCartCommand request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { result.Id }, value: null);
        }

        /// <summary>
        /// Puts an item in cart.
        /// </summary>
        /// <param name="cartId">Requested cart.</param>
        /// <param name="id">Updated item.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes an item in cart.
        /// </summary>
        /// <param name="cartId">Requested cart.</param>
        /// <param name="id">Updated item.</param>
        /// <returns></returns>
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
