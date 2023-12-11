using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class BlogCommentQueries : BaseQuery<BlogComment>, IBlogCommentQueries
	{
		public BlogCommentQueries(ApplicationContext dbContext) : base(dbContext) { }

		public async Task<IEnumerable<BlogComment>> GetAllBlogCommentsByBlogId(Guid blogId)
		{
			return await dbSet
				.Include(x => x.User)
				.Where(x => x.BlogId.Equals(blogId))
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<BlogComment> GetBlogCommentById(Guid blogCommentId)
		{
			return await dbSet
				.Include(x => x.User)
				.Where(x => x.Id.Equals(blogCommentId))
				.AsNoTracking()
				.FirstOrDefaultAsync();
		}
	}
}
