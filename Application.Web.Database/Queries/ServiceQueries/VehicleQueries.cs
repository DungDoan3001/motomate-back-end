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
                .Include(b => b.Model).ThenInclude(m => m.Collection)              
                .Include(b => b.VehicleImages).ThenInclude(bi => bi.Image)
                .Skip(pagination.pageSize * (pagination.pageNumber - 1))
                .Take(pagination.pageSize)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await dbSet
                .Include(b => b.Model).ThenInclude(m => m.Collection)
                .Include(b => b.VehicleImages).ThenInclude(bi => bi.Image)
                .ToListAsync();
        }

        public async Task<int> CountVehiclesAsync()
        {
            return await dbSet.CountAsync();
        }
    }
}
