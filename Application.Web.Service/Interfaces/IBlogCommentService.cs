using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
	public interface IBlogCommentService
	{
		Task<(IEnumerable<BlogComment>, PaginationMetadata)> GetAllBlogCommentByBlogIdAsync(PaginationRequestModel pagination, Guid blogId);
	}
}
