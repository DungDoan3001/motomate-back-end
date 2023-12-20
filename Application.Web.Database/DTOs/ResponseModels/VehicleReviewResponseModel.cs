using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
	public class VehicleReviewResponseModel
	{
		[JsonPropertyName("vehicleId")]
		public Guid VehicleId { get; set; }

		[JsonPropertyName("userId")]
		public Guid UserId { get; set; }

		[JsonPropertyName("username")]
		public string Username { get; set; }

		[JsonPropertyName("email")]
		public string Email { get; set; }

		[JsonPropertyName("avatar")]
		public string Avatar { get; set; }

		[JsonPropertyName("rating")]
		public int Rating { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("content")]
		public string Content { get; set; }

		[JsonPropertyName("images")]
		public List<string> Images { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }
	}
}
