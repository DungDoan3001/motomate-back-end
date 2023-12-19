using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Applicaton.Web.API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/brand")]
	[ApiController]
	public class BrandController : ControllerBase
	{
		private readonly IBrandService _brandService;
		private readonly ILogger<BrandController> _logger;
		private readonly IMapper _mapper;
		private const string controllerPrefix = "Brand";
		private const int maxPageSize = 20;

		public BrandController(ILogger<BrandController> logger, IMapper mapper, IBrandService brandService)
		{
			_brandService = brandService;
			_logger = logger;
			_mapper = mapper;
		}

		/// <summary>
		/// Acquire brands information with pagination
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BrandResponseModel>>> GetBrandsAsync([FromQuery] PaginationRequestModel pagination)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (brands, paginationMetadata) = await _brandService.GetBrandsAsync(pagination);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

				var brandsToReturn = _mapper.Map<IEnumerable<BrandResponseModel>>(brands);

				return Ok(brandsToReturn);
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
		/// Acquire all brands information
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<BrandResponseModel>>> GetAllBrandsAsync()
		{
			try
			{
				var brands = await _brandService.GetAllBrandsAsync();

				var brandsToReturn = _mapper.Map<IEnumerable<BrandResponseModel>>(brands);

				return Ok(brandsToReturn);
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
		/// Acquire brand information by identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("{id}")]
		public async Task<ActionResult<BrandResponseModel>> GetBrandAsync([FromRoute] Guid id)
		{
			try
			{
				var brand = await _brandService.GetBrandByIdAsync(id);

				if (brand == null)
					return NotFound();

				var brandToReturn = _mapper.Map<BrandResponseModel>(brand);

				return Ok(brandToReturn);
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
		/// Create a brand
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="201">Successfully created item.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpPost]
		public async Task<ActionResult<BrandResponseModel>> CreateBrandAsync([FromBody] BrandRequestModel requestModel)
		{
			try
			{
				if (requestModel.Name.IsNullOrEmpty())
					throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

				var brand = await _brandService.CreateBrandAsync(requestModel);

				var brandToReturn = _mapper.Map<BrandResponseModel>(brand);

				return Created($"brand/{brandToReturn.Id}", brandToReturn);
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
		/// Update brand information by identification.
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully updated item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpPut("{id}")]
		public async Task<ActionResult<BrandResponseModel>> UpdateBrandAsync([FromBody] BrandRequestModel requestModel, [FromRoute] Guid id)
		{
			try
			{
				if (requestModel.Name.IsNullOrEmpty())
					throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

				var brand = await _brandService.UpdateBrandAsync(requestModel, id);

				var brandToReturn = _mapper.Map<BrandResponseModel>(brand);

				return Ok(brandToReturn);
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
		/// Delete brand by identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="204">Successfully deleted item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBranchAsync([FromRoute] Guid id)
		{
			try
			{
				var result = await _brandService.DeleteBrandAsync(id);

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
