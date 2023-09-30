using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public sealed class VehicleImageConfiguration : IEntityTypeConfiguration<VehicleImage>
	{
		public void Configure(EntityTypeBuilder<VehicleImage> builder)
		{
			builder.ToTable("table_vehicle_image");

			// Image - Vehicle
			builder.HasKey(vi => new { vi.VehicleId, vi.ImageId });

			builder.HasOne(vi => vi.Vehicle)
				.WithMany(v => v.VehicleImages)
				.HasForeignKey(vi => vi.VehicleId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(vi => vi.Image)
				.WithMany(i => i.VehicleImages)
				.HasForeignKey(vi => vi.ImageId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
