using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
    public interface ICollectionQueries
    {
        Task<List<Collection>> GetAllCollectionsAsync();
        Task<Collection> GetCollectionByIdAsync(Guid id);
        Task<Collection> GetCollectionByNameAsync(string name);
        Task<bool> CheckIfCollectionExisted(string name);
        Task<List<Collection>> GetCollectionsWithPaginationAync(PaginationRequestModel pagination);
        Task<int> CountCollectionsAsync();
    }
}
