using Application.Web.Database.Models;
using Stripe;

namespace Application.Web.Service.Interfaces
{
	public interface IOrderService
	{
		Task<List<TripRequest>> CreateTripRequestsFromStripeEventAsync(Event stripeEvent);
	}
}
