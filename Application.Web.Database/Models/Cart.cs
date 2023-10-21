using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
    public class Cart : BaseModel
    {
        [Column("FK_user_id")]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<CartVehicle> CartVehicles { get; set; }
    }
}
