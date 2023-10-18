using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class Chat : BaseModel
	{
		[Column("last_updated_at")]
		public DateTime LastUpdatedAt { get; set; }

		public virtual ICollection<ChatMember> ChatMembers { get; set; }
		public virtual ICollection<Message> Messages { get; set; }
	}
}
