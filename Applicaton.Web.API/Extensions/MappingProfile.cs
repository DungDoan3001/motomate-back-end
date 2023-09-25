using Application.Web.Database.Models;
using AutoMapper;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.DTOs.RequestModels;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Applicaton.Web.API.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var textInfo = CultureInfo.CurrentCulture.TextInfo;

            // WeatherForecast
            CreateMap<WeatherForecast, WeatherForecastResponseModel>();
            CreateMap<WeatherForecastRequestModel, WeatherForecast>();

            // User
            CreateMap<UserRegistrationRequestModel, User>();
            CreateMap<User, UserResponseModel>();
            CreateMap<UserRequestModel, User>();
            // Brand
            CreateMap<Brand, BrandResponseModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => textInfo.ToTitleCase(src.Name.ToLower())))
                .AfterMap((src, dest) =>
                {
                    var brandImage = new ImageOfBrand();

                    if(src.BrandImages.Any())
                    {
                        var image = src.BrandImages.FirstOrDefault().Image;

                        brandImage.Image = image.ImageUrl;

                        brandImage.PublicId = image.PublicId;
                    };

                    dest.Image = brandImage;
                });
            CreateMap<BrandRequestModel, Brand>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim().ToUpper()));

            CreateMap<Collection, CollectionsOfBrand>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => textInfo.ToTitleCase(src.Name.ToLower())));

            // Collection
            CreateMap<Collection, CollectionResponseModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => textInfo.ToTitleCase(src.Name.ToLower())));
            CreateMap<CollectionRequestModel, Collection>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim().ToUpper()));

            CreateMap<Model, ModelsOfCollection>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => textInfo.ToTitleCase(src.Name.ToLower())));
            CreateMap<Brand, BrandOfCollection>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => textInfo.ToTitleCase(src.Name.ToLower())));



            // Color
            CreateMap<Color, ColorResponseModel>()
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => textInfo.ToTitleCase(src.Name.ToLower())));
            CreateMap<ColorRequestModel, Color>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Color.Trim().ToUpper()));

            // Model
            CreateMap<Model, ModelResponseModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => textInfo.ToTitleCase(src.Name.ToLower())))
                .AfterMap((src, dest) =>
                {
                    var colors = new List<ColorOfModel>();

                    foreach(var modelColor in src.ModelColors)
                    {
                        colors.Add(new ColorOfModel
                        {
                            Id = modelColor.Color.Id,
                            Color = textInfo.ToTitleCase(modelColor.Color.Name.ToLower())
                        });
                    }

                    dest.Colors = colors;
                });
                
            CreateMap<Collection, CollectionOfModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => textInfo.ToTitleCase(src.Name.ToLower())));

            CreateMap<ModelRequestModel, Model>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim().ToUpper()));

            // Vehicles
            CreateMap<VehicleRequestModel, Vehicle>()
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.ColorName.Trim().ToUpper()))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.Trim().ToUpper()));

            CreateMap<Vehicle, VehicleResponseModel>()
                .AfterMap((src, dest) =>
                {
                    var specifications = new VehicleSpecifications
                    {
                        ModelId = src.Model.Id,
                        ModelName = textInfo.ToTitleCase(src.Model.Name),
                        Year = src.Model.Year,
                        Capacity = src.Model.Capacity,
                        CollectionId = src.Model.CollectionId,
                        CollectionName = textInfo.ToTitleCase(src.Model.Collection.Name),
                        BrandId = src.Model.Collection.Brand.Id,
                        BrandName = textInfo.ToTitleCase(src.Model.Collection.Brand.Name),
                    };

                    dest.Color = textInfo.ToTitleCase(src.Color.Name);
                    dest.Specifications = specifications;
                });

            CreateMap<User, VehicleOwner>()
                .AfterMap((src, dest) =>
                {
                    dest.Name = textInfo.ToTitleCase(src.LastName + " " + src.FirstName);
					dest.Email = src.Email;
					dest.PhoneNumber = src.PhoneNumber;
                    dest.Address = src.Address;
				});
		}
    }
}
