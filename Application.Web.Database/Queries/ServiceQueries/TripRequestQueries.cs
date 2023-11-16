using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class TripRequestQueries : BaseQuery<TripRequest>, ITripRequestQueries
	{
		public TripRequestQueries(ApplicationContext dbContext) : base(dbContext) { }

		public async Task<List<TripRequest>> GetTripRequestsBasedOnPaymentIntentId(string paymentIntentId)
		{
			return await dbSet
				.Where(x => x.PaymentIntentId.Equals(paymentIntentId))
				.ToListAsync();
		}
	}
}
