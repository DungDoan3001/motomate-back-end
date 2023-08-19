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
            CreateMap<Collection, CollectionsOfBrand>();

            // Collection
            CreateMap<Collection, CollectionResponseModel>();
            CreateMap<CollectionRequestModel, Collection>();
            CreateMap<Model, ModelsOfCollection>();
            CreateMap<Brand, BrandOfCollection>();

        }
    }
}
