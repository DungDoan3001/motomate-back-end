using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.Models;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/order/checkout")]
	[ApiController]
	public class CheckoutController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<CheckoutController> _logger;
		private readonly ICheckoutService _checkoutService;
		private static string controllerPrefix = "Checkout";

		public CheckoutController(
			IMapper mapper, 
			ILogger<CheckoutController> logger, 
			ICheckoutService checkoutService)
		{
            _mapper = mapper;
			_logger = logger;
			_checkoutService = checkoutService;
		}

		[HttpPost]
		public async Task<ActionResult<CheckoutOrderResponseModel>> CreateOrUpdateCheckoutOrder([FromBody] CheckoutOrderRequestModel checkoutOrderRequest)
		{
			try
			{
				var order = await _checkoutService.CreateOrUpdateOrderAsync(checkoutOrderRequest);

				var orderToReturn = _mapper.Map<CheckoutOrderResponseModel>(order);

				return orderToReturn;
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
