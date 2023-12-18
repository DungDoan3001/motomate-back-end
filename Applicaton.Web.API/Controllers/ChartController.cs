using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.DTOs.ResponseModels.ChartResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/chart")]
	[ApiController]
	public class ChartController : ControllerBase
	{
		private readonly ILogger<ChartController> _logger;
		private readonly IAppCache _cache;
		private CacheKeyConstants _cacheKeyConstants;
		private readonly IChartService _chartService;
		private const string controllerPrefix = "Chart";

		public ChartController(ILogger<ChartController> logger, IAppCache cache, CacheKeyConstants cacheKeyConstants, IChartService chartService)
		{
			_logger = logger;
			_cache = cache;
			_cacheKeyConstants = cacheKeyConstants;
			_chartService = chartService;
		}

		/// <summary>
		/// Get total vehicle chart
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("total/vehicles")]
		public async Task<ActionResult<TotalVehicleResponseModel>> GetTotalVehiclesAsync()
		{
			try
			{
				var key = $"chart-total-vehicle";

				var result = await _cache.GetOrAddAsync(
					key,
					async () => await _chartService.GetTotalVehiclesAsync(),
					TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

				_cacheKeyConstants.AddKeyToList(key);

				return Ok(result);
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
		/// Get total users chart
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("total/users")]
		public async Task<ActionResult<TotalUserResponseModel>> GetTotalUsersAsync()
		{
			try
			{
				var key = $"chart-total-users";

				var result = await _cache.GetOrAddAsync(
					key,
					async () => await _chartService.GetTotalUsersAsync(),
					TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

				_cacheKeyConstants.AddKeyToList(key);

				return Ok(result);
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
		/// Get total profits chart
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("total/profits")]
		public async Task<ActionResult<TotalUserResponseModel>> GetTotalProfitsAsync()
		{
			try
			{
				var key = $"chart-total-profits";

				var result = await _cache.GetOrAddAsync(
					key,
					async () => await _chartService.GetTotalProfitsAsync(),
					TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

				_cacheKeyConstants.AddKeyToList(key);

				return Ok(result);
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
		/// Get top lessees
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("top/lessees")]
		public async Task<ActionResult<TopLesseeResponseModel>> GetTopLesseesAsync()
		{
			try
			{
				var key = $"chart-top-lessees";

				var result = await _cache.GetOrAddAsync(
								key,
								async () => await _chartService.GetTopLesseesAsync(),
								TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

				_cacheKeyConstants.AddKeyToList(key);

				return Ok(result);
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
		/// Get top lessors
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("top/lessors")]
		public async Task<ActionResult<TopLessorResponseModel>> GetTopLessorsAsync()
		{
			try
			{
				var key = $"chart-top-lessors";

				var result = await _cache.GetOrAddAsync(
								key,
								async () => await _chartService.GetTopLessorsAsync(),
								TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

				_cacheKeyConstants.AddKeyToList(key);

				return Ok(result);
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
		/// Get revenue in a year
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("revenue/{year}")]
		public async Task<ActionResult<TotalRevenueChartResponseModel>> GetTotalRevenueAsync([FromRoute] int year)
		{
			try
			{
				var totalRevenueKey = $"chart-total-revenue-{year}";
				var totalCompletedTripRequestKey = $"chart-total-complete-trip-requested-{year}";

				var totalRevenue = await _cache.GetOrAddAsync(
								totalRevenueKey,
								async () => await _chartService.GetTotalRevenueAsync(year),
								TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

				var totalCompletedTripRequest = await _cache.GetOrAddAsync(
								totalCompletedTripRequestKey,
								async () => await _chartService.GetTotalCompletedTripRequestAsync(year),
								TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

				_cacheKeyConstants.AddKeyToList(totalRevenueKey);
				_cacheKeyConstants.AddKeyToList(totalCompletedTripRequestKey);

				return Ok(new TotalRevenueChartResponseModel
				{
					TotalRevenue = totalRevenue,
					TotalRentedAndCompletedVehicles = totalCompletedTripRequest,
				});
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
