using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
	{
		public void Configure(EntityTypeBuilder<BlogCategory> builder)
		{
			builder.ToTable("table_blog_category");
		}
	}
}
