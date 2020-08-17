using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Cart;

namespace Domain.Repositories
{
    public interface ICartRepository
    {
        IEnumerable<Guid> GetCarts();
        Task<CartSession> GetAsync(Guid id);
        Task<CartSession> AddOrUpdateAsync(CartSession item);
    }
}
