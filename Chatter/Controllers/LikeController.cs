using Chatter.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Chatter.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddLike(int postId)
        {
            var userId = User.Identity.Name;

            if(await _likeService.IsLikedAsync(postId, userId))
            {
                await _likeService.RemoveLikeAsync(postId, userId);
                return RedirectToAction("Index","Post");
            }
            else 
            {
                await _likeService.AddLikeAsync(postId, userId);
            }

            return RedirectToAction("Index", "Post");
        }

    }
}
