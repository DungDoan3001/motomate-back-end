using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class CheckOutOrderVehicleConfiguration : IEntityTypeConfiguration<CheckOutOrderVehicle>
	{
		public void Configure(EntityTypeBuilder<CheckOutOrderVehicle> builder)
		{
			builder.ToTable("table_checkout_order_vehicle");

			builder.HasKey(x => new { x.CheckoutId, x.VehicleId });

			builder
				.HasOne(x => x.CheckOutOrder)
				.WithMany(x => x.CheckOutOrderVehicles)
				.HasForeignKey(x => x.CheckoutId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(x => x.Vehicle)
				.WithMany(x => x.CheckOutOrderVehicles)
				.HasForeignKey(x => x.VehicleId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
