using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Application.Web.Service.Services
{
    public class ModelService : IModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Model> _modelRepo;
        private readonly IGenericRepository<ModelColor> _modelColorRepo;
        private readonly IModelQueries _modelQueries;
        private readonly ICollectionService _collectionService;
        private readonly IColorService _colorService;
        private readonly IMapper _mapper;

        public ModelService(IUnitOfWork unitOfWork, IMapper mapper, IModelQueries modelQueries, ICollectionService collectionService, IColorService colorService)
        {
            _unitOfWork = unitOfWork;
            _modelRepo = unitOfWork.GetBaseRepo<Model>();
            _modelColorRepo = unitOfWork.GetBaseRepo<ModelColor>();
            _modelQueries = modelQueries;
            _collectionService = collectionService;
            _colorService = colorService;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<Model>, PaginationMetadata)> GetModelsAsync(PaginationRequestModel pagination)
        {
            var totalItemCount = await _modelQueries.CountModelsAysnc();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

            var modelsToReturn = await _modelQueries.GetModelsWithPaginationAync(pagination);

            return (modelsToReturn, paginationMetadata);
        }

        public async Task<IEnumerable<Model>> GetAllModelsAsync()
        {
            var modelsToReturn = await _modelQueries.GetAllModelsAsync();

            return modelsToReturn;
        }

        public async Task<Model> GetModelByIdAsync(Guid modelId)
        {
            var modelToReturn = await _modelQueries.GetModelByIdAsync(modelId);

            return modelToReturn;
        }

        public async Task<Model> CreateModelAsync(ModelRequestModel requestModel)
        {
            var newModel = _mapper.Map<Model>(requestModel);

            var isModelExisted = await _modelQueries.CheckIfModelExisted(newModel.Name);

            if (isModelExisted)
                throw new StatusCodeException(message: "Model name already exsited.", statusCode: StatusCodes.Status409Conflict);
            else
            {
                var (collection, modelColors) = await HandleModelCollectionAndColors(requestModel, newModel.Id);

                _modelRepo.Add(newModel);

                _modelColorRepo.AddRange(modelColors);

                await _unitOfWork.CompleteAsync();

                return newModel;
            }
        }

        public async Task<Model> UpdateModelAsync(ModelRequestModel requestModel, Guid modelId)
        {
            var model = await _modelQueries.GetModelByIdAsync(modelId);

            if (model == null)
                throw new StatusCodeException(message: "Model not found.", statusCode: StatusCodes.Status404NotFound);
            else
            {
                var modelToUpdate = _mapper.Map<ModelRequestModel, Model>(requestModel, model);

                var isModelExisted = await _modelQueries.CheckIfModelExisted(modelToUpdate.Name);
                
                if (isModelExisted)
                    throw new StatusCodeException(message: "Model name already exsited.", statusCode: StatusCodes.Status409Conflict);
                else
                {
                    var (collection, modelColors) = await HandleModelCollectionAndColors(requestModel, modelToUpdate.Id);

                    _modelColorRepo.DeleteRange(model.ModelColors);

                    _modelRepo.Update(modelToUpdate);

                    _modelColorRepo.AddRange(modelColors);

                    await _unitOfWork.CompleteAsync();

                    return modelToUpdate;
                }
            }
        }

        public async Task<bool> DeleteModelAsync(Guid modelId)
        {
            var model = await _modelRepo.GetById(modelId);

            if (model == null)
                throw new StatusCodeException(message: "Model not found.", statusCode: StatusCodes.Status404NotFound);

            _modelRepo.Delete(modelId);

            await _unitOfWork.CompleteAsync();

            return true;
        }

        private async Task<(Collection, List<ModelColor>)> HandleModelCollectionAndColors(ModelRequestModel requestModel, Guid modelId)
        {
            var collection = await _collectionService.GetCollectionByIdAsync(requestModel.CollectionId);

            if (collection == null)
                throw new StatusCodeException(message: "Collection not found.", statusCode: StatusCodes.Status404NotFound);

            var modelColors = new List<ModelColor>();

            foreach (var colorId in requestModel.ColorIds)
            {
                var color = await _colorService.GetColorByIdAsync(colorId);

                if (color == null)
                    throw new StatusCodeException(message: "Color not found.", statusCode: StatusCodes.Status404NotFound);

                modelColors.Add(new ModelColor
                {
                    ColorId = colorId,
                    ModelId = modelId
                });
            }

            return (collection, modelColors);
        }
    }
}
