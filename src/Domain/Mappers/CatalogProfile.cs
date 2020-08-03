using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.Requests.Item;
using Domain.Responses.Item;
using AutoMapper;
using Domain.Requests.Artist;
using Domain.Requests.Genre;

namespace Domain.Mappers
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<ItemResponse, Item>().ReverseMap();
            CreateMap<AddItemRequest, Item>().ReverseMap();
            CreateMap<EditItemRequest, Item>().ReverseMap();
            CreateMap<GenreResponse, Genre>().ReverseMap();
            CreateMap<GetGenreRequest, Genre>().ReverseMap();
            CreateMap<ArtistResponse, Artist>().ReverseMap();
            CreateMap<GetArtistRequest, Artist>().ReverseMap();
            CreateMap<Price, PriceResponse>().ReverseMap();
        }
    }
}
