using AutoMapper;
using RA.Entitys;
using RA.Models;

namespace RA
{
    public class RestaurantMappingProfile: Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(x => x.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(x => x.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(x => x.PostCode, c => c.MapFrom(s => s.Address.PostalCode));
           
            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address,
                c=>c.MapFrom(dto => new Address()
                { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));

            CreateMap<CreateDishDto, Dish>();
                    
        }
    }
}
