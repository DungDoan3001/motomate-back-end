using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
    public class Image : BaseModel
    {
        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Column("public_id")]
        public string PublicId { get; set; }

        public virtual ICollection<BrandImage> BrandImages { get; set;}

        public virtual ICollection<VehicleImage> VehicleImages { get; set; }
    }
}
