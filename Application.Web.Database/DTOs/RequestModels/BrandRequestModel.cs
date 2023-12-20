using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class BrandRequestModel
	{
		[Required(ErrorMessage = "Brand name is required.")]
		public string Name { get; set; }

		public string ImageUrl { get; set; }

		public string PublicId { get; set; }
	}
}
