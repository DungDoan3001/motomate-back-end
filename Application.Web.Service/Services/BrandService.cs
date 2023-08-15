using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;

namespace Application.Web.Service.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Brand> _brandRepo;
        private readonly IBrandQueries _brandQueries;

        public BrandService(IUnitOfWork unitOfWork, IBrandQueries brandQueries)
        {
            _unitOfWork = unitOfWork;
            _brandRepo = unitOfWork.GetBaseRepo<Brand>();
            _brandQueries = brandQueries;
        }

        public async Task<(IEnumerable<Brand>, PaginationMetadata)> GetBrandsAsync(PaginationRequestModel pagination)
        {
            var totalItemCount = await _brandQueries.CountBrandsAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

            var brandToReturn = await _brandQueries.GetBrandsWithPaginationAync(pagination);

            return (brandToReturn, paginationMetadata);
        }

        public async Task<Brand> GetBrandByIdAsync(Guid brandId)
        {
            var result = await _brandRepo.GetById(brandId);
            return result;
        }
    }
}
