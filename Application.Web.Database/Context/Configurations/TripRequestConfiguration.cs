using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class TripRequestConfiguration : IEntityTypeConfiguration<TripRequest>
	{
		public void Configure(EntityTypeBuilder<TripRequest> builder)
		{
			builder.ToTable("table_trip_request");

			builder
				.HasOne(x => x.Lessee)
				.WithMany(x => x.LesseeTripRequests)
				.HasForeignKey(x => x.LesseeId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(x => x.Lessor)
				.WithMany(x => x.LessorTripRequests)
				.HasForeignKey(x => x.LessorId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(x => x.Vehicle)
				.WithMany(x => x.TripRequests)
				.HasForeignKey(x => x.VehicleId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
