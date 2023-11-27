using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class BlogCommentRequestModel
	{
		[Required]
		[JsonPropertyName("userId")]
		public Guid UserId { get; set; }

		[JsonPropertyName("comment")]
		public string Comment { get; set; }
	}
}
