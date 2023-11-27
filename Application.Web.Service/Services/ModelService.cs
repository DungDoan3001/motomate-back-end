using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Queries.ServiceQueries;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using AutoMapper;
using LazyCache;
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
		private readonly IAppCache _cache;
		private CacheKeyConstants _cacheKeyConstants;

		public ModelService(IUnitOfWork unitOfWork, IMapper mapper, IModelQueries modelQueries, ICollectionService collectionService, IColorService colorService, IAppCache cache, CacheKeyConstants cacheKeyConstants)
        {
            _unitOfWork = unitOfWork;
            _modelRepo = unitOfWork.GetBaseRepo<Model>();
            _modelColorRepo = unitOfWork.GetBaseRepo<ModelColor>();
            _modelQueries = modelQueries;
            _collectionService = collectionService;
            _colorService = colorService;
            _mapper = mapper;
            _cache = cache;
			_cacheKeyConstants = cacheKeyConstants;

		}

        public async Task<(IEnumerable<Model>, PaginationMetadata)> GetModelsAsync(PaginationRequestModel pagination)
        {
			var key = $"{_cacheKeyConstants.ModelCacheKey}-All";

            var models = await _cache.GetOrAddAsync(
                key,
                async () => await _modelQueries.GetAllModelsAsync(),
                TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			return Helpers.Helpers.GetPaginationModel<Model>(models, pagination);
		}

        public async Task<IEnumerable<Model>> GetAllModelsAsync()
        {
			var key = $"{_cacheKeyConstants.ModelCacheKey}-All";

			var modelsToReturn = await _cache.GetOrAddAsync(
				key,
				async () => await _modelQueries.GetAllModelsAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			return modelsToReturn;
        }

        public async Task<Model> GetModelByIdAsync(Guid modelId)
        {
			var key = $"{_cacheKeyConstants.ModelCacheKey}-ID-{modelId}";

			var modelToReturn = await _cache.GetOrAddAsync(
			    key,
				async () => await _modelQueries.GetModelByIdAsync(modelId),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			if (modelToReturn == null)
				throw new StatusCodeException(message: "Model not found.", statusCode: StatusCodes.Status404NotFound);

			return modelToReturn;
        }

		public async Task<IEnumerable<Model>> GetModelsByCollectionIdAsync(Guid collectionId)
		{
			var key = $"{_cacheKeyConstants.ModelCacheKey}-collectionID-{collectionId}";

			var modelsToReturn = await _cache.GetOrAddAsync(
				key,
				async () => await _modelQueries.GetModelsByCollectionIdAsync(collectionId),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			return modelsToReturn;
		}

		public async Task<Model> CreateModelAsync(ModelRequestModel requestModel)
        {
            var newModel = _mapper.Map<Model>(requestModel);

            var isModelExisted = await _modelQueries.CheckIfModelExisted(newModel.Name);

            if (isModelExisted)
                throw new StatusCodeException(message: "Model name already existed.", statusCode: StatusCodes.Status409Conflict);
            else
            {
                var (collection, modelColors) = await HandleModelCollectionAndColors(requestModel, newModel.Id);

                _modelRepo.Add(newModel);

                _modelColorRepo.AddRange(modelColors);

                await _unitOfWork.CompleteAsync();

				await Task.Run(() =>
				{
					foreach (var key in _cacheKeyConstants.CacheKeyList)
					{
						_cache.Remove(key);
					}

					_cacheKeyConstants.CacheKeyList = new List<string>();
				});

				return newModel;
            }
        }

        public async Task<Model> UpdateModelAsync(ModelRequestModel requestModel, Guid modelId)
        {
            var model = await _modelQueries.GetModelByIdAsync(modelId);

            var originalModelName = model.Name;

            if (model == null)
                throw new StatusCodeException(message: "Model not found.", statusCode: StatusCodes.Status404NotFound);
            else
            {
                var modelToUpdate = _mapper.Map<ModelRequestModel, Model>(requestModel, model);

                var isModelExisted = await _modelQueries.CheckIfModelExisted(modelToUpdate.Name);
                
                if (isModelExisted && (modelToUpdate.Name.ToUpper() != originalModelName.ToUpper()))
                    throw new StatusCodeException(message: "Model name already existed.", statusCode: StatusCodes.Status409Conflict);
                else
                {
                    var (collection, modelColors) = await HandleModelCollectionAndColors(requestModel, modelToUpdate.Id);

                    _modelColorRepo.DeleteRange(model.ModelColors);

                    _modelRepo.Update(modelToUpdate);

                    _modelColorRepo.AddRange(modelColors);

                    await _unitOfWork.CompleteAsync();

                    _unitOfWork.Detach(modelToUpdate);

					await Task.Run(() =>
					{
						foreach (var key in _cacheKeyConstants.CacheKeyList)
						{
							_cache.Remove(key);
						}

						_cacheKeyConstants.CacheKeyList = new List<string>();
					});

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

			await Task.Run(() =>
			{
				foreach (var key in _cacheKeyConstants.CacheKeyList)
				{
					_cache.Remove(key);
				}

				_cacheKeyConstants.CacheKeyList = new List<string>();
			});

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

                _unitOfWork.Detach(color);
            }

            return (collection, modelColors);
        }
    }
}
