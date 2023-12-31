using System.Globalization;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.Models;
using Application.Web.Service.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static Application.Web.Database.DTOs.ResponseModels.CheckoutOrderResponseModel;

namespace Applicaton.Web.API.Extensions
{
	public class MapUserAction : IMappingAction<User, UserResponseModel>
	{
		private readonly UserManager<User> _userManager;

		public MapUserAction(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public void Process(User source, UserResponseModel desination, ResolutionContext context)
		{
			var roles = _userManager.GetRolesAsync(source).Result;
			desination.Roles = roles.ToList();

			var picture = new PictureOfUser();

			if (source.Picture.IsNullOrEmpty())
			{
				picture.ImageUrl = null;
				picture.PublicId = null;
			}
			else if (source.PublicId.IsNullOrEmpty())
			{
				picture.ImageUrl = source.Picture;
				picture.PublicId = null;
			}
			else
			{
				picture.ImageUrl = source.Picture;
				picture.PublicId = source.PublicId;
			}

			desination.Image = picture;
		}
	}

	public sealed class MappingProfile : Profile
	{
		public MappingProfile()
		{
			var textInfo = CultureInfo.CurrentCulture.TextInfo;

			// WeatherForecast
			CreateMap<WeatherForecast, WeatherForecastResponseModel>();
			CreateMap<WeatherForecastRequestModel, WeatherForecast>();

			// User
			CreateMap<UserRegistrationRequestModel, User>();
			CreateMap<User, UserResponseModel>()
				.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
				.ForMember(dest => dest.IsLocked, opt => opt.MapFrom(src => src.IsLocked))
				.AfterMap<MapUserAction>();

			CreateMap<UserRequestModel, User>()
				.AfterMap((src, dest) =>
				{
					dest.Picture = src.Image.ImageUrl;
					dest.PublicId = src.Image.PublicId;
				});

			// Role
			CreateMap<Role, RoleReponseModel>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

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

					var unavailableDates = new List<VehicleUnavailableDate>();

					if (src.TripRequests.Count > 0)
					{
						foreach (var tripRequest in src.TripRequests.Where(x => x.PickUpDateTime > DateTime.UtcNow))
						{
							unavailableDates.Add(new VehicleUnavailableDate
							{
								From = tripRequest.PickUpDateTime,
								To = tripRequest.DropOffDateTime
							});
						}
					}

					dest.TotalRating = src.VehicleReviews.Any() ? src.VehicleReviews.Count : 0;
					dest.Rating = src.VehicleReviews.Any() ? (decimal)src.VehicleReviews.Average(x => x.Rating) : 0;
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
					dest.UnavailableDates = unavailableDates;
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
					dest.CreatedDate = src.CreatedAt;
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


			// Cart
			CreateMap<Cart, CartResponseModel>()
				.AfterMap((src, dest) =>
				{
					dest.UserId = src.User.Id;

					dest.UserName = src.User.UserName;

					dest.Shops = new List<ShopOfCart>();

					var groupsOfShop = src.CartVehicles.GroupBy(x => x.Vehicle.Owner);

					foreach (var shop in groupsOfShop)
					{
						var shopToReturn = new ShopOfCart
						{
							LessorId = shop.Key.Id,
							LessorName = shop.Key.UserName,
							LessorImage = shop.Key.Picture,
							Address = shop.Key.Address ?? "",
							Vehicles = new List<VehicleOfLessor>()
						};

						foreach (var item in shop)
						{
							var vehilcleOfLessor = new VehicleOfLessor
							{
								VehicleId = item.Vehicle.Id,
								VehicleName = textInfo.ToTitleCase(item.Vehicle.Model.Name.ToLower()),
								Brand = textInfo.ToTitleCase(item.Vehicle.Model.Collection.Brand.Name.ToLower()),
								Address = textInfo.ToTitleCase(item.Vehicle.Address.ToLower()),
								Color = textInfo.ToTitleCase(item.Vehicle.Color.Name.ToLower()),
								Price = item.Vehicle.Price,
								LicensePlate = item.Vehicle.LicensePlate,
								Image = item.Vehicle.VehicleImages.OrderBy(x => x.Image.CreatedAt).FirstOrDefault().Image.ImageUrl,
								UnavailableDates = new List<VehicleUnavailableDateOfCart>()
							};

							if (item.PickUpDateTime.HasValue && item.DropOffDateTime.HasValue)
							{
								vehilcleOfLessor.PickUpDateTime = DateTime.SpecifyKind(item.PickUpDateTime.Value, DateTimeKind.Utc);
								vehilcleOfLessor.DropOffDateTime = DateTime.SpecifyKind(item.DropOffDateTime.Value, DateTimeKind.Utc);
							}
							else
							{
								vehilcleOfLessor.PickUpDateTime = null;
								vehilcleOfLessor.DropOffDateTime = null;
							}

							if (item.Vehicle.TripRequests.Count > 0)
							{
								foreach (var tripRequest in item.Vehicle.TripRequests)
								{
									vehilcleOfLessor.UnavailableDates.Add(new VehicleUnavailableDateOfCart
									{
										From = DateTime.SpecifyKind(tripRequest.PickUpDateTime, DateTimeKind.Utc),
										To = DateTime.SpecifyKind(tripRequest.DropOffDateTime, DateTimeKind.Utc)
									});
								}
							}

							shopToReturn.Vehicles.Add(vehilcleOfLessor);
						}

						dest.Shops.Add(shopToReturn);
					}
				});

			// Checkout
			CreateMap<CheckOutOrder, CheckoutOrderResponseModel>()
				.AfterMap((src, dest) =>
				{
					dest.UserId = src.User.Id;

					dest.UserName = src.User.UserName;

					dest.PaymentIntentId = src.PaymentIntentId ?? null;
					dest.ClientSecret = src.ClientSecret ?? null;

					dest.Shops = new List<ShopOfCheckout>();

					var groupsOfShop = src.CheckOutOrderVehicles.GroupBy(x => x.Vehicle.Owner);

					foreach (var shop in groupsOfShop)
					{
						var shopToReturn = new ShopOfCheckout
						{
							LessorId = shop.Key.Id,
							LessorName = shop.Key.UserName,
							LessorImage = shop.Key.Picture,
							Vehicles = new List<VehicleOfLessorOfCheckout>()
						};

						foreach (var item in shop)
						{
							var vehicleOfLessorCheckout = new VehicleOfLessorOfCheckout
							{
								VehicleId = item.Vehicle.Id,
								VehicleName = textInfo.ToTitleCase(item.Vehicle.Model.Name.ToLower()),
								Brand = textInfo.ToTitleCase(item.Vehicle.Model.Collection.Brand.Name.ToLower()),
								Color = textInfo.ToTitleCase(item.Vehicle.Color.Name.ToLower()),
								Price = item.Vehicle.Price,
								LicensePlate = item.Vehicle.LicensePlate,
								Image = item.Vehicle.VehicleImages.OrderBy(x => x.Image.CreatedAt).FirstOrDefault().Image.ImageUrl,
								PickUpLocation = item.PickUpLocation,
								DropOffLocation = item.DropOffLocation,
								PickUpDateTime = DateTime.SpecifyKind(item.PickUpDateTime, DateTimeKind.Utc),
								DropOffDateTime = DateTime.SpecifyKind(item.PickUpDateTime, DateTimeKind.Utc),
								RentDates = new List<VehicleUnavailableDateOfCheckout>()
							};

							if (item.Vehicle.TripRequests.Count > 0)
							{
								foreach (var tripRequest in item.Vehicle.TripRequests)
								{
									vehicleOfLessorCheckout.RentDates.Add(new VehicleUnavailableDateOfCheckout
									{
										From = tripRequest.PickUpDateTime,
										To = tripRequest.DropOffDateTime,
									});
								}
							}

							shopToReturn.Vehicles.Add(vehicleOfLessorCheckout);
						}

						dest.Shops.Add(shopToReturn);
					}
				});

			// Trip request
			CreateMap<List<TripRequest>, TripRequestReponseModel>()
				.AfterMap((src, dest) =>
				{
					dest.ParentOrderId = src.FirstOrDefault().ParentOrderId;

					dest.UserId = src.FirstOrDefault().Lessee.Id;

					dest.UserName = src.FirstOrDefault().Lessee.UserName;

					dest.FullName = src.FirstOrDefault().Lessee.FullName;

					dest.Email = src.FirstOrDefault().Lessee.Email;

					dest.Phone = src.FirstOrDefault().Lessee.PhoneNumber;

					dest.Address = src.FirstOrDefault().Lessee.Address;

					dest.Avatar = src.FirstOrDefault().Lessee.Picture;

					dest.TotalAmmount = 0;

					dest.CreatedAt = DateTime.SpecifyKind(src.FirstOrDefault().Created_At, DateTimeKind.Utc);

					dest.Status = Helpers.GetParentOrderStatus(src);

					dest.DateRent = new DateRentOfTripRequest
					{
						From = src.Min(x => x.PickUpDateTime),
						To = src.Max(x => x.DropOffDateTime)
					};

					dest.Shops = new List<ShopOfTripRequest>();

					var groupsOfShop = src.GroupBy(x => x.Lessor);

					var shops = new List<ShopOfTripRequest>();

					foreach (var shop in groupsOfShop)
					{
						var shopToReturn = new ShopOfTripRequest
						{
							LessorId = shop.Key.Id,
							LessorName = shop.Key.UserName,
							LessorImage = shop.Key.Picture,
							Vehicles = new List<VehicleOfLessorOfTripRequest>()
						};

						foreach (var item in shop)
						{
							var tripRequestVehicleOfLessor = new VehicleOfLessorOfTripRequest
							{
								RequestId = item.Id,
								VehicleId = item.Vehicle.Id,
								IsReviewed = item.VehicleReview != null ? true : false,
								VehicleName = textInfo.ToTitleCase(item.Vehicle.Model.Name.ToLower()),
								Address = item.Vehicle.Address,
								Brand = textInfo.ToTitleCase(item.Vehicle.Model.Collection.Brand.Name.ToLower()),
								Color = textInfo.ToTitleCase(item.Vehicle.Color.Name.ToLower()),
								Price = item.Ammount * (int)Math.Round(((decimal)item.DropOffDateTime.Subtract(item.PickUpDateTime).TotalHours / 24), 2),
								LicensePlate = item.Vehicle.LicensePlate,
								Image = item.Vehicle.VehicleImages.OrderBy(x => x.Image.CreatedAt).FirstOrDefault().Image.ImageUrl,
								PickUpLocation = item.PickUpLocation,
								DropOffLocation = item.DropOffLocation,
								PickUpDateTime = DateTime.SpecifyKind(item.PickUpDateTime, DateTimeKind.Utc),
								DropOffDateTime = DateTime.SpecifyKind(item.DropOffDateTime, DateTimeKind.Utc),
							};

							dest.TotalAmmount += tripRequestVehicleOfLessor.Price;

							if (!item.Status && item.InCompleteTrip == null && item.CompletedTrip == null)
							{
								tripRequestVehicleOfLessor.Status = Constants.PENDING;
							}
							else if (item.Status && item.InCompleteTrip == null && item.CompletedTrip == null)
							{
								tripRequestVehicleOfLessor.Status = Constants.ONGOING;
							}
							else if (!item.Status && item.InCompleteTrip != null && item.CompletedTrip == null)
							{
								tripRequestVehicleOfLessor.Status = Constants.CANCELED;
							}
							else if (item.Status && item.InCompleteTrip == null && item.CompletedTrip != null)
							{
								tripRequestVehicleOfLessor.Status = Constants.COMPLETED;
							}

							shopToReturn.Vehicles.Add(tripRequestVehicleOfLessor);
						}

						dest.Shops.Add(shopToReturn);
					}
				});

			// BlogCategory
			CreateMap<BlogCategoryRequestModel, BlogCategory>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToUpper().Trim()));

