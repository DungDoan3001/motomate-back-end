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

		public static Role LESSOR = new Role
		{
			Id = Guid.Parse("20308981-486a-456e-833c-1a8821121b3e"),
			Name = "Lessor",
			NormalizedName = "LESSOR"
		};

		public static Role STAFF = new Role
		{
			Id = Guid.Parse("19021360-14c6-49ab-a91b-79c388115f4e"),
			Name = "Staff",
			NormalizedName = "STAFF"
		};

		public static List<Role> DEFAULT_ROLES = new List<Role>
		{
			ADMIN, USER, LESSOR, STAFF
		};
	}
}
