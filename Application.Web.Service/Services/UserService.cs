﻿using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using AutoMapper;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Application.Web.Service.Services
{
	public class UserService: IUserService
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<User> _userRepo;
		private readonly UserManager<User> _userManager;
		private readonly IAppCache _cache;
		private readonly IMapper _mapper;
		private readonly CacheKeyConstants _cacheKeyConstants;
		private readonly IUserQueries _userQueries;

		public UserService(IUnitOfWork unitOfWork, IAppCache cache, IMapper mapper, UserManager<User> userManager, CacheKeyConstants cacheKeyConstants, IUserQueries userQueries)
        {
            _unitOfWork = unitOfWork;
            _userRepo = unitOfWork.GetBaseRepo<User>();
			_userManager = userManager;
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

        public async Task<List<User>> GetAllUsersInformationAsync()
        {
			var key = $"{_cacheKeyConstants.UserCacheKey}-All";

			var users = await _cache.GetOrAddAsync(
				key,
				async () => await _userQueries.GetAllUsersAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

            return users;
		}

        public async Task<(List<User>, PaginationMetadata)> GetAllUserInformationWithPaginationAsync(PaginationRequestModel pagination)
        {
            var key = $"{_cacheKeyConstants.UserCacheKey}-All";

            var users = await _cache.GetOrAddAsync(
                key,
                async () => await _userQueries.GetAllUsersAsync(),
                TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

            _cacheKeyConstants.AddKeyToList(key);

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
            if(!Helpers.Helpers.IsValidPhoneNumber(requestModel.PhoneNumber.Trim()))
                throw new StatusCodeException(message: "Phone number is not valid.", statusCode: StatusCodes.Status400BadRequest);

            user.FirstName = requestModel.FirstName.Trim();

            user.LastName = requestModel.LastName.Trim();

            user.Address = requestModel.Address.Trim();

            user.PhoneNumber = requestModel.PhoneNumber.Trim();

            user.DateOfBirth = requestModel.DateOfBirth;

            user.Picture = requestModel.Image.ImageUrl.Trim();

			user.PublicId = requestModel.Image.PublicId;
		}
    }
}
