using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Web.Service.Services
{
	public class CheckoutService : ICheckoutService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<CheckOutOrder> _checkoutOrderRepo;
		private readonly IGenericRepository<CheckOutOrderVehicle> _checkoutOrderVehicleRepo;

		private readonly ICheckoutOrderQueries _checkoutOrderQueries;
		private readonly IUserQueries _userQueries;
		private readonly IVehicleQueries _vehicleQueries;

		private readonly IPaymentService _paymentService;

		public CheckoutService(
			IUnitOfWork unitOfWork, 
			ICheckoutOrderQueries checkoutOrderQueries, 
			IUserQueries userQueries, 
			IVehicleQueries vehicleQueries,
			IPaymentService paymentService)
        {
			_unitOfWork = unitOfWork;
			_checkoutOrderRepo = unitOfWork.GetBaseRepo<CheckOutOrder>();
			_checkoutOrderVehicleRepo = unitOfWork.GetBaseRepo<CheckOutOrderVehicle>();

			_checkoutOrderQueries = checkoutOrderQueries;
			_userQueries = userQueries;
			_vehicleQueries = vehicleQueries;

			_paymentService = paymentService;
		}

		public async Task<CheckOutOrder> CreateOrUpdateOrderAsync(CheckoutOrderRequestModel checkoutOrder)
		{
			var order = await _checkoutOrderQueries.GetCheckOutOrderByUserIdAsync(checkoutOrder.UserId);

			if(order == null)
			{
				order = await HandleCreateNewCheckoutOrder(checkoutOrder, order);
			}
			else
			{
				await HandleUpdateCheckoutOrder(checkoutOrder, order);
			}

			_unitOfWork.Detach(order);

			var latestOrder = await _checkoutOrderQueries.GetCheckOutOrderByUserIdAsync(order.UserId);

			var paymentIntent = await _paymentService.CreateOrUpdatePaymentIntent(latestOrder);

			latestOrder.PaymentIntentId = paymentIntent.Id ?? latestOrder.PaymentIntentId;
			latestOrder.ClientSecret = paymentIntent.ClientSecret ?? latestOrder.ClientSecret;

			_checkoutOrderRepo.Update(latestOrder);

			await _unitOfWork.CompleteAsync();

			return latestOrder;
		}

		private async Task HandleUpdateCheckoutOrder(CheckoutOrderRequestModel checkoutOrder, CheckOutOrder order)
		{
			_checkoutOrderVehicleRepo.DeleteRange(order.CheckOutOrderVehicles);

			order.CheckOutOrderVehicles = new List<CheckOutOrderVehicle>();

			foreach (var vehicleData in checkoutOrder.Vehicles)
			{
				await validateInputCheckoutVehicle(vehicleData);

				order.CheckOutOrderVehicles.Add(new CheckOutOrderVehicle
				{
					VehicleId = vehicleData.VehicleId,
					PickUpDateTime = vehicleData.PickUpDateTime,
					DropOffDateTime = vehicleData.DropOffDateTime,
					PickUpLocation = vehicleData.PickUpLocation,
					DropOffLocation = vehicleData.DropOffLocation,
					CheckoutId = order.Id,
				});
			}

			_checkoutOrderVehicleRepo.AddRange(order.CheckOutOrderVehicles);

			await _unitOfWork.CompleteAsync();
		}

		private async Task<CheckOutOrder> HandleCreateNewCheckoutOrder(CheckoutOrderRequestModel checkoutOrder, CheckOutOrder order)
		{
			var isUserExist = await _userQueries.CheckIfUserExisted(checkoutOrder.UserId);
			if (!isUserExist)
				throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

			order = new CheckOutOrder
			{
				UserId = checkoutOrder.UserId,
				CheckOutOrderVehicles = new List<CheckOutOrderVehicle>()
			};

			foreach (var vehicleData in checkoutOrder.Vehicles)
			{
				await validateInputCheckoutVehicle(vehicleData);

				order.CheckOutOrderVehicles.Add(new CheckOutOrderVehicle
				{
					VehicleId = vehicleData.VehicleId,
					PickUpDateTime = vehicleData.PickUpDateTime,
					DropOffDateTime = vehicleData.DropOffDateTime,
					PickUpLocation = vehicleData.PickUpLocation,
					DropOffLocation = vehicleData.DropOffLocation,
					CheckoutId = order.Id,
				});
			};

			_checkoutOrderRepo.Add(order);
			_checkoutOrderVehicleRepo.AddRange(order.CheckOutOrderVehicles);

			await _unitOfWork.CompleteAsync();

			return order;
		}

		private async Task validateInputCheckoutVehicle(VehicleToCheckOut vehicleData)
		{
			var isVehicleExist = await _vehicleQueries.CheckIfVehicleExisted(vehicleData.VehicleId);
			if (!isVehicleExist)
				throw new StatusCodeException(message: "Vehicle not found.", statusCode: StatusCodes.Status404NotFound);

			var totalDays = vehicleData.DropOffDateTime.Subtract(vehicleData.PickUpDateTime).Days;
			if (totalDays <= 0)
				throw new StatusCodeException(message: "Drop off day can not be equals or less than pick up date.", statusCode: StatusCodes.Status409Conflict);
		}
	}
}
