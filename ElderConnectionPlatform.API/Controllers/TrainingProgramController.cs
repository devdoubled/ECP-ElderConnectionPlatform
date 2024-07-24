using Application.IServices;
using Application.ResponseModels;
using Application.Services;
using Application.ViewModels.TrainingProgramViewModels;
using Infracstructures.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElderConnectionPlatform.API.Controllers
{
	[Route("api/trainning-programs")]
	[ApiController]
	public class TrainingProgramController : ControllerBase
	{
		private readonly ITrainingProgramService _trainingProgramService;

		public TrainingProgramController(ITrainingProgramService trainingProgramService)
		{
			_trainingProgramService = trainingProgramService;
		}


		#region Get All Training Program
		[HttpGet]
        [Authorize("ConnectorPolicy")]
        public async Task<IActionResult> GetAllTrainingProgramAsync(int pageIndex = 0, int pageSize = 10)
		{
			var result = await _trainingProgramService.GetAllTrainingProgramAsync(pageIndex, pageSize);
			return Ok(result);
		}
        #endregion

        #region Get Training Program Detail By Id
        [HttpGet("{id}")]
        [Authorize("ConnectorPolicy")]
        public async Task<IActionResult> GetTrainingProgramDetailByIdAsync(int id)
        {
            var result = await _trainingProgramService.GetTrainingProgramDetailAsync(id);
            return Ok(result);
        }
        #endregion

        #region Create Training Program
        [HttpPost]
        [Authorize("ConnectorPolicy")]
        public async Task<IActionResult> CreatedTrainingProgramAsync(TrainingProgramAddModel trainingProgramAddModel)
		{
			try
			{
				var result = await _trainingProgramService.AddTrainingProgramAsync(trainingProgramAddModel);

				return Ok(result);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new FailedResponseModel
				{
					Status = StatusCodes.Status400BadRequest,
					Message = "Invalid parameters.",
					Errors = ex.Message
				});
			}
		}
		#endregion

		#region Update Training Program By Id
		[HttpPut("{id}")]
        [Authorize("ConnectorPolicy")]
        public async Task<IActionResult> UpdateTrainingProgramByIdAsync(int id, TrainingProgramUpdateModel trainingProgramUpdateModel)
		{
			var result = await _trainingProgramService.UpdateTrainingProgramAsync(id, trainingProgramUpdateModel);
			return Ok(result);
		}
		#endregion

		#region Remove Training Program By Id
		[HttpDelete("{id}")]
        [Authorize("AdminPolicy")]
        public async Task<IActionResult> RemoveTrainingProgramIdByIdAsync(int id)
		{
			var result = await _trainingProgramService.RemoveTrainingProgramAsync(id);
			return Ok(result);
		}
        #endregion

    }
}
