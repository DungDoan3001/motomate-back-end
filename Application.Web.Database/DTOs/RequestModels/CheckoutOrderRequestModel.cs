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
		[JsonPropertyName("vehicles")]
		public List<VehicleToCheckOut> Vehicles { get; set; }
	}

	public class VehicleToCheckOut
	{
		[Required]
		[JsonPropertyName("vehicleId")]
		public Guid VehicleId { get; set; }

		[Required]
		[JsonPropertyName("pickUpDateTime")]
		public DateTime PickUpDateTime { get; set; }

		[Required]
		[JsonPropertyName("dropOffDateTime")]
		public DateTime DropOffDateTime { get; set; }

		[Required]
		[JsonPropertyName("pickUpLocation")]
		public string PickUpLocation { get; set; }

		[Required]
		[JsonPropertyName("dropOffLocation")]
		public string DropOffLocation { get; set; }
	}
}
