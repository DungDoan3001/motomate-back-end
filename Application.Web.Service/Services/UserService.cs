using Application.Web.Database.Models;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Web.Service.Services
{
    public class UserService: IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserInformationByEmailAsync(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

            return user;
        }
    }
}
