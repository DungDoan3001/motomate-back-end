namespace Application.Web.Database.DTOs.RequestModels
{
	public class WeatherForecastRequestModel
	{
		public DateTime Date { get; set; } = DateTime.UtcNow;

		public int TemperatureC { get; set; } = 32;

		public string Summary { get; set; }
	}
}
