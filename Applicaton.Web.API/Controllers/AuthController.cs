using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        private readonly string controllerPrefix = "Auth";

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationRequestModel userRegistration)
        {
            var userResult = await _authService.RegisterUserAsync(userRegistration);
            if(!userResult.Succeeded)
            {
                return new BadRequestObjectResult(userResult);
            } else
            {
                _logger.LogInformation($"[{controllerPrefix}] Created a user with user = {userRegistration.UserName}");
                return StatusCode(StatusCodes.Status201Created);
            } 
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginRequestModel userLogin)
        {
            bool validateResult = await _authService.ValidateUserAsync(userLogin);
            if(!validateResult)
            {
                _logger.LogWarning($"[{controllerPrefix}] An unauthorized access with user: {userLogin.UserName}");
                return Unauthorized();
            } else
            {
                var token = new
                {
                    Token = await _authService.CreateTokenAsync(userLogin)
                };
                _logger.LogInformation($"[{controllerPrefix}] Created token for user: {userLogin.UserName}");
                return Ok(token);
            }
        }
    }
}
