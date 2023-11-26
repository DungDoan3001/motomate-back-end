using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class VehicleReviewConfiguration : IEntityTypeConfiguration<VehicleReview>
	{
		public void Configure(EntityTypeBuilder<VehicleReview> builder)
		{
			builder.ToTable("table_vehicle_review");

			builder
				.HasOne(x => x.Vehicle)
				.WithMany(x => x.VehicleReviews)
				.HasForeignKey(x => x.VehicleId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
