using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels.ChartResponseModels
{
	public class TotalRevenueResponseModel
	{
		[JsonPropertyName("totalRevenue")]
		public RevenuePerYear TotalRevenue { get; set; }
	}

	public class RevenuePerYear
	{
		[JsonPropertyName("year")]
		public int Year { get; set; }

		[JsonPropertyName("months")]
		public Dictionary<string, decimal> Months { get; set; }
	}
}
