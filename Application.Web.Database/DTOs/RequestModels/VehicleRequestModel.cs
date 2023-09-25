using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class VehicleRequestModel
	{
		[Required(ErrorMessage = "Owner ID is required.")]
		public Guid OwnerId { get; set; }

		[Required(ErrorMessage = "Model Id is required.")]
		public Guid ModelId { get; set; }

		[Required(ErrorMessage = "Price is required.")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Location is required.")]
		public string Location { get; set; }

		[Required(ErrorMessage = "City is required.")]
		public string City { get; set; }

		[Required(ErrorMessage = "Purchase date is required.")]
		public DateTime PurchaseDate { get; set; }

		[Required(ErrorMessage = "Condition percentage is required.")]
		public int ConditionPercentage { get; set; }

		[Required(ErrorMessage = "License plate is required.")]
		public string LicensePlate { get; set; }

		[Required(ErrorMessage = "License number is required.")]
		public string InsuranceNumber { get; set; }

		[Required(ErrorMessage = "Expiry date is required.")]
		public DateTime InsuranceExpiry { get; set; }

		[Required(ErrorMessage = "Status is required")]
		public int Status { get; set; }

		[Required(ErrorMessage = "Color is required")]
		public string ColorName { get; set; }

		public List<RequestVehicleImage> Images { get; set; }
	}

	public class RequestVehicleImage
	{
		public string ImageUrl { get; set; }
		public string PublicId { get; set; }
	}
}
