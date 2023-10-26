using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class BlogQueries : BaseQuery<Blog>, IBlogQueries
	{
		public BlogQueries(ApplicationContext dbContext) : base(dbContext) { }
	}
}
