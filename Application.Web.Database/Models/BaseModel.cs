using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
    public class BaseModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
