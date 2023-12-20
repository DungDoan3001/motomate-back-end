using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class TripRequestStatusRequestModel
	{
		[JsonPropertyName("status")]
		public string Status { get; set; }

		[JsonPropertyName("requestIds")]
		public List<Guid> RequestIds { get; set; }

		[JsonPropertyName("reason")]
		public string Reason { get; set; }
	}
}
