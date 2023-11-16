using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class CheckoutOrderRequestModel
	{
		[Required]
		[JsonPropertyName("userId")]
		public Guid UserId { get; set; }

		[Required]
		[JsonPropertyName("pickUpLocation")]
		public string PickUpLocation { get; set; }

		[Required]
		[JsonPropertyName("dropOffLocation")]
		public string DropOffLocation { get; set; }

		[Required]
		[JsonPropertyName("vehicleIds")]
		public List<Guid> VehicleIds { get; set; }
	}
}
