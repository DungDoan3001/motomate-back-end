using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels.ChartResponseModels
{
	public class TotalUserResponseModel
	{
		[JsonPropertyName("totalUsers")]
		public int TotalUsers { get; set; }

		[JsonPropertyName("percentageIncreaseByLastWeek")]
		public decimal PercentageIncreaseByLastWeek { get; set; }
	}
}
