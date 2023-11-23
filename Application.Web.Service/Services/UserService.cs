using Application.Web.Database.Constants;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Diacritics.Extensions;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Service.Services
{
	public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<User> _userRepo;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAppCache _cache;
        private readonly IMapper _mapper;
        private readonly CacheKeyConstants _cacheKeyConstants;
        private readonly IUserQueries _userQueries;

        public UserService(IUnitOfWork unitOfWork, IAppCache cache, IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, CacheKeyConstants cacheKeyConstants, IUserQueries userQueries)
        {
            _unitOfWork = unitOfWork;
            _userRepo = unitOfWork.GetBaseRepo<User>();
            _userManager = userManager;
            _roleManager = roleManager;
            _cache = cache;
            _mapper = mapper;
            _cacheKeyConstants = cacheKeyConstants;
            _userQueries = userQueries;

        }

        public async Task<User> GetUserInformationByEmailAsync(string email)
        {
            var key = $"{_cacheKeyConstants.UserCacheKey}-{email}";

            var user = await _cache.GetOrAddAsync(
                key,
                async () => await _userQueries.GetUserByEmailAsync(email),
                TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

            _cacheKeyConstants.AddKeyToList(key);

            if (user == null)
                throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

            return user;
        }


        public async Task<List<Role>> GetAllAvailableRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return roles;
        }

        public async Task<User> GetUserInformationByIdAsync(Guid id)
        {
            var key = $"{_cacheKeyConstants.UserCacheKey}-{id}";

            var user = await _cache.GetOrAddAsync(
                key,
                async () => await _userQueries.GetUserByIdAsync(id),
                TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

            _cacheKeyConstants.AddKeyToList(key);

            if (user == null)
                throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

            return user;
        }

        public async Task<User> GetUserInformationByUsernameAsync(string username)
        {
            var key = $"{_cacheKeyConstants.UserCacheKey}-{username}";

            var user = await _cache.GetOrAddAsync(
                key,
                async () => await _userQueries.GetUserByUsernameAsync(username),
                TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

            _cacheKeyConstants.AddKeyToList(key);

            if (user == null)
                throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

            return user;
        }

        public async Task<List<User>> GetAllUsersInformationAsync(UserQuery userQuery)
        {
            var key = $"{_cacheKeyConstants.UserCacheKey}-All";

            var users = await _cache.GetOrAddAsync(
                key,
                async () => await _userQueries.GetAllUsersAsync(),
                TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

            _cacheKeyConstants.AddKeyToList(key);

            users = await HandleUserQuery(userQuery, users);

            return users;
        }

        public async Task<(List<User>, PaginationMetadata)> GetAllUserInformationWithPaginationAsync(PaginationRequestModel pagination, UserQuery userQuery)
        {
            var key = $"{_cacheKeyConstants.UserCacheKey}-All";

            var users = await _cache.GetOrAddAsync(
                key,
                async () => await _userQueries.GetAllUsersAsync(),
                TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

            _cacheKeyConstants.AddKeyToList(key);

            users = await HandleUserQuery(userQuery, users);

            var totalItemCount = users.Count;

            var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

            var usersToReturn = users
                .Skip(pagination.pageSize * (pagination.pageNumber - 1))
                .Take(pagination.pageSize)
                .ToList();

            return (usersToReturn, paginationMetadata);
        }

        public async Task<User> UpdateUserAsync(UserRequestModel requestModel, string username)
        {
            var user = await _userQueries.GetUserByUsernameAsync(username) ?? throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

            PopulateUserRequestModel(requestModel, user);

            _userRepo.Update(user);

            await _unitOfWork.CompleteAsync();

            _unitOfWork.Detach(user);

            await Task.Run(() =>
            {
                foreach (var key in _cacheKeyConstants.CacheKeyList)
                {
                    _cache.Remove(key);
                }

                _cacheKeyConstants.CacheKeyList = new List<string>();
            });

            return user;
        }

        public async Task<User> UpdateUserRoleAsync(UserRoleRequestModel roleRequestModel)
        {
            var user = await _userManager.FindByIdAsync(roleRequestModel.UserId.ToString()) ?? throw new StatusCodeException(message: "User not found!", statusCode: StatusCodes.Status404NotFound);

            var newRole = SeedDatabaseConstant.DEFAULT_ROLES.FirstOrDefault(x => x.Id.Equals(roleRequestModel.RoleId)) ?? throw new StatusCodeException(message: "Role not found", statusCode: StatusCodes.Status404NotFound);

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Contains(newRole.Name)) throw new StatusCodeException(message: "Role already existed.", statusCode: StatusCodes.Status409Conflict);

            await _userManager.RemoveFromRolesAsync(user, userRoles);

            var result = await _userManager.AddToRoleAsync(user, newRole.Name);

            return await _userQueries.GetUserByIdAsync(roleRequestModel.UserId);
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            var userToDelete = await _userManager.FindByNameAsync(username);

            if (userToDelete == null)
                throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

            var result = await _userManager.DeleteAsync(userToDelete);

            await Task.Run(() =>
            {
                foreach (var key in _cacheKeyConstants.CacheKeyList)
                {
                    _cache.Remove(key);
                }

                _cacheKeyConstants.CacheKeyList = new List<string>();
            });

            return result.Succeeded;
        }

        private static void PopulateUserRequestModel(UserRequestModel requestModel, User user)
        {
            if (!Helpers.Helpers.IsValidPhoneNumber(requestModel.PhoneNumber.Trim()))
                throw new StatusCodeException(message: "Phone number is not valid.", statusCode: StatusCodes.Status400BadRequest);

            user.FirstName = requestModel.FirstName.Trim();

            user.LastName = requestModel.LastName.Trim();

            user.Address = requestModel.Address.Trim();

            user.PhoneNumber = requestModel.PhoneNumber.Trim();

            user.DateOfBirth = requestModel.DateOfBirth;

            user.Picture = requestModel.Image.ImageUrl.Trim();

            user.PublicId = requestModel.Image.PublicId;
        }

        private async Task<List<User>> HandleUserQuery(UserQuery userQuery, List<User> users)
        {
            if (userQuery.Roles != null && userQuery.Roles.Count > 0)
            {
                var userRoleHoler = new List<User>();
                foreach (var role in userQuery.Roles)
                {
                    var specificRole = SeedDatabaseConstant.DEFAULT_ROLES.FirstOrDefault(x => x.NormalizedName.Equals(role.ToUpper().Trim()));

                    if (specificRole == null) continue;

                    var usersInRole = await _userManager.GetUsersInRoleAsync(specificRole.Name);

                    var result = users.Intersect(usersInRole.ToList(), new UserComparer()).ToList();

                    var differentUsers = result.Except(userRoleHoler, new UserComparer()).ToList();

                    userRoleHoler.AddRange(differentUsers);
                }
                users = userRoleHoler;
            }

            if (!String.IsNullOrEmpty(userQuery.Query))
            {
                var trimmedQuery = userQuery.Query.ToUpper().Trim().RemoveDiacritics();

                users = users
                    .Where(x =>
                            x.NormalizedUserName.ToUpper().Trim().RemoveDiacritics().Contains(trimmedQuery) ||
                            x.NormalizedEmail.ToUpper().Trim().RemoveDiacritics().Contains(trimmedQuery) ||
                            x.FullName.ToUpper().Trim().RemoveDiacritics().Contains(trimmedQuery) ||
                            (x.PhoneNumber?.ToUpper().Trim().RemoveDiacritics() ?? "").Contains(trimmedQuery))
                    .ToList();
            }

            return users;
        }

        private class UserComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                return x.UserName == y.UserName;
            }

            public int GetHashCode(User obj)
            {
                return obj.UserName.GetHashCode();

            }
        }
    }
}
