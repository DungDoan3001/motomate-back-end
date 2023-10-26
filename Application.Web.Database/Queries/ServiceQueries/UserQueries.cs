using System.Runtime.CompilerServices;
using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class UserQueries : BaseQuery<User>, IUserQueries
	{
		public UserQueries(ApplicationContext dbContext) : base(dbContext)
		{
		}
		
		public async Task<bool> CheckIfUserExisted(Guid userId)
		{
			return await dbSet
				.AnyAsync(x => x.Id.Equals(userId));
		}

		public async Task<List<User>> GetAllUsersAsync()
		{
			var users = await dbSet
				.Include(x => x.UserRoles).ThenInclude(x => x.Role)
				.ToListAsync();
			return users;
		}

		public async Task<User> GetUserByIdAsync(Guid userId)
		{
			return await dbSet
				.Include(x => x.UserRoles).ThenInclude(x => x.Role)
				.Where(x => x.Id.Equals(userId))
				.FirstOrDefaultAsync();
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await dbSet
				.Include(x => x.UserRoles).ThenInclude(x => x.Role)
				.Where(x => x.NormalizedEmail.Equals(email.ToUpper().Trim()))
				.FirstOrDefaultAsync();
		}

		public async Task<User> GetUserByUsernameAsync(string username)
		{
			return await dbSet
				.Include(x => x.UserRoles).ThenInclude(x => x.Role)
				.Where(x => x.NormalizedUserName.Equals(username.ToUpper().Trim()))
				.FirstOrDefaultAsync();
		}
	}
}
