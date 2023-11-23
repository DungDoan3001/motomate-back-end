using System.Text.RegularExpressions;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Stripe;

namespace Application.Web.Service.Services
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<TripRequest> _tripRequestRepo;
		private readonly IGenericRepository<CheckOutOrder> _checkoutOrderRepo;
		private readonly IGenericRepository<CompletedTrip> _completedTripRepo;
		private readonly IGenericRepository<InCompleteTrip> _inCompleteTripRepo;
		private readonly IGenericRepository<Cart> _cartRepo;
		private readonly IGenericRepository<CartVehicle> _cartVehicleRepo;
		private readonly ICheckoutOrderQueries _checkoutOrderQueries;
		private readonly ITripRequestQueries _tripRequestQueries;
		private readonly IEmailService _emailService;
		private readonly IPaymentService _paymentService;

		public OrderService(
			IUnitOfWork unitOfWork,
			ICheckoutOrderQueries checkoutOrderQueries,
			ITripRequestQueries tripRequestQueries,
			IEmailService emailService,
			IPaymentService paymentService
			)
		{
			_unitOfWork = unitOfWork;
			_tripRequestRepo = unitOfWork.GetBaseRepo<TripRequest>();
			_checkoutOrderRepo = unitOfWork.GetBaseRepo<CheckOutOrder>();
			_completedTripRepo = unitOfWork.GetBaseRepo<CompletedTrip>();
			_inCompleteTripRepo = unitOfWork.GetBaseRepo<InCompleteTrip>();
			_cartVehicleRepo = unitOfWork.GetBaseRepo<CartVehicle>();
			_cartRepo = unitOfWork.GetBaseRepo<Cart>();

			_checkoutOrderQueries = checkoutOrderQueries;
			_tripRequestQueries = tripRequestQueries;

			_emailService = emailService;
			_paymentService = paymentService;
		}


		public async Task<List<TripRequest>> GetAllTripRequestsByParentOrderId(string parentOrderId, string? lessorUsername = "")
		{
			var result = await _tripRequestQueries.GetTripRequestsBasedOnParentOrderId(parentOrderId);

			return result;
		}

		public async Task<(List<List<TripRequest>>, PaginationMetadata)> GetTripRequestsByLessorIdAsync(PaginationRequestModel pagination, Guid lessorId)
		{
			var tripRequests = await _tripRequestQueries.GetAllTripRequestsBasedOnLessorId(lessorId);

			var groupTripRequestsByParentIds = tripRequests.GroupBy(x => x.ParentOrderId).ToList();

			var tripRequestsByParentId = new List<List<TripRequest>>();

            foreach (var tripRequestsByParentOrderId in groupTripRequestsByParentIds)
            {
				tripRequestsByParentId.Add(tripRequestsByParentOrderId.ToList());
            }

			var totalItemCount = tripRequestsByParentId.Count;

			var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

			var TripRequestsToReturn = tripRequestsByParentId
				.Skip(pagination.pageSize * (pagination.pageNumber - 1))
				.Take(pagination.pageSize)
				.ToList();

			return (TripRequestsToReturn, paginationMetadata);
        }

		public async Task<(List<List<TripRequest>>, PaginationMetadata)> GetTripRequestsByLesseeIdAsync(PaginationRequestModel pagination,Guid lesseeId)
		{
			var tripRequests = await _tripRequestQueries.GetAllTripRequestsBasedOnLesseeId(lesseeId);

			var groupTripRequestsByParentIds = tripRequests.GroupBy(x => x.ParentOrderId).ToList();

			var tripRequestsByParentId = new List<List<TripRequest>>();

			foreach (var tripRequestsByParentOrderId in groupTripRequestsByParentIds)
			{
				tripRequestsByParentId.Add(tripRequestsByParentOrderId.ToList());
			}

			var totalItemCount = tripRequestsByParentId.Count;

			var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

			var TripRequestsToReturn = tripRequestsByParentId
				.Skip(pagination.pageSize * (pagination.pageNumber - 1))
				.Take(pagination.pageSize)
				.ToList();

			return (TripRequestsToReturn, paginationMetadata);
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

		public async Task<List<TripRequest>> UpdateTripRequestStatusAsync(TripRequestStatusRequestModel requestModel)
		{
			var isValidStatus = Constants.AVAILABLE_UPDATE_TRIP_STATUS.Contains(requestModel.Status);

			if (!isValidStatus)
				throw new StatusCodeException(message: $"Only allow these valid status fields: {String.Join(", ", Constants.AVAILABLE_UPDATE_TRIP_STATUS)}", statusCode: StatusCodes.Status400BadRequest);

			var listParentIds = await _tripRequestQueries.GetParentIdsFromTripRequests(requestModel.RequestIds);

			if(listParentIds.Count == 0)
				throw new StatusCodeException(message: "Must have a request to update.", statusCode: StatusCodes.Status400BadRequest);

			if(listParentIds.Count > 1)
				throw new StatusCodeException(message: "Can only update status from one parent Order", statusCode: StatusCodes.Status400BadRequest);

			foreach (var request in requestModel.RequestIds)
			{
				if (requestModel.Status.Equals(Constants.APPROVED))
				{
					_ = await ApproveTripRequestAsync(request);
				}
				else if (requestModel.Status.Equals(Constants.COMPLETED))
				{
					_ = await CompleteTripRequestAsync(request);
				}
				else if (requestModel.Status.Equals(Constants.CANCELED))
				{
					_ = await CancelTripRequestAsync(request, requestModel.Reason);
				}
			}

			return await _tripRequestQueries.GetTripRequestsBasedOnParentOrderId(listParentIds.FirstOrDefault());
		}

		private async Task<bool> ApproveTripRequestAsync(Guid tripRequestId)
		{
			var tripRequest = await _tripRequestQueries.GetTripRequestByIdAsync(tripRequestId) ?? throw new StatusCodeException(message: "Trip not found.", statusCode: StatusCodes.Status404NotFound);
		
			var tripStatus = GetTripRequestStatus(tripRequest);

			if (!tripStatus.Equals(Constants.PENDING))
				throw new StatusCodeException(message: $"Can not {Constants.APPROVED} due to the current status is {tripStatus}");

			tripRequest.Status = true;

			_tripRequestRepo.Update(tripRequest);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(tripRequest);

			return true;
		}

		private async Task<bool> CompleteTripRequestAsync(Guid tripRequestId)
		{
			var tripRequest = await _tripRequestQueries.GetTripRequestByIdAsync(tripRequestId) ?? throw new StatusCodeException(message: "Trip not found.", statusCode: StatusCodes.Status404NotFound);

			var tripStatus = GetTripRequestStatus(tripRequest);

			if (!tripStatus.Equals(Constants.ONGOING))
				throw new StatusCodeException(message: $"Can not {Constants.COMPLETED} due to the current status is {tripStatus}");

			tripRequest.Status = true;

			var tripComplete = new CompletedTrip
			{
				TripId = tripRequest.Id,
				Duration = tripRequest.DropOffDateTime - tripRequest.PickUpDateTime,
				Ammount = tripRequest.Ammount * _paymentService.CalculateTotalRentDays(tripRequest.PickUpDateTime, tripRequest.DropOffDateTime)
			};

			_tripRequestRepo.Update(tripRequest);
			_completedTripRepo.Add(tripComplete);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(tripRequest);
			_unitOfWork.Detach(tripComplete);

			return true;
		}

		private async Task<bool> CancelTripRequestAsync(Guid tripRequestId, string reason)
		{
			var tripRequest = await _tripRequestQueries.GetTripRequestByIdAsync(tripRequestId) ?? throw new StatusCodeException(message: "Trip not found.", statusCode: StatusCodes.Status404NotFound);

			var tripStatus = GetTripRequestStatus(tripRequest);

			if (!tripStatus.Equals(Constants.PENDING) && !tripStatus.Equals(Constants.ONGOING))
				throw new StatusCodeException(message: $"Can not {Constants.CANCELED} due to the current status is {tripStatus}");

			decimal refundAmmounts = tripRequest.Ammount * _paymentService.CalculateTotalRentDays(tripRequest.PickUpDateTime, tripRequest.DropOffDateTime);

			var refund = await _paymentService.RefundPayment(tripRequest.PaymentIntentId, refundAmmounts, reason);

			tripRequest.Status = false;

			var inCompleteTrip = new InCompleteTrip
			{
				TripId = tripRequest.Id,
				Reason = reason,
				RefundId = refund.Id,
				CancelTime = DateTime.UtcNow
			};

			_tripRequestRepo.Update(tripRequest);
			_inCompleteTripRepo.Add(inCompleteTrip);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(tripRequest);
			_unitOfWork.Detach(inCompleteTrip);

			return true;
		}

		public async Task SendEmailsForTripRequest(List<TripRequest> tripRequests)
		{
			var lessors = tripRequests.GroupBy(x => x.Lessor).Select(x => x.Key).ToList();
			var lessee = tripRequests.FirstOrDefault().Lessee;
			var parentOrderId = tripRequests.FirstOrDefault().ParentOrderId;

			await SendEmailToLesseeAsync(tripRequests.Count, lessee, parentOrderId);

			await SendEmailToLessorsAsync(tripRequests, lessors, lessee, parentOrderId);
		}

		private string GetTripRequestStatus(TripRequest tripRequest)
		{
			if (!tripRequest.Status && tripRequest.InCompleteTrip == null && tripRequest.CompletedTrip == null)
			{
				return Constants.PENDING;
			}
			else if (tripRequest.Status && tripRequest.InCompleteTrip == null && tripRequest.CompletedTrip == null)
			{
				return Constants.ONGOING;
			}
			else if (!tripRequest.Status && tripRequest.InCompleteTrip != null && tripRequest.CompletedTrip == null)
			{
				return Constants.CANCELED;
			}
			else if (tripRequest.Status && tripRequest.InCompleteTrip == null && tripRequest.CompletedTrip != null)
			{
				return Constants.COMPLETED;
			}

			return null;
		}

		private async Task SendEmailToLessorsAsync(List<TripRequest> tripRequests, List<User> lessors, User lessee, string parentOrderId)
		{
			foreach (var lessor in lessors)
			{
				string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "newOrderFromCustomer.html");
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
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "orderSuccess.html");
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
			var cartVehicleToDelete = new List<CartVehicle>();

			var currentTimeStamp = DateTime.UtcNow;

			var parentOrderId = Guid.NewGuid().ToString().Split("-").LastOrDefault();

			foreach (var vehicleToCheckout in checkoutOrder.CheckOutOrderVehicles)
			{
				var cart = await _cartRepo.FindOne(x => x.UserId.Equals(checkoutOrder.UserId)) ?? throw new StatusCodeException(message: "Cart not found with user.", statusCode: StatusCodes.Status409Conflict);
				var cartVehicle = await _cartVehicleRepo.FindOne(x => x.VehicleId.Equals(vehicleToCheckout.Vehicle.Id) && x.CartId.Equals(cart.Id)) ?? throw new StatusCodeException(message: "Vehicle in cart.", statusCode: StatusCodes.Status409Conflict);

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
				cartVehicleToDelete.Add(cartVehicle);
			}

			_tripRequestRepo.AddRange(tripRequests);
			_cartVehicleRepo.DeleteRange(cartVehicleToDelete);
			_checkoutOrderRepo.Delete(checkoutOrder.Id);

			await _unitOfWork.CompleteAsync();
		}
	}
}
