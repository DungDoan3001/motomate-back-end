using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class VehicleReviewQueries : BaseQuery<VehicleReview>
	{
		public VehicleReviewQueries(ApplicationContext dbContext) : base(dbContext) { }


		public async Task<int> CountTotalReviewByVehicleId(Guid vehicleId)
		{
			return await dbSet.CountAsync(x => x.VehicleId.Equals(vehicleId));
		}

		public async Task<decimal> GetAverageRatingByVehicleId(Guid vehicleId)
		{
			return (decimal) await dbSet
				.Where(x => x.VehicleId.Equals(vehicleId))
				.AverageAsync(x => x.Rating);
		}

		public async Task<IEnumerable<VehicleReview>> GetAllVehicleReviewByVehicleIdAsync(Guid vehicleId)
		{
			return await dbSet
				.Where(x => x.VehicleId.Equals(vehicleId))
				.ToListAsync();
		}
	}
}
