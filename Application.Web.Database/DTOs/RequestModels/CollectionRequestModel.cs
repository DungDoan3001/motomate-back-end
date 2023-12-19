using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class CollectionRequestModel
	{
		[Required(ErrorMessage = "Collection name is required.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Brand identification is required.")]
		public Guid BrandId { get; set; }
	}
}
