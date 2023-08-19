using Application.Web.Database.DTOs.RequestModels;
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
    public class CollectionService : ICollectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Collection> _collectionRepo;
        private readonly ICollectionQueries _collectionQueries;
        private readonly IMapper _mapper;

        public CollectionService(IUnitOfWork unitOfWork, IMapper mapper, ICollectionQueries collectionQueries)
        {
            _unitOfWork = unitOfWork;
            _collectionRepo = unitOfWork.GetBaseRepo<Collection>();
            _collectionQueries = collectionQueries;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionAsync()
        {
            var collectionToReturn = await _collectionQueries.GetAllCollectionsAsync();

            return collectionToReturn;
        }

        public async Task<Collection> GetCollectionByIdAsync(Guid collectionId)
        {
            var collectionToReturn = await _collectionQueries.GetCollectionByIdAsync(collectionId);

            return collectionToReturn;
        }

        public async Task<Collection> CreateCollectionAsync(CollectionRequestModel requestModel)
        {
            var newCollection = _mapper.Map<Collection>(requestModel);

            var isCollectionExisted = await _collectionQueries.CheckIfCollectionExisted(newCollection.Name);

            if (isCollectionExisted)
                throw new StatusCodeException(message: "Collection name already exsited.", statusCode: StatusCodes.Status409Conflict);
            else
            {
                _collectionRepo.Add(newCollection);

                await _unitOfWork.CompleteAsync();

                return newCollection;
            }
        }

        public async Task<Collection> UpdateCollectionAsync(CollectionRequestModel requestModel, Guid collectionId)
        {
            var collection = await _collectionRepo.GetById(collectionId);

            if (collection == null)
                throw new StatusCodeException(message: "Collection not found.", statusCode: StatusCodes.Status404NotFound);
            else
            {
                var collectionToUpdate = _mapper.Map<CollectionRequestModel, Collection>(requestModel, collection);

                _collectionRepo.Update(collectionToUpdate);

                await _unitOfWork.CompleteAsync();

                return collectionToUpdate;
            }
        }

        public async Task<bool> DeleteCollectionAsync(Guid collectionId)
        {
            var collection = await _collectionRepo.GetById(collectionId);

            if (collection == null)
                throw new StatusCodeException(message: "Collection not found.", statusCode: StatusCodes.Status404NotFound);

            _collectionRepo.Delete(collectionId);

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
