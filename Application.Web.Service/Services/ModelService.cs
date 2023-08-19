using Application.Web.Database.Models;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;
using AutoMapper;

namespace Application.Web.Service.Services
{
    public class ModelService : IModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Model> _modelRepo;
        private readonly IMapper _mapper;

        public ModelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _modelRepo = unitOfWork.GetBaseRepo<Model>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<Model>> GetModelsAsync()
        {
            return null;
        }
    }
}
