using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Applicaton.Web.API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/blog")]
	[Authorize]
	[ApiController]
	public class BlogController : ControllerBase
	{
		private readonly ILogger<BlogController> _logger;
		private readonly IMapper _mapper;
		private readonly IBlogService _blogService;
		private readonly IBlogCommentService _blogCommentService;
		private const string controllerPrefix = "Blog";
		private const int maxPageSize = 20;

		public BlogController(ILogger<BlogController> logger, IMapper mapper, IBlogService blogService, IBlogCommentService blogCommentService)
		{
			_logger = logger;
			_mapper = mapper;
			_blogService = blogService;
			_blogCommentService = blogCommentService;
		}

		/// <summary>
		/// Acquire blogs.
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BlogResponseModel>>> GetBlogsAsync([FromQuery] PaginationRequestModel pagination)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (blogs, paginationMetadata) = await _blogService.GetAllBlogsAsync(pagination);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

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
		[AllowAnonymous]
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
		/// Acquire related blogs based on blog information by identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]		
		[HttpGet("{id}/related")]
		public async Task<ActionResult<BlogResponseModel>> GetBlogRelatedAsync([FromRoute] Guid id)
		{
			try
			{
				var blog = await _blogService.GetRelatedBlogAsync(id);

				if (blog == null)
					return NotFound();

				var blogToReturn = _mapper.Map<List<BlogResponseModel>>(blog);

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
		/// Acquire blog reviews information by identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]
		[HttpGet("{blogId}/comment")]
		public async Task<ActionResult<IEnumerable<BlogCommentResponseModel>>> GetBlogReviewsAsync([FromQuery] PaginationRequestModel pagination, [FromRoute] Guid blogId)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (comments, paginationMetadata) = await _blogCommentService.GetAllBlogCommentByBlogIdAsync(pagination, blogId);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

				var commentsToReturn = _mapper.Map<IEnumerable<BlogCommentResponseModel>>(comments);

				return Ok(commentsToReturn);
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
		/// Update blog comment by blog specification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully created item.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpPost("{blogId}/comment")]
		public async Task<ActionResult<BlogCommentResponseModel>> CreateBlogReviewAsync([FromRoute] Guid blogId, [FromBody] BlogCommentRequestModel requestModel)
		{
			try
			{
				var review = await _blogCommentService.CreateBlogCommentAsync(requestModel, blogId);

				var reviewToReturn = _mapper.Map<BlogCommentResponseModel>(review);

				return Ok(reviewToReturn);
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
		[Authorize(Policy = "UserRight")]
		[HttpPut("comment/{commentId}")]
		public async Task<ActionResult<BlogResponseModel>> UpdateBlogCommentAsync([FromBody] BlogCommentRequestModel requestModel, [FromRoute] Guid commentId)
		{
			try
			{
				var comment = await _blogCommentService.UpdateBlogCommentAsync(requestModel, commentId);

				var reviewToReturn = _mapper.Map<BlogCommentResponseModel>(comment);

				return Ok(reviewToReturn);
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
		/// Delete comment by identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="204">Successfully deleted item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpDelete("comment/{commentId}")]
		public async Task<IActionResult> DeleteCommentAsync([FromRoute] Guid commentId)
		{
			try
			{
				var result = await _blogCommentService.DeleteBlogCommentAsync(commentId);

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


		/// <summary>
		/// Create a blog
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="201">Successfully created item.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "AdminRight")]
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
		[Authorize(Policy = "AdminRight")]
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
		[Authorize(Policy = "AdminRight")]
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
