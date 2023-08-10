using Microsoft.AspNetCore.Identity;

namespace Application.Web.Database.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
