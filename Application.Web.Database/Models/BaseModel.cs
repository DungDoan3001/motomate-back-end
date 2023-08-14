using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
