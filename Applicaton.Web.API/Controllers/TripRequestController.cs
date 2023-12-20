using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Applicaton.Web.API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/order")]
	[Authorize]
	[ApiController]
	public class TripRequestController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<TripRequestController> _logger;
		private readonly IOrderService _orderService;

		private const string controllerPrefix = "TripRequest";
		private const int maxPageSize = 20;

		public TripRequestController(
			IMapper mapper,
			ILogger<TripRequestController> logger,
			IOrderService orderService
			)
		{
			_mapper = mapper;
			_logger = logger;

			_orderService = orderService;
		}

		/// <summary>
		/// Acquire orders information by parentOrderId
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpGet("parent/{parentOrderId}")]
		public async Task<ActionResult<TripRequestReponseModel>> GetTripRequestByParentOrderId([FromQuery] TripRequestQuery query, [FromRoute] string parentOrderId)
		{
			try
			{
				var tripRequests = await _orderService.GetAllTripRequestsByParentOrderId(parentOrderId, query);

				var tripRequestsToReturn = _mapper.Map<IEnumerable<TripRequest>, TripRequestReponseModel>(tripRequests);

				return Ok(tripRequestsToReturn);
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
		/// Acquire orders information by parentOrderId
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpGet("payment/{paymentIntentId}")]
		public async Task<ActionResult<TripRequestReponseModel>> GetTripRequestByPaymentIntentId([FromQuery] TripRequestQuery query, [FromRoute] string paymentIntentId)
		{
			try
			{
				var tripRequests = await _orderService.GetAllTripRequestsByPaymentIntentId(paymentIntentId, query);

				var tripRequestsToReturn = _mapper.Map<IEnumerable<TripRequest>, TripRequestReponseModel>(tripRequests);

				return Ok(tripRequestsToReturn);
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
		/// Acquire orders information with pagination
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpGet("")]
		public async Task<ActionResult<IEnumerable<TripRequestReponseModel>>> GetAllTripRequest([FromQuery] TripRequestQuery query, [FromQuery] PaginationRequestModel pagination)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (tripRequests, paginationMetadata) = await _orderService.GetAllTripRequests(pagination, query);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

				var tripRequestsToReturn = _mapper.Map<IEnumerable<IEnumerable<TripRequest>>, IEnumerable<TripRequestReponseModel>>(tripRequests);

				return Ok(tripRequestsToReturn.OrderByDescending(x => x.CreatedAt).ToList());
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
		/// Acquire orders information with pagination by lessorId
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpGet("lessor/{lessorId}")]
		public async Task<ActionResult<IEnumerable<TripRequestReponseModel>>> GetTripRequestByLessorId([FromQuery] TripRequestQuery query, [FromQuery] PaginationRequestModel pagination, [FromRoute] Guid lessorId)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (tripRequests, paginationMetadata) = await _orderService.GetTripRequestsByLessorIdAsync(pagination, lessorId, query);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

				var tripRequestsToReturn = _mapper.Map<IEnumerable<IEnumerable<TripRequest>>, IEnumerable<TripRequestReponseModel>>(tripRequests);

				return Ok(tripRequestsToReturn.OrderByDescending(x => x.CreatedAt).ToList());
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
		/// Acquire orders information with pagination by lesseeId
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpGet("lessee/{lesseeId}")]
		public async Task<ActionResult<IEnumerable<TripRequestReponseModel>>> GetTripRequestLesseeId([FromQuery] TripRequestQuery query, [FromQuery] PaginationRequestModel pagination, [FromRoute] Guid lesseeId)
		{
			try
			{
				var (tripRequests, paginationMetadata) = await _orderService.GetTripRequestsByLesseeIdAsync(pagination, lesseeId, query);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

				var tripRequestsToReturn = _mapper.Map<IEnumerable<IEnumerable<TripRequest>>, IEnumerable<TripRequestReponseModel>>(tripRequests);

				return Ok(tripRequestsToReturn.OrderByDescending(x => x.CreatedAt).ToList());
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
		/// Update per request status of Trip requests. Only allow status field to: "Approved", "Canceled", "Complete"
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpPut("status")]
		public async Task<ActionResult<IEnumerable<TripRequestReponseModel>>> UpdateTripRequestStatusAsync([FromBody] TripRequestStatusRequestModel requestModel)
		{
			try
			{
				var tripRequests = await _orderService.UpdateTripRequestStatusAsync(requestModel);

				var tripRequestsToReturn = _mapper.Map<IEnumerable<TripRequest>, TripRequestReponseModel>(tripRequests);

				return Ok(tripRequestsToReturn);
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
		/// Add review to trip request per vehicle
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
		[HttpPost("review")]
		public async Task<ActionResult<IEnumerable<TripRequestReponseModel>>> CreateReviewToTripRequest([FromBody] TripRequestReviewRequestModel requestModel)
		{
			try
			{
				var result = await _orderService.CreateNewReviewTripRequestAsync(requestModel);

				return result ? NoContent() : BadRequest();
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
