using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class UserTypeType
    {
        [Key]
        public Guid UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }

        [Key]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
