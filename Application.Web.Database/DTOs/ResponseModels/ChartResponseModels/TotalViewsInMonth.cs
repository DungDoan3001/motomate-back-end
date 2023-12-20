using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels.ChartResponseModels
{
	public class TotalViewsInMonth
	{
		[JsonPropertyName("totalViews")]
		public TotalViews TotalViews { get; set; }
	}

	public class TotalViews
	{
		[JsonPropertyName("year")]
		public int Year { get; set; }

		[JsonPropertyName("month")]
		public int Month { get; set; }

		[JsonPropertyName("days")]
		public Dictionary<string, int> Days { get; set; }
	}
}
