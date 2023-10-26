using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
	public interface IBlogCategoryService
	{
		Task<IEnumerable<BlogCategory>> GetAllCategoryAsync();
		Task<BlogCategory> CreateBlogCategoryAsync(BlogCategoryRequestModel requestModel);
		Task<BlogCategory> UpdateCategoryAsync(BlogCategoryRequestModel requestModel, Guid categoryId);
		Task<bool> DeleteCategoryAsync(Guid categoryId);	
	}
}
