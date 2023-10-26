using System.Runtime.CompilerServices;
using Application.Web.Database.Constants;
using Application.Web.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Context
{
    public class ApplicationContext : IdentityDbContext<User, Role, Guid>, IApplicationContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }

        // Place DbSet here:
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandImage> BrandImages { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<ModelColor> ModelColors { get; set; }
        public DbSet<ResetPassword> ResetPasswords { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleImage> VehicleImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartVehicle> CartVehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            base.OnModelCreating(builder);

			SeedDatabase(builder);

            //Delete "AspNet" name of identity table
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tablename = entityType.GetTableName();
                if (tablename.StartsWith("AspNet"))
                {
                    entityType.SetTableName($"table_{tablename[6..].ToLower()}");
                }
            }
        }

        private static void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(SeedDatabaseConstant.DEFAULT_ROLES);
        }

        public DbSet<T> GetSet<T>()
            where T : class
        {
			return this.Set<T>();
        }
    }
}
