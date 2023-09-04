using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
    public class ResetPassword : BaseModel
    {
        [Column("FK_user_id")]
        public Guid UserId { get; set; }

        [Column("token")]
        public string Token { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; }
    }
}
