using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class ModelRequestModel
	{
		[Required(ErrorMessage = "Model's name is required.")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Year is required.")]
		public int Year { get; set; }
		[Required(ErrorMessage = "Capacity is required.")]
		public int Capacity { get; set; }
		[Required(ErrorMessage = "Collection identification is required.")]
		public Guid CollectionId { get; set; }
		[Required(ErrorMessage = "List if color identifications if required.")]
		public List<Guid> ColorIds { get; set; }
		//[Required(ErrorMessage = "List is model color is required.")]
		//public List<ColorsOfModelRequest> Colors { get; set; }
	}

	public class ColorsOfModelRequest
	{
		[Required(ErrorMessage = "Color identification is requried.")]
		public Guid Id { get; set; }
		public string Color { get; set; }
	}
}
