using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
    public interface IModelQueries
    {
        Task<List<Model>> GetAllModelsAsync();
        Task<Model> GetModelByCollectionIdAsync(Guid id);
        Task<Model> GetModelByNameAsync(string name);
        Task<bool> CheckIfModelExisted(string name);
        Task<List<Model>> GetModelsWithPaginationAync(PaginationRequestModel pagination);
        Task<int> CountModelsAysnc();
        Task<List<Model>> GetModelsByCollectionIdAsync(Guid collectionId);
	}
}
