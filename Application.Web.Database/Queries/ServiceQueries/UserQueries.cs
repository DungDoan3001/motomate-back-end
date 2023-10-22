using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class UserQueries : BaseQuery<User>, IUserQueries
	{
		public UserQueries(ApplicationContext dbContext) : base(dbContext) { }
		
		public async Task<bool> CheckIfUserExisted(Guid userId)
		{
			return await dbSet
				.AnyAsync(x => x.Id.Equals(userId));
		}
	}
}
