using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class CheckoutOrderQueries : BaseQuery<CheckOutOrder>, ICheckoutOrderQueries
	{
		public CheckoutOrderQueries(ApplicationContext dbContext) : base(dbContext) { }

		public async Task<bool> IsOrderExistByUserId(Guid userId)
		{
			return await dbSet
				.AnyAsync(x => x.UserId.Equals(userId));
		}

		public async Task<CheckOutOrder> GetCheckOutOrderByUserIdAsync(Guid userId)
		{
			return await dbSet
				.Include(x => x.User)
				.Include(x => x.CheckOutOrderVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.Owner)
				.Include(x => x.CheckOutOrderVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.Color)
				.Include(x => x.CheckOutOrderVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleImages).ThenInclude(x => x.Image)
				.Include(x => x.CheckOutOrderVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.Collection).ThenInclude(x => x.Brand)
				.Include(c => c.CheckOutOrderVehicles).ThenInclude(cv => cv.Vehicle).ThenInclude(x => x.TripRequests.Where(x => x.PickUpDateTime > DateTime.UtcNow))
				.FirstOrDefaultAsync(x => x.UserId.Equals(userId));
		}

		public async Task<CheckOutOrder> GetCheckOutOrderByPaymentIntentIdAsync(string paymentIntentId)
		{
			return await dbSet
				.Include(x => x.User)
				.Include(x => x.CheckOutOrderVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.Owner)
				.Include(x => x.CheckOutOrderVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.Color)
				.Include(x => x.CheckOutOrderVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleImages).ThenInclude(x => x.Image)
				.Include(x => x.CheckOutOrderVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.Collection).ThenInclude(x => x.Brand)
				.Include(c => c.CheckOutOrderVehicles).ThenInclude(cv => cv.Vehicle).ThenInclude(x => x.TripRequests.Where(x => x.PickUpDateTime > DateTime.UtcNow))
				.FirstOrDefaultAsync(x => x.PaymentIntentId.Equals(paymentIntentId));
		}
	}
}
