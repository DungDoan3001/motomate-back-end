using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Stripe;

namespace Application.Web.Service.Interfaces
{
	public interface IOrderService
	{
		Task<IEnumerable<TripRequest>> CreateTripRequestsFromStripeEventAsync(Event stripeEvent);
		Task<IEnumerable<TripRequest>> GetAllTripRequestsByParentOrderId(string parentOrderId, TripRequestQuery query);
		Task<IEnumerable<TripRequest>> GetAllTripRequestsByPaymentIntentId(string paymentIntentId, TripRequestQuery query);
		Task SendEmailsForTripRequest(List<TripRequest> tripRequests);
		Task<IEnumerable<TripRequest>> UpdateTripRequestStatusAsync(TripRequestStatusRequestModel requestModel);
		Task<(IEnumerable<IEnumerable<TripRequest>>, PaginationMetadata)> GetTripRequestsByLessorIdAsync(PaginationRequestModel pagination, Guid lessorId, TripRequestQuery query);
		Task<(IEnumerable<IEnumerable<TripRequest>>, PaginationMetadata)> GetTripRequestsByLesseeIdAsync(PaginationRequestModel pagination, Guid lesseeId, TripRequestQuery query);
		Task<(IEnumerable<IEnumerable<TripRequest>>, PaginationMetadata)> GetAllTripRequests(PaginationRequestModel pagination, TripRequestQuery query);
		Task<bool> CreateNewReviewTripRequestAsync(TripRequestReviewRequestModel requestModel);
	}
}
