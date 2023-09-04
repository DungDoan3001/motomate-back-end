using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
    public class BrandImage
    {
        [Column("PK_FK_brand_id")]
        public Guid BrandId { get; set; }

        [Column("PK_FK_image_id")]
        public Guid ImageId { get; set; }

        public Image Image { get; set; }

        public Brand Brand { get; set; }
    }
}
