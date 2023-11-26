using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class VehicleReviewImage
	{
		[Column("PK_FK_vehicle_review_id")]
		public Guid VehicleReviewId { get; set; }

		[Column("PK_FK_image_id")]
		public Guid ImageId { get; set; }

		public virtual VehicleReview VehicleReview { get; set; }
		public virtual Image Image { get; set; }
	}
}
