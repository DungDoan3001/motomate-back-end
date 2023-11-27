using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserInformationByEmailAsync(string email);
        Task<User> GetUserInformationByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersInformationAsync(UserQuery userQuery);
        Task<User> UpdateUserAsync(UserRequestModel requestModel, string username);
        Task<bool> DeleteUserAsync(string username);
        Task<User> GetUserInformationByIdAsync(Guid id);
        Task<(IEnumerable<User>, PaginationMetadata)> GetAllUserInformationWithPaginationAsync(PaginationRequestModel pagination, UserQuery userQuery);
        Task<User> UpdateUserRoleAsync(UserRoleRequestModel roleRequestModel);
		Task<IEnumerable<Role>> GetAllAvailableRolesAsync();
	}
}
