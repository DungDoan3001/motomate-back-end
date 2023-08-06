using Application.Web.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Context
{
    public class ApplicationContext : IdentityDbContext<User>, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        // Place DbSet here:

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

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

        public DbSet<T> GetSet<T>()
            where T : class
        {
            return this.Set<T>();
        }
    }
}
