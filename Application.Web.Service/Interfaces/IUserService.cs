﻿using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserInformationByEmailAsync(string email);
        Task<User> GetUserInformationByUsernameAsync(string username);
        Task<List<User>> GetAllUsersInformationAsync();
    }
}
