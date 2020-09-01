using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Cart;

namespace Domain.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Guid>> GetCarts(int pageSize, int pageIndex);
        long Count { get; }
        Task<CartSession> GetAsync(Guid id);
        Task<CartSession> AddOrUpdateAsync(CartSession item);
    }
}
