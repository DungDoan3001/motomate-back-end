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
	[Route("api/vehicle")]
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
        /// Acquire vehicles information with pagination
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get items information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet]
        public async Task<IActionResult> GetVehiclesAsync([FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                if (pagination.pageSize > maxPageSize)
                {
                    pagination.pageSize = maxPageSize;
                }

                var (vehicles, paginationMetadata) = await _vehicleService.GetVehiclesAsync(pagination);

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
        [HttpGet("all")]
        public async Task<IActionResult> GetAllVehiclesAsync()
        {
            try
            {
                var vehicles = await _vehicleService.GetAllVehicleAsync();

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
        /// Acquire vehicle information by identification
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get item information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleAsync([FromRoute] Guid id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

                if (vehicle == null)
                    return NotFound();

                var vehicleToReturn = _mapper.Map<BrandResponseModel>(vehicle);

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
		[HttpPost]
		public async Task<IActionResult> CreateVehicleAsync([FromBody] VehicleRequestModel requestModel)
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

        private void VerifyRequestModel(VehicleRequestModel requestModel)
        {
			if (requestModel.LicensePlate.IsNullOrEmpty())
				throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

			if (requestModel.InsuranceNumber.IsNullOrEmpty())
				throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);
		}
	}
}
