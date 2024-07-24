using Application.IServices;
using Application.ViewModels.ElderInformationViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElderConnectionPlatform.API.Controllers
{
	[Route("api/elder-infos")]
	[ApiController]
	public class ElderInformationController : ControllerBase
	{
		private readonly IElderInformationService _elderInformationService;

		public ElderInformationController(IElderInformationService elderInformationService)
		{
			_elderInformationService = elderInformationService;
		}

		#region Get Elder Information by Child Id
		[HttpGet("by-child/{id}")]
		public async Task<IActionResult> GetElderInformationByChildIdAsync(string id)
		{
			var result = await _elderInformationService.GetElderInformationByChildIdAsync(id);
			return Ok(result);
		}
		#endregion

		#region Update Elder Information
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateElderInformationAsync(int id, ElderInformationUpdateModel elderInformationUpdateModel)
		{
			var result = await _elderInformationService.UpdateElderInformationAsync(id, elderInformationUpdateModel);
			return Ok();
		}
		#endregion
	}
}
