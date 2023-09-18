using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
    public class VehicleImage
    {
        [Column("PK_FK_vehicle_id")]
        public Guid VehicleId { get; set; }

		[Column("PK_FK_image_id")]
		public Guid ImageId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual Image Image { get; set; }
    }
}
