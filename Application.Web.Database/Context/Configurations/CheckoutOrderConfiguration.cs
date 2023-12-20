using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class CheckoutOrderConfiguration : IEntityTypeConfiguration<CheckOutOrder>
	{
		public void Configure(EntityTypeBuilder<CheckOutOrder> builder)
		{
			builder.ToTable("table_checkout_order");

			builder
				.HasOne(x => x.User)
				.WithOne(x => x.CheckOutOrder)
				.HasForeignKey<CheckOutOrder>(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
