using Application.Web.Database.DTOs.ResponseModels.ChartResponseModels;

namespace Application.Web.Service.Interfaces
{
	public interface IChartService
	{
		Task<TotalVehicleResponseModel> GetTotalVehiclesAsync();
		Task<TotalUserResponseModel> GetTotalUsersAsync();
		Task<TotalProfitResponseModel> GetTotalProfitsAsync();
		Task<TotalRevenueResponseModel> GetTotalRevenueAsync(int year);
		Task<TotalCompletedTripResponseModel> GetTotalCompletedTripRequestAsync(int year);
		Task<IEnumerable<TopLesseeResponseModel>> GetTopLesseesAsync();
		Task<IEnumerable<TopLessorResponseModel>> GetTopLessorsAsync();
	}
}
