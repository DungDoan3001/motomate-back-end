using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Application.Web.Service.Services
{
	public class CartService : ICartService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<Cart> _cartRepo;
		private readonly IGenericRepository<CartVehicle> _cartVehicleRepo;
		private readonly ICartQueries _cartQueries;
		private readonly IUserQueries _userQueries;
		private readonly IVehicleQueries _vehicleQueries;

		public CartService(IMapper mapper, IUnitOfWork unitOfWork, ICartQueries cartQueries, IUserQueries userQueries, IVehicleQueries vehicleQueries)
		{
			_mapper = mapper;
            _unitOfWork = unitOfWork;
			_cartRepo = unitOfWork.GetBaseRepo<Cart>();
			_cartVehicleRepo = unitOfWork.GetBaseRepo<CartVehicle>();
            _cartQueries = cartQueries;
			_userQueries = userQueries;
			_vehicleQueries = vehicleQueries;

		}

		public async Task<Cart> GetCartByUserIdAsync(Guid userId)
		{
			return await _cartQueries.GetCartByUserIdAsync(userId);
		}

		public async Task<Cart> AddToCartByUserIdAsync(CartRequestModel requestModel)
		{
			await CheckRequestModel(requestModel);

			var cartId = await _cartQueries.GetCartIdByUserIdAsync(requestModel.UserId);

			if (cartId.Equals(Guid.Empty))
			{
				await HandleNewCartAsync(requestModel);
			}
			else
			{
				var isVehicleInCart = await _cartQueries.CheckIfVehicleExistedInCart(cartId, requestModel.VehicleId);

				if (isVehicleInCart)
				{
					throw new StatusCodeException(message: "Vehicle already in cart.", statusCode: StatusCodes.Status409Conflict);
				}

				await HandleOldCartAsync(requestModel, cartId);
			}

			return await _cartQueries.GetCartByUserIdAsync(requestModel.UserId);
		}

		public async Task<Cart> UpdateCartAsync(CartRequestModel requestModel)
		{
			await CheckRequestModel(requestModel);

			var cartId = await _cartQueries.GetCartIdByUserIdAsync(requestModel.UserId);

			if (cartId.Equals(Guid.Empty))
			{
				throw new StatusCodeException(message: "Cart not found.", statusCode: StatusCodes.Status404NotFound);
			}
			else
			{
				var isVehicleInCart = await _cartQueries.CheckIfVehicleExistedInCart(cartId, requestModel.VehicleId);

				if (!isVehicleInCart)
				{
					throw new StatusCodeException(message: "Vehicle not found in cart.", statusCode: StatusCodes.Status404NotFound);
				}

				var cartVehicle = await _cartVehicleRepo.FindOne(x => x.CartId.Equals(cartId) && x.VehicleId.Equals(requestModel.VehicleId));

				if(requestModel.PickUpDateTime.HasValue && requestModel.DropOffDatetime.HasValue)
				{
					cartVehicle.PickUpDateTime = DateTime.SpecifyKind(requestModel.PickUpDateTime.Value, DateTimeKind.Utc);
					cartVehicle.DropOffDateTime = DateTime.SpecifyKind(requestModel.DropOffDatetime.Value, DateTimeKind.Utc);
				} else
				{
					cartVehicle.PickUpDateTime = null;
					cartVehicle.DropOffDateTime = null;
				}
				

				_cartVehicleRepo.Update(cartVehicle);

				await _unitOfWork.CompleteAsync();

				_unitOfWork.Detach(cartVehicle);
			}

			return await _cartQueries.GetCartByUserIdAsync(requestModel.UserId);
		}

		private async Task CheckRequestModel(CartRequestModel requestModel)
		{
			var isValidUser = await _userQueries.CheckIfUserExisted(requestModel.UserId);

			if (!isValidUser)
			{
				throw new StatusCodeException(message: "Invalid User", statusCode: StatusCodes.Status400BadRequest);
			}

			var isValidVehicle = await _vehicleQueries.CheckIfVehicleExisted(requestModel.VehicleId);

			if (!isValidVehicle)
			{
				throw new StatusCodeException(message: "Invalid Vehicle", statusCode: StatusCodes.Status400BadRequest);
			}

			if(requestModel.PickUpDateTime != null && requestModel.PickUpDateTime < DateTime.UtcNow)
			{
				throw new StatusCodeException(message: "Invalid Datetime input", statusCode: StatusCodes.Status400BadRequest);
			}

			if (requestModel.DropOffDatetime != null && requestModel.DropOffDatetime < DateTime.UtcNow)
			{
				throw new StatusCodeException(message: "Invalid Datetime input", statusCode: StatusCodes.Status400BadRequest);
			} 
			else if (requestModel.PickUpDateTime != null && requestModel.PickUpDateTime > DateTime.UtcNow && requestModel.PickUpDateTime > requestModel.DropOffDatetime)
			{
				throw new StatusCodeException(message: "Pickup datetime can not larger than drop off datetime.", statusCode: StatusCodes.Status400BadRequest);
			}
		}

		private async Task HandleOldCartAsync(CartRequestModel requestModel, Guid cartId)
		{
			var newCartVehicle = new CartVehicle
			{
				CartId = cartId,
				VehicleId = requestModel.VehicleId,
				PickUpDateTime = null,
				DropOffDateTime = null,
			};

			_cartVehicleRepo.Add(newCartVehicle);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(newCartVehicle);
		}

		private async Task HandleNewCartAsync(CartRequestModel requestModel)
		{
			var newCart = new Cart
			{
				UserId = requestModel.UserId,
			};

			var cartVehicle = new CartVehicle
			{
				CartId = newCart.Id,
				VehicleId = requestModel.VehicleId,
				PickUpDateTime = null,
				DropOffDateTime = null,
			};

			_cartRepo.Add(newCart);

			_cartVehicleRepo.Add(cartVehicle);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(newCart);
			_unitOfWork.Detach(cartVehicle);
		}

		public async Task<bool> DeleteCartItemAsync(CartRequestModel requestModel)
		{
			await CheckRequestModel(requestModel);

			var cartId = await _cartQueries.GetCartIdByUserIdAsync(requestModel.UserId);

			if(cartId.Equals(Guid.Empty))
			{
				throw new StatusCodeException(message: "Cart not found.", statusCode: StatusCodes.Status404NotFound);
			} else
			{
				var isVehicleInCart = await _cartQueries.CheckIfVehicleExistedInCart(cartId, requestModel.VehicleId);

				if(!isVehicleInCart)
				{
					throw new StatusCodeException(message: "Vehicle not found in cart.", statusCode: StatusCodes.Status404NotFound);
				}

				var item = await _cartVehicleRepo.FindOne(x => x.VehicleId.Equals(requestModel.VehicleId) && x.CartId.Equals(cartId));

				_cartVehicleRepo.DeleteByEntity(item);

				await _unitOfWork.CompleteAsync();

				_unitOfWork.Detach(item);
			}

			return true;
		}
	}
}
