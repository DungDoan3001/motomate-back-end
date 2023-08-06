using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Web.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _config = configuration;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterUserAsync(UserRegistrationRequestModel userRegistration)
        {
            var user = _mapper.Map<User>(userRegistration);
            var result = await _userManager.CreateAsync(user, userRegistration.Password);
            return result;
        }

        public async Task<bool> ValidateUserAsync(UserLoginRequestModel userLogin)
        {
            User user = await _userManager.FindByNameAsync(userLogin.UserName);
            var result = user != null && await _userManager.CheckPasswordAsync(user, userLogin.Password);
            return result;
        }

        public async Task<String> CreateTokenAsync(UserLoginRequestModel userLogin)
        {
            User user = await _userManager.FindByNameAsync(userLogin.UserName);
            var signingCreadentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCreadentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
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
            // Create claims and claim UserName
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
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
