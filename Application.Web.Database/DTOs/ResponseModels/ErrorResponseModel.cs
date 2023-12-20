using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
	public class ErrorResponseModel
	{
		[JsonPropertyName("message")]
		public string Message { get; set; }

		[JsonPropertyName("statusCode")]
		public int StatusCode { get; set; }

		[JsonPropertyName("errors")]
		public List<string> Errors { get; set; }
	}
}
