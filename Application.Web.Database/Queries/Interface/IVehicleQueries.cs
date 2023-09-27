using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
    public  interface IVehicleQueries
    {
        Task<List<Vehicle>> GetVehiclesWithPaginationAync(PaginationRequestModel pagination);
        Task<int> CountVehiclesAsync();
        Task<Vehicle> GetByIdAsync(Guid vehicleId);
		Task<List<Vehicle>> GetAllVehiclesAsync();
        Task<bool> CheckIfLicensePlateExisted(string licensePlate);
        Task<bool> CheckIfInsuranceNumberExisted(string insuranceNumber);
        Task<List<Vehicle>> GetAllVehiclesByOwnerIdAsync(Guid ownerId);

	}
}
