using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Application.Web.Service.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("send")]
        public async Task<IActionResult> SendEmailAsync([FromQuery] string toEmail, [FromQuery] string subject)
        {
            try
            {
                bool result = await _emailService.SendEmailAsync("Name", toEmail, subject, "<h1>Xin chào từ motormate</h1>");
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

        [HttpPost("send/bulk")]
        public async Task<IActionResult> SendEmailAsync([FromBody] List<BulkEmailSendingRequestModel> emailSenders, [FromQuery] string subject, [FromQuery] string body)
        {
            try
            {
                bool result = await _emailService.SendBulkBccEmailAsync(emailSenders, subject, body);
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
