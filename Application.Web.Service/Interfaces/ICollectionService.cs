using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
    public interface ICollectionService
    {
        Task<IEnumerable<Collection>> GetAllCollectionAsync();
        Task<Collection> GetCollectionByIdAsync(Guid id);
        Task<Collection> CreateCollectionAsync(CollectionRequestModel requestModel);
        Task<Collection> UpdateCollectionAsync(CollectionRequestModel requestModel, Guid collectionId);
        Task<bool> DeleteCollectionAsync(Guid collectionId);
        Task<(IEnumerable<Collection>, PaginationMetadata)> GetCollectionsAsync(PaginationRequestModel pagination);
    }
}
