using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
	public sealed class MessageResponseModel
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("chatId")]
		public Guid ChatId { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }

		[JsonPropertyName("time")]
		public DateTime Time { get; set; }

		[JsonPropertyName("user")]
		public MemberOfMessage User { get; set; }
	}

	public sealed class MemberOfMessage
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("username")]
		public string Username { get; set; }

		[JsonPropertyName("avatar")]
		public string Avatar { get; set; }
	}

}
