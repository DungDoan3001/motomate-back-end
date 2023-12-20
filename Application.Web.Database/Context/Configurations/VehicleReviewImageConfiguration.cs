using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class VehicleReviewImageConfiguration : IEntityTypeConfiguration<VehicleReviewImage>
	{
		public void Configure(EntityTypeBuilder<VehicleReviewImage> builder)
		{
			builder.ToTable("table_vehicle_review_image");

			builder.HasKey(x => new { x.ImageId, x.VehicleReviewId });

			builder
				.HasOne(x => x.VehicleReview)
				.WithMany(x => x.VehicleReviewImages)
				.HasForeignKey(x => x.VehicleReviewId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(x => x.Image)
				.WithMany(x => x.VehicleReviewImages)
				.HasForeignKey(x => x.ImageId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
