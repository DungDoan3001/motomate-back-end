using Application.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
    [Route("api/utils")]
    [ApiController]
    public class UtilitiesController : ControllerBase
    {
        private readonly IUtilitiesService _utilitiesService;

        public UtilitiesController(IUtilitiesService utilitiesService)
        {
            _utilitiesService = utilitiesService;
        }

        [HttpGet("cronjob")]
        public async Task<IActionResult> CronJobActivator()
        {
            var result = await _utilitiesService.CronJobActivator();

            return Ok(result);
        }
    }
}
