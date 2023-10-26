using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
	{
		public void Configure(EntityTypeBuilder<UserRole> builder)
		{
			builder
				.HasOne(x => x.Role)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(r => r.RoleId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(x => x.User)
				.WithMany(u => u.UserRoles)
				.HasForeignKey(u => u.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
