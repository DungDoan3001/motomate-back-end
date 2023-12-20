using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class BlogRequestModel
	{
		[Required(ErrorMessage = "Title is required")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Content is required")]
		public string Content { get; set; }

		[Required(ErrorMessage = "ShortDescription is required")]
		public string ShortDescription { get; set; }

		[Required(ErrorMessage = "ImageUrl is required")]
		public string ImageUrl { get; set; }

		[Required(ErrorMessage = "PublicId is required")]
		public string PublicId { get; set; }

		[Required(ErrorMessage = "AuthorId is required")]
		public Guid AuthorId { get; set; }

		[Required(ErrorMessage = "CategoryId is required")]
		public Guid CategoryId { get; set; }
	}
}
