﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IArtistRepository : IRepository
    {
        Task<IEnumerable<Artist>> GetAsync();
        Task<Artist> GetAsync(Guid id);
        Artist Add(Artist item);
    }
}
