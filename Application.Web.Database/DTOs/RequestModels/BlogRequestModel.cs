namespace Application.Web.Database.DTOs.RequestModels
{
	public class BlogRequestModel
	{
		public string Title { get; set; }
		public string Content { get; set; }
		public string ShortDescription { get; set; }
		public string ImageUrl { get; set; }
		public string PublicId { get; set; }
		public Guid AuthorId { get; set; }
		public Guid CategoryId { get; set; }
	}
}
