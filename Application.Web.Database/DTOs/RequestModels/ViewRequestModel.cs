using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class ViewRequestModel
	{
		[JsonPropertyName("continent")]
		public string Continent { get; set; }

		[JsonPropertyName("continentGeoNameId")]
		public string ContinentGeoNameId { get; set; }

		[JsonPropertyName("country")]
		public string Country { get; set; }

		[JsonPropertyName("countryCode")]
		public string CountryCode { get; set; }

		[JsonPropertyName("countryGeoNameId")]
		public string CountryGeoNameId { get; set; }

		[JsonPropertyName("ipAddress")]
		public string IpAddress { get; set; }

		[JsonPropertyName("latitude")]
		public string Latitude { get; set; }

		[JsonPropertyName("longtitude")]
		public string Longitude { get; set; }

		[JsonPropertyName("region")]
		public string Region { get; set; }

		[JsonPropertyName("regionGeoNameId")]
		public string RegionGeoNameId { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }
	}
}
