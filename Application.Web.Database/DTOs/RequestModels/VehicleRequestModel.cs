using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class VehicleRequestModel
	{
		[Required(ErrorMessage = "Owner ID is required.")]
		public Guid OwnerId { get; set; }

		[Required(ErrorMessage = "Model Id is required.")]
		public Guid ModelId { get; set; }

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

		public List<RequestVehicleImage> Images { get; set; }
	}

	public class RequestVehicleImage
	{
		public string ImageUrl { get; set; }
		public string PublicId { get; set; }
	}
}
