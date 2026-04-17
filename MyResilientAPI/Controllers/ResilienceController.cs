using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyResilientAPI.Services;
using Polly;

namespace MyResilientAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ResilienceController : ControllerBase
	{
		private readonly ResilienceService _service;

		public ResilienceController(ResilienceService service)
		{
			_service = service;
		}
		
		[HttpGet]
		public async Task<IActionResult> Retry(CancellationToken cancellationToken)
		{
			var result = await _service.DoWork(cancellationToken);
			return Ok(result);
		}
	}
}
