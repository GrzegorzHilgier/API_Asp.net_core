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

        public long Count => _cartContext.CartSessions.Count();

        public async Task<IEnumerable<Guid>> GetCarts(int pageSize, int pageIndex)
        {
            return await _cartContext
                .CartSessions
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .Select(c => c.Id)
                .ToListAsync();
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
                _cartContext.Add(item);
                await UnitOfWork.SaveChangesAsync();
                return await GetAsync(item.Id);
            }

            existingSession = item;
            return existingSession;
        }
    }
}
