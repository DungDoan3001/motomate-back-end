using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
    public interface IVehicleService
    {
        Task<(IEnumerable<Vehicle>, PaginationMetadata)> GetVehiclesAsync(PaginationRequestModel pagination, VehicleQuery vehicleQuery);
		Task<List<Vehicle>> GetAllVehicleAsync(VehicleQuery vehicleQuery);
        Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId);
        Task<Vehicle> CreateVehicleAsync(VehicleRequestModel requestModel);
        Task<Vehicle> UpdateVehicleAsync(VehicleRequestModel requestModel, Guid vehicleId);
        Task<bool> DeleteVehicleAsync(Guid vehicleId);
	}
}
