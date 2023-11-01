using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
    public class ModelResponseModel
    {
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

        [JsonPropertyName("year")]
        public string Year { get; set; }

        [JsonPropertyName("capacity")]
        public string Capacity { get; set; }

        [JsonPropertyName("collection")]
        public CollectionOfModel Collection { get; set; }

        [JsonPropertyName("colors")]
        public List<ColorOfModel> Colors { get; set; }
    }

    public class CollectionOfModel
    {
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }
    }

    public class ColorOfModel
    {
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("color")]
		public string Color { get; set; }

        [JsonPropertyName("hexCode")]
        public string HexCode { get; set; }
    }
}
