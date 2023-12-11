using Application.Web.Database.Context;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class VehicleQueries : BaseQuery<Vehicle>, IVehicleQueries
    {
        public VehicleQueries(ApplicationContext dbContext) : base(dbContext) { }

        public async Task<List<Vehicle>> GetVehiclesWithPaginationAync(PaginationRequestModel pagination)
        {
            return await dbSet
                .Include(v => v.Model).ThenInclude(m => m.Collection).ThenInclude(c => c.Brand)
                .Include(v => v.VehicleImages.OrderBy(x => x.Image.CreatedAt))
                                .ThenInclude(vi => vi.Image)
                .Include(v => v.Owner)
                .Include(c => c.Color)
                .Include(x => x.VehicleReviews)
				.Include(ov => ov.TripRequests)
				.Skip(pagination.pageSize * (pagination.pageNumber - 1))
                .Take(pagination.pageSize)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await dbSet
                .Include(v => v.Model).ThenInclude(m => m.Collection).ThenInclude(c => c.Brand)
                .Include(v => v.VehicleImages.OrderBy(x => x.Image.CreatedAt))
                                .ThenInclude(vi => vi.Image)
				.Include(v => v.Owner)
				.Include(c => c.Color)
				.Include(x => x.VehicleReviews)
				.Include(ov => ov.TripRequests)
				.ToListAsync();
        }

		public async Task<List<Vehicle>> GetAllVehiclesByOwnerIdAsync(Guid ownerId)
		{
			return await dbSet
				.Include(v => v.Model).ThenInclude(m => m.Collection).ThenInclude(c => c.Brand)
				.Include(v => v.VehicleImages.OrderBy(x => x.Image.CreatedAt))
                                .ThenInclude(vi => vi.Image)
				.Include(v => v.Owner)
				.Include(c => c.Color)
                .Include(x => x.VehicleReviews)
				.Include(ov => ov.TripRequests)
				.Where(v => v.OwnerId.Equals(ownerId))
				.ToListAsync();
		}

		public async Task<Vehicle> GetByIdAsync(Guid vehicleId)
        {
            return await dbSet
                .Include(v => v.Model).ThenInclude(m => m.Collection).ThenInclude(c => c.Brand)
                .Include(v => v.VehicleImages.OrderBy(x => x.Image.CreatedAt))
                                .ThenInclude(vi => vi.Image)
                .Include(v => v.Owner)
                .Include(c => c.Color)
                .Include(x => x.VehicleReviews)
				.Include(ov => ov.TripRequests)
				.Where(v => v.Id.Equals(vehicleId))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckIfVehicleExisted(Guid vehicleId)
        {
            return await dbSet
                .AnyAsync(v => v.Id.Equals(vehicleId));
        }

        public async Task<bool> CheckIfLicensePlateExisted(string licensePlate)
        {
            return await dbSet
                .AnyAsync(v => v.LicensePlate.ToUpper().Equals(licensePlate.ToUpper()));
        }

		public async Task<bool> CheckIfInsuranceNumberExisted(string insuranceNumber)
		{
			return await dbSet
				.AnyAsync(v => v.InsuranceNumber.ToUpper().Equals(insuranceNumber.ToUpper()));
		}

		public async Task<int> CountVehiclesAsync()
        {
            return await dbSet.CountAsync();
        }
	}
}
