using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class Model : BaseModel
	{
		[Column("name")]
		public string Name { get; set; }

		[Column("year")]
		public string Year { get; set; }

		[Column("capacity")]
		public string Capacity { get; set; }

		[Column("FK_collection_id")]
		public Guid CollectionId { get; set; }

		public virtual Collection Collection { get; set; }

		public virtual ICollection<ModelColor> ModelColors { get; set; }

		public virtual ICollection<Vehicle> Vehicles { get; set; }
	}
}
