using Application.Web.Database.Models;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;

namespace Application.Web.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Vehicle> _productRepo;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepo = unitOfWork.GetBaseRepo<Vehicle>();
        }
    }
}
