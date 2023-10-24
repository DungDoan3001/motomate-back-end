using Application.Web.Database.Models;
using AutoMapper;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.DTOs.RequestModels;
using System.Globalization;
using Application.Web.Service.Helpers;

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

                    if (src.BrandImages.Any())
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

                    foreach (var modelColor in src.ModelColors)
                    {
                        colors.Add(new ColorOfModel
                        {
                            Id = modelColor.Color.Id,
                            Color = textInfo.ToTitleCase(modelColor.Color.Name.ToLower()),
                            HexCode = modelColor.Color.HexCode
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
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Trim().ToUpper()))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District.Trim().ToUpper()))
                .ForMember(dest => dest.Ward, opt => opt.MapFrom(src => src.Ward.Trim().ToUpper()))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Trim().ToUpper()))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate.Trim().ToUpper()))
                .ForMember(dest => dest.InsuranceNumber, opt => opt.MapFrom(src => src.InsuranceNumber.Trim().ToUpper()));

            CreateMap<Vehicle, VehicleResponseModel>()
                .AfterMap((src, dest) =>
                {
                    var specifications = new VehicleSpecifications
                    {
                        ModelId = src.Model.Id,
                        ModelName = textInfo.ToTitleCase(src.Model.Name.ToLower()),
                        Year = src.Model.Year,
                        Capacity = src.Model.Capacity,
                        CollectionId = src.Model.CollectionId,
                        CollectionName = textInfo.ToTitleCase(src.Model.Collection.Name.ToLower()),
                        BrandId = src.Model.Collection.Brand.Id,
                        BrandName = textInfo.ToTitleCase(src.Model.Collection.Brand.Name.ToLower()),
                        Color = textInfo.ToTitleCase(src.Color.Name.ToLower()),
                        HexCode = src.Color.HexCode,
                    };

                    var vehicleImages = new List<ImageOfVehicle>();

                    foreach (var image in src.VehicleImages)
                    {
                        vehicleImages.Add(new ImageOfVehicle
                        {
                            Image = image.Image.ImageUrl,
                            PublicId = image.Image.PublicId
                        });
                    }

                    dest.Address = textInfo.ToTitleCase(src.Address.ToLower());
                    dest.Ward = textInfo.ToTitleCase(src.Ward.ToLower());
                    dest.District = textInfo.ToTitleCase(src.District.ToLower());
                    dest.City = textInfo.ToTitleCase(src.City.ToLower());
                    dest.LicensePlate = textInfo.ToTitleCase(src.LicensePlate.ToLower());
                    dest.InsuranceNumber = textInfo.ToTitleCase(src.InsuranceNumber.ToLower());
                    dest.Status = textInfo.ToTitleCase(Constants.statusValues[src.Status].ToLower());

                    dest.IsAvaiable = src.IsAvailable;
                    dest.IsActive = src.IsActive;
                    dest.IsLocked = src.IsLocked;

                    dest.Specifications = specifications;
                    dest.Images = vehicleImages;
                });

            CreateMap<User, VehicleOwner>()
                .AfterMap((src, dest) =>
                {
                    var fullName = src.LastName + " " + src.FirstName;

                    dest.OwnerId = src.Id;
                    dest.Name = textInfo.ToTitleCase(fullName.ToLower());
                    dest.Username = textInfo.ToTitleCase(src.UserName.ToLower());
                    dest.Picture = src.Picture;
                    dest.Email = src.Email;
                    dest.PhoneNumber = src.PhoneNumber;
                    dest.Address = src.Address;
                });

            // Chats
            CreateMap<Chat, ChatResponseModel>()
                .AfterMap((src, dest) =>
                {
                    dest.Id = src.Id;

                    var members = new List<MemberOfChat>();
                    foreach (var member in src.ChatMembers)
                    {
                        members.Add(new MemberOfChat
                        {
                            Id = member.User.Id,
                            Username = member.User.UserName,
                            Avatar = member.User.Picture
                        });
                    };

                    dest.Members = members;

                    if (src.Messages.Count > 0)
                    {
                        var latestMessge = src.Messages.FirstOrDefault();

                        dest.LatestMessage = new MessageResponseModel
                        {
                            Id = latestMessge.Id,
                            ChatId = latestMessge.ChatId,
                            Message = latestMessge.Content,
                            Time = latestMessge.CreatedAt,
                            User = new MemberOfMessage
                            {
                                Id = latestMessge.Sender.Id,
                                Username = latestMessge.Sender.UserName,
                                Avatar = latestMessge.Sender.Picture
                            }
                        };
                    }
                });

            // Messages
            CreateMap<Message, MessageResponseModel>()
                .AfterMap((src, dest) =>
                {
                    dest.Id = src.Id;
                    dest.ChatId = src.ChatId;
                    dest.Message = src.Content;
                    dest.Time = src.CreatedAt;

                    dest.User = new MemberOfMessage
                    {
                        Id = src.Sender.Id,
                        Username = src.Sender.UserName,
                        Avatar = src.Sender.Picture
                    };
                });

            CreateMap<MessageRequestModel, Message>()
                .AfterMap((src, dest) =>
                {
                    dest.SenderId = src.SenderId;
                    dest.Content = src.Message;
                    dest.CreatedAt = DateTime.UtcNow;
                });

            CreateMap<Cart, CartResponseModel>()
                .AfterMap((src, dest) =>
                {
                    dest.UserId = src.User.Id;

                    dest.Shops = new List<ShopOfCart>();

                    var groupsOfShop = src.CartVehicles.GroupBy(x => x.Vehicle.Owner);

                    var shops = new List<ShopOfCart>();

                        foreach (var shop in groupsOfShop)
                        {
                            var shopToReturn = new ShopOfCart
                            {
                                LessorId = shop.Key.Id,
                                LessorName = shop.Key.UserName,
                                LessorImage = shop.Key.Picture,
                                Vehicles = new List<VehicleOfLessor>()
                            };

                            foreach (var item in shop)
                            {
                                shopToReturn.Vehicles.Add(new VehicleOfLessor
                                {
                                    VehicleId = item.Vehicle.Id,
                                    VehicleName = textInfo.ToTitleCase(item.Vehicle.Model.Name.ToLower()),
                                    Brand = textInfo.ToTitleCase(item.Vehicle.Model.Collection.Brand.Name.ToLower()),
                                    Color = textInfo.ToTitleCase(item.Vehicle.Color.Name.ToLower()),
                                    Price = item.Vehicle.Price,
                                    LicensePlate = item.Vehicle.LicensePlate,
                                    Image = item.Vehicle.VehicleImages.OrderBy(x => x.Image.CreatedAt).FirstOrDefault().Image.ImageUrl
								});
                            }

                           dest.Shops.Add(shopToReturn);
                        }
                });

            // BlogCategory
            CreateMap<BlogCategoryRequestModel, BlogCategory>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToUpper()));
		}
    }
}
