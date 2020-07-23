using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IItemRepository : IRepository
    {
        public long Count { get; }
        public Task<IEnumerable<Item>> GetAsync();
        public Task<IEnumerable<Item>> GetAsync(int pageSize, int pageIndex);
        Task<Item> GetAsync(Guid id);
        Item Add(Item item);
        Item Update(Item item);
        Item Delete(Item item);
    }
}
