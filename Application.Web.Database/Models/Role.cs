using Microsoft.AspNetCore.Identity;

namespace Application.Web.Database.Models
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<UserRole> UserRoles { get; set;}
    }
}
