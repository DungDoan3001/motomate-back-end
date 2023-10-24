using System.Security.Claims;
using Application.Web.Database.Constants;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.Models;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Applicaton.Web.API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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
		private const int maxPageSize = 20;

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
        public async Task<ActionResult<IEnumerable<UserResponseModel>>> GetAllUsersAsync()
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
		/// Get all users information with pagition
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserResponseModel>>> GetAllUsersWithPaginationAsync([FromQuery] PaginationRequestModel pagination)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (users, paginationMetadata) = await _userService.GetAllUserInformationWithPaginationAsync(pagination);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

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
        public async Task<ActionResult<UserResponseModel>> GetUserByUsernameAsync([FromRoute] string username)
        {
            try
            {
                if(username.IsNullOrEmpty())
                    return BadRequest("username route is not null");

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
        public async Task<ActionResult<UserResponseModel>> GetCurrentUserInformationAsync()
        {
            try
            {
                var claimValues = IdentityHelpers.GetCurrentLoginUserClaims(HttpContext.User.Identity as ClaimsIdentity);

                var userDetail = await _userService.GetUserInformationByEmailAsync(claimValues.IdentityEmail);

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
        /// Update current user information.
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully updated item information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpPut("{username}")]
        public async Task<ActionResult<UserResponseModel>> UpdateUserInformation([FromRoute] string username, [FromBody] UserRequestModel requestModel)
        {
            try
            {
                if (username.IsNullOrEmpty())
                    return BadRequest("username route is not null");

                var claimValues = IdentityHelpers.GetCurrentLoginUserClaims(HttpContext.User.Identity as ClaimsIdentity);

                if (!claimValues.IdentityUsername.Equals(username) || 
                    claimValues.IdentityRoles.Any(x => x.Equals(SeedDatabaseConstant.ADMIN.Name)))
                    return Unauthorized();

                var user = await _userService.UpdateUserAsync(requestModel, username);

                var userToReturn = _mapper.Map<UserResponseModel>(user);

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
        /// Delete user information.
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully updated item information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUserInformation([FromRoute] string username)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(username);

                if (!result)
                    throw new StatusCodeException(message: "Error hit.", statusCode: StatusCodes.Status500InternalServerError);
                else
                    return NoContent();
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
