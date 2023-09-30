using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class Message : BaseModel
	{
		[Column("FK_sender_id")]
		public Guid SenderId { get; set; }

		[Column("FK_chat_id")]
		public Guid ChatId { get; set; }

		[Column("content")]
		public string Content { get; set; }

		[Column("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public virtual User Sender { get; set; }
		public virtual Chat Chat { get; set; }
	}
}
