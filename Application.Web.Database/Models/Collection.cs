using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class Collection : BaseModel
	{
		[Column("name")]
		public string Name { get; set; }

		[Column("FK_brand_id")]
		public Guid BrandId { get; set; }

		public virtual Brand Brand { get; set; }

		public virtual ICollection<Model> Models { get; set; }
	}
}
