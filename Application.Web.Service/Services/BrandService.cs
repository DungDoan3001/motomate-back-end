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
using LazyCache;
using Microsoft.AspNetCore.Http;

namespace Application.Web.Service.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Brand> _brandRepo;
        private readonly IGenericRepository<BrandImage> _brandImageRepo;
        private readonly IGenericRepository<Image> _imageRepo;
        private readonly IBrandQueries _brandQueries;
        private readonly IMapper _mapper;
		private IAppCache _cache;
		private CacheKeyConstants _cacheKeyConstants;

		public BrandService(IUnitOfWork unitOfWork, IBrandQueries brandQueries, IMapper mapper, IAppCache cache, CacheKeyConstants cacheKeyConstants)
        {
            _unitOfWork = unitOfWork;
            _brandRepo = unitOfWork.GetBaseRepo<Brand>();
            _brandImageRepo = unitOfWork.GetBaseRepo<BrandImage>();
            _imageRepo = unitOfWork.GetBaseRepo<Image>();
            _brandQueries = brandQueries;
            _mapper = mapper;
			_cache = cache;
            _cacheKeyConstants = cacheKeyConstants;
        }

        public async Task<(IEnumerable<Brand>, PaginationMetadata)> GetBrandsAsync(PaginationRequestModel pagination)
        {
            var key = $"{_cacheKeyConstants.BrandCacheKey}-All";

            var brands = await _cache.GetOrAddAsync(
                key, 
                async () => await _brandQueries.GetAllBrandsAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

            _cacheKeyConstants.AddKeyToList(key);

			var totalItemCount = brands.Count;

            var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

            var brandsToReturn = brands
                .Skip(pagination.pageSize * (pagination.pageNumber - 1))
                .Take(pagination.pageSize);

			return (brandsToReturn, paginationMetadata);
        }

        public async Task<List<Brand>> GetAllBrandsAsync()
        {
			var key = $"{_cacheKeyConstants.BrandCacheKey}-All";

			var brands = await _cache.GetOrAddAsync(
                key, 
                async () => await _brandQueries.GetAllBrandsAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			return brands;
        }

        public async Task<Brand> GetBrandByIdAsync(Guid brandId)
        {
			var key = $"{_cacheKeyConstants.BrandCacheKey}-ID-{brandId}";

            var result = await _cache.GetOrAddAsync(
                key, 
                async () => await _brandQueries.GetByIdAsync(brandId), 
                TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			return result;
        }

        public async Task<Brand> CreateBrandAsync(BrandRequestModel requestModel)
        {
            var newBrand = _mapper.Map<Brand>(requestModel);

            var isBrandExisted = await _brandQueries.CheckIfBrandExisted(newBrand.Name);

            if(isBrandExisted)
                throw new StatusCodeException(message: "Brand name already existed.", statusCode: StatusCodes.Status409Conflict);
            else
            {
                var (newImage, brandImage) = HandleNewBrandImage(requestModel, newBrand.Id);

                _brandRepo.Add(newBrand);

                _imageRepo.Add(newImage);

                _brandImageRepo.Add(brandImage);

                await _unitOfWork.CompleteAsync();

				await Task.Run(() =>
				{
					foreach (var key in _cacheKeyConstants.CacheKeyList)
					{
						_cache.Remove(key);
					}

					_cacheKeyConstants.CacheKeyList = new List<string>();
				});

				return newBrand;
            }
        }

        public async Task<Brand> UpdateBrandAsync(BrandRequestModel requestModel, Guid brandId)
        {
            var brand = await _brandQueries.GetByIdAsync(brandId);

            var originalBrandName = brand.Name;

            if (brand == null)
                throw new StatusCodeException(message: "Brand not found.", statusCode: StatusCodes.Status404NotFound);
            else
            {
                var brandToUpdate = _mapper.Map<BrandRequestModel, Brand>(requestModel, brand);

                var isBrandExisted = await _brandQueries.CheckIfBrandExisted(brandToUpdate.Name);

                if (isBrandExisted && (brandToUpdate.Name.ToUpper() != originalBrandName.ToUpper()))
                    throw new StatusCodeException(message: "Brand name already existed.", statusCode: StatusCodes.Status409Conflict);
                else
                {
                    foreach (var brandImageToDelete in brand.BrandImages)
                    {
                        _imageRepo.Delete(brandImageToDelete.ImageId);
                    }

                    await _unitOfWork.CompleteAsync();

                    var (newImage, brandImage) = HandleNewBrandImage(requestModel, brandToUpdate.Id);

                    _brandRepo.Update(brandToUpdate);

                    _imageRepo.Add(newImage);

                    _brandImageRepo.Add(brandImage);

                    await _unitOfWork.CompleteAsync();

					await Task.Run(() =>
					{
						foreach (var key in _cacheKeyConstants.CacheKeyList)
						{
							_cache.Remove(key);
						}

						_cacheKeyConstants.CacheKeyList = new List<string>();
					});

					return brandToUpdate;
                }
            }
        }

        public async Task<bool> DeleteBrandAsync(Guid brandId)
        {
            var brand = await _brandRepo.GetById(brandId);

            if (brand == null)
                throw new StatusCodeException(message: "Brand not found.", statusCode: StatusCodes.Status404NotFound);

            _brandRepo.Delete(brandId);

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

        private (Image, BrandImage) HandleNewBrandImage(BrandRequestModel requestModel, Guid brandId)
        {
            var newImage = new Image
            {
                ImageUrl = requestModel.ImageUrl,
                PublicId = requestModel.PublicId,
            };

            var brandImage = new BrandImage
            {
                BrandId = brandId,
                ImageId = newImage.Id,
            };

            return(newImage, brandImage);
        }
    }
}
