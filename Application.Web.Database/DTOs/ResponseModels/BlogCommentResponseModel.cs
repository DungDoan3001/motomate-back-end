using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
	public class BlogCommentResponseModel
	{
		[JsonPropertyName("commentId")]
		public Guid CommentId { get; set; }

		[JsonPropertyName("blogId")]
		public Guid BlogId { get; set; }

		[JsonPropertyName("userId")]
		public Guid UserId { get; set; }

		[JsonPropertyName("username")]
		public string Username { get; set; }

		[JsonPropertyName("fullName")]
		public string FullName { get; set; }

		[JsonPropertyName("Email")]
		public string Email { get; set; }

		[JsonPropertyName("avatar")]
		public string Avatar { get; set; }

		[JsonPropertyName("Comment")]
		public string Comment { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }
	}
}
