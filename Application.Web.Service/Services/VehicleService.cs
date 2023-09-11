using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Queries.ServiceQueries;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;

namespace Application.Web.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Vehicle> _productRepo;
        private readonly IVehicleQueries _vehicleQueries;

        public VehicleService(IUnitOfWork unitOfWork, IVehicleQueries vehicleQueries)
        {
            _unitOfWork = unitOfWork;
            _productRepo = unitOfWork.GetBaseRepo<Vehicle>();
            _vehicleQueries = vehicleQueries;
        }

        public async Task<(IEnumerable<Vehicle>, PaginationMetadata)> GetVehiclesAsync(PaginationRequestModel pagination)
        {
            var totalItemCount = await _vehicleQueries.CountVehiclesAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

            var brandToReturn = await _vehicleQueries.GetVehiclesWithPaginationAync(pagination);

            return (brandToReturn, paginationMetadata);
        }

        public async Task<List<Vehicle>> GetAllVehicleAsync()
        {
            var brandToReturn = await _vehicleQueries.GetAllVehiclesAsync();

            return brandToReturn;
        }
    }
}
