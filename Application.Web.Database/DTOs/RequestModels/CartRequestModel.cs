using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class CartRequestModel
	{
		[JsonPropertyName("userId")]
		public Guid UserId { get; set; }

		[JsonPropertyName("vehicleId")]
		public Guid VehicleId { get; set; }

		[JsonPropertyName("pickUpDateTime")]
		public DateTime? PickUpDateTime { get; set; } = null;

		[JsonPropertyName("DropOffDateTime")]
		public DateTime? DropOffDatetime { get; set; } = null;
	}
}
