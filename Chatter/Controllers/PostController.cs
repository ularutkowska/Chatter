using Chatter.Models;
using Chatter.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Chatter.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();    
            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if(post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Date = DateTime.UtcNow;
                post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                await _postService.CreatePostAsync(post);
                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }
    }
}
