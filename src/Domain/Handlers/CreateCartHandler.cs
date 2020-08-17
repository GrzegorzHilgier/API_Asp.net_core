using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Commands;
using Domain.Entities.Cart;
using Domain.Repositories;
using Domain.Responses.Cart;
using Domain.Services;
using MediatR;

namespace Domain.Handlers
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CartSessionResponse>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;

        public CreateCartHandler(ICartRepository repository, IMapper mapper, IItemService itemService)
        {
            _repository = repository;
            _mapper = mapper;
            _itemService = itemService;
        }

        public async Task<CartSessionResponse> Handle(CreateCartCommand command, CancellationToken token)
        {
            var entity = new CartSession
            {
                //Items?
                User = new CartUser
                {
                    Email = command.UserEmail
                },
                ValidityDate = DateTimeOffset.Now.AddDays(1),
                Id = Guid.NewGuid()
            };
            var session = await _repository.AddOrUpdateAsync(entity);
            var result = _mapper.Map<CartSessionResponse>(session);
            return result;
        }
    }
}
