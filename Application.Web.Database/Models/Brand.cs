using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class Brand : BaseModel
	{
		[Column("name")]
		public string Name { get; set; }

		[Column("FK_image_id")]
		public Guid ImageId { get; set; }

		public virtual ICollection<Collection> Collections { get; set; }

		public virtual ICollection<BrandImage> BrandImages { get; set; }
	}
}
