namespace Application.Web.Database.DTOs.ServiceModels
{
	public class VehicleQuery
	{
		public string? BrandName { get; set; } = string.Empty;

		public string? ModelName { get; set; } = string.Empty;

		public string? CollectionName { get; set; } = string.Empty;

		public bool? IsSortPriceDesc { get; set; } = null;
	}
}
