using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Requests.Artist;
using Domain.Requests.Genre;
using Domain.Responses.Item;

namespace Domain.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        public GenreService(
            IGenreRepository genreRepository,
            IItemRepository itemRepository,
            IMapper mapper)
        {
            _genreRepository = genreRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreResponse>> GetGenreAsync()
        {
            var result = await _genreRepository.GetAsync();
            return result.Select(x =>_mapper.Map<GenreResponse>(x));
        }

        public async Task<GenreResponse> GetGenreAsync(GetGenreRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var result = await _genreRepository.GetAsync(request.Id);
            return result == null ? null : _mapper.Map<GenreResponse>(result);
        }

        public async Task<IEnumerable<ItemResponse>> GetItemByGenreIdAsync(GetGenreRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var result = await
                _itemRepository.GetItemByArtistIdAsync(request.Id);
            return result.Select(x =>_mapper.Map<ItemResponse>(x));
        }

        public async Task<GenreResponse> AddGenreAsync(AddGenreRequest request)
        {
            var item = _mapper.Map<Genre>(request);
            var result = _genreRepository.Add(item);
            await _genreRepository.UnitOfWork.SaveChangesAsync();
            return _mapper.Map<GenreResponse>(result);
        }
    }
}
