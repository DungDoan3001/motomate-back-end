using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	internal class CartConfiguration : IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.ToTable("table_cart");

			builder
				.HasOne(c => c.User)
				.WithMany(u => u.Carts)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
