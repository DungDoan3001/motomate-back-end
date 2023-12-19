using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels.ChartResponseModels
{
	public class TopLesseeResponseModel
	{
		[JsonPropertyName("username")]
		public string Username { get; set; }

		[JsonPropertyName("fullName")]
		public string FullName { get; set; }

		[JsonPropertyName("avatar")]
		public string Avatar { get; set; }

		[JsonPropertyName("totalTripRequested")]
		public int TotalTripRequested { get; set; }

		[JsonPropertyName("totalSpent")]
		public decimal TotalSpent { get; set; }

	}
}
