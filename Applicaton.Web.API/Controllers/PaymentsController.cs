using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.Models;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;
using Application.Web.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/payments")]
	[ApiController]
	public class PaymentsController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<Cart> _cartRepo;
		private readonly IPaymentService _paymentService;
		private readonly ICartService _cartService;

		public PaymentsController(IMapper mapper, IUnitOfWork unitOfWork, IPaymentService paymentService, ICartService cartService)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_cartRepo = unitOfWork.GetBaseRepo<Cart>();
			_paymentService = paymentService;
			_cartService = cartService;
		}

		//[HttpPost]
		//public async Task<ActionResult<CartResponseModel>> CreateOrUpdatePaymentIntent(Guid userId)
		//{
		//	var cart = await _cartService.GetCartByUserIdAsync(userId);

		//	if (cart == null) return NotFound();

		//	var intent = await _paymentService.CreateOrUpdatePaymentIntent(cart);

		//	if(intent == null) return BadRequest();

		//	cart.PaymentIntentId = cart.PaymentIntentId ?? intent.Id;
		//	cart.ClientSecret = cart.ClientSecret ?? intent.ClientSecret;

		//	_cartRepo.Update(cart);

		//	await _unitOfWork.CompleteAsync();

		//	_unitOfWork.Detach(cart);

		//	return _mapper.Map<CartResponseModel>(cart);
		//}
	}
}
