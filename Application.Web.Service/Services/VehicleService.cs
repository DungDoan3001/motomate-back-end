using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Diacritics.Extensions;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Web.Service.Services
{
	public class VehicleService : IVehicleService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<Vehicle> _vehicleRepo;
		private readonly IGenericRepository<Image> _imageRepo;
		private readonly IGenericRepository<VehicleImage> _vehicleImageRepo;
		private readonly IVehicleQueries _vehicleQueries;
		private readonly IVehicleReviewQueries _vehicleReviewQueries;
		private readonly IModelQueries _modelQueries;
		private readonly IColorQueries _colorQueries;
		private readonly IUserService _userService;
		private readonly IModelService _modelService;
		private readonly IAppCache _cache;
		private CacheKeyConstants _cacheKeyConstants;

		public VehicleService(IUnitOfWork unitOfWork, IMapper mapper,
								IVehicleQueries vehicleQueries, IColorQueries colorQueries, IModelQueries modelQueries, IVehicleReviewQueries vehicleReviewQueries,
								IUserService userService, IModelService modelService,
								IAppCache cache, CacheKeyConstants cacheKeyConstants)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_vehicleRepo = unitOfWork.GetBaseRepo<Vehicle>();
			_imageRepo = unitOfWork.GetBaseRepo<Image>();
			_vehicleImageRepo = unitOfWork.GetBaseRepo<VehicleImage>();
			_vehicleQueries = vehicleQueries;
			_vehicleReviewQueries = vehicleReviewQueries;
			_modelQueries = modelQueries;
			_colorQueries = colorQueries;
			_userService = userService;
			_modelService = modelService;
			_cache = cache;
			_cacheKeyConstants = cacheKeyConstants;
		}

		public async Task<(IEnumerable<VehicleReview>, PaginationMetadata)> GetVehicleReviewByVehicleIdAsync(PaginationRequestModel pagination, Guid vehicleId)
		{
			var vehicleReviews = await _vehicleReviewQueries.GetAllVehicleReviewByVehicleIdAsync(vehicleId) ?? throw new StatusCodeException(message: "Vehicle not found.", statusCode: StatusCodes.Status404NotFound);

			return Helpers.Helpers.GetPaginationModel<VehicleReview>(vehicleReviews, pagination);
		}

		public async Task<(IEnumerable<Vehicle>, PaginationMetadata)> GetVehiclesAsync(PaginationRequestModel pagination, VehicleQuery vehicleQuery)
		{
			var key = $"{_cacheKeyConstants.VehicleCacheKey}-All";

			var vehicles = await _cache.GetOrAddAsync(
				key,
				async () => await _vehicleQueries.GetAllVehiclesAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			vehicles = HandleVehicleQuery(vehicleQuery, vehicles);

			return Helpers.Helpers.GetPaginationModel<Vehicle>(vehicles, pagination);
		}

		public async Task<(IEnumerable<Vehicle>, PaginationMetadata)> GetAllVehiclesByOwnerIdAsync(PaginationRequestModel pagination, VehicleQuery vehicleQuery, Guid ownerId)
		{
			var key = $"{_cacheKeyConstants.VehicleCacheKey}-Owner-{ownerId}";

			var vehicles = await _cache.GetOrAddAsync(
				key,
				async () => await _vehicleQueries.GetAllVehiclesByOwnerIdAsync(ownerId),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			vehicles = HandleVehicleQuery(vehicleQuery, vehicles);

			return Helpers.Helpers.GetPaginationModel<Vehicle>(vehicles, pagination);
		}

		public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync(VehicleQuery vehicleQuery)
		{
			var key = $"{_cacheKeyConstants.VehicleCacheKey}-All";

			var vehicles = await _cache.GetOrAddAsync(
				key,
				async () => await _vehicleQueries.GetAllVehiclesAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			var vehiclesToReturn = HandleVehicleQuery(vehicleQuery, vehicles);

			return vehiclesToReturn;
		}

		public async Task<IEnumerable<Vehicle>> GetRelatedVehicleAsync(Guid vehicleId)
		{
			var key = $"{_cacheKeyConstants.VehicleCacheKey}-All";

			var vehicles = await _cache.GetOrAddAsync(
				key,
				async () => await _vehicleQueries.GetAllVehiclesAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			var vehicleToCheck = vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId)) ?? throw new StatusCodeException(message: "Vehicle not found.", statusCode: StatusCodes.Status404NotFound);

			var vehicleQuery = new VehicleQuery
			{
				Models = new List<string> { vehicleToCheck.Model.Name },
				Cities = new List<string> { vehicleToCheck.City }
			};

			var vehiclesToReturn = HandleVehicleQuery(vehicleQuery, vehicles)
									.Take(20)
									.AsQueryable()
									.AsNoTracking()
									.ToList();

			vehiclesToReturn.Remove(vehicleToCheck);

			return vehiclesToReturn;
		}

		public async Task<(IEnumerable<Vehicle>, PaginationMetadata)> GetVehiclesByStatusAsync(PaginationRequestModel pagination, VehicleQuery vehicleQuery, string statusRoute)
		{
			if (!Constants.statusValues.Any(x => x.Value.ToUpper().Trim() == statusRoute.ToUpper().Trim()))
			{
				throw new StatusCodeException(message: "Invalid status.", statusCode: StatusCodes.Status400BadRequest);
			}

			var key = $"{_cacheKeyConstants.VehicleCacheKey}-All";

			var vehicles = await _cache.GetOrAddAsync(
				key,
				async () => await _vehicleQueries.GetAllVehiclesAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			vehicles = HandleVehicleQuery(vehicleQuery, vehicles);

			var statusNumber = Constants.statusValues
								.Where(x => x.Value
												.ToUpper()
												.Trim()
												.Equals(statusRoute
														.ToUpper()
														.Trim()))
												.Select(x => x.Key)
												.FirstOrDefault();

			vehicles = vehicles.Where(x => x.Status.Equals(statusNumber)).ToList();

			return Helpers.Helpers.GetPaginationModel<Vehicle>(vehicles, pagination);
		}

		public async Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId)
		{
			var key = $"{_cacheKeyConstants.VehicleCacheKey}-ID-{vehicleId}";

			var vehicle = await _cache.GetOrAddAsync(
				key,
				async () => await _vehicleQueries.GetByIdAsync(vehicleId),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			if (vehicle == null)
				throw new StatusCodeException(message: "Vehicle not found.", statusCode: StatusCodes.Status404NotFound);

			return vehicle;
		}

		public async Task<(bool, bool)> HandleLockVehicleAsync(Guid vehicleId)
		{
			var vehicle = await _vehicleRepo.GetById(vehicleId);

			vehicle.IsLocked = !vehicle.IsLocked;

			_vehicleRepo.Update(vehicle);

			await _unitOfWork.CompleteAsync();

			await Task.Run(() =>
			{
				foreach (var key in _cacheKeyConstants.CacheKeyList)
				{
					_cache.Remove(key);
				}

				_cacheKeyConstants.CacheKeyList = new List<string>();
			});

			return (true, vehicle.IsLocked);
		}

		public async Task<Vehicle> UpdateVehicleStatusAsync(Guid vehicleId, int statusNumber)
		{
			var vehicle = await _vehicleRepo.GetById(vehicleId);

			vehicle.Status = statusNumber;

			_vehicleRepo.Update(vehicle);

			await _unitOfWork.CompleteAsync();

			await Task.Run(() =>
			{
				foreach (var key in _cacheKeyConstants.CacheKeyList)
				{
					_cache.Remove(key);
				}

				_cacheKeyConstants.CacheKeyList = new List<string>();
			});

			return await GetVehicleByIdAsync(vehicleId);
		}

		public async Task<Vehicle> CreateVehicleAsync(VehicleRequestModel requestModel)
		{
			_ = await _userService.GetUserInformationByIdAsync(requestModel.OwnerId);
			_ = await _modelService.GetModelByIdAsync(requestModel.ModelId);

			await ValidateVehicleAsync(requestModel);

			var newVehicle = _mapper.Map<Vehicle>(requestModel);

			newVehicle.ColorId = await _colorQueries.GetColorIdByColorNameAsync(requestModel.ColorName);

			HandleVehicleStaticValue(newVehicle);

			var (newImages, vehicleImages) = HandleNewVehicleImages(requestModel, newVehicle.Id);

			_vehicleRepo.Add(newVehicle);

			_imageRepo.AddRange(newImages);

			_vehicleImageRepo.AddRange(vehicleImages);

			await _unitOfWork.CompleteAsync();

			await Task.Run(() =>
			{
				foreach (var key in _cacheKeyConstants.CacheKeyList)
				{
					_cache.Remove(key);
				}

				_cacheKeyConstants.CacheKeyList = new List<string>();
			});

			return await GetVehicleByIdAsync(newVehicle.Id);
		}


		public async Task<Vehicle> UpdateVehicleAsync(VehicleRequestModel requestModel, Guid vehicleId)
		{
			var vehicle = await _vehicleQueries.GetByIdAsync(vehicleId) ?? throw new StatusCodeException(message: "Vehicle not found.", statusCode: StatusCodes.Status404NotFound);

			var originalLicensePlate = vehicle.LicensePlate;

			var originalInsuranceNumber = vehicle.InsuranceNumber;

			var originalVehicleImages = vehicle.VehicleImages;

			var originalImages = originalVehicleImages.Select(x => x.Image);

			_ = await _userService.GetUserInformationByIdAsync(requestModel.OwnerId);
			_ = await _modelService.GetModelByIdAsync(requestModel.ModelId);

			await ValidateVehicleAsync(requestModel, originalLicensePlate, originalInsuranceNumber);

			var (newImages, vehicleImages) = HandleNewVehicleImages(requestModel, vehicleId);

			_imageRepo.DeleteRange(originalImages);

			var vehicleToUpdate = _mapper.Map<VehicleRequestModel, Vehicle>(requestModel, vehicle);

			var colorId = await _colorQueries.GetColorIdByColorNameAsync(requestModel.ColorName);

			vehicleToUpdate.ColorId = colorId;
			vehicleToUpdate.Status = Constants.statusValues.FirstOrDefault(x => x.Value.Equals(Constants.PENDING)).Key;

			_vehicleRepo.Update(vehicleToUpdate);

			_imageRepo.AddRange(newImages);

			_vehicleImageRepo.AddRange(vehicleImages);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(vehicleToUpdate);
			_unitOfWork.DetachRange(newImages);
			_unitOfWork.DetachRange(vehicleImages);

			await Task.Run(() =>
			{
				foreach (var key in _cacheKeyConstants.CacheKeyList)
				{
					_cache.Remove(key);
				}

				_cacheKeyConstants.CacheKeyList = new List<string>();
			});

			return await _vehicleQueries.GetByIdAsync(vehicleId);
		}

		public async Task<bool> DeleteVehicleAsync(Guid vehicleId)
		{
			var vehicle = await GetVehicleByIdAsync(vehicleId);

			_imageRepo.DeleteRange(vehicle.VehicleImages.Select(x => x.Image));

			_vehicleRepo.Delete(vehicleId);

			await _unitOfWork.CompleteAsync();

			await Task.Run(() =>
			{
				foreach (var key in _cacheKeyConstants.CacheKeyList)
				{
					_cache.Remove(key);
				}

				_cacheKeyConstants.CacheKeyList = new List<string>();
			});

			return true;
		}

		private static (List<Image>, List<Database.Models.VehicleImage>) HandleNewVehicleImages(VehicleRequestModel requestModel, Guid vehicleId)
		{
			var imageList = new List<Image>();
			var vehicleImageList = new List<Database.Models.VehicleImage>();

			foreach (var image in requestModel.Images)
			{
				var newImage = new Image
				{
					ImageUrl = image.Image,
					PublicId = image.PublicId
				};

				var vehicleImage = new VehicleImage
				{
					VehicleId = vehicleId,
					ImageId = newImage.Id
				};

				imageList.Add(newImage);
				vehicleImageList.Add(vehicleImage);
			}

			return (imageList, vehicleImageList);
		}

		private static void HandleVehicleStaticValue(Vehicle newVehicle)
		{
			newVehicle.Status = Constants.statusValues.FirstOrDefault(x => x.Value.Equals(Constants.PENDING)).Key;
			newVehicle.IsActive = true;
			newVehicle.IsAvailable = true;
			newVehicle.IsLocked = false;
		}

		private async Task ValidateVehicleAsync(VehicleRequestModel newModel)
		{
			var isLicensePlateExisted = await _vehicleQueries.CheckIfLicensePlateExisted(newModel.LicensePlate);

			var isInsuranceNumberExisted = await _vehicleQueries.CheckIfInsuranceNumberExisted(newModel.InsuranceNumber);

			var idColorExisted = await _modelQueries.CheckIfColorExistInModel(newModel.ColorName, newModel.ModelId);

			if (newModel.ConditionPercentage > 100 || newModel.ConditionPercentage < 0)
				throw new StatusCodeException(message: "Invalid percetage number.", statusCode: StatusCodes.Status409Conflict);

			if (isLicensePlateExisted)
				throw new StatusCodeException(message: "License plate already existed.", statusCode: StatusCodes.Status409Conflict);

			if (!idColorExisted)
				throw new StatusCodeException(message: "Color not found in model.", statusCode: StatusCodes.Status409Conflict);

			if (isInsuranceNumberExisted)
				throw new StatusCodeException(message: "Insurance number already existed.", statusCode: StatusCodes.Status409Conflict);

		}

		private async Task ValidateVehicleAsync(VehicleRequestModel newModel, string originalLicensePlate, string originalInsuranceNumber)
		{
			var isLicensePlateMatchOriginal = newModel.LicensePlate.ToUpper().Equals(originalLicensePlate.ToUpper());

			var isInsuranceNumberMatchOriginal = newModel.InsuranceNumber.ToUpper().Equals(originalInsuranceNumber.ToUpper());

			var idColorExisted = await _modelQueries.CheckIfColorExistInModel(newModel.ColorName, newModel.ModelId);

			if (newModel.ConditionPercentage > 100 || newModel.ConditionPercentage < 0)
				throw new StatusCodeException(message: "Invalid percetage number.", statusCode: StatusCodes.Status409Conflict);

			if (!idColorExisted)
				throw new StatusCodeException(message: "Color not found in model.", statusCode: StatusCodes.Status409Conflict);

			if (!isLicensePlateMatchOriginal)
			{
				var isLicensePlateExisted = await _vehicleQueries.CheckIfLicensePlateExisted(newModel.LicensePlate);

				if (isLicensePlateExisted)
					throw new StatusCodeException(message: "License plate already existed.", statusCode: StatusCodes.Status409Conflict);
			}

			if (!isInsuranceNumberMatchOriginal)
			{
				var isInsuranceNumberExisted = await _vehicleQueries.CheckIfInsuranceNumberExisted(newModel.InsuranceNumber);

				if (isInsuranceNumberExisted)
					throw new StatusCodeException(message: "Insurance number already existed.", statusCode: StatusCodes.Status409Conflict);
			}

		}

		private static List<Vehicle> HandleVehicleQuery(VehicleQuery vehicleQuery, List<Vehicle> vehicles)
		{
			if (vehicleQuery.Brands != null && vehicleQuery.Brands.Count > 0)
			{
				var vehicleQueryHolder = new List<Vehicle>();
				foreach (var brand in vehicleQuery.Brands)
				{
					var result = vehicles
						.Where(x => x.Model.Collection.Brand.Name
										.ToUpper()
										.Trim()
										.RemoveDiacritics()
										.Contains(brand
													.ToUpper()
													.Trim()
													.RemoveDiacritics()))
						.ToList();

					vehicleQueryHolder.AddRange(result);
				}
				vehicles = vehicleQueryHolder;
			}

			if (vehicleQuery.Collections != null && vehicleQuery.Collections.Count > 0)
			{
				var vehicleQueryHolder = new List<Vehicle>();
				foreach (var collection in vehicleQuery.Collections)
				{
					var result = vehicles
						.Where(x => x.Model.Collection.Name
										.ToUpper()
										.Trim()
										.RemoveDiacritics()
										.Contains(collection
													.ToUpper()
													.Trim()
													.RemoveDiacritics()))
						.ToList();

					vehicleQueryHolder.AddRange(result);
				}
				vehicles = vehicleQueryHolder;
			}

			if (vehicleQuery.Models != null && vehicleQuery.Models.Count > 0)
			{
				var vehicleQueryHolder = new List<Vehicle>();
				foreach (var model in vehicleQuery.Models)
				{
					var result = vehicles
						.Where(x => x.Model.Name
										.ToUpper()
										.Trim()
										.RemoveDiacritics()
										.Contains(model
													.ToUpper()
													.Trim()
													.RemoveDiacritics()))
						.ToList();

					vehicleQueryHolder.AddRange(result);
				}
				vehicles = vehicleQueryHolder;
			}

			if (vehicleQuery.Cities != null && vehicleQuery.Cities.Count > 0)
			{
				var vehicleQueryHolder = new List<Vehicle>();
				foreach (var city in vehicleQuery.Cities)
				{
					var result = vehicles
						.Where(x => x.City
										.ToUpper()
										.Trim()
										.RemoveDiacritics()
										.Contains(city
													.ToUpper()
													.Trim()
													.RemoveDiacritics()))
						.ToList();

					vehicleQueryHolder.AddRange(result);
				}
				vehicles = vehicleQueryHolder;
			}

			if (vehicleQuery.IsSortPriceDesc.HasValue)
			{
				if (vehicleQuery.IsSortPriceDesc.Value == true)
				{
					vehicles = vehicles
						.OrderByDescending(v => v.Price)
						.ToList();
				}
				else
				{
					vehicles = vehicles
					   .OrderBy(v => v.Price)
					   .ToList();
				}
			}

			if (!vehicleQuery.Search.IsNullOrEmpty())
			{
				string trimmedSearch = vehicleQuery.Search.Trim().ToUpper().RemoveDiacritics();

				vehicles = vehicles.Where(x =>
											x.Model.Collection.Brand.Name.ToUpper().Trim().RemoveDiacritics().Contains(trimmedSearch) ||
											x.Model.Collection.Name.ToUpper().Trim().RemoveDiacritics().Contains(trimmedSearch) ||
											x.Model.Name.ToUpper().Trim().RemoveDiacritics().Contains(trimmedSearch) ||
											x.City.ToUpper().Trim().RemoveDiacritics().Contains(trimmedSearch) ||
											x.Owner.UserName.ToUpper().Trim().RemoveDiacritics().Contains(trimmedSearch))
								   .ToList();
			}

			if (vehicleQuery.DateRent != null)
			{
				var from = vehicleQuery.DateRent.From;
				var to = vehicleQuery.DateRent.To;

				if (to <= from)
					throw new StatusCodeException(message: "To can not before From.", statusCode: StatusCodes.Status400BadRequest);

				var checkIfVehicleIsBooked = ((DateTime From, DateTime To, DateTime PickUpDate, DateTime DropOffTime) =>
				{
					if (PickUpDate < From && DropOffTime >= From && DropOffTime <= To)
					{
						return true;
					}
					else if (PickUpDate >= From && PickUpDate <= To && DropOffTime >= From && DropOffTime <= To)
					{
						return true;
					}
					else if (PickUpDate >= From && PickUpDate <= To && DropOffTime > To)
					{
						return true;
					}
					else if (PickUpDate < From && DropOffTime > To)
					{
						return true;
					}
					else
					{
						return false;
					}

				});

				vehicles = vehicles
					.Where(x => x.TripRequests.Any(x => checkIfVehicleIsBooked(from, to, x.PickUpDateTime, x.DropOffDateTime)).Equals(false))
					.ToList();
			}

			return vehicles;
		}
	}
}
