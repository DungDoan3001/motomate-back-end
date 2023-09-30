using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class ChatMember
	{
		[Column("PK_FK_chat_id")]
		public Guid ChatId { get; set; }

		[Column("PK_FK_user_id")]
		public Guid UserId { get; set; }

		public virtual Chat Chat { get; set; }
		public virtual User User { get; set; }
	}
}
