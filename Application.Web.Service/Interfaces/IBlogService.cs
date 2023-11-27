using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
	public interface IBlogService
	{
		Task<(IEnumerable<Blog>, PaginationMetadata)> GetAllBlogsAsync(PaginationRequestModel pagination);
		Task<Blog> GetBlogByIdAsync(Guid blogId);
		Task<Blog> CreateBlogAsync(BlogRequestModel requestModel);
		Task<Blog> UpdateBlogAsync(BlogRequestModel requestModel, Guid blogId);
		Task<bool> DeleteBlogAsync(Guid blogId);
		Task<IEnumerable<Blog>> GetRelatedBlogAsync(Guid blogId);
	}
}
