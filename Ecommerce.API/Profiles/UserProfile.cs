using AutoMapper;

namespace Ecommerce.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Models.UserForCreationDto, Entities.User>();
        }
    }
}