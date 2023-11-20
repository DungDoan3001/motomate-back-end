using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Web.Database.Constants;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Web.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<ResetPassword> _resetPasswordRepo;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _resetPasswordRepo = unitOfWork.GetBaseRepo<ResetPassword>();
            _userManager = userManager;
            _emailService = emailService;
            _config = configuration;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterUserAsync(UserRegistrationRequestModel userRegistration)
        {
            var user = await _userManager.FindByEmailAsync(userRegistration.Email);

            if (user != null)
                throw new StatusCodeException(message: "User existed.", statusCode: StatusCodes.Status409Conflict);

            if (!userRegistration.Password.Equals(userRegistration.PasswordConfirm))
                throw new StatusCodeException(message: "Password not match.", statusCode: StatusCodes.Status400BadRequest);

            var newUser = _mapper.Map<User>(userRegistration);

            var result = await _userManager.CreateAsync(newUser, userRegistration.Password);

            await _userManager.AddToRoleAsync(newUser, SeedDatabaseConstant.USER.Name);

            return result;
        }

        public async Task<bool> ValidateUserAsync(UserLoginRequestModel userLogin)
        {
            User user = await _userManager.FindByEmailAsync(userLogin.Email);

            var result = user != null && await _userManager.CheckPasswordAsync(user, userLogin.Password);

            return result;
        }

        public async Task<bool> SendEmailResetPassword(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user != null)
            {
                var changePasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                var resetPasswordTokens = await _resetPasswordRepo.Find(x => x.UserId == user.Id);

                if (!resetPasswordTokens.IsNullOrEmpty())
                {
                    _resetPasswordRepo.DeleteRange(resetPasswordTokens);
                }

                ResetPassword resetPassword = new()
                {
                    Token = changePasswordToken,
                    UserId = user.Id
                };

                _resetPasswordRepo.Add(resetPassword);

                await _unitOfWork.CompleteAsync();

                string encodedToken = Helpers.GuidBase64.GetEncodedGuid(resetPassword.Id);

                await SendChangePasswordEmailAsync(user, encodedToken);
                
                return true;
            }
            else
                throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);
            
        }

        public async Task<bool> ChangePassword(string encodedToken, ChangePasswordRequestModel changePasswordRequest)
        {
            bool isBase64 = Helpers.GuidBase64.IsBase64Format(encodedToken);

            if(!isBase64)
                throw new StatusCodeException(message: "Invalid Base64 format.", statusCode: StatusCodes.Status400BadRequest);

            Guid resetPasswordId = Helpers.GuidBase64.GetDecodedGuid(encodedToken);

            ResetPassword resetPassword = await _resetPasswordRepo.FindOne(x => x.Id == resetPasswordId);

            if (resetPassword == null)
                throw new StatusCodeException(message: "Token is invalid", statusCode: StatusCodes.Status400BadRequest);

            var checkExpire = DateTime.Compare(resetPassword.CreatedDate.AddHours(24), DateTime.UtcNow);

            if(checkExpire == -1)
            {
                _resetPasswordRepo.Delete(resetPassword.Id);
                throw new StatusCodeException(message: "Token expired.", statusCode: StatusCodes.Status400BadRequest);
            }

            User user = await _userManager.FindByIdAsync(resetPassword.UserId.ToString());

            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, changePasswordRequest.ConfirmPassword);
            
            if(!result.Succeeded)
            {
                throw new StatusCodeException(message: "Error while handle reset.", statusCode: StatusCodes.Status409Conflict);
            }

            _resetPasswordRepo.Delete(resetPassword.Id);

            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<string> HandleGoogleSSOAsync(string tokenCredential)
        {
            GoogleJsonWebSignature.Payload userPayload = await GoogleJsonWebSignature.ValidateAsync(tokenCredential);
            
            var user = await _userManager.FindByEmailAsync(userPayload.Email);
            
            string jwtToken = "";
            
            if(user == null)
            {
                string username = Helpers.Helpers.ExtractEmailAddress(userPayload.Email);
                
                User newUser = new()
                {
                    Email = userPayload.Email,
                    UserName = username,
                    FirstName = userPayload.GivenName,
                    LastName = userPayload.FamilyName,
                    Picture = userPayload.Picture
                };
                
                var result = await _userManager.CreateAsync(newUser);
                
                await _userManager.AddToRoleAsync(newUser, SeedDatabaseConstant.USER.Name);
                
                if (result.Succeeded) jwtToken = await CreateTokenAsync(newUser.Email);
            } 
            else
                jwtToken = await CreateTokenAsync(user.Email);

            return jwtToken;
        }

        public async Task<string> CreateTokenAsync(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);

            var signingCreadentials = GetSigningCredentials();

            var claims = await GetClaims(user);

            var tokenOptions = GenerateTokenOptions(signingCreadentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private async Task<bool> SendChangePasswordEmailAsync(User user, string encodedToken)
        {
            SendEmailOptions emailOptions = new()
            {
                ToName = user.FullName,
                ToEmail = user.Email,
                Body = encodedToken,
                Subject = "[Motormate] Password Reset"
            };

            return await _emailService.SendEmailAsync(emailOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _config.GetSection("JwtConfig");

            var key = Encoding.UTF8.GetBytes(jwtConfig["serect"]);

            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim("Avatar", user.Picture)
            }; 

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _config.GetSection("JwtConfig");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}
