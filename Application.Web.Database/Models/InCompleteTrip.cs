using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class InCompleteTrip : BaseModel
	{
		[Column("FK_trip_id")]
		public Guid TripId { get; set; }

		[Column("reason")]
		public string Reason { get; set; }

		[Column("cancel_time")]
		public DateTime CancelTime { get; set; } = DateTime.UtcNow;

		public virtual TripRequest TripRequest { get; set; }
	}
}
