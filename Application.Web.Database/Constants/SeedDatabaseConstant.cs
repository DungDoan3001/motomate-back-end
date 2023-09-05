using Application.Web.Database.Models;

namespace Application.Web.Database.Constants
{
    public static class SeedDatabaseConstant
    {
        public static Role ADMIN = new Role
        {
            Id = Guid.Parse("60929087-1227-4efd-af43-e9ae2524eb0e"),
            Name = "Admin",
            NormalizedName = "ADMIN"
        };

        public static Role USER = new Role
        {
            Id = Guid.Parse("7e8e25ca-fd0a-4271-b7e9-fe61ffcff2c1"),
            Name = "User",
            NormalizedName = "USER"
        };

        public static List<Role> DEFAULT_ROLES = new List<Role>
        {
            ADMIN, USER
        };
    }
}
