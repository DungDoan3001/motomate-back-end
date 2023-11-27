using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class BlogComment : BaseModel
	{
		[Column("FK_user_id")]
		public Guid UserId { get; set; }

		[Column("FK_blog_id")]
		public Guid BlogId { get; set; }

		[Column("comment")]
		public string Comment { get; set; }

		[Column("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public virtual User User { get; set; }
		public virtual Blog Blog { get; set; }
	}
}
