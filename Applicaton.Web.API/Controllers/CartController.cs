using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Application.Web.Service.Services;
using Applicaton.Web.API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/cart")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ILogger<CartController> _logger;
		private readonly IMapper _mapper;
		private readonly ICartService _cartService;
		private const string controllerPrefix = "Cart";

		public CartController(ILogger<CartController> logger, IMapper mapper, ICartService cartService)
        {
            _logger = logger;
            _mapper = mapper;
            _cartService = cartService;
        }

		/// <summary>
		/// Acquire a cart by user identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("{userId}")]
		public async Task<ActionResult<CartResponseModel>> GetCartByUserIdAsync([FromRoute] Guid userId)
		{
			try
			{
				var cart = await _cartService.GetCartByUserIdAsync(userId);

				var cartToReturn = _mapper.Map<CartResponseModel>(cart);

				return Ok(cartToReturn);
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
		/// Add item into cart
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="201">Successfully created item.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpPost]
		public async Task<ActionResult<CartResponseModel>> CreateCartAsync([FromBody] CartRequestModel requestModel)
		{
			try
			{
				var cart = await _cartService.AddToCartByUserIdAsync(requestModel);

				var cartToReturn = _mapper.Map<CartResponseModel>(cart);

				return Ok(cartToReturn);
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
		/// Update cart vehicle information async
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="201">Successfully created item.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpPut]
		public async Task<ActionResult<CartResponseModel>> UpdateCartVehicleAsync([FromBody] CartRequestModel requestModel)
		{
			try
			{
				var cart = await _cartService.UpdateCartAsync(requestModel);

				var cartToReturn = _mapper.Map<CartResponseModel>(cart);

				return Ok(cartToReturn);
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
		/// Delete cart item by identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="204">Successfully deleted item information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpDelete("{UserId}/{VehicleId}")]
		public async Task<IActionResult> DeleteCartItemAsync([FromRoute] CartRequestModel requestModel)
		{
			try
			{
				var result = await _cartService.DeleteCartItemAsync(requestModel);

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
