using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Catalog
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string GenreDescription { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
