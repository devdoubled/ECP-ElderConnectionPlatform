using Application;
using Application.Exceptions;
using Application.IServices;
using Application.ResponseModels;
using Application.Services;
using Application.ViewModels.AddressViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace ElderConnectionPlatform.API.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressService _addressService;

        public AddressController(IUnitOfWork unitOfWork, IAddressService addressService)
        {
            _unitOfWork = unitOfWork;
            _addressService = addressService;
        }
        #region Get Account Address By Account Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceTypeById(string id, int pageSize = 10, int pageIndex = 0)
        {
            var result = await _addressService.GetAccountAddressByAccountIdAsync(id, pageSize, pageIndex);
            return Ok(result);
        }
        #endregion

        #region Create account address
        [HttpPost()]
        public async Task<IActionResult> CreatedAddress(AddressAddModel addressAddModel)
        {
            try
            {
                var result = await _addressService.AddAccountAddressAsync(addressAddModel);

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

        #region Update Account Address By Id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccountAddressAsync(int id, AddressUpdateModel addressUpdateModel)
        {
            var result = await _addressService.UpdateAccountAddressAsync(id, addressUpdateModel);
            return Ok(result);
        }
        #endregion 

        #region Delete Account Address By Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountAddressAsync(int id)
        {
            var result = await _addressService.DeleteccountAddressAsync(id);
            return Ok(result);
        }
        #endregion

    }
}
