using System;
using AutoMapper;

namespace Ecommerce.API.Profiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<Entities.CartItem, Models.CartItemDto>();
            CreateMap<Models.CartItemForCreationDto, Entities.CartItem>();
            CreateMap<Entities.CartItem, Models.CartItemForCreationDto>();
            CreateMap<Entities.CartItem, Models.CartItemForUpdateDto>();
            CreateMap<Models.CartItemForUpdateDto, Entities.CartItem>();
        }
    }
}