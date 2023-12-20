using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class BlogCommentConfiguration : IEntityTypeConfiguration<BlogComment>
	{
		public void Configure(EntityTypeBuilder<BlogComment> builder)
		{
			builder.ToTable("table_blog_comment");

			builder
				.HasOne(x => x.User)
				.WithMany(x => x.BlogComments)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(x => x.Blog)
				.WithMany(x => x.BlogComments)
				.HasForeignKey(x => x.BlogId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
