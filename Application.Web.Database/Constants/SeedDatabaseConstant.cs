using Application.Web.Database.Models;

namespace Application.Web.Database.Constants
{
    public static class SeedDatabaseConstant
    {
        public static List<Role> DEFAULT_ROLES = new List<Role>
        {
            new Role
            {
                Id = Guid.Parse("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
                Name = "User",
                NormalizedName = "USER"
            }
        };
    }
}
