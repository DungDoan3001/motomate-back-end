using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;
using Stripe;

namespace Application.Web.Service.Interfaces
{
	public interface IOrderService
	{
		Task<List<TripRequest>> CreateTripRequestsFromStripeEventAsync(Event stripeEvent);
		Task<List<TripRequest>> GetAllTripRequestsByParentOrderId(string parentOrderId, string? lessorUsername = "");
		Task SendEmailsForTripRequest(List<TripRequest> tripRequests);
		Task<List<TripRequest>> UpdateTripRequestStatusAsync(TripRequestStatusRequestModel requestModel);
		Task<List<List<TripRequest>>> GetTripRequestsByLessorIdAsync(Guid lessorId);
	}
}
