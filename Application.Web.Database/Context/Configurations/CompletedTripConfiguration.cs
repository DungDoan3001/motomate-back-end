using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class CompletedTripConfiguration : IEntityTypeConfiguration<CompletedTrip>
	{
		public void Configure(EntityTypeBuilder<CompletedTrip> builder)
		{
			builder.ToTable("table_completed_trip");

			builder
				.HasOne(x => x.TripRequest)
				.WithOne(x => x.CompletedTrip)
				.HasForeignKey<CompletedTrip>(x => x.TripId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
