using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class VehicleReview : BaseModel
	{
		[Column("FK_vehicle_id")]
		public Guid VehicleId { get; set; }

		[Column("title")]
		public string Title { get; set; }

		[Column("content")]
		public string Content { get; set; }

		[Column("rating")]
		public decimal Rating { get; set; } // From 1 -> 5

		[Column("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public virtual Vehicle Vehicle { get; set; }

		public virtual ICollection<VehicleReviewImage> VehicleReviewImages { get; set; }
	}
}
