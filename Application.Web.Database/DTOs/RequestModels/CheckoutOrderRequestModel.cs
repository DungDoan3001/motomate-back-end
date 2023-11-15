using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class CheckoutOrderRequestModel
	{
		[JsonPropertyName("userId")]
		public Guid UserId { get; set; }

		[JsonPropertyName("vehicleIds")]
		public List<Guid> VehicleIds { get; set; }
	}
}
