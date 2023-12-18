using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels.ChartResponseModels
{
	public class TotalRevenueChartResponseModel
	{
		[JsonPropertyName("TotalRevenue")]
		public TotalRevenueResponseModel TotalRevenue {  get; set; }

		[JsonPropertyName("totalRentedAndCompletedVehicles")]
		public TotalCompletedTripResponseModel TotalRentedAndCompletedVehicles { get; set; }
	}
}
