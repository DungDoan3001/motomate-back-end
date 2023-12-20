using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.DTOs.ServiceModels;
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
	[Route("api/vehicle")]
	[Authorize]
	[ApiController]
	public class VehicleController : ControllerBase
	{
		private readonly ILogger<VehicleController> _logger;
		private readonly IMapper _mapper;
		private readonly IVehicleService _vehicleService;
		private const string controllerPrefix = "Product";
		private const int maxPageSize = 20;

		public VehicleController(ILogger<VehicleController> logger, IMapper mapper, IVehicleService vehicleService)
		{
			_logger = logger;
			_mapper = mapper;
			_vehicleService = vehicleService;
		}

		/// <summary>
		/// Acquire vehicle reviews information with pagination
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]
		[HttpGet("review/{vehicleId}")]
		public async Task<ActionResult<IEnumerable<VehicleReviewResponseModel>>> GetVehicleReviewsByVehicleId([FromQuery] PaginationRequestModel pagination, [FromRoute] Guid vehicleId)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (reviews, paginationMetadata) = await _vehicleService.GetVehicleReviewByVehicleIdAsync(pagination, vehicleId);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

				var reviewsToReturn = _mapper.Map<IEnumerable<VehicleReviewResponseModel>>(reviews);

				return Ok(reviewsToReturn);
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
		/// Acquire vehicles information with pagination
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<VehicleResponseModel>>> GetVehiclesByStatusAsync([FromQuery] PaginationRequestModel pagination, [FromQuery] VehicleQuery vehicleQuery)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (vehicles, paginationMetadata) = await _vehicleService.GetVehiclesAsync(pagination, vehicleQuery);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

				var vehiclesToReturn = _mapper.Map<IEnumerable<VehicleResponseModel>>(vehicles);

				return Ok(vehiclesToReturn);
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
		/// Acquire all vehicles information
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]
		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<VehicleResponseModel>>> GetAllVehiclesAsync([FromQuery] VehicleQuery vehicleQuery)
		{
			try
			{
				var vehicles = await _vehicleService.GetAllVehiclesAsync(vehicleQuery);

				var vehiclesToReturn = _mapper.Map<IEnumerable<VehicleResponseModel>>(vehicles);

				return Ok(vehiclesToReturn);
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
		/// Acquire all related information
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]
		[HttpGet("related/{vehicleId}")]
		public async Task<ActionResult<IEnumerable<VehicleResponseModel>>> GetRelatedVehicleAsync(Guid vehicleId)
		{
			try
			{
				var vehicles = await _vehicleService.GetRelatedVehicleAsync(vehicleId);

				var vehiclesToReturn = _mapper.Map<IEnumerable<VehicleResponseModel>>(vehicles);

				return Ok(vehiclesToReturn);
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
		/// Acquire all vehicles by owner indentification.
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]
		[HttpGet("owner/{ownerId}")]
		public async Task<ActionResult<IEnumerable<VehicleResponseModel>>> GetAllVehiclesByOwnerIdAsync([FromQuery] PaginationRequestModel pagination, [FromQuery] VehicleQuery vehicleQuery, [FromRoute] Guid ownerId)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (vehicles, paginationMetadata) = await _vehicleService.GetAllVehiclesByOwnerIdAsync(pagination, vehicleQuery, ownerId);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

				var vehiclesToReturn = _mapper.Map<IEnumerable<VehicleResponseModel>>(vehicles);

				return Ok(vehiclesToReturn);
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
		/// Acquire vehicles per status information with pagination. valid input are "pending", "approved", "denied"
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]
		[HttpGet("status/{statusRoute}")]
		public async Task<ActionResult<IEnumerable<VehicleResponseModel>>> GetVehiclesAsync([FromQuery] PaginationRequestModel pagination, [FromQuery] VehicleQuery vehicleQuery, [FromRoute] string statusRoute)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (vehicles, paginationMetadata) = await _vehicleService.GetVehiclesByStatusAsync(pagination, vehicleQuery, statusRoute);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

				var vehiclesToReturn = _mapper.Map<IEnumerable<VehicleResponseModel>>(vehicles);

				return Ok(vehiclesToReturn);
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
		/// Lock and unlock vehicle by vehicle indentification.
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpPost("{vehicleId}/lock")]
		public async Task<IActionResult> LockVehicleAsync([FromRoute] Guid vehicleId)
		{
			try
			{
				var (result, isVehicleLocked) = await _vehicleService.HandleLockVehicleAsync(vehicleId);

				return result ? Ok(new
				{
					Id = vehicleId,
					IsLocked = isVehicleLocked
				}) : throw new StatusCodeException("Error!", StatusCodes.Status500InternalServerError);
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
		/// Update vehicle status.
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpPost("{vehicleId}/status/{statusNumber}")]
		public async Task<ActionResult<VehicleResponseModel>> UpdateVehicleStatusAsync([FromRoute] Guid vehicleId, [FromRoute] int statusNumber)
		{
			try
			{
				if (!Constants.validStatusNumbers.Contains(statusNumber))
				{
					return BadRequest("Invalid status number");
				}

				var vehicle = await _vehicleService.UpdateVehicleStatusAsync(vehicleId, statusNumber);

				var vehicleToReturn = _mapper.Map<VehicleResponseModel>(vehicle);

				return Ok(vehicleToReturn);
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
		/// Acquire vehicle information by identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<ActionResult<VehicleResponseModel>> GetVehicleAsync([FromRoute] Guid id)
		{
			try
			{
				var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

				if (vehicle == null)
					return NotFound();

				var vehicleToReturn = _mapper.Map<VehicleResponseModel>(vehicle);

				return Ok(vehicleToReturn);
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
		/// Create a vehicle
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="201">Successfully created item.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpPost]
		public async Task<ActionResult<VehicleResponseModel>> CreateVehicleAsync([FromBody] VehicleRequestModel requestModel)
		{
			try
			{
				VerifyRequestModel(requestModel);

				var vehicle = await _vehicleService.CreateVehicleAsync(requestModel);

				var vehicleToReturn = _mapper.Map<VehicleResponseModel>(vehicle);

				return Created($"vehicle/{vehicleToReturn.Id}", vehicleToReturn);
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
		/// Update vehicle information by identification.
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully updated item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpPut("{id}")]
		public async Task<ActionResult<VehicleResponseModel>> UpdateVehicleAsync([FromBody] VehicleRequestModel requestModel, [FromRoute] Guid id)
		{
			try
			{
				VerifyRequestModel(requestModel);

				var vehicle = await _vehicleService.UpdateVehicleAsync(requestModel, id);

				var vehicleToReturn = _mapper.Map<VehicleResponseModel>(vehicle);

				return Ok(vehicleToReturn);
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
		/// Delete vehicle by identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="204">Successfully deleted item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteVehicleAsync([FromRoute] Guid id)
		{
			try
			{
				var result = await _vehicleService.DeleteVehicleAsync(id);

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

		private void VerifyRequestModel(VehicleRequestModel requestModel)
		{
			if (requestModel.LicensePlate.IsNullOrEmpty())
				throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

			if (requestModel.InsuranceNumber.IsNullOrEmpty())
				throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);
		}
	}
}
