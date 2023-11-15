using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public sealed class ChatConfiguration : IEntityTypeConfiguration<Chat>
	{
		public void Configure(EntityTypeBuilder<Chat> builder)
		{
			builder.ToTable("table_chat");

			builder.HasMany(c => c.Messages)
				.WithOne(m => m.Chat)
				.HasForeignKey(m => m.ChatId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
