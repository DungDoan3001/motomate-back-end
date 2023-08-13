﻿using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Helpers;
using Application.Web.Service.Services;
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
                _logger.LogError($"{controllerPrefix} error at {Helpers.GetCallerName()}: {ex.Message}", ex);
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
                    _logger.LogWarning($"[{controllerPrefix}] An unauthorized access with user: {userLogin.Email}");
                    return Unauthorized();
                }
                else
                {
                    var token = new
                    {
                        Token = await _authService.CreateTokenAsync(userLogin.Email)
                    };
                    _logger.LogInformation($"[{controllerPrefix}] Created token for user: {userLogin.Email}");
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at {Helpers.GetCallerName()}: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.Message }
                });
            }
        }

        [HttpPost("change/password")]
        public async Task<IActionResult> SendResetPasswordEmail([FromBody] EmailResetPasswordRequestModel request)
        {
            try
            {
                if (request.Email.IsNullOrEmpty())
                {
                    return BadRequest("Email is not null");
                }
                bool result = await _authService.SendEmailResetPassword(request.Email);
                return result ? Ok("Confirmed email has been sent") : BadRequest("Can not send the email");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at {Helpers.GetCallerName()}: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.InnerException.Message }
                });
            }
        }

        [HttpPost("change/password/{encodedToken}")]
        public async Task<IActionResult> ChangeUserPassword([FromRoute] string encodedToken, [FromBody] ChangePasswordRequestModel changePasswordRequest)
        {
            try
            {
                if (!changePasswordRequest.NewPassword.Equals(changePasswordRequest.ConfirmPassword))
                {
                    return BadRequest("Confirm password is not matched");
                }
                bool result = await _authService.ChangePassword(encodedToken, changePasswordRequest);
                return result ? Ok("Success") : BadRequest("Failed");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at {Helpers.GetCallerName()}: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.InnerException.Message }
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
                _logger.LogError($"{controllerPrefix} error at {Helpers.GetCallerName()}: {ex.Message}", ex);
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
