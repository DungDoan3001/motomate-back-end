using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
    public class ModelColor
    {
        [Column("PK_FK_model_id")]
        public Guid ModelId { get; set; }

        [Column("PK_FK_color_id")]
        public Guid ColorId { get; set; }

        public virtual Color Color { get; set; }

        public virtual Model Model { get; set; }
    }
}
