using Application.Web.Database.Models;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;

namespace Application.Web.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Vehicle> _productRepo;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepo = unitOfWork.GetBaseRepo<Vehicle>();
        }
    }
}
