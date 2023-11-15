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
				.WithMany(x => x.CheckOutOrders)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
