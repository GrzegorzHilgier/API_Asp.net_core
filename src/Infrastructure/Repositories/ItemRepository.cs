﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Catalog;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly CatalogContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ItemRepository(CatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public long Count => _context.Items.Count();

        public async Task<IEnumerable<Item>> GetAsync()
        {
            return await _context
                        .Items
                        .Where(x => !x.IsDeleted)
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetAsync(int pageSize, int pageIndex)
        {
            return await _context
                .Items
                .Where(x => !x.IsDeleted)
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .Include(item => item.Artist)
                .Include(item => item.Genre)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Item> GetAsync(Guid id)
        {
            var item = await _context.Items
                .AsNoTracking()
                .Where(x => x.Id == id && !x.IsDeleted)
                .Include(x => x.Genre)
                .Include(x => x.Artist)
                .FirstOrDefaultAsync();
            return item;
        }

        public async Task<IEnumerable<Item>> GetItemByArtistIdAsync(Guid id)
        {
            var items = await _context
                .Items
                .Where(item => item.ArtistId == id)
                .Include(x => x.Genre)
                .Include(x => x.Artist)
                .ToListAsync();
            return items;
        }

        public async Task<IEnumerable<Item>> GetItemByGenreIdAsync(Guid id)
        {
            var items = await _context.Items
                .Where(item => item.GenreId == id)
                .Include(x => x.Genre)
                .Include(x => x.Artist)
                .ToListAsync();
            return items;
        }
        
        public Item Add(Item item)
        {
            return _context.Items
                .Add(item).Entity;
        }

        public Item Update(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }

        public Item Delete(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
