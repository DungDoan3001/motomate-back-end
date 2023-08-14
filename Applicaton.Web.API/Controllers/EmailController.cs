using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IEmailService _emailService;
        private readonly string controllerPrefix = "Email";

        public EmailController(ILogger<AuthController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        /// <summary>
        /// Send email to user.
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully sent the email.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet("send")]
        public async Task<IActionResult> SendEmailAsync([FromBody] SendEmailOptions emailOptions)
        {
            try
            {
                bool result = await _emailService.SendEmailAsync(emailOptions);
                return result ? Ok() : BadRequest();
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
        /// Send bulk email to users.
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully sent the emails.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpPost("send/bulk")]
        public async Task<IActionResult> SendBulkEmailAsync([FromBody] SendBulkEmailOptions bulkEmailOptions)
        {
            try
            {
                bool result = await _emailService.SendBulkBccEmailAsync(bulkEmailOptions);
                return result ? Ok() : BadRequest();
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
