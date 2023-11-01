using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
	public class BlogResponseModel
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("name")]
		public string Title { get; set; }

		[JsonPropertyName("content")]
		public string Content { get; set; }

		[JsonPropertyName("shortDescription")]
		public string ShortDescription { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		[JsonPropertyName("category")]
		public CategoryOfBlog Category { get; set; }

		[JsonPropertyName("author")]
		public AuthorOfBlog Author { get; set; }

		[JsonPropertyName("image")]
		public ImageOfBlog Image { get; set; }
	}

	public class ImageOfBlog
	{
		[JsonPropertyName("imageUrl")]
		public string ImageUrl { get; set; }

		[JsonPropertyName("publicId")]
		public string PublicId { get; set; }
	}

	public class AuthorOfBlog
	{
		[JsonPropertyName("authorId")]
		public Guid AuthorId { get; set; }

		[JsonPropertyName("username")]
		public string Username { get; set; }

		[JsonPropertyName("picture")]
		public string Picture { get; set; }
	}

	public class CategoryOfBlog
	{
		[JsonPropertyName("categoryId")]
		public Guid CategoryId { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}
