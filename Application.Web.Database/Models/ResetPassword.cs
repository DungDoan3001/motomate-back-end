using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class ResetPassword
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public virtual User? User { get; set; }
    }
}
