using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Catalog;
using Domain.Repositories;
using Domain.Requests.Artist;
using Domain.Responses.Item;

namespace Domain.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        public ArtistService(
            IArtistRepository artistRepository,
            IItemRepository itemRepository,
            IMapper mapper)
        {
            _artistRepository = artistRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArtistResponse>> GetArtistsAsync()
        {
            var result = await _artistRepository.GetAsync();
            return result.Select(x =>_mapper.Map<ArtistResponse>(x));
        }

        public async Task<ArtistResponse> GetArtistAsync(GetArtistRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var result = await _artistRepository.GetAsync(request.Id);
            return result == null ? null : _mapper.Map<ArtistResponse>(result);
        }

        public async Task<IEnumerable<ItemResponse>> GetItemByArtistIdAsync(GetArtistRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var result = await
                _itemRepository.GetItemByArtistIdAsync(request.Id);
            return result.Select(x =>_mapper.Map<ItemResponse>(x));
        }

        public async Task<ArtistResponse> AddArtistAsync(AddArtistRequest request)
        {
            var item = _mapper.Map<Artist>(request);
            var result = _artistRepository.Add(item);
            await _artistRepository.UnitOfWork.SaveChangesAsync();
            return _mapper.Map<ArtistResponse>(result);
        }
    }
}
