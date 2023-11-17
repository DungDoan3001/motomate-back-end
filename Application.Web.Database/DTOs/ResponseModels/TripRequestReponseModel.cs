﻿using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
	public class TripRequestReponseModel
	{
		[JsonPropertyName("parentOrderId")]
		public string ParentOrderId { get; set; }

		[JsonPropertyName("userId")]
		public Guid UserId { get; set; }

		[JsonPropertyName("username")]
		public string UserName { get; set; }

		[JsonPropertyName("shops")]
		public List<ShopOfTripRequest> Shops { get; set; }
	}

	public class ShopOfTripRequest
	{
		[JsonPropertyName("lessorId")]
		public Guid LessorId { get; set; }

		[JsonPropertyName("lessorName")]
		public string LessorName { get; set; }

		[JsonPropertyName("lessorImage")]
		public string LessorImage { get; set; }

		[JsonPropertyName("vehicles")]
		public List<VehicleOfLessorOfTripRequest> Vehicles { get; set; }
	}

	public class VehicleOfLessorOfTripRequest
	{
		[JsonPropertyName("vehicleId")]
		public Guid VehicleId { get; set; }

		[JsonPropertyName("vehicleName")]
		public string VehicleName { get; set; }

		[JsonPropertyName("pickUpLocation")]
		public string PickUpLocation { get; set; }

		[JsonPropertyName("dropOffLocation")]
		public string DropOffLocation { get; set; }

		[JsonPropertyName("pickUpDateTime")]
		public DateTime PickUpDateTime { get; set; }

		[JsonPropertyName("dropOffDateTime")]
		public DateTime DropOffDateTime { get; set; }

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
	}
}
