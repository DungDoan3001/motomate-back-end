using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels.ChartResponseModels
{
	public class TotalVehicleResponseModel
	{
		[JsonPropertyName("totalVehicles")]
		public int TotalVehicles { get; set; }

		[JsonPropertyName("percentageIncreaseByLastWeek")]
		public decimal PercentageIncreaseByLastWeek { get; set; }
	}
}
