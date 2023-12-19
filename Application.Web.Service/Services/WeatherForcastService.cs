using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;
using Application.Web.Service.Interfaces;

namespace Application.Web.Service.Services
{
	public class WeatherForcastService : IWeatherForcastService
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		public WeatherForcastService() { }

		public IEnumerable<WeatherForecast> GetWeatherForcasts()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToList();
		}

		public WeatherForecast CreateWeatherForecast(WeatherForecastRequestModel requestModel)
		{
			return new WeatherForecast
			{
				Date = requestModel.Date,
				TemperatureC = requestModel.TemperatureC,
				Summary = requestModel.Summary
			};
		}
	}
}
