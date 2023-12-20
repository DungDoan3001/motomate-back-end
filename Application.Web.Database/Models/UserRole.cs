using Microsoft.AspNetCore.Identity;

namespace Application.Web.Database.Models
{
	public class UserRole : IdentityUserRole<Guid>
	{
		public virtual Role Role { get; set; }
		public virtual User User { get; set; }
	}
}
