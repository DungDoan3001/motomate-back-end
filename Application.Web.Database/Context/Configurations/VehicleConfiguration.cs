using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
	{
		public void Configure(EntityTypeBuilder<Vehicle> builder)
		{
			builder.ToTable("table_vehicle");

			// User (Owner) - Vehicle
			builder.HasOne(v => v.Owner)
				.WithMany(u => u.Vehicles)
				.HasForeignKey(v => v.OwnerId)
				.OnDelete(DeleteBehavior.Cascade);

			// Color - Vehicle
			builder.HasOne(v => v.Color)
				.WithMany(c => c.Vehicles)
				.HasForeignKey(v => v.ColorId)
				.OnDelete(DeleteBehavior.Cascade);

			// Model - Vehicle
			builder.HasOne(v => v.Model)
				.WithMany(m => m.Vehicles)
				.HasForeignKey(v => v.ModelId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
