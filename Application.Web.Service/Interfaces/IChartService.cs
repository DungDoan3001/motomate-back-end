using Application.Web.Database.DTOs.ResponseModels.ChartResponseModels;

namespace Application.Web.Service.Interfaces
{
	public interface IChartService
	{
		Task<TotalVehicleResponseModel> GetTotalVehiclesAsync();
		Task<TotalViewsResponseModel> GetTotalViewsAsync();
		Task<TotalUserResponseModel> GetTotalUsersAsync();
		Task<TotalProfitResponseModel> GetTotalProfitsAsync();
		Task<TotalRevenueResponseModel> GetTotalRevenueAsync(int year);
		Task<TotalCompletedTripResponseModel> GetTotalCompletedTripRequestAsync(int year);
		Task<TotalViewsInMonth> GetTotalViewsInAMonthAsync(int year, int month);
		Task<IEnumerable<TopLesseeResponseModel>> GetTopLesseesAsync();
		Task<IEnumerable<TopLessorResponseModel>> GetTopLessorsAsync();
	}
}
