using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
    public class BrandResponseModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("image")]
        public ImageOfBrand Image { get; set; }

        [JsonPropertyName("collections")]
        public IEnumerable<CollectionsOfBrand> Collections { get; set; }
    }

    public class CollectionsOfBrand
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class ImageOfBrand
    {
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("publicId")]
        public string PublicId { get; set; }
    }
}