			CreateMap<BlogCategory, BlogCategoryResponseModel>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => textInfo.ToTitleCase(src.Name.ToLower())));

			// Blog
			CreateMap<BlogRequestModel, Blog>()
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.ToUpper().Trim()))
				.ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
				.ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
				.ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

			CreateMap<Blog, BlogResponseModel>()
				.AfterMap((src, dest) =>
				{
					dest.Id = src.Id;
					dest.Title = textInfo.ToTitleCase(src.Title.ToLower());
					dest.Content = src.Content;
					dest.ShortDescription = src.ShortDescription;
					dest.CreatedAt = src.Created_At;

					dest.Author = new AuthorOfBlog
					{
						AuthorId = src.Author.Id,
						Username = src.Author.UserName,
						Picture = src.Author.Picture,
					};

					dest.Image = new ImageOfBlog
					{
						ImageUrl = src.Image.ImageUrl,
						PublicId = src.Image.PublicId
					};

					dest.Category = new CategoryOfBlog
					{
						CategoryId = src.Category.Id,
						Name = textInfo.ToTitleCase(src.Category.Name.ToLower())
					};
				});
			CreateMap<BlogCategory, CategoryOfBlog>();
			CreateMap<User, AuthorOfBlog>();
			CreateMap<Image, ImageOfBlog>();

			CreateMap<VehicleReview, VehicleReviewResponseModel>()
				.AfterMap((src, dest) =>
				{
					dest.VehicleId = src.VehicleId;
					dest.UserId = src.User.Id;
					dest.Username = src.User.UserName;
					dest.Email = src.User.Email;
					dest.Avatar = src.User.Picture;
					dest.CreatedAt = DateTime.SpecifyKind(src.CreatedAt, DateTimeKind.Utc);

					dest.Rating = src.Rating;
					dest.Title = src.Title;
					dest.Content = src.Content;

					dest.Images = src.VehicleReviewImages.Select(x => x.Image.ImageUrl).ToList();
				});

			CreateMap<BlogComment, BlogCommentResponseModel>()
				.AfterMap((src, dest) =>
				{
					dest.BlogId = src.BlogId;
					dest.CommentId = src.Id;
					dest.UserId = src.User.Id;

					dest.Username = src.User.UserName;
					dest.FullName = src.User.FullName;
					dest.Email = src.User.Email;
					dest.Avatar = src.User.Picture;

					dest.Comment = src.Comment;
					dest.CreatedAt = DateTime.SpecifyKind(src.CreatedAt, DateTimeKind.Utc);
				});
		}
	}
}
