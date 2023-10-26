using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
	public interface IUserQueries
	{
		Task<bool> CheckIfUserExisted(Guid userId);
		Task<List<User>> GetAllUsersAsync();
		Task<User> GetUserByIdAsync(Guid userId);
		Task<User> GetUserByEmailAsync(string email);
		Task<User> GetUserByUsernameAsync(string username);
	}
}
