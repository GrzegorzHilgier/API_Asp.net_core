using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Catalog
{
    public class Artist
    {
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; }  
        public ICollection<Item> Items { get; set; }

    }
}
