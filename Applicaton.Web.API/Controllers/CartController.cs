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
		public async Task<IActionResult> GetCartByUserIdAsync([FromRoute] Guid userId)
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
    }
}
