using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class CartVehicleConfiguration : IEntityTypeConfiguration<CartVehicle>
	{
		public void Configure(EntityTypeBuilder<CartVehicle> builder)
		{
			builder.ToTable("table_cart_vehicle");

			builder.HasKey(cv => new { cv.CartId, cv.VehicleId });

			builder
				.HasOne(cv => cv.Vehicle)
				.WithMany(v => v.CartVehicles)
				.HasForeignKey(cv => cv.VehicleId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(cv => cv.Cart)
				.WithMany(c => c.CartVehicles)
				.HasForeignKey(cv => cv.CartId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
