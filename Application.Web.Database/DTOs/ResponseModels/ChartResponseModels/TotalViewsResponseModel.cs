using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels.ChartResponseModels
{
	public class TotalViewsResponseModel
	{
		[JsonPropertyName("totalViews")]
		public decimal TotalViews { get; set; }

		[JsonPropertyName("percentageIncreaseByLastWeek")]
		public decimal PercentageIncreaseByLastWeek { get; set; }
	}
}
