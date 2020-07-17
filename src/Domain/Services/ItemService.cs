using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Requests.Item;
using Domain.Responses.Item;

namespace Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _itemMapper;

        public ItemService(IItemRepository itemRepository, IMapper itemMapper)
        {
            _itemRepository = itemRepository;
            _itemMapper = itemMapper;
        }
        public async Task<IEnumerable<ItemResponse>> GetItemsAsync()
        {
            var result = await _itemRepository.GetAsync();
            return result.Select(x => _itemMapper.Map<ItemResponse>(x));
        }

        public async Task<ItemResponse> GetItemAsync(GetItemRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var entity = await _itemRepository.GetAsync(request.Id);
            return _itemMapper.Map<ItemResponse>(entity);
        }

        public async Task<ItemResponse> AddItemAsync(AddItemRequest request)
        {
            var item = _itemMapper.Map<Item>(request);
            var result = _itemRepository.Add(item);
            await _itemRepository.UnitOfWork.SaveChangesAsync();
            return _itemMapper.Map<ItemResponse>(result);
        }

        public async Task<ItemResponse> EditItemAsync(EditItemRequest request)
        {
            var existingRecord = await _itemRepository.GetAsync(request.Id);
            if (existingRecord == null)
            {
                throw new ArgumentException($"Entity with {request.Id} is not present");
            }
            var entity = _itemMapper.Map<Item>(request);
            var result = _itemRepository.Update(entity);
            await _itemRepository.UnitOfWork.SaveChangesAsync();
            return _itemMapper.Map<ItemResponse>(result);
        }
    }
}
