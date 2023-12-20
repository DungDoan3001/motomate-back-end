using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
	public interface IWeatherForcastService
	{
		IEnumerable<WeatherForecast> GetWeatherForcasts();
		WeatherForecast CreateWeatherForecast(WeatherForecastRequestModel requestModel);
	}
}