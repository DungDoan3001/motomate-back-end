using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
    public interface IUtilitiesService
    {
        Task<bool> CronJobActivator();
        Task<ViewResponseModel> AddViewAsync(ViewRequestModel requestModel);
	}
}
