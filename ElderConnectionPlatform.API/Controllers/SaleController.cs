using Application.IServices;
using Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.ResponseModels;
using Application.ViewModels.SaleViewModels;
using Application.Services;
using Infracstructures.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace ElderConnectionPlatform.API.Controllers
{
	[Route("api/sales")]
	[ApiController]
	public class SaleController : ControllerBase
	{
		private readonly ISaleService _saleService;

		public SaleController(ISaleService saleService)
		{
			_saleService = saleService;
		}

        #region Get All Sale
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllSaleAsync(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _saleService.GetAllSaleAsync(pageIndex, pageSize);
            return Ok(result);
        }
        #endregion

        #region Get Sale by Id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSaleByIdAsync(int id)
        {
            var result = await _saleService.GetSaleByIdAsync(id);
            return Ok(result);
        }
        #endregion

        #region Create sale
        [HttpPost]
        [Authorize("AdminPolicy")]
        public async Task<IActionResult> CreatedSaleAsync(SaleAddModel saleAddModel)
		{
			try
			{
				var result = await _saleService.AddSaleAsync(saleAddModel);

				return Ok(result);
			}
			catch (ArgumentException ex)
			{
				// return status code bad request for validation
				return BadRequest(new FailedResponseModel
				{
					Status = StatusCodes.Status400BadRequest,
					Message = "Invalid parameters.",
					Errors = ex.Message
				});
			}
		}
		#endregion

		#region Update Sale By Id
		[HttpPut("{id}")]
        [Authorize("AdminPolicy")]
        public async Task<IActionResult> UpdateSaleByIdAsync(int id, SaleUpdateModel saleUpdateModel)
		{
			var result = await _saleService.UpdateSaleAsync(id, saleUpdateModel);
			return Ok(result);
		}
		#endregion

		#region Remove Sale By Id
		[HttpDelete("{id}")]
        [Authorize("AdminPolicy")]
        public async Task<IActionResult> RemoveSaleByIdAsync(int id)
		{
			var result = await _saleService.RemoveSaleAsync(id);
			return Ok(result);
		}
		#endregion
	}
}
