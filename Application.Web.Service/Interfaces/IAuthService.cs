using Application.Web.Database.DTOs.RequestModels;
using Microsoft.AspNetCore.Identity;

namespace Application.Web.Service.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegistrationRequestModel userRegistration);
        Task<bool> ValidateUserAsync(UserLoginRequestModel userLogin);
        Task<string> HandleGoogleSSOAsync(string tokenCredential);
        Task<string> CreateTokenAsync(string username);
        Task<bool> SendEmailResetPassword(string userEmail);
        Task<bool> ChangePassword(string encodedToken, ChangePasswordRequestModel changePasswordRequest);
        Task<bool> LockUserAccountAsync(Guid userId);

	}
}