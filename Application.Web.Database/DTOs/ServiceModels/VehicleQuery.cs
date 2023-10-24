namespace Application.Web.Database.DTOs.ServiceModels
{
	public class VehicleQuery
	{
		public List<string>? Brands { get; set; }

		public List<string>? Models { get; set; }

		public List<string>? Collections { get; set; }

		public List<string>? Cities { get; set; }

		public string? Search {  get; set; }

		public bool? IsSortPriceDesc { get; set; } = null;
	}
}
