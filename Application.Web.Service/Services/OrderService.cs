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
using AutoMapper;
using Diacritics.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Stripe;

namespace Application.Web.Service.Services
{
	public class OrderService : IOrderService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<TripRequest> _tripRequestRepo;
		private readonly IGenericRepository<CheckOutOrder> _checkoutOrderRepo;
		private readonly IGenericRepository<CompletedTrip> _completedTripRepo;
		private readonly IGenericRepository<InCompleteTrip> _inCompleteTripRepo;
		private readonly IGenericRepository<Cart> _cartRepo;
		private readonly IGenericRepository<VehicleReview> _vehicleReviewRepo;
		private readonly IGenericRepository<VehicleReviewImage> _vehicleReviewImageRepo;
		private readonly IGenericRepository<Image> _imageRepo;
		private readonly IGenericRepository<CartVehicle> _cartVehicleRepo;
		private readonly ICheckoutOrderQueries _checkoutOrderQueries;
		private readonly ITripRequestQueries _tripRequestQueries;
		private readonly IUserQueries _userQueries;
		private readonly IEmailService _emailService;
		private readonly IPaymentService _paymentService;

		public OrderService(
			IMapper mapper,
			IUnitOfWork unitOfWork,
			ICheckoutOrderQueries checkoutOrderQueries,
			ITripRequestQueries tripRequestQueries,
			IEmailService emailService,
			IPaymentService paymentService,
			IUserQueries userQueries
			)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_tripRequestRepo = unitOfWork.GetBaseRepo<TripRequest>();
			_checkoutOrderRepo = unitOfWork.GetBaseRepo<CheckOutOrder>();
			_completedTripRepo = unitOfWork.GetBaseRepo<CompletedTrip>();
			_inCompleteTripRepo = unitOfWork.GetBaseRepo<InCompleteTrip>();
			_cartVehicleRepo = unitOfWork.GetBaseRepo<CartVehicle>();
			_cartRepo = unitOfWork.GetBaseRepo<Cart>();
			_vehicleReviewRepo = unitOfWork.GetBaseRepo<VehicleReview>();
			_vehicleReviewImageRepo = unitOfWork.GetBaseRepo<VehicleReviewImage>();
			_imageRepo = unitOfWork.GetBaseRepo<Image>();

			_checkoutOrderQueries = checkoutOrderQueries;
			_tripRequestQueries = tripRequestQueries;
			_userQueries = userQueries;

