using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Application.Web.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/blog")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		private readonly ILogger<BlogController> _logger;
		private readonly IMapper _mapper;
		private readonly IBlogService _blogService;
		private const string controllerPrefix = "Blog";
		private const int maxPageSize = 20;

		public BlogController(ILogger<BlogController> logger, IMapper mapper, IBlogService blogService)
		{
			_logger = logger;
			_mapper = mapper;
			_blogService = blogService;
		}

		/// <summary>
		/// Acquire blogs.
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BlogResponseModel>>> GetBlogsAsync()
		{
			try
			{
				var blogs = await _blogService.GetAllBlogsAsync();

				var blogToReturn = _mapper.Map<IEnumerable<BlogResponseModel>>(blogs);

				return Ok(blogToReturn);
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
		/// Acquire blog information by identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("{id}")]
		public async Task<ActionResult<BlogResponseModel>> GetBlogAsync([FromRoute] Guid id)
		{
			try
			{
				var blog = await _blogService.GetBlogByIdAsync(id);

				if (blog == null)
					return NotFound();

				var blogToReturn = _mapper.Map<BlogResponseModel>(blog);

				return Ok(blogToReturn);
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
		/// Create a blog
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="201">Successfully created item.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpPost]
		public async Task<ActionResult<BlogResponseModel>> CreateBlogAsync([FromBody] BlogRequestModel requestModel)
		{
			try
			{
				if (requestModel.Title.IsNullOrEmpty())
					throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

				if (requestModel.Content.IsNullOrEmpty())
					throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

				if (requestModel.ShortDescription.IsNullOrEmpty())
					throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

				var blog = await _blogService.CreateBlogAsync(requestModel);

				var blogToReturn = _mapper.Map<BlogResponseModel>(blog);

				return Created($"blog/{blogToReturn.Id}", blogToReturn);
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
		/// Update blog information by identification.
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully updated item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpPut("{id}")]
		public async Task<ActionResult<BlogResponseModel>> UpdateBlogAsync([FromBody] BlogRequestModel requestModel, [FromRoute] Guid id)
		{
			try
			{
				if (requestModel.Title.IsNullOrEmpty())
					throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

				if (requestModel.Content.IsNullOrEmpty())
					throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

				if (requestModel.ShortDescription.IsNullOrEmpty())
					throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

				var blog = await _blogService.UpdateBlogAsync(requestModel, id);

				var blogToReturn = _mapper.Map<BlogResponseModel>(blog);

				return Ok(blogToReturn);
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
		/// Delete blog by identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="204">Successfully deleted item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBlogAsync([FromRoute] Guid id)
		{
			try
			{
				var result = await _blogService.DeleteBlogAsync(id);

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
