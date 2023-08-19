using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
    public interface IBrandService
    {
        Task<(IEnumerable<Brand>, PaginationMetadata)> GetBrandsAsync(PaginationRequestModel pagination);
        Task<Brand> GetBrandByIdAsync(Guid brandId);
        Task<Brand> CreateBrandAsync(BrandRequestModel requestModel);
        Task<Brand> UpdateBrandAsync(BrandRequestModel requestModel, Guid brandId);
        Task<bool> DeleteBrandAsync(Guid brandId);
        Task<List<Brand>> GetAllBrandsAsync();
    }
}
