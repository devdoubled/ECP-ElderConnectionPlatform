using Application.IServices;
using Application.ResponseModels;
using Application.Services;
using Application.ViewModels.FavoriteListViewModels;
using Application.ViewModels.JobScheduleViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElderConnectionPlatform.API.Controllers
{
    [Route("api/favorite-lists")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFavoriteListById(int id)
        {
            var favorite = await _favoriteService.GetFavoriteListByIdAsync(id);
            return favorite == null
               ? NotFound()
               : Ok(favorite);
        }

        [HttpGet("by-customer/{id}")]
        [Authorize]
        public async Task<IActionResult> GetFavoriteListByCustomerId(string id, int pageIndex = 0, int pageSize = 10)
        {
            var favorites = await _favoriteService.GetFavoriteListByCustomerIdAsync(id, pageIndex, pageSize);
            return favorites == null
               ? NotFound()
               : Ok(favorites);
        }

        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> CreateFavoriteList(FavoriteListCreateViewModel favoriteListCreateViewModel)
        {
            var result = await _favoriteService.CreateConnectorToFavoriteListAsync(favoriteListCreateViewModel);
            return Ok(result);
        }
    }
}
