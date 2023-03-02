using AutoMapper;

namespace Ecommerce.API.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Entities.Order, Models.OrderDto>();
            CreateMap<Entities.OrderItem, Models.OrderItemDto>();
            CreateMap<Entities.Order, Models.OrderWithoutItemsDto>();
        }
    }
}