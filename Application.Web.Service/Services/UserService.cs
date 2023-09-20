using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Service.Services
{
	public class UserService: IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, UserManager<User> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<User> GetUserInformationByEmailAsync(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

            return user;
        }

        public async Task<User> GetUserInformationByIdAsync(Guid id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
				throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

			return user;
		}

        public async Task<User> GetUserInformationByUsernameAsync(string username)
        {
            User user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

            return user;
        }

        public async Task<List<User>> GetAllUsersInformationAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> UpdateUserAsync(UserRequestModel requestModel, string username)
        {
            var currentUser = await _userManager.FindByNameAsync(username);

            if(currentUser == null)
                throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

            var userToUpdate = currentUser;

            PopulateUserRequestModel(requestModel, userToUpdate);

            var result = await _userManager.UpdateAsync(userToUpdate);

            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    throw new StatusCodeException(message: error.Description, statusCode: StatusCodes.Status500InternalServerError);
                }
            }

            return userToUpdate;
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            var userToDelete = await _userManager.FindByNameAsync(username);

            if (userToDelete == null)
                throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

            var result = await _userManager.DeleteAsync(userToDelete);

            return result.Succeeded;
        }

        private void PopulateUserRequestModel(UserRequestModel requestModel, User user)
        {
            if(!Helpers.Helpers.IsValidPhoneNumber(requestModel.PhoneNumber.Trim()))
                throw new StatusCodeException(message: "Phone number is not valid.", statusCode: StatusCodes.Status400BadRequest);

            user.FirstName = requestModel.FirstName.Trim();

            user.LastName = requestModel.LastName.Trim();

            user.Address = requestModel.Address.Trim();

            user.PhoneNumber = requestModel.PhoneNumber.Trim();

            user.DateOfBirth = requestModel.DateOfBirth;
        }
    }
}
