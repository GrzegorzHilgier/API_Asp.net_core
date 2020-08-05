using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Responses.Item
{
    public class ItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LabelName { get; set; }
        public PriceResponse Price { get; set; }
        public string PictureUri { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public string Format { get; set; }
        public int AvailableStock { get; set; }
        public GenreResponse Genre { get; set; }
        public ArtistResponse Artist { get; set; }
    }
}
