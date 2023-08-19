using Application.Web.Database.Models;
using AutoMapper;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.DTOs.RequestModels;

namespace Applicaton.Web.API.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // WeatherForecast
            CreateMap<WeatherForecast, WeatherForecastResponseModel>();
            CreateMap<WeatherForecastRequestModel, WeatherForecast>();

            // User
            CreateMap<UserRegistrationRequestModel, User>();

            // Brand
            CreateMap<Brand, BrandResponseModel>();
            CreateMap<BrandRequestModel, Brand>();
            CreateMap<Collection, BrandCollections>()
                .ForMember(dest => dest.CollectionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CollectionName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
