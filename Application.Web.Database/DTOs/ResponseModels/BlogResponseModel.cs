namespace Application.Web.Database.DTOs.ResponseModels
{
	public class BlogResponseModel
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string ShortDescription { get; set; }
		public DateTime CreatedAt { get; set; }
		public CategoryOfBlog Category { get; set; }
		public AuthorOfBlog Author { get; set; }
		public ImageOfBlog Image { get; set; }
	}

	public class ImageOfBlog
	{
		public string ImageUrl { get; set; }
		public string PublicId { get; set; }
	}

	public class AuthorOfBlog
	{
		public Guid AuthorId { get; set; }
		public string Username { get; set; }
		public string Picture { get; set; }
	}

	public class CategoryOfBlog
	{
		public Guid CategoryId { get; set; }
		public string Name { get; set; }
	}
}
