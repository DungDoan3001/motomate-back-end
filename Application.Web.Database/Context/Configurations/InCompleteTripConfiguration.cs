using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class InCompleteTripConfiguration : IEntityTypeConfiguration<InCompleteTrip>
	{
		public void Configure(EntityTypeBuilder<InCompleteTrip> builder)
		{
			builder.ToTable("table_in_complete_trip");

			builder
				.HasOne(x => x.TripRequest)
				.WithOne(x => x.InCompleteTrip)
				.HasForeignKey<InCompleteTrip>(x => x.TripId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
