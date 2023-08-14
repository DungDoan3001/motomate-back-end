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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // User - ResetPassword
            builder.Entity<ResetPassword>()
                .HasOne(rp => rp.User)
                .WithOne(u => u.ResetPassword)
                .HasForeignKey<ResetPassword>(rp => rp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Brand - Collection
            builder.Entity<Brand>()
                .HasMany(b => b.Collections)
                .WithOne(c => c.Brand)
                .HasForeignKey(c => c.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            // Collection - Model
            builder.Entity<Collection>()
                .HasMany<Model>(c => c.Models)
                .WithOne(m => m.Collection)
                .HasForeignKey(m => m.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Collection - Color
            builder.Entity<ModelColor>()
                .HasKey(mc => new
                {
                    mc.ModelId,
                    mc.ColorId
                });
            builder.Entity<ModelColor>()
                .HasOne<Color>(mc => mc.Color)
                .WithMany(c => c.ModelColors)
                .HasForeignKey(c => c.ColorId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ModelColor>()
                .HasOne<Model>(mc => mc.Model)
                .WithMany(m => m.ModelColors)
                .HasForeignKey(m => m.ModelId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
            SeedDatabase(builder);

            //Delete "AspNet" name of identity table
            foreach (var entityType in builder.Model.GetEntityTypes())
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
