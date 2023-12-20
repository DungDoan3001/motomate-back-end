using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
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
	public class CollectionService : ICollectionService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<Collection> _collectionRepo;
		private readonly ICollectionQueries _collectionQueries;
		private readonly IMapper _mapper;
		private IAppCache _cache;
		private CacheKeyConstants _cacheKeyConstants;

		public CollectionService(IUnitOfWork unitOfWork, IMapper mapper, ICollectionQueries collectionQueries, IAppCache cache, CacheKeyConstants cacheKeyConstants)
		{
			_unitOfWork = unitOfWork;
			_collectionRepo = unitOfWork.GetBaseRepo<Collection>();
			_collectionQueries = collectionQueries;
			_mapper = mapper;
			_cache = cache;
			_cacheKeyConstants = cacheKeyConstants;
		}

		public async Task<(IEnumerable<Collection>, PaginationMetadata)> GetCollectionsAsync(PaginationRequestModel pagination)
		{
			var key = $"{_cacheKeyConstants.CollectionCacheKey}-All";

			var collections = await _cache.GetOrAddAsync(
				key,
				async () => await _collectionQueries.GetAllCollectionsAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			return Helpers.Helpers.GetPaginationModel<Collection>(collections, pagination);
		}

		public async Task<IEnumerable<Collection>> GetAllCollectionAsync()
		{
			var key = $"{_cacheKeyConstants.CollectionCacheKey}-All";

			var collectionsToReturn = await _cache.GetOrAddAsync(
				key,
				async () => await _collectionQueries.GetAllCollectionsAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			return collectionsToReturn;
		}

		public async Task<Collection> GetCollectionByIdAsync(Guid collectionId)
		{
			var key = $"{_cacheKeyConstants.CollectionCacheKey}-ID-{collectionId}";

			var collectionToReturn = await _cache.GetOrAddAsync(
				key,
				async () => await _collectionQueries.GetCollectionByIdAsync(collectionId),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			return collectionToReturn;
		}

		public async Task<Collection> CreateCollectionAsync(CollectionRequestModel requestModel)
		{
			var newCollection = _mapper.Map<Collection>(requestModel);

			var isCollectionExisted = await _collectionQueries.CheckIfCollectionExisted(newCollection.Name);

			if (isCollectionExisted)
				throw new StatusCodeException(message: "Collection name already existed.", statusCode: StatusCodes.Status409Conflict);
			else
			{
				_collectionRepo.Add(newCollection);

				await _unitOfWork.CompleteAsync();

				await Task.Run(() =>
				{
					foreach (var key in _cacheKeyConstants.CacheKeyList)
					{
						_cache.Remove(key);
					}

					_cacheKeyConstants.CacheKeyList = new List<string>();
				});

				return newCollection;
			}
		}

		public async Task<Collection> UpdateCollectionAsync(CollectionRequestModel requestModel, Guid collectionId)
		{
			var collection = await _collectionRepo.GetById(collectionId);

			var originalCollectionName = collection.Name;

			if (collection == null)
				throw new StatusCodeException(message: "Collection not found.", statusCode: StatusCodes.Status404NotFound);
			else
			{
				var collectionToUpdate = _mapper.Map<CollectionRequestModel, Collection>(requestModel, collection);

				var isCollectionExisted = await _collectionQueries.CheckIfCollectionExisted(collectionToUpdate.Name);

				if (isCollectionExisted && (collectionToUpdate.Name.ToUpper() != originalCollectionName.ToUpper()))
					throw new StatusCodeException(message: "Collection name already existed.", statusCode: StatusCodes.Status409Conflict);
				else
				{
					_collectionRepo.Update(collectionToUpdate);

					await _unitOfWork.CompleteAsync();

					await Task.Run(() =>
					{
						foreach (var key in _cacheKeyConstants.CacheKeyList)
						{
							_cache.Remove(key);
						}

						_cacheKeyConstants.CacheKeyList = new List<string>();
					});

					return collectionToUpdate;
				}
			}
		}

		public async Task<bool> DeleteCollectionAsync(Guid collectionId)
		{
			var collection = await _collectionRepo.GetById(collectionId);

			if (collection == null)
				throw new StatusCodeException(message: "Collection not found.", statusCode: StatusCodes.Status404NotFound);

			_collectionRepo.Delete(collectionId);

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
	}
}
