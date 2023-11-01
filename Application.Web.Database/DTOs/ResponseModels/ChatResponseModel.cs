using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
	public sealed class ChatResponseModel
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("members")]
		public List<MemberOfChat> Members { get; set; }

		[JsonPropertyName("latestMessage")]
		public MessageResponseModel LatestMessage { get; set; }
	}

	public sealed class MemberOfChat
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("username")]
		public string Username { get; set; }

		[JsonPropertyName("avatar")]
		public string Avatar { get; set; }
	}
}
