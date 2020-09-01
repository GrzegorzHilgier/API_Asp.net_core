using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Commands;
using Domain.Entities.Cart;
using Domain.Repositories;
using Domain.Requests.Item;
using Domain.Responses.Cart;
using Domain.Responses.Item;
using Domain.Services;
using MediatR;

namespace Domain.Handlers
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CartSessionResponse>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public CreateCartHandler(ICartRepository repository, IMapper mapper, IItemRepository itemRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<CartSessionResponse> Handle(CreateCartCommand command, CancellationToken token)
        {
            var items = new ConcurrentBag<CartItem>();
            Parallel.ForEach(command.ItemsIds,async id =>
            {
                var item = await _itemRepository.GetAsync(id);
                if (item != default)
                {
                    items.Add(new CartItem{ItemId = item.Id});
                }
            });

            var entity = new CartSession
            {
                User = new CartUser
                {
                    Email = command.UserEmail
                },
                Items = items,
                ValidityDate = DateTimeOffset.Now.AddDays(1),
            };
            var session = await _repository.AddAsync(entity);
            var result = _mapper.Map<CartSessionResponse>(session);
            return result;
        }
    }
}
