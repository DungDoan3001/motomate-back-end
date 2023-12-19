using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public sealed class BrandImageConfiguration : IEntityTypeConfiguration<BrandImage>
	{
		public void Configure(EntityTypeBuilder<BrandImage> builder)
		{
			builder.ToTable("table_brand_image");

			// Brand - Image
			builder.HasKey(bi => new { bi.BrandId, bi.ImageId });

			builder.HasOne(bi => bi.Brand)
				.WithMany(b => b.BrandImages)
				.HasForeignKey(bi => bi.BrandId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(bi => bi.Image)
				.WithMany(i => i.BrandImages)
				.HasForeignKey(bi => bi.ImageId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
