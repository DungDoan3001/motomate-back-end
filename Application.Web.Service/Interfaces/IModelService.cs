using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
	public interface IModelService
	{
		Task<IEnumerable<Model>> GetAllModelsAsync();
		Task<Model> GetModelByIdAsync(Guid modelId);
		Task<Model> CreateModelAsync(ModelRequestModel requestModel);
		Task<Model> UpdateModelAsync(ModelRequestModel requestModel, Guid modelId);
		Task<bool> DeleteModelAsync(Guid modelId);
		Task<(IEnumerable<Model>, PaginationMetadata)> GetModelsAsync(PaginationRequestModel pagination);
		Task<IEnumerable<Model>> GetModelsByCollectionIdAsync(Guid collectionId);

	}
}
