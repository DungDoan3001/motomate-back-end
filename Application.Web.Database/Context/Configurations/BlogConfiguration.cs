using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class BlogConfiguration : IEntityTypeConfiguration<Blog>
	{
		public void Configure(EntityTypeBuilder<Blog> builder)
		{
			builder.ToTable("table_blog");

			// Blog - category
			builder
				.HasOne(b => b.Category)
				.WithMany(c => c.Blogs)
				.HasForeignKey(b => b.CategoryId)
				.OnDelete(DeleteBehavior.Cascade);

			// Blog - Author
			builder
				.HasOne(b => b.Author)
				.WithMany(a => a.Blogs)
				.HasForeignKey(b => b.AuthorId)
				.OnDelete(DeleteBehavior.Cascade);

			// Blog - Image
			builder
				.HasOne(b => b.Image)
				.WithOne(i => i.Blog)
				.HasForeignKey<Blog>(b => b.ImageId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
