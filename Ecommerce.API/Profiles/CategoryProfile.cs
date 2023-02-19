using System;
using AutoMapper;

namespace Ecommerce.API.Profiles
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<Entities.Category, Models.CategoryDto>();
			CreateMap<Entities.Category, Models.CategoryWithoutProductsDto>();
		}
	}
}

