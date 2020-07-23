using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Requests.Artist;
using Domain.Responses.Item;

namespace Domain.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistResponse>> GetArtistsAsync();
        Task<ArtistResponse> GetArtistAsync(GetArtistRequest request);
        Task<IEnumerable<ItemResponse>> GetItemByArtistIdAsync(GetArtistRequest request);
        Task<ArtistResponse> AddArtistAsync(AddArtistRequest request);
    }
}
