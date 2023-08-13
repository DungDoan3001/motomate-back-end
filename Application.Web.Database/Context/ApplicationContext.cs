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
        public DbSet<ResetPassword> ResetPassword { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResetPassword>()
                .HasOne(rp => rp.User)
                .WithOne(u => u.ResetPassword)
                .HasForeignKey<ResetPassword>(rp => rp.UserId);

            base.OnModelCreating(modelBuilder);
            SeedDatabase(modelBuilder);

            //Delete "AspNet" name of identity table
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tablename = entityType.GetTableName();
                if (tablename.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tablename.Substring(6));
                }
            }
        }

        private void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                            .HasData(SeedDatabaseConstant.DEFAULT_ROLES);
        }

        public DbSet<T> GetSet<T>()
            where T : class
        {
            return this.Set<T>();
        }
    }
}
