using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
	public interface IBlogCategoryQueries
	{
		Task<bool> IsCategoryExisted(string name);
		Task<BlogCategory> GetByIdAsync(Guid categoryId);
	}
}
