using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
	public class View : BaseModel
	{
		[Column("continent")]
		public string Continent { get; set; }

		[Column("continent_geo_name_id")]
		public string ContinentGeoNameId { get; set; }

		[Column("country")]
		public string Country { get; set; }

		[Column("country_code")]
		public string CountryCode { get; set; }

		[Column("country_geo_name_id")]
		public string CountryGeoNameId { get; set; }

		[Column("ip_address")]
		public string IpAddress { get; set; }

		[Column("latitude")]
		public string Latitude { get; set; }

		[Column("longtitude")]
		public string Longitude { get; set; }

		[Column("region")]
		public string Region { get; set; }

		[Column("region_geo_name_id")]
		public string RegionGeoNameId { get; set; }

		[Column("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
