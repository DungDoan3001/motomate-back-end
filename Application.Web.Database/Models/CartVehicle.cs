using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
    public class CartVehicle
	{
		[Column("PK_FK_cart_id")]
		public Guid CartId { get; set; }

		[Column("PK_FK_vehicle_id")]
		public Guid VehicleId { get; set; }

		public virtual Cart Cart { get; set; }
		public virtual Vehicle Vehicle { get; set; }
	}
}
