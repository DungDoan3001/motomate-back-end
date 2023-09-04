using System.Security.Claims;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.Models;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Applicaton.Web.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly string controllerPrefix = "User";

        public UserController(IMapper mapper, ILogger<AuthController> logger, IUserService userService)
        {
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Acquire all users information
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get items information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersInformationAsync();

                var usersToReturn = _mapper.Map<List<User>, List<UserResponseModel>>(users);

                return Ok(usersToReturn);
            }
            catch (StatusCodeException ex)
            {
                return StatusCode(ex.StatusCode, new ErrorResponseModel
                {
                    Message = ex.Message,
                    StatusCode = ex.StatusCode
                });
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

        /// <summary>
        /// Acquire user information by username
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get item information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet("{username}/details")]
        public async Task<IActionResult> GetUserByUsernameAsync([FromRoute] string username)
        {
            try
            {
                if(username.IsNullOrEmpty())
                    return BadRequest("username query is not null");

                var userDetail = await _userService.GetUserInformationByUsernameAsync(username);

                var userToReturn = _mapper.Map<User, UserResponseModel>(userDetail);

                return Ok(userToReturn);
            }
            catch (StatusCodeException ex)
            {
                return StatusCode(ex.StatusCode, new ErrorResponseModel
                {
                    Message = ex.Message,
                    StatusCode = ex.StatusCode
                });
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

        /// <summary>
        /// Acquire current request user information.
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get item information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet("details")]
        public async Task<IActionResult> GetCurrentUserInformationAsync()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                bool isIdentityHaveClaims = identity.Claims.Any();

                if (!isIdentityHaveClaims)
                    throw new StatusCodeException(message: "User not authenticated.", statusCode: StatusCodes.Status403Forbidden);

                var identityEmail = identity.FindFirst(ClaimTypes.Email);

                if(identityEmail == null)
                    throw new StatusCodeException(message: "Missing user information.", statusCode: StatusCodes.Status400BadRequest);

                var userDetail = await _userService.GetUserInformationByEmailAsync(identityEmail.Value);

                var userToReturn = _mapper.Map<User, UserResponseModel>(userDetail);

                return Ok(userToReturn);
            }
            catch (StatusCodeException ex)
            {
                return StatusCode(ex.StatusCode, new ErrorResponseModel
                {
                    Message = ex.Message,
                    StatusCode = ex.StatusCode
                });
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
