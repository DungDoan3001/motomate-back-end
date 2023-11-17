using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class TripRequest : BaseModel             
	{
		[Column("FK_lessee_id")]
		public Guid LesseeId { get; set; }

		[Column("FK_lessor_id")]
		public Guid LessorId { get; set; }

		[Column("FK_vehicle_id")]
		public Guid VehicleId { get; set; }

		[Column("status")]
		public bool Status { get; set; } // true = complete, false = incomplete

		[Column("pick_up_date_time")]
		public DateTime PickUpDateTime { get; set; }

		[Column("drop_off_date_time")]
		public DateTime DropOffDateTime { get; set; }

		[Column("pick_up_location")]
		public string PickUpLocation { get; set; }

		[Column("drop_off_location")]
		public string DropOffLocation { get; set; }

		[Column("payment_intent_id")]
		public string PaymentIntentId { get; set; }

		[Column("parent_order_id")]
		public string ParentOrderId { get; set; }

		[Column("created_at")]
		public DateTime Created_At { get; set; } = DateTime.UtcNow;

		public virtual User Lessee { get; set; }
		public virtual User Lessor { get; set; }
		public virtual Vehicle Vehicle { get; set; }
	}
}
