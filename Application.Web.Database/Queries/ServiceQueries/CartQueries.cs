using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class CartQueries : BaseQuery<Cart>, ICartQueries
	{
		public CartQueries(ApplicationContext dbContext) : base(dbContext) { }

		public async Task<Cart> GetCartByUserIdAsync(Guid userId)
		{
			return await dbSet
				.Include(c => c.User)
				.Include(c => c.CartVehicles).ThenInclude(cv => cv.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.Collection)
				.Include(c => c.CartVehicles).ThenInclude(cv => cv.Vehicle).ThenInclude(x => x.Owner)
				.Where(c => c.UserId.Equals(userId))
				.FirstOrDefaultAsync();
		}

		public async Task<Guid> GetCartIdByUserIdAsync(Guid userId)
		{
			return await dbSet
				.Where(c => c.UserId.Equals(userId))
				.Select(c => c.Id)
				.FirstOrDefaultAsync();
		}

		public async Task<bool> CheckIfVehicleExistedInCart(Guid cartId, Guid vehicleId)
		{
			return await dbSet
				.Include(c => c.CartVehicles)
				.Where(c => c.Id.Equals(cartId))
				.AnyAsync(c => c.CartVehicles.Any(cv => cv.VehicleId.Equals(vehicleId)));
		}
	}
}
