using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class CompletedTrip : BaseModel
	{
		[Column("FK_trip_id")]
		public Guid TripId { get; set; }

		[Column("duration")]
		public TimeSpan Duration { get; set; }

		[Column("ammount")]
		public decimal Ammount { get; set; }

		public virtual TripRequest TripRequest { get; set; }
	}
}
