using System.Text.RegularExpressions;
using Application.Web.Database.DTOs.ServiceModels;
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
		private readonly IEmailService _emailService;

		public OrderService(
			IUnitOfWork unitOfWork,
			ICheckoutOrderQueries checkoutOrderQueries,
			ITripRequestQueries tripRequestQueries,
			IEmailService emailService
			)
		{
			_unitOfWork = unitOfWork;
			_tripRequestRepo = unitOfWork.GetBaseRepo<TripRequest>();
			_checkoutOrderRepo = unitOfWork.GetBaseRepo<CheckOutOrder>();

			_checkoutOrderQueries = checkoutOrderQueries;
			_tripRequestQueries = tripRequestQueries;

			_emailService = emailService;
		}


		public async Task<List<TripRequest>> GetAllTripRequestsByParentOrderId(string parentOrderId, string? lessorUsername = "")
		{
			var result = await _tripRequestQueries.GetTripRequestsBasedOnParentOrderId(parentOrderId);

			return result;
		}

		public async Task<List<TripRequest>> CreateTripRequestsFromStripeEventAsync(Event stripeEvent)
		{
			var charge = (Charge)stripeEvent.Data.Object;

			if(charge.Status == "succeeded")
			{
				await CreateNewTripRequests(charge);

				var tripRequests = await _tripRequestQueries.GetTripRequestsBasedOnPaymentIntentId(charge.PaymentIntentId);

				await SendEmailsForTripRequest(tripRequests);

				return tripRequests;
			}

			return null;
		}

		public async Task SendEmailsForTripRequest(List<TripRequest> tripRequests)
		{
			var lessors = tripRequests.GroupBy(x => x.Lessor).Select(x => x.Key).ToList();
			var lessee = tripRequests.FirstOrDefault().Lessee;
			var parentOrderId = tripRequests.FirstOrDefault().ParentOrderId;

			await SendEmailToLesseeAsync(tripRequests.Count, lessee, parentOrderId);

			await SendEmailToLessorsAsync(tripRequests, lessors, lessee, parentOrderId);
		}

		private async Task SendEmailToLessorsAsync(List<TripRequest> tripRequests, List<User> lessors, User lessee, string parentOrderId)
		{
			foreach (var lessor in lessors)
			{
				string filePath = @"../Application.Web.Service/Statics/newOrderFromCustomer.html";
				using (StreamReader reader = new StreamReader(filePath))
				{
					string content = await reader.ReadToEndAsync();
					content = Regex.Replace(content, "###ORDER_ID###", parentOrderId);
					content = Regex.Replace(content, "###USERNAME###", lessee.UserName);

					var emailOptions = new SendEmailOptions
					{
						Subject = $"You have received a new order with {tripRequests.Count(x => x.Lessor.Equals(lessor))} vehicles !!!",
						Body = content,
						ToEmail = lessor.Email,
						ToName = lessor.FullName,
					};

					_ = await _emailService.SendEmailAsync(emailOptions);
				}
			}
		}

		private async Task SendEmailToLesseeAsync(int ordersCount, User lessee, string parentOrderId)
		{
			string filePath = @"../Application.Web.Service/Statics/orderSuccess.html";
			using (StreamReader reader = new StreamReader(Path.GetFullPath(filePath)))
			{
				string content = await reader.ReadToEndAsync();
				content = Regex.Replace(content, "###ORDER_ID###", parentOrderId);

				var emailOptions = new SendEmailOptions
				{
					Subject = $"Your {ordersCount} new orders have been created !!!",
					Body = content,
					ToEmail = lessee.Email,
					ToName = lessee.FullName,
				};

				_ = await _emailService.SendEmailAsync(emailOptions);
			}
		}

		private async Task CreateNewTripRequests(Charge charge)
		{
			var checkoutOrder = await _checkoutOrderQueries.GetCheckOutOrderByPaymentIntentIdAsync(charge.PaymentIntentId);

			var tripRequests = new List<TripRequest>();

			var currentTimeStamp = DateTime.UtcNow;

			var parentOrderId = Guid.NewGuid().ToString().Split("-").LastOrDefault();

			foreach (var vehicleToCheckout in checkoutOrder.CheckOutOrderVehicles)
			{
				var tripRequest = new TripRequest
				{
					LesseeId = checkoutOrder.UserId,
					LessorId = vehicleToCheckout.Vehicle.OwnerId,
					VehicleId = vehicleToCheckout.Vehicle.Id,
					Status = false,
					Ammount = vehicleToCheckout.Vehicle.Price,
					PickUpDateTime = vehicleToCheckout.PickUpDateTime,
					DropOffDateTime = vehicleToCheckout.DropOffDateTime,
					PickUpLocation = vehicleToCheckout.PickUpLocation,
					DropOffLocation = vehicleToCheckout.DropOffLocation,
					Created_At = currentTimeStamp,
					PaymentIntentId = charge.PaymentIntentId,
					ParentOrderId = parentOrderId,
				};

				tripRequests.Add(tripRequest);
			}

			_tripRequestRepo.AddRange(tripRequests);
			_checkoutOrderRepo.Delete(checkoutOrder.Id);

			await _unitOfWork.CompleteAsync();
		}
	}
}
