using Application.Web.Database.Models;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;
using AutoMapper;

namespace Application.Web.Service.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Collection> _modelRepo;
        private readonly IMapper _mapper;

        public CollectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _modelRepo = unitOfWork.GetBaseRepo<Collection>();
            _mapper = mapper;
        }
    }
}
