using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class VehicleReviewQueries : BaseQuery<VehicleReview>, IVehicleReviewQueries
	{
		public VehicleReviewQueries(ApplicationContext dbContext) : base(dbContext) { }


		public async Task<int> CountTotalReviewByVehicleId(Guid vehicleId)
		{
			return await dbSet.CountAsync(x => x.VehicleId.Equals(vehicleId));
		}

		public async Task<decimal> GetAverageRatingByVehicleId(Guid vehicleId)
		{
			return (decimal)await dbSet
				.Where(x => x.VehicleId.Equals(vehicleId))
				.AverageAsync(x => x.Rating);
		}

		public async Task<IEnumerable<VehicleReview>> GetAllVehicleReviewByVehicleIdAsync(Guid vehicleId)
		{
			return await dbSet
				.Include(x => x.User)
				.Include(x => x.VehicleReviewImages).ThenInclude(x => x.Image)
				.Where(x => x.VehicleId.Equals(vehicleId))
				.OrderByDescending(x => x.CreatedAt)
				.ToListAsync();
		}
	}
}
