using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class CheckOutOrderVehicle
	{
		[Column("PK_FK_checkout_id")]
		public Guid CheckoutId { get; set; }

		[Column("PK_FK_vehicle_id")]
		public Guid VehicleId { get; set; }

		public virtual CheckOutOrder CheckOutOrder { get; set; }
		public virtual Vehicle Vehicle { get; set; }
	}
}
