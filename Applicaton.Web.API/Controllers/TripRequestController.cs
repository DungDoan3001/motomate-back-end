﻿using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/order")]
	[ApiController]
	public class TripRequestController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<TripRequestController> _logger;
		private readonly IOrderService _orderService;

		private const string controllerPrefix = "TripRequest";

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
		/// Acquire orders information with pagination by parentOrderId
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("parent/{parentOrderId}")]
		public async Task<ActionResult<IEnumerable<TripRequestReponseModel>>> GetTripRequestByParentOrderId([FromQuery] TripRequestQuery query, [FromRoute] string parentOrderId)
		{
			try
			{
				var tripRequests = await _orderService.GetAllTripRequestsByParentOrderId(parentOrderId, query.LessorUsername);

				var tripRequestsToReturn = _mapper.Map<List<TripRequest>, TripRequestReponseModel>(tripRequests);

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
		/// Acquire orders information with pagination by lessorId
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("lessor/{lessorId}")]
		public async Task<ActionResult<IEnumerable<TripRequestReponseModel>>> GetTripRequestByLessorId([FromRoute] Guid lessorId)
		{
			try
			{
				var tripRequests = await _orderService.GetTripRequestsByLessorIdAsync(lessorId);

				var tripRequestsToReturn = _mapper.Map<List<List<TripRequest>>, TripRequestReponseModel>(tripRequests);

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
		/// Acquire orders information with pagination by lesseeId
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("lessee/{lesseeId}")]
		public async Task<ActionResult<IEnumerable<TripRequestReponseModel>>> GetTripRequestLesseeId([FromRoute] Guid lesseeId)
		{
			try
			{
				var tripRequests = await _orderService.GetTripRequestsByLesseeIdAsync(lesseeId);

				var tripRequestsToReturn = _mapper.Map<List<List<TripRequest>>, TripRequestReponseModel>(tripRequests);

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
		/// Update per request status of Trip requests. Only allow status field to: "Approved", "Canceled", "Complete"
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpPut("status")]
		public async Task<ActionResult<IEnumerable<TripRequestReponseModel>>> UpdateTripRequestStatusAsync([FromBody] TripRequestStatusRequestModel requestModel)
		{
			try
			{
				var tripRequests = await _orderService.UpdateTripRequestStatusAsync(requestModel);

				var tripRequestsToReturn = _mapper.Map<List<TripRequest>, TripRequestReponseModel>(tripRequests);

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

		[HttpPost("test/{parentOrderId}")]
		public async Task<IActionResult> TestEndpoint([FromRoute] string parentOrderId)
		{
			try
			{
				var request = await _orderService.GetTripRequestsByLessorIdAsync(Guid.Parse(parentOrderId));

				var tripRequestsToReturn = _mapper.Map<List<List<TripRequest>>, List<TripRequestReponseModel>>(request);

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
				//return StatusCode(StatusCodes.Status409Conflict, new ErrorResponseModel
				//{
				//	Message = "Error while performing action.",
				//	StatusCode = StatusCodes.Status500InternalServerError,
				//	Errors = { ex.Message, ex.InnerException.Message }
				//});

				return Ok(ex.Message);
			}
		}
	}
}
