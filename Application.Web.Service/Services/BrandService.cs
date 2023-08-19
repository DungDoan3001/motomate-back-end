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
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Brand> _brandRepo;
        private readonly IBrandQueries _brandQueries;
        private readonly IMapper _mapper;

        public BrandService(IUnitOfWork unitOfWork, IBrandQueries brandQueries, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _brandRepo = unitOfWork.GetBaseRepo<Brand>();
            _brandQueries = brandQueries;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<Brand>, PaginationMetadata)> GetBrandsAsync(PaginationRequestModel pagination)
        {
            var totalItemCount = await _brandQueries.CountBrandsAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

            var brandToReturn = await _brandQueries.GetBrandsWithPaginationAync(pagination);

            return (brandToReturn, paginationMetadata);
        }

        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            var brandToReturn = await _brandQueries.GetAllBrandsAsync();

            return brandToReturn;
        }

        public async Task<Brand> GetBrandByIdAsync(Guid brandId)
        {
            var result = await _brandRepo.GetById(brandId);
            return result;
        }

        public async Task<Brand> CreateBrandAsync(BrandRequestModel requestModel)
        {
            var newBrand = _mapper.Map<Brand>(requestModel);

            var isBrandExisted = await _brandQueries.CheckIfBrandExisted(newBrand.Name);

            if(isBrandExisted)
                throw new StatusCodeException(message: "Brand name already exsited.", statusCode: StatusCodes.Status409Conflict);
            else
            {
                _brandRepo.Add(newBrand);

                await _unitOfWork.CompleteAsync();

                return newBrand;
            }
        }

        public async Task<Brand> UpdateBrandAsync(BrandRequestModel requestModel, Guid brandId)
        {
            var brand = await _brandRepo.GetById(brandId);

            if (brand == null)
                throw new StatusCodeException(message: "Brand not found.", statusCode: StatusCodes.Status404NotFound);
            else
            {
                var brandToUpdate = _mapper.Map<BrandRequestModel, Brand>(requestModel, brand);

                _brandRepo.Update(brandToUpdate);

                await _unitOfWork.CompleteAsync();

                return brandToUpdate;
            }
        }

        public async Task<bool> DeleteBrandAsync(Guid brandId)
        {
            var brand = await _brandRepo.GetById(brandId);

            if (brand == null)
                throw new StatusCodeException(message: "Brand not found.", statusCode: StatusCodes.Status404NotFound);

            _brandRepo.Delete(brandId);

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
