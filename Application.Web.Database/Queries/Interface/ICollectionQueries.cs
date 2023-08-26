using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
    public interface ICollectionQueries
    {
        Task<List<Collection>> GetAllCollectionsAsync();
        Task<Collection> GetCollectionByIdAsync(Guid id);
        Task<Collection> GetCollectionByNameAsync(string name);
        Task<bool> CheckIfCollectionExisted(string name);
    }
}
