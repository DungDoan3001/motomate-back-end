using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class ViewConfiguration : IEntityTypeConfiguration<View>
	{
		public void Configure(EntityTypeBuilder<View> builder)
		{
			builder.ToTable("table_view");

			builder.HasIndex(e => e.IpAddress);
			builder.HasIndex(e => e.CreatedAt);
		}
	}
}
