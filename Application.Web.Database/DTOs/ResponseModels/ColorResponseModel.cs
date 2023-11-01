using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
    public class ColorResponseModel
    {

		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("color")]
		public string Color { get; set; }

        [JsonPropertyName("hexCode")]
        public string HexCode { get; set; }
    }
}
