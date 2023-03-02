using System;
using AutoMapper;

namespace Ecommerce.API.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Entities.Cart, Models.CartDto>();
        }
    }
}