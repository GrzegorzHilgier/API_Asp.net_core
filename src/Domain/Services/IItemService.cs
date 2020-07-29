using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Requests.Item;
using Domain.Responses.Item;

namespace Domain.Services
{
    public interface IItemService
    {
        Task<IEnumerable<ItemResponse>> GetItemsAsync();
        Task<PaginatedEntity<ItemResponse>> GetItemsAsync(int pageSize, int pageIndex);
        Task<ItemResponse> GetItemAsync(GetItemRequest request);
        Task<ItemResponse> AddItemAsync(AddItemRequest request);
        Task<ItemResponse> EditItemAsync(EditItemRequest request);
        Task<ItemResponse> DeleteItemAsync(DeleteItemRequest request);
    }
}
