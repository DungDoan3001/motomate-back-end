using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
	public interface IBlogService
	{
		Task<(List<Blog>, PaginationMetadata)> GetAllBlogsAsync(PaginationRequestModel pagination);
		Task<Blog> GetBlogByIdAsync(Guid blogId);
		Task<Blog> CreateBlogAsync(BlogRequestModel requestModel);
		Task<Blog> UpdateBlogAsync(BlogRequestModel requestModel, Guid blogId);
		Task<bool> DeleteBlogAsync(Guid blogId);
	}
}
