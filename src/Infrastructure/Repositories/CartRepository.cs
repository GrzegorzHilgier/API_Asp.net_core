using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Cart;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartContext _cartContext;
        public IUnitOfWork UnitOfWork => _cartContext;

        public CartRepository(CartContext cartContext)
        {
            _cartContext = cartContext;
        }
        public IEnumerable<Guid> GetCarts()
        {
            return _cartContext
                .CartSessions
                .Select(c => c.Id)
                .ToArray();
        }

        public async Task<CartSession> GetAsync(Guid id)
        {
            return await _cartContext
                .CartSessions
                .Where(c => c.Id == id)
                .Include(x => x.Items)
                .Include(y => y.User)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<CartSession> AddOrUpdateAsync(CartSession item)
        {
            var existingSession = await GetAsync(item.Id);
            if (existingSession == null)
            {
                await _cartContext.AddAsync(item);
                return await GetAsync(item.Id);
            }
            else
            {
                existingSession = item;
                return existingSession;
            }
        }
    }
}
