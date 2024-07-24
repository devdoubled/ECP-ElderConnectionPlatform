using Application;
using Application.IServices;
using Application.ResponseModels;
using Application.ViewModels.JobScheduleViewModels;
using Application.ViewModels.PostViewModels;
using Application.ViewModels.MultiRequestViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using Application.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ElderConnectionPlatform.API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostService _postService;

        public PostController(IUnitOfWork unitOfWork, IPostService postService)
        {
            _unitOfWork = unitOfWork;
            _postService = postService;
        }
        #region Get post by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            try
            {
                var post = await _postService.GetPostByIdAsync(id);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(new FailedResponseModel
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Bad request.",
                    Errors = ex.Message
                });
            }
        }
        #endregion

        #region Get post by customer id
        [HttpGet("by-customer/{id}")]
        public async Task<IActionResult> GetPostByCustomerId(string id, int pageIndex = 0, int pageSize = 10)
        {
            var posts = await _postService.GetAllPostByCustomerIdAsync(id, pageIndex, pageSize);

            return posts == null
                ? throw new NotExistsException()
                : (IActionResult)Ok(posts);
        }
        #endregion

        #region Get all posts
        [HttpGet("status")]
        public async Task<IActionResult> GetAllPostsByStatus(int status, int pageIndex = 0, int pageSize = 10)
        {
            var posts = await _postService.GetAllPostListByStatusPaginationAsync(status, pageIndex, pageSize);

            return (posts == null) ? NotFound() : Ok(posts);

        }
        #endregion

        #region Check if post is expired
        [HttpGet("check-post-expired/{id}")]
        public async Task<IActionResult> CheckPostExpired(int id)
        {
            var result = await _postService.CheckIfPostIsexpired(id);
            return (result == null)
                ? NotFound()
                : Ok(result);
        }
        #endregion

        #region Create post
        [HttpPost()]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
        {
            try
            {
                var result = await _postService.CreatePostAsync
                (request.PostCreateViewModel, request.JobScheduleCreateViewModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new FailedResponseModel
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Bad request.",
                    Errors = ex.Message
                });
            }

        }
        #endregion

        #region Apply post
        [HttpPost("apply-post/{id}")]
        public async Task<IActionResult> ApplyPost(int id, [Required] string connectorId)
        {
            try
            {
                var result = await _postService.ApplyPost(id, connectorId);
                return (result == null)
                    ? NotFound()
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new FailedResponseModel
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Bad request.",
                    Errors = ex.Message
                });
            }
        }
        #endregion

        #region Update post by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id,
            [FromBody] UpdatePostRequest request)
        {
            var result = await _postService.UpdatePostAsync(
                id,
                request.PostUpdateViewModel,
                request.JobScheduleUpdateViewModel);
            return Ok(result);
        }
        #endregion

        #region Delete post
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _postService.DeletePostAsync(id);
            return Ok(result);
        }
        #endregion

    }
}
