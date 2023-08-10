using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            try
            {
                var userResult = await _authService.RegisterUserAsync(userRegistration);
                if (!userResult.Succeeded)
                {
                    return new BadRequestObjectResult(userResult);
                }
                else
                {
                    _logger.LogInformation($"[{controllerPrefix}] Created a user with user = {userRegistration.UserName}");
                    return StatusCode(StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at Get(): {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.Message }
                });
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginRequestModel userLogin)
        {
            try
            {
                bool validateResult = await _authService.ValidateUserAsync(userLogin);
                if (!validateResult)
                {
                    _logger.LogWarning($"[{controllerPrefix}] An unauthorized access with user: {userLogin.UserName}");
                    return Unauthorized();
                }
                else
                {
                    var token = new
                    {
                        Token = await _authService.CreateTokenAsync(userLogin.UserName)
                    };
                    _logger.LogInformation($"[{controllerPrefix}] Created token for user: {userLogin.UserName}");
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at Get(): {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.Message }
                });
            }
        }

        [HttpPost("sso/google")]
        public async Task<IActionResult> GoogleSSOProvider([FromBody] GoogleTokenRequestModel tokenRequest)
        {
            try
            {
                bool validation = tokenRequest.TokenCredential.IsNullOrEmpty();
                if (validation)
                {
                    return BadRequest();
                }
                else
                {
                    var token = new
                    {
                        Token = await _authService.HandleGoogleSSOAsync(tokenRequest.TokenCredential)
                    };
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at Get(): {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.Message }
                });
            }
        }
    }
}
