using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class BlogCategoryQueries : BaseQuery<BlogCategory>, IBlogCategoryQueries
	{
		public BlogCategoryQueries(ApplicationContext dbContext) : base(dbContext) { }

		public async Task<bool> IsCategoryExisted(string name)
		{
			return await dbSet
				.AnyAsync(bc => bc.Name.ToUpper().Trim().Equals(name.ToUpper().Trim()));
		}

		public async Task<BlogCategory> GetByIdAsync(Guid categoryId)
		{
			return await dbSet
				.Where(bc => bc.Id.Equals(categoryId))
				.AsNoTracking()
				.FirstOrDefaultAsync();
		}
	}
}
