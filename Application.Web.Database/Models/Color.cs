using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
    public class Color : BaseModel
    {
        [Column("name")]
        public string Name { get; set; }

        public virtual ICollection<ModelColor> ModelColors { get; set; }
    }
}
