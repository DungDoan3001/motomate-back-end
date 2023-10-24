using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class Blog : BaseModel
	{
		[Column("title")]
		public string Title { get; set; }

		[Column("content")]
		public string Content { get; set; }

		[Column("short_description")]
		public string ShortDescription { get; set; }

		[Column("created_at")]
		public DateTime Created_At { get; set; }

		[Column("FK_author_id")]
		public Guid AuthorId { get; set; }

		[Column("FK_image_id")]
		public Guid ImageId { get; set; }

		[Column("FK_category_id")]
		public Guid CategoryId { get; set; }

		public virtual User Author { get; set; }
		public virtual Image Image { get; set; }
		public virtual BlogCategory Category { get; set; }
	}
}
