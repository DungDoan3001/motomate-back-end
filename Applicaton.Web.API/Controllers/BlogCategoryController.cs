using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Application.Web.Service.Services;
using Applicaton.Web.API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/blog/category")]
	[ApiController]
	public class BlogCategoryController : ControllerBase
	{
		private readonly ILogger<BlogCategoryController> _logger;
		private readonly IMapper _mapper;
		private readonly IBlogCategoryService _blogCategoryService;
		private const string controllerPrefix = "Brand";
		private const int maxPageSize = 20;

		public BlogCategoryController(ILogger<BlogCategoryController> logger, IMapper mapper, IBlogCategoryService blogCategoryService)
		{
			_logger = logger;
			_mapper = mapper;
			_blogCategoryService = blogCategoryService;
		}

		/// <summary>
		/// Acquire blog categories.
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BlogCategoryResponseModel>>> GetCategoriesAsync()
		{
			try
			{
				var categories = await _blogCategoryService.GetAllCategoryAsync();

				var categoriesToReturn = _mapper.Map<IEnumerable<BlogCategoryResponseModel>>(categories);

				return Ok(categoriesToReturn);
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
		/// Create a blog category
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="201">Successfully created item.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpPost]
		public async Task<ActionResult<BlogCategoryResponseModel>> CreateCategoryAsync([FromBody] BlogCategoryRequestModel requestModel)
		{
			try
			{
				if (requestModel.Name.IsNullOrEmpty())
					throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

				var category = await _blogCategoryService.CreateBlogCategoryAsync(requestModel);

				var categoryToReturn = _mapper.Map<BlogCategoryResponseModel>(category);

				return Created($"blogCategory/{categoryToReturn.Id}", categoryToReturn);
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
		/// Update blog category information by identification.
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully updated item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpPut("{id}")]
		public async Task<ActionResult<BlogCategoryResponseModel>> UpdateCategoryAsync([FromBody] BlogCategoryRequestModel requestModel, [FromRoute] Guid id)
		{
			try
			{
				if (requestModel.Name.IsNullOrEmpty())
					throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

				var category = await _blogCategoryService.UpdateCategoryAsync(requestModel, id);

				var categoryToReturn = _mapper.Map<BlogCategoryResponseModel>(category);

				return Ok(categoryToReturn);
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
		/// Delete blog category by identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="204">Successfully deleted item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid id)
		{
			try
			{
				var result = await _blogCategoryService.DeleteCategoryAsync(id);

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
