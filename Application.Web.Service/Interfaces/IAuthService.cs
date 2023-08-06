using Application.Web.Database.DTOs.RequestModels;
using Microsoft.AspNetCore.Identity;

namespace Application.Web.Service.Services
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(UserLoginRequestModel userLogin);
        Task<IdentityResult> RegisterUserAsync(UserRegistrationRequestModel userRegistration);
        Task<bool> ValidateUserAsync(UserLoginRequestModel userLogin);
    }
}