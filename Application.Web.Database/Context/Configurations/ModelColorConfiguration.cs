using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public sealed class ModelColorConfiguration : IEntityTypeConfiguration<ModelColor>
	{
		public void Configure(EntityTypeBuilder<ModelColor> builder)
		{
			builder.ToTable("table_model_color");

			// Model - Color
			builder.HasKey(mc => new { mc.ModelId, mc.ColorId });

			builder.HasOne(mc => mc.Color)
				.WithMany(c => c.ModelColors)
				.HasForeignKey(c => c.ColorId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(mc => mc.Model)
				.WithMany(m => m.ModelColors)
				.HasForeignKey(m => m.ModelId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
