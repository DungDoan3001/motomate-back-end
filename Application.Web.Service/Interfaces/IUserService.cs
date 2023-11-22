using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserInformationByEmailAsync(string email);
        Task<User> GetUserInformationByUsernameAsync(string username);
        Task<List<User>> GetAllUsersInformationAsync(UserQuery userQuery);
        Task<User> UpdateUserAsync(UserRequestModel requestModel, string username);
        Task<bool> DeleteUserAsync(string username);
        Task<User> GetUserInformationByIdAsync(Guid id);
        Task<(List<User>, PaginationMetadata)> GetAllUserInformationWithPaginationAsync(PaginationRequestModel pagination, UserQuery userQuery);
        Task<bool> UpdateUserRoleAsync(UserRoleRequestModel roleRequestModel);
        Task<List<Role>> GetAllAvailableRolesAsync();
	}
}
