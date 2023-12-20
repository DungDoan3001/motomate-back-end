using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class BlogCategory : BaseModel
	{
		[Column("name")]
		public string Name { get; set; }

		public virtual ICollection<Blog> Blogs { get; set; }
	}
}
