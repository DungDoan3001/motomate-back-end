using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public sealed class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
	{
		public void Configure(EntityTypeBuilder<ChatMember> builder)
		{
			builder.ToTable("table_chat_member");

			builder.HasKey(cm => new { cm.UserId, cm.ChatId });

			builder.HasOne(cm => cm.User)
				.WithMany(u => u.ChatMembers)
				.HasForeignKey(cm => cm.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(cm => cm.Chat)
				.WithMany(c => c.ChatMembers)
				.HasForeignKey(cm => cm.ChatId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
