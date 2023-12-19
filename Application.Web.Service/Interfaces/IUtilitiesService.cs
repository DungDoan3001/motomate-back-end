using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;

namespace Application.Web.Service.Interfaces
{
	public interface IUtilitiesService
	{
		Task<bool> CronJobActivator();
		Task<ViewResponseModel> AddViewAsync(ViewRequestModel requestModel);
	}
}
