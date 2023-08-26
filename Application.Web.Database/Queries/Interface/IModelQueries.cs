using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
    public interface IModelQueries
    {
        Task<List<Model>> GetAllModelsAsync();
        Task<Model> GetModelByIdAsync(Guid id);
        Task<Model> GetModelByNameAsync(string name);
        Task<bool> CheckIfModelExisted(string name);
    }
}
