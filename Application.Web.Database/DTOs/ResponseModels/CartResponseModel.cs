using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
    public class CartResponseModel
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("shops")]
        public List<ShopOfCart> Shops { get; set; }
    }

    public class ShopOfCart
    {
        [JsonPropertyName("lessorId")]
        public Guid LessorId { get; set; }

        [JsonPropertyName("lessorName")]
        public string LessorName { get; set; }

        [JsonPropertyName("lessorImage")]
        public string LessorImage { get; set; }

        [JsonPropertyName("vehicles")]
        public List<VehicleOfLessor> Vehicles { get; set; }
    }

    public class VehicleOfLessor
    {
        [JsonPropertyName("vehicleId")]
        public Guid VehicleId { get; set; }

        [JsonPropertyName("vehicleName")]
        public string VehicleName { get; set; }

        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

		[JsonPropertyName("price")]
		public decimal Price { get; set; }

        [JsonPropertyName("licensePlate")]
        public string LicensePlate { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("pickUpDateTime")]
        public DateTime? PickUpDateTime { get; set; }

        [JsonPropertyName("dropOffDateTime")]
        public DateTime? DropOffDateTime { get; set; }

        [JsonPropertyName("unavailableDates")]
        public List<VehicleUnavailableDateOfCart> UnavailableDates { get; set; }
    }

    public class VehicleUnavailableDateOfCart
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
