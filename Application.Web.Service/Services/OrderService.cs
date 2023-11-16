using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;
using Stripe;

namespace Application.Web.Service.Services
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<TripRequest> _tripRequestRepo;
		private readonly IGenericRepository<CheckOutOrder> _checkoutOrderRepo;
		private readonly ICheckoutOrderQueries _checkoutOrderQueries;
		private readonly ITripRequestQueries _tripRequestQueries;

		public OrderService(
			IUnitOfWork unitOfWork,
			ICheckoutOrderQueries checkoutOrderQueries,
			ITripRequestQueries tripRequestQueries
			)
		{
			_unitOfWork = unitOfWork;
			_tripRequestRepo = unitOfWork.GetBaseRepo<TripRequest>();
			_checkoutOrderRepo = unitOfWork.GetBaseRepo<CheckOutOrder>();

			_checkoutOrderQueries = checkoutOrderQueries;
			_tripRequestQueries = tripRequestQueries;
		}


		public async Task<List<TripRequest>> CreateTripRequestsFromStripeEventAsync(Event stripeEvent)
		{
			var charge = (Charge)stripeEvent.Data.Object;

			if(charge.Status == "succeeded")
			{
				await CreateNewTripRequests(charge);

				return await _tripRequestQueries.GetTripRequestsBasedOnPaymentIntentId(charge.PaymentIntentId);
			}

			return null;
		}

		private async Task CreateNewTripRequests(Charge charge)
		{
			var checkoutOrder = await _checkoutOrderQueries.GetCheckOutOrderByPaymentIntentIdAsync(charge.PaymentIntentId);

			var tripRequests = new List<TripRequest>();

			var currentTimeStamp = DateTime.UtcNow;

			var vehiclesToCheckout = checkoutOrder.CheckOutOrderVehicles.Select(x => x.Vehicle).ToList();

			foreach (var vehicle in vehiclesToCheckout)
			{
				var tripRequest = new TripRequest
				{
					LesseeId = checkoutOrder.UserId,
					LessorId = vehicle.OwnerId,
					VehicleId = vehicle.Id,
					Status = false,
					PickUpLocation = checkoutOrder.PickUpLocation,
					DropOffLocation = checkoutOrder.DropOffLocation,
					Created_At = currentTimeStamp,
					PaymentIntentId = charge.PaymentIntentId
				};

				tripRequests.Add(tripRequest);
			}

			_tripRequestRepo.AddRange(tripRequests);
			_checkoutOrderRepo.Delete(checkoutOrder.Id);

			await _unitOfWork.CompleteAsync();
		}
	}
}
