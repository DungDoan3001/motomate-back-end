using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
	public interface IVehicleReviewQueries
	{
		Task<int> CountTotalReviewByVehicleId(Guid vehicleId);
		Task<decimal> GetAverageRatingByVehicleId(Guid vehicleId);
		Task<IEnumerable<VehicleReview>> GetAllVehicleReviewByVehicleIdAsync(Guid vehicleId);
	}
}
