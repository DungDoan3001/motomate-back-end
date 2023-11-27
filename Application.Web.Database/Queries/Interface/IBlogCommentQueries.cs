using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
	public interface IBlogCommentQueries
	{
		Task<IEnumerable<BlogComment>> GetAllBlogCommentsByBlogId(Guid blogId);
		Task<BlogComment> GetBlogCommentById(Guid blogCommentId);
	}
}
