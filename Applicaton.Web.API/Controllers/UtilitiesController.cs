using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using LazyCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/utils")]
	[Authorize]
	[ApiController]
	public class UtilitiesController : ControllerBase
	{
		private readonly ILogger<UtilitiesController> _logger;
		private readonly IAppCache _cache;
		private readonly IUtilitiesService _utilitiesService;
		private CacheKeyConstants _cacheKeyConstants;
		private const string controllerPrefix = "Utilities";

		public UtilitiesController(ILogger<UtilitiesController> logger, IUtilitiesService utilitiesService, CacheKeyConstants cacheKeyConstants, IAppCache cache)
		{
			_logger = logger;
			_cache = cache;
			_utilitiesService = utilitiesService;
			_cacheKeyConstants = cacheKeyConstants;
		}

		[AllowAnonymous]
		[HttpGet("cronjob")]
		public async Task<IActionResult> CronJobActivator()
		{
			var result = await _utilitiesService.CronJobActivator();

			return Ok(result);
		}

		[AllowAnonymous]
		[HttpGet("cache/clear")]
		public async Task<IActionResult> ClearCacheAsync()
		{
			await Task.Run(() =>
			{
				foreach (var key in _cacheKeyConstants.CacheKeyList)
				{
					_cache.Remove(key);
				}

				_cacheKeyConstants.CacheKeyList = new List<string>();
			});

			return Ok();
		}

		/// <summary>
		/// Add View
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully created item.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]
		[HttpPost("view")]
		public async Task<ActionResult<ViewResponseModel>> AddViewAsync([FromBody] ViewRequestModel requestModel)
		{
			try
			{
				var result = await _utilitiesService.AddViewAsync(requestModel);

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
	}
}
