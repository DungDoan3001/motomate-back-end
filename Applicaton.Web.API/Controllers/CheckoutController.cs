using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.Models;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Application.Web.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/order/checkout")]
	[ApiController]
	public class CheckoutController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;
		private readonly ILogger<CheckoutController> _logger;
		private readonly ICheckoutService _checkoutService;
		private readonly IOrderService _orderService;
		private static string controllerPrefix = "Checkout";

		public CheckoutController(
			IConfiguration configuration,
			IMapper mapper, 
			ILogger<CheckoutController> logger, 
			ICheckoutService checkoutService,
			IOrderService orderService)
		{
			_configuration = configuration;
			_mapper = mapper;
			_logger = logger;
			_checkoutService = checkoutService;
			_orderService = orderService;
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

		[HttpPost("webhook")]
		public async Task<ActionResult> StripeWebhook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

			var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _configuration["StripeSettings:WhSecret"]);

			var tripRequests = await _orderService.CreateTripRequestsFromStripeEventAsync(stripeEvent);

			return new EmptyResult();
		}
    }
}
