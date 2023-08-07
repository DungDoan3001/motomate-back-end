using Microsoft.AspNetCore.Identity;

namespace Application.Web.Database.Constants
{
    public static class SeedDatabaseConstant
    {
        public static List<IdentityRole> DEFAULT_ROLES = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = "7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1",
                Name = "User",
                NormalizedName = "USER"
            }
        };
    }
}
