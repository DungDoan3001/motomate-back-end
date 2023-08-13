using Microsoft.AspNetCore.Identity;

namespace Application.Web.Database.Models
{
    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string? Picture { get; set; }

        public virtual ResetPassword? ResetPassword { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
