using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
	public interface ITripRequestQueries
	{
		Task<List<TripRequest>> GetTripRequestsBasedOnPaymentIntentId(string paymentIntentId);
	}
}
