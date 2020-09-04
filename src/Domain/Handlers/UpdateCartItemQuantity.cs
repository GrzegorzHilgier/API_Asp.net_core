using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Commands;
using Domain.Repositories;
using Domain.Responses.Cart;
using Domain.Services;
using MediatR;

namespace Domain.Handlers
{
    public class UpdateCartItemQuantity : IRequestHandler<UpdateCartItemQuantityCommand, CartSessionResponse>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;
        private readonly IItemService _service;

        public UpdateCartItemQuantity(ICartRepository repository, IMapper mapper, IItemService service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<CartSessionResponse> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var cartDetail = await _repository.GetAsync(request.CartId);
            if (request.IsAddOperation)
            {
                cartDetail.Items.FirstOrDefault(x => x.Id == request.CartItemId)?.IncreaseQuantity();
            }
            else
            {
                cartDetail.Items.FirstOrDefault(x => x.Id == request.CartItemId)?.DecreaseQuantity();
            }

            cartDetail.Items.ToList().RemoveAll(x => x.Quantity <= 0);
            var result = await _repository.UpdateAsync(cartDetail);
            var response = _mapper.Map<CartSessionResponse>(result);
            return response;
        }
    }
}
