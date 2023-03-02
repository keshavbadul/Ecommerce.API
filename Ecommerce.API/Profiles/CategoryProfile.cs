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
			CreateMap<Models.CategoryForCreationDto, Entities.Category>();
			CreateMap<Entities.Category, Models.CategoryForUpdateDto>();
			CreateMap<Models.CategoryForUpdateDto, Entities.Category>();
		}
	}
}

