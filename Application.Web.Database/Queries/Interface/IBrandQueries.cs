using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
	public interface IBrandQueries
	{
		Task<List<Brand>> GetBrandsWithPaginationAync(PaginationRequestModel pagination);
		Task<int> CountBrandsAsync();
		Task<Brand> GetByBrandNameAsync(string name);
		Task<List<Brand>> GetAllBrandsAsync();
		Task<bool> CheckIfBrandExisted(string name);
		Task<Brand> GetByIdAsync(Guid brandId);
	}
}
