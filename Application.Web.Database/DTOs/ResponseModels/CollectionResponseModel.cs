using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
	public class CollectionResponseModel
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("models")]
		public IEnumerable<ModelsOfCollection> Models { get; set; }

		[JsonPropertyName("brand")]
		public BrandOfCollection Brand { get; set; }
	}

	public class ModelsOfCollection
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }
	}

	public class BrandOfCollection
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}
