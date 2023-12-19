using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
	[Route("")]
	[ApiController]
	public class DefaultController : ControllerBase
	{
		public DefaultController()
		{

		}

		[Route(""), HttpGet]
		[ApiExplorerSettings(IgnoreApi = true)]
		public RedirectResult RedirectToSwaggerUi()
		{
			return Redirect("/swagger/");
		}
	}
}
