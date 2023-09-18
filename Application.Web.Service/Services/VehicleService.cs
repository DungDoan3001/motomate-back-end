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
		private readonly IGenericRepository<Database.Models.VehicleImage> _vehicleImageRepo;
		private readonly IVehicleQueries _vehicleQueries;

        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper, IVehicleQueries vehicleQueries)
        {
			_mapper = mapper;
            _unitOfWork = unitOfWork;
            _vehicleRepo = unitOfWork.GetBaseRepo<Vehicle>();
			_imageRepo = unitOfWork.GetBaseRepo<Image>();
			_vehicleImageRepo = unitOfWork.GetBaseRepo<Database.Models.VehicleImage>();
            _vehicleQueries = vehicleQueries;
        }

        public async Task<(IEnumerable<Vehicle>, PaginationMetadata)> GetVehiclesAsync(PaginationRequestModel pagination)
        {
            var totalItemCount = await _vehicleQueries.CountVehiclesAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

            var brandToReturn = await _vehicleQueries.GetVehiclesWithPaginationAync(pagination);

            return (brandToReturn, paginationMetadata);
        }

        public async Task<List<Vehicle>> GetAllVehicleAsync()
        {
            var brandToReturn = await _vehicleQueries.GetAllVehiclesAsync();

            return brandToReturn;
        }

		public async Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId)
		{
			var result = await _vehicleQueries.GetByIdAsync(vehicleId);
			return result;
		}

		public async Task<Vehicle> CreateVehicleAsync(VehicleRequestModel requestModel)
		{
			var newVehicle = _mapper.Map<Vehicle>(requestModel);

			await ValidateVehicleAsync(newVehicle);

			var (newImages, vehicleImages) = HandleNewVehicleImages(requestModel, newVehicle.Id);

			_vehicleRepo.Add(newVehicle);

			_imageRepo.AddRange(newImages);

			_vehicleImageRepo.AddRange(vehicleImages);

			await _unitOfWork.CompleteAsync();

			return await GetVehicleByIdAsync(newVehicle.Id);
		}

		//public async Task<Brand> UpdateBrandAsync(BrandRequestModel requestModel, Guid brandId)
		//{
		//	var brand = await _brandQueries.GetByIdAsync(brandId);

		//	var originalBrandName = brand.Name;

		//	if (brand == null)
		//		throw new StatusCodeException(message: "Brand not found.", statusCode: StatusCodes.Status404NotFound);
		//	else
		//	{
		//		var brandToUpdate = _mapper.Map<BrandRequestModel, Brand>(requestModel, brand);

		//		var isBrandExisted = await _brandQueries.CheckIfBrandExisted(brandToUpdate.Name);

		//		if (isBrandExisted && (brandToUpdate.Name.ToUpper() != originalBrandName.ToUpper()))
		//			throw new StatusCodeException(message: "Brand name already exsited.", statusCode: StatusCodes.Status409Conflict);
		//		else
		//		{
		//			foreach (var brandImageToDelete in brand.BrandImages)
		//			{
		//				_brandImageRepo.DeleteByEntity(brandImageToDelete);

		//				_imageRepo.Delete(brandImageToDelete.ImageId);
		//			}

		//			var (newImage, brandImage) = HandleNewBrandImage(requestModel, brandToUpdate.Id);

		//			_brandRepo.Update(brandToUpdate);

		//			_imageRepo.Add(newImage);

		//			_brandImageRepo.Add(brandImage);

		//			await _unitOfWork.CompleteAsync();

		//			return brandToUpdate;
		//		}
		//	}
		//}

		//public async Task<bool> DeleteBrandAsync(Guid brandId)
		//{
		//	var brand = await _brandRepo.GetById(brandId);

		//	if (brand == null)
		//		throw new StatusCodeException(message: "Brand not found.", statusCode: StatusCodes.Status404NotFound);

		//	_brandRepo.Delete(brandId);

		//	await _unitOfWork.CompleteAsync();

		//	return true;
		//}

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

				var vehicleImage = new Database.Models.VehicleImage
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

			if (isLicensePlateExisted)
				throw new StatusCodeException(message: "License plate already exsited.", statusCode: StatusCodes.Status409Conflict);

			if (isInsuranceNumberExisted)
				throw new StatusCodeException(message: "Insurance number already exsited.", statusCode: StatusCodes.Status409Conflict);
		}
	}
}
