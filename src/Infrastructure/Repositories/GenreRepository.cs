using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly CatalogContext _catalogContext;
        public IUnitOfWork UnitOfWork => _catalogContext;
        public GenreRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public async Task<IEnumerable<Genre>> GetAsync()
        {
            return await _catalogContext.Genre
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Genre> GetAsync(Guid id)
        {
            var item = await _catalogContext.Genre
                .FindAsync(id);
            if (item == null) return null;
            _catalogContext.Entry(item).State = EntityState.Detached;
            return item;
        }
        public Genre Add(Genre item)
        {
            return _catalogContext.Genre.Add(item).Entity;
        }
    }
}
