using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Stripe;

namespace Application.Web.Service.Interfaces
{
	public interface IOrderService
	{
		Task<List<TripRequest>> CreateTripRequestsFromStripeEventAsync(Event stripeEvent);
		Task<List<TripRequest>> GetAllTripRequestsByParentOrderId(string parentOrderId, TripRequestQuery query);
		Task<List<TripRequest>> GetAllTripRequestsByPaymentIntentId(string paymentIntentId, TripRequestQuery query);
		Task SendEmailsForTripRequest(List<TripRequest> tripRequests);
		Task<List<TripRequest>> UpdateTripRequestStatusAsync(TripRequestStatusRequestModel requestModel);
		Task<(List<List<TripRequest>>, PaginationMetadata)> GetTripRequestsByLessorIdAsync(PaginationRequestModel pagination, Guid lessorId, TripRequestQuery query);
		Task<(List<List<TripRequest>>, PaginationMetadata)> GetTripRequestsByLesseeIdAsync(PaginationRequestModel pagination, Guid lesseeId, TripRequestQuery query);
		Task<(List<List<TripRequest>>, PaginationMetadata)> GetAllTripRequests(PaginationRequestModel pagination, TripRequestQuery query);
	}
}
