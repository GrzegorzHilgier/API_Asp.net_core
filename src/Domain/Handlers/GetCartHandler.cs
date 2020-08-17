using System;
using System.Collections.Generic;
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
    public class GetCartHandler : IRequestHandler<GetCartCommand, CartSessionResponse>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;

        public GetCartHandler(ICartRepository repository, IMapper mapper, IItemService itemService)
        {
            _repository = repository;
            _mapper = mapper;
            _itemService = itemService;
        }

        public async Task<CartSessionResponse> Handle(GetCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(request.Id);
            var response = _mapper.Map<CartSessionResponse>(result);
            return response;
        }
    }
}
