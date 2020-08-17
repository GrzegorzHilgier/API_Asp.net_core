using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Entities.Cart;
using Domain.Responses.Cart;

namespace Domain.Mappers
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartSession, CartSessionResponse>().ReverseMap();
        }
    }
}
