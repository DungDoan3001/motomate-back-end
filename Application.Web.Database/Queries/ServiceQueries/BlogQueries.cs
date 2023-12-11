using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class BlogQueries : BaseQuery<Blog>, IBlogQueries
	{
		public BlogQueries(ApplicationContext dbContext) : base(dbContext) { }

		public async Task<List<Blog>> GetAllBlogsAsync()
		{
			return await dbSet
				.Include(b => b.Author)
				.Include(b => b.Category)
				.Include(b => b.Image)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Blog> GetBlogById(Guid blogId)
		{
			return await dbSet
				.Include(b => b.Author)
				.Include(b => b.Category)
				.Include(b => b.Image)
				.Where(b => b.Id.Equals(blogId))
				.AsNoTracking()
				.FirstOrDefaultAsync();
		}

		public async Task<bool> CheckIfBlogExist(Guid blogId)
		{
			return await dbSet
				.AnyAsync(x => x.Id.Equals(blogId));
		}
	}
}
