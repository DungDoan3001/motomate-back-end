using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
	public interface IBlogQueries
	{
		Task<List<Blog>> GetAllBlogsAsync();
		Task<Blog> GetBlogById(Guid blogId);
		Task<bool> CheckIfBlogExist(Guid blogId);
	}
}
