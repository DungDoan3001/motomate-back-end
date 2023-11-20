using Application.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/mock")]
	[ApiController]
	public class MockDataController : ControllerBase
	{
		private readonly IMockDataService _mockDataService;

		public MockDataController(IMockDataService mockDataService)
		{
			_mockDataService = mockDataService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var result = await _mockDataService.MockDataAsync();

				if(result)
				{
					return Ok(result);
				} else 
				{ 
					return BadRequest(); 
				}
			}
			catch (Exception ex)
			{
				return Ok(ex);
			}
		}
	}
}
