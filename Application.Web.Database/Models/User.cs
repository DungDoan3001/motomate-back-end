using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Application.Web.Database.Models
{
    public class User : IdentityUser<Guid>
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("full_name")]
        public string FullName => FirstName + " " + LastName;

        [Column("picture")]
        public string Picture { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        public virtual ResetPassword? ResetPassword { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
