using Application.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
    [Route("api/utils")]
    [ApiController]
    public class UtilitiesController : ControllerBase
    {
        private readonly IUtilitiesService _utilitiesService;
		private readonly IChartService _chartService;

		public UtilitiesController(IUtilitiesService utilitiesService, IChartService chartService)
        {
            _utilitiesService = utilitiesService;
			_chartService = chartService;

		}

        [HttpGet("cronjob")]
        public async Task<IActionResult> CronJobActivator()
        {
            var result = await _utilitiesService.CronJobActivator();

            return Ok(result);
        }
    }
}