			_emailService = emailService;
			_paymentService = paymentService;
		}


		public async Task<List<TripRequest>> GetAllTripRequestsByParentOrderId(string parentOrderId, TripRequestQuery query)
		{
			var result = await _tripRequestQueries.GetTripRequestsBasedOnParentOrderId(parentOrderId) ?? throw new StatusCodeException(message: "parentId not found.", statusCode: StatusCodes.Status404NotFound);

			var resultToReturn = HandleTripRequestQuery(new List<List<TripRequest>> { result }, query);

			return resultToReturn.FirstOrDefault();
		}

		public async Task<List<TripRequest>> GetAllTripRequestsByPaymentIntentId(string paymentIntentId, TripRequestQuery query)
		{
			var result = await _tripRequestQueries.GetTripRequestsBasedOnPaymentIntentId(paymentIntentId) ?? throw new StatusCodeException(message: "parentId not found.", statusCode: StatusCodes.Status404NotFound);

			var resultToReturn = HandleTripRequestQuery(new List<List<TripRequest>> { result }, query);

			return resultToReturn.FirstOrDefault();
		}

		public async Task<(List<List<TripRequest>>, PaginationMetadata)> GetTripRequestsByLessorIdAsync(PaginationRequestModel pagination, Guid lessorId, TripRequestQuery query)
		{
			var tripRequests = await _tripRequestQueries.GetAllTripRequestsBasedOnLessorId(lessorId);

			var groupTripRequestsByParentIds = tripRequests.GroupBy(x => x.ParentOrderId).ToList();

			var tripRequestsByParentId = new List<List<TripRequest>>();

            foreach (var tripRequestsByParentOrderId in groupTripRequestsByParentIds)
            {
				tripRequestsByParentId.Add(tripRequestsByParentOrderId.ToList());
            }

			tripRequestsByParentId = HandleTripRequestQuery(tripRequestsByParentId, query);

			var totalItemCount = tripRequestsByParentId.Count;

			var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

			var TripRequestsToReturn = tripRequestsByParentId
				.Skip(pagination.pageSize * (pagination.pageNumber - 1))
				.Take(pagination.pageSize)
				.ToList();

			return (TripRequestsToReturn, paginationMetadata);
        }

		public async Task<(List<List<TripRequest>>, PaginationMetadata)> GetAllTripRequests(PaginationRequestModel pagination, TripRequestQuery query)
		{
			var tripRequests = await _tripRequestQueries.GetAllTripRequests();

			var groupTripRequestsByParentIds = tripRequests.GroupBy(x => x.ParentOrderId).ToList();

			var tripRequestsByParentId = new List<List<TripRequest>>();

			foreach (var tripRequestsByParentOrderId in groupTripRequestsByParentIds)
			{
				tripRequestsByParentId.Add(tripRequestsByParentOrderId.ToList());
			}

			tripRequestsByParentId = HandleTripRequestQuery(tripRequestsByParentId, query);

			var totalItemCount = tripRequestsByParentId.Count;

			var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

			var TripRequestsToReturn = tripRequestsByParentId
				.Skip(pagination.pageSize * (pagination.pageNumber - 1))
				.Take(pagination.pageSize)
				.ToList();

			return (TripRequestsToReturn, paginationMetadata);
		}

		public async Task<(List<List<TripRequest>>, PaginationMetadata)> GetTripRequestsByLesseeIdAsync(PaginationRequestModel pagination,Guid lesseeId, TripRequestQuery query)
		{
			var tripRequests = await _tripRequestQueries.GetAllTripRequestsBasedOnLesseeId(lesseeId);

			var groupTripRequestsByParentIds = tripRequests.GroupBy(x => x.ParentOrderId).ToList();

			var tripRequestsByParentId = new List<List<TripRequest>>();

			foreach (var tripRequestsByParentOrderId in groupTripRequestsByParentIds)
			{
				tripRequestsByParentId.Add(tripRequestsByParentOrderId.ToList());
			}

			tripRequestsByParentId = HandleTripRequestQuery(tripRequestsByParentId, query);

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

			listParentIds = listParentIds.Distinct().ToList();

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

		public async Task<bool> CreateNewReviewTripRequestAsync(TripRequestReviewRequestModel requestModel)
		{
			var tripRequest = await HandleCheckingReviewModel(requestModel);

			var vehicleReview = new VehicleReview
			{
				TripRequestId = tripRequest.Id,
				UserId = requestModel.UserId,
				VehicleId = tripRequest.Vehicle.Id,
				Title = requestModel.Title,
				Content = requestModel.Content,
				Rating = requestModel.Rating,
			};

			var newImageList = new List<Image>();
			var newVehicleReviewImageList = new List<VehicleReviewImage>();

			foreach(var image in requestModel.Images)
			{
				var newImage = new Image
				{
					ImageUrl = image.Image,
					PublicId = image.PublicId,
				};

				var vehicleReviewImage = new VehicleReviewImage
				{
					ImageId = newImage.Id,
					VehicleReviewId = vehicleReview.Id,
				};

				newImageList.Add(newImage);
				newVehicleReviewImageList.Add(vehicleReviewImage);
			};

			_vehicleReviewRepo.Add(vehicleReview);
			_imageRepo.AddRange(newImageList);
			_vehicleReviewImageRepo.AddRange(newVehicleReviewImageList);

			await _unitOfWork.CompleteAsync();

			return true;
		}

		private async Task<TripRequest> HandleCheckingReviewModel(TripRequestReviewRequestModel requestModel)
		{
			var isUserExisted = await _userQueries.CheckIfUserExisted(requestModel.UserId);
			var tripRequest = await _tripRequestQueries.GetTripRequestByIdAsync(requestModel.RequestId);

            if (!isUserExisted)
				throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);

			if (tripRequest == null)
				throw new StatusCodeException(message: "Request Id not found.", statusCode: StatusCodes.Status404NotFound);

			if (tripRequest.CompletedTrip == null)
				throw new StatusCodeException(message: "The request is not completed yet.", statusCode: StatusCodes.Status409Conflict);

			if (!Constants.ALLOW_RATING_INPUT.Contains(requestModel.Rating))
				throw new StatusCodeException(message: "Rating does not allow, only allow 1, 2, 3, 4, 5.", statusCode: StatusCodes.Status400BadRequest);

			return tripRequest;
        }

		private List<List<TripRequest>> HandleTripRequestQuery(List<List<TripRequest>> parentTripRequests, TripRequestQuery query)
		{
			var result = new List<List<TripRequest>>();

			if(!query.Status.IsNullOrEmpty())
			{
				var queryStatus = query.Status.Trim().ToUpper().RemoveDiacritics();
				if (queryStatus.Equals(Constants.PENDING.ToUpper()))
				{
					parentTripRequests = parentTripRequests.Where(x => Helpers.Helpers.GetParentOrderStatus(x).Equals(Constants.PENDING)).ToList();
				} 
				else if (queryStatus.Equals(Constants.ONGOING.ToUpper()))
				{
					parentTripRequests = parentTripRequests.Where(x => Helpers.Helpers.GetParentOrderStatus(x).Equals(Constants.ONGOING)).ToList();
				}
				else if (queryStatus.Equals(Constants.COMPLETED.ToUpper()))
				{
					parentTripRequests = parentTripRequests.Where(x => Helpers.Helpers.GetParentOrderStatus(x).Equals(Constants.COMPLETED)).ToList();
				}
				else if (queryStatus.Equals(Constants.CANCELED.ToUpper()))
				{
					parentTripRequests = parentTripRequests.Where(x => Helpers.Helpers.GetParentOrderStatus(x).Equals(Constants.CANCELED)).ToList();
				}
				else
				{
					throw new StatusCodeException(message: $"Only allow value {Constants.PENDING}, {Constants.ONGOING}, {Constants.COMPLETED}, {Constants.CANCELED}", statusCode: StatusCodes.Status400BadRequest);
				}
			}

            foreach (var parentTripRequest in parentTripRequests)
            {
				var holder = parentTripRequest;

				if (!query.SearchQuery.IsNullOrEmpty())
				{
					var searchQuery = query.SearchQuery.ToUpper().Trim().RemoveDiacritics();

					holder = holder
						.Where(x => x.ParentOrderId
									.ToUpper()
									.Trim()
									.RemoveDiacritics()
									.Contains(searchQuery))
						.ToList();
				}

				if (!query.LessorUsername.IsNullOrEmpty())
				{					
					holder = holder
						.Where(x => x.Lessor.UserName
											.ToUpper()
											.Trim()
											.RemoveDiacritics()
											.Equals(query.LessorUsername.ToUpper().Trim().RemoveDiacritics()))
						.ToList();
				}

				if (!query.LesseeUsername.IsNullOrEmpty())
				{
					holder = holder
						.Where(x => x.Lessee.UserName
											.ToUpper()
											.Trim()
											.RemoveDiacritics()
											.Equals(query.LesseeUsername.ToUpper().Trim().RemoveDiacritics()))
						.ToList();
				}

				result.Add(holder);
            }

			return result.Where(s => s.Count > 0).Distinct().ToList();
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
