using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels.ChartResponseModels
{
	public class TotalCompletedTripResponseModel
	{
		[JsonPropertyName("totalCompletedTrip")]
		public TotalCompletedTrip TotalCompletedTrip { get; set; }
	}

	public class TotalCompletedTrip
	{
		[JsonPropertyName("year")]
		public int Year { get; set; }

		[JsonPropertyName("months")]
		public Dictionary<string, int> Months { get; set; }
	}
}
