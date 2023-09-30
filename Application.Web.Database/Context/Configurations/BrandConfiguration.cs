using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public sealed class BrandConfiguration : IEntityTypeConfiguration<Brand>
	{
		public void Configure(EntityTypeBuilder<Brand> builder)
		{
			builder.ToTable("table_brand");

			// Brand - Collection
			builder.HasMany(b => b.Collections)
				.WithOne(c => c.Brand)
				.HasForeignKey(c => c.BrandId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
