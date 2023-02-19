using System;
using AutoMapper;

namespace Ecommerce.API.Profiles
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<Entities.Product, Models.ProductDto>();
			CreateMap<Models.ProductForCreationDto, Entities.Product>();
			CreateMap<Entities.Product, Models.ProductDto>();
			CreateMap<Entities.Product, Models.ProductForUpdateDto>();
			CreateMap<Models.ProductForUpdateDto, Entities.Product>();
		}
	}
}

