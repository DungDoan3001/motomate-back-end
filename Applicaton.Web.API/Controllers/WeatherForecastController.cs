using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.Models;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
    [ApiController]
    [Route("weatherforecast")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper _mapper;
        private readonly IWeatherForcastService _weatherForcastService;
        private readonly string controllerPrefix = "WeatherForecast";

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper, IWeatherForcastService weatherForcastService)
        {
            _logger = logger;
            _mapper = mapper;
            _weatherForcastService = weatherForcastService;
        }

        /// <summary>
        /// Get the Weather forecasts information.
        /// </summary>
        /// <returns>List of weather forecast objects</returns>
        /// <response code="200">Successfully get all weather forecasts</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet(Name = "weatherforcast/all")]
        [Authorize]
        public ActionResult<IEnumerable<WeatherForecastResponseModel>> Get()
        {
            try
            {
                IEnumerable<WeatherForecast> weatherForecasts = _weatherForcastService.GetWeatherForcasts();
                IEnumerable<WeatherForecastResponseModel> result = _mapper
                    .Map<IEnumerable<WeatherForecastResponseModel>>(weatherForecasts);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at Get(): {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = {ex.Message}
                });
            }
        }

        /// <summary>
        /// Create the Weather forecast.
        /// </summary>
        /// <returns>A weather forecast created DTO.</returns>
        /// <response code="201">Successfully created the weather forecast</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpPost(Name = "weatherforecast")]
        [Authorize]
        public ActionResult<WeatherForecastResponseModel> Create(
            [FromBody] WeatherForecastRequestModel weatherForecastRequestModel)
        {
            try
            {
                WeatherForecast weatherForecast = _weatherForcastService
                    .CreateWeatherForecast(weatherForecastRequestModel);

                WeatherForecastResponseModel result = _mapper
                    .Map<WeatherForecastResponseModel>(weatherForecast);

                return Created("Weatherforecast created.", result);
            } catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at Create(): {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.Message }
                });
            }
        }
    }
}