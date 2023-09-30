using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public sealed class ResetPasswordConfiguration : IEntityTypeConfiguration<ResetPassword>
	{
		public void Configure(EntityTypeBuilder<ResetPassword> builder)
		{
			builder.ToTable("table_reset_password");

			// ResetPassword - User
			builder.HasOne(rp => rp.User)
				.WithOne(u => u.ResetPassword)
				.HasForeignKey<ResetPassword>(rp => rp.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
