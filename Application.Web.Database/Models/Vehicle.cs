using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class Vehicle : BaseModel
	{
		[Column("FK_owner_id")]
		public Guid OwnerId { get; set; }

		[Column("FK_model_id")]
		public Guid ModelId { get; set; }

		[Column("purchase_date")]
		public DateTime PurchaseDate { get; set; }

		[Column("condition_percentage")]
		public int ConditionPercentage { get; set; } = 0;

		[Column("license_plate")]
		public string LicensePlate { get; set; }

		[Column("insurance_number")]
		public string InsuranceNumber { get; set; }

		[Column("insurance_expiry")]
		public DateTime InsuranceExpiry { get; set; }

		[Column("status")]
		public int Status { get; set; } = 0; // 1-2-3 Waiting, Approve, Deny

		[Column("price")]
		public decimal Price { get; set; }

		[Column("address")]
		public string Address { get; set; }

		[Column("district")]
		public string District { get; set; }

		[Column("ward")]
		public string Ward { get; set; }

		[Column("city")]
		public string City { get; set; }

		[Column("is_active")]
		public bool IsActive { get; set; } = true;

		[Column("is_lock")]
		public bool IsLocked { get; set; } = false;

		[Column("is_available")]
		public bool IsAvailable { get; set; } = true;

		[Column("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		[Column("FK_color_id")]
		public Guid ColorId { get; set; }

		public virtual User Owner { get; set; }

		public virtual Model Model { get; set; }

		public virtual Color Color { get; set; }

		public virtual ICollection<VehicleImage> VehicleImages { get; set; }

		public virtual ICollection<CartVehicle> CartVehicles { get; set; }

		public virtual ICollection<TripRequest> TripRequests { get; set; }

		public virtual ICollection<CheckOutOrderVehicle> CheckOutOrderVehicles { get; set; }

		public virtual ICollection<VehicleReview> VehicleReviews { get; set; }
	}
}
