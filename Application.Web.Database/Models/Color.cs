using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
    public class Color : BaseModel
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("hex_code")]
        public string HexCode { get; set; }

        public virtual ICollection<ModelColor> ModelColors { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
