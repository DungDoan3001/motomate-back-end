using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels.ChartResponseModels
{
	public class TopLessorResponseModel
	{
		[JsonPropertyName("username")]
		public string Username { get; set; }

		[JsonPropertyName("fullName")]
		public string FullName { get; set; }

		[JsonPropertyName("avatar")]
		public string Avatar { get; set; }

		[JsonPropertyName("totalAmmountVehiclesForRent")]
		public int TotalAmmountVehiclesForRent { get; set; }

		[JsonPropertyName("profits")]
		public decimal Profits { get; set; }
	}
}
