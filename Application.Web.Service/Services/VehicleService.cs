using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;

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
		private readonly IUserService _userService;
		private readonly IModelService _modelService;

		public VehicleService(IUnitOfWork unitOfWork, IMapper mapper, IVehicleQueries vehicleQueries, IUserService userService, IModelService modelService)
        {
			_mapper = mapper;
            _unitOfWork = unitOfWork;
            _vehicleRepo = unitOfWork.GetBaseRepo<Vehicle>();
			_imageRepo = unitOfWork.GetBaseRepo<Image>();
			_vehicleImageRepo = unitOfWork.GetBaseRepo<VehicleImage>();
            _vehicleQueries = vehicleQueries;
			_userService = userService;
			_modelService = modelService;
        }

        public async Task<(IEnumerable<Vehicle>, PaginationMetadata)> GetVehiclesAsync(PaginationRequestModel pagination, VehicleQuery vehicleQuery)
		{
			var totalItemCount = await _vehicleQueries.CountVehiclesAsync();

			var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

			var vehicles = await _vehicleQueries.GetAllVehiclesAsync();
			
			vehicles = HandleVehicleQuery(vehicleQuery, vehicles);

			var vehiclesToReturn = vehicles
				.Skip(pagination.pageSize * (pagination.pageNumber - 1))
				.Take(pagination.pageSize)
				.ToList();

			return (vehiclesToReturn, paginationMetadata);
		}

		public async Task<List<Vehicle>> GetAllVehicleAsync(VehicleQuery vehicleQuery)
        {
            var vehicles = await _vehicleQueries.GetAllVehiclesAsync();

			var vehiclesToReturn = HandleVehicleQuery(vehicleQuery, vehicles);

            return vehiclesToReturn;
        }

		public async Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId)
		{
			var vehicle = await _vehicleQueries.GetByIdAsync(vehicleId);

			if (vehicle == null)
				throw new StatusCodeException(message: "Vehicle not found.", statusCode: StatusCodes.Status404NotFound);
			
			return vehicle;
		}

		public async Task<Vehicle> CreateVehicleAsync(VehicleRequestModel requestModel)
		{
			var newVehicle = _mapper.Map<Vehicle>(requestModel);

			_ = await _userService.GetUserInformationByIdAsync(requestModel.OwnerId);
			_ = await _modelService.GetModelByIdAsync(requestModel.ModelId);

			await ValidateVehicleAsync(newVehicle);

			var (newImages, vehicleImages) = HandleNewVehicleImages(requestModel, newVehicle.Id);

			_vehicleRepo.Add(newVehicle);

			_imageRepo.AddRange(newImages);

			_vehicleImageRepo.AddRange(vehicleImages);

			await _unitOfWork.CompleteAsync();

			return await GetVehicleByIdAsync(newVehicle.Id);
		}

		public async Task<Vehicle> UpdateVehicleAsync(VehicleRequestModel requestModel, Guid vehicleId)
		{
			var vehicle = await GetVehicleByIdAsync(vehicleId);

			var originalLicensePlate = vehicle.LicensePlate;
			var originalInsuranceNumber = vehicle.InsuranceNumber;
			var originalVehicleImages = vehicle.VehicleImages;
			var originalImages = originalVehicleImages.Select(x => x.Image);

			var vehicleToUpdate = _mapper.Map<VehicleRequestModel, Vehicle>(requestModel, vehicle);

			_ = await _userService.GetUserInformationByIdAsync(requestModel.OwnerId);
			_ = await _modelService.GetModelByIdAsync(requestModel.ModelId);

			await ValidateVehicleAsync(vehicleToUpdate, originalLicensePlate, originalInsuranceNumber);

			var (newImages, vehicleImages) = HandleNewVehicleImages(requestModel, vehicleId);

			_imageRepo.DeleteRange(originalImages);

			_vehicleRepo.Update(vehicleToUpdate);

			_imageRepo.AddRange(newImages);

			_vehicleImageRepo.AddRange(vehicleImages);

			await _unitOfWork.CompleteAsync();

			return await GetVehicleByIdAsync(vehicleToUpdate.Id);
		}

		public async Task<bool> DeleteVehicleAsync(Guid vehicleId)
		{
			var vehicle = await GetVehicleByIdAsync(vehicleId);

			_imageRepo.DeleteRange(vehicle.VehicleImages.Select(x => x.Image));

			_vehicleRepo.Delete(vehicleId);

			await _unitOfWork.CompleteAsync();

			return true;
		}

		private (List<Image>, List<Database.Models.VehicleImage>) HandleNewVehicleImages(VehicleRequestModel requestModel, Guid vehicleId)
		{
			var imageList = new List<Image>();
			var vehicleImageList = new List<Database.Models.VehicleImage>();

			foreach(var image in requestModel.Images)
			{
				var newImage = new Image
				{
					ImageUrl = image.ImageUrl,
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

		private async Task ValidateVehicleAsync(Vehicle newModel)
		{
			var isLicensePlateExisted = await _vehicleQueries.CheckIfLicensePlateExisted(newModel.LicensePlate);
			var isInsuranceNumberExisted = await _vehicleQueries.CheckIfInsuranceNumberExisted(newModel.InsuranceNumber);

			int[] validStatusState = [0, 1, 2];

			if(validStatusState.Contains(newModel.Status))
				throw new StatusCodeException(message: "Invalid status number.", statusCode: StatusCodes.Status409Conflict);

			if (newModel.ConditionPercentage > 100 || newModel.ConditionPercentage < 0)
				throw new StatusCodeException(message: "Invalid percetage number.", statusCode: StatusCodes.Status409Conflict);

			if (isLicensePlateExisted)
				throw new StatusCodeException(message: "License plate already existed.", statusCode: StatusCodes.Status409Conflict);

			if (isInsuranceNumberExisted)
				throw new StatusCodeException(message: "Insurance number already existed.", statusCode: StatusCodes.Status409Conflict);

		}

		private async Task ValidateVehicleAsync(Vehicle newModel, string originalLicensePlate, string originalInsuranceNumber)
		{
			var isLicensePateMatchOriginal = newModel.LicensePlate.ToUpper().Equals(originalLicensePlate.ToUpper());

			var isInsuranceNumberMatchOriginal = newModel.InsuranceNumber.ToUpper().Equals(originalInsuranceNumber.ToUpper());

			int[] validStatusState = [0, 1, 2];

			if (validStatusState.Contains(newModel.Status))
				throw new StatusCodeException(message: "Invalid status number.", statusCode: StatusCodes.Status409Conflict);

			if (newModel.ConditionPercentage > 100 || newModel.ConditionPercentage < 0)
				throw new StatusCodeException(message: "Invalid percetage number.", statusCode: StatusCodes.Status409Conflict);

			if (!isLicensePateMatchOriginal)
			{
				var isLicensePlateExisted = await _vehicleQueries.CheckIfLicensePlateExisted(newModel.LicensePlate);

				if(isLicensePlateExisted)
					throw new StatusCodeException(message: "License plate already existed.", statusCode: StatusCodes.Status409Conflict);
			}

			if(!isInsuranceNumberMatchOriginal)
			{
				var isInsuranceNumberExisted = await _vehicleQueries.CheckIfInsuranceNumberExisted(newModel.InsuranceNumber);

				if (isInsuranceNumberExisted)
					throw new StatusCodeException(message: "Insurance number already existed.", statusCode: StatusCodes.Status409Conflict);
			}

		}

		private static List<Vehicle> HandleVehicleQuery(VehicleQuery vehicleQuery, List<Vehicle> vehicles)
		{
			if (!string.IsNullOrEmpty(vehicleQuery.ModelName))
			{
				var modelName = vehicleQuery.ModelName.Trim().ToUpper();

				vehicles = vehicles
					.Where(v => v.Model.Name.ToUpper().Equals(modelName))
					.ToList();
			}

			if (!string.IsNullOrEmpty(vehicleQuery.CollectionName))
			{
				var collectionName = vehicleQuery.CollectionName.Trim().ToUpper();

				vehicles = vehicles
					.Where(v => v.Model.Collection.Name.ToUpper().Equals(collectionName))
					.ToList();
			}

			if (!string.IsNullOrEmpty(vehicleQuery.BrandName))
			{
				var brandName = vehicleQuery.BrandName.Trim().ToUpper();

				vehicles = vehicles
					.Where(v => v.Model.Collection.Brand.Name.ToUpper().Equals(brandName))
					.ToList();
			}

			if(vehicleQuery.IsSortPriceDesc.HasValue)
			{
				if(vehicleQuery.IsSortPriceDesc.Value == true)
				{
					vehicles = vehicles
						.OrderByDescending(v => v.Price)
						.ToList();
				} else
				{
					 vehicles = vehicles
						.OrderBy(v => v.Price)
						.ToList();
				}
			}

			return vehicles;
		}
	}
}
