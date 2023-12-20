using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/order/checkout")]
	[Authorize]
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


		/// <summary>
		/// Checkout order to create payment intent for client to pay
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[Authorize(Policy = "UserRight")]
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


		/// <summary>
		/// Endpoint for stripe api to call webhook to invoke create orders
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[AllowAnonymous]
		[HttpPost("webhook")]
		public async Task<ActionResult> StripeWebhook()
		{
			try
			{
				var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

				var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _configuration["StripeSettings:WhSecret"]);

				var tripRequests = await _orderService.CreateTripRequestsFromStripeEventAsync(stripeEvent);

				return new EmptyResult();
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