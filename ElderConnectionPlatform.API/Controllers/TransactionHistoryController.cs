using Application;
using Application.IServices;
using Application.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElderConnectionPlatform.API.Controllers
{
    [Route("api/transaction-histories")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionHistoryService _transactionHistoryService;

        public TransactionHistoryController(IUnitOfWork unitOfWork, ITransactionHistoryService transactionHistoryService)
        {
            _unitOfWork = unitOfWork;
            _transactionHistoryService = transactionHistoryService;
        }

        [HttpGet("by-account/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllTransactionHistoryByAccountId
            (string id, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _transactionHistoryService
                .GetAllTransactionHistoryByAccountIdAsync(id, pageIndex, pageSize);
            return Ok(result);
        }
    }
}
