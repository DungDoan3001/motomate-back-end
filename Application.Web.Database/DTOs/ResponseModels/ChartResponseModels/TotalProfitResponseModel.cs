using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels.ChartResponseModels
{
	public class TotalProfitResponseModel
	{
		[JsonPropertyName("totalProfits")]
		public decimal TotalProfits { get; set; }

		[JsonPropertyName("percentageIncreaseByLastWeek")]
		public decimal PercentageIncreaseByLastWeek { get; set; }
	}
}
