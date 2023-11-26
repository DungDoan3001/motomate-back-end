using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
    public interface IVehicleService
    {
        Task<(IEnumerable<Vehicle>, PaginationMetadata)> GetVehiclesAsync(PaginationRequestModel pagination, VehicleQuery vehicleQuery);
		Task<List<Vehicle>> GetAllVehiclesAsync(VehicleQuery vehicleQuery);
        Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId);
        Task<Vehicle> CreateVehicleAsync(VehicleRequestModel requestModel);
        Task<Vehicle> UpdateVehicleAsync(VehicleRequestModel requestModel, Guid vehicleId);
        Task<bool> DeleteVehicleAsync(Guid vehicleId);
        Task<(IEnumerable<Vehicle>, PaginationMetadata)> GetAllVehiclesByOwnerIdAsync(PaginationRequestModel pagination, VehicleQuery vehicleQuery, Guid ownerId);
		Task<(bool, bool)> HandleLockVehicleAsync(Guid vehicleId);
        Task<Vehicle> UpdateVehicleStatusAsync(Guid vehicleId, int statusNumber);
        Task<(IEnumerable<Vehicle>, PaginationMetadata)> GetVehiclesByStatusAsync(PaginationRequestModel pagination, VehicleQuery vehicleQuery, string statusRoute);
        Task<List<Vehicle>> GetRelatedVehicleAsync(Guid vehicleId);
	}
}
