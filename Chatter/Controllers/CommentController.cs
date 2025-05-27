using Chatter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Chatter.Services;

namespace Chatter.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public CommentController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int postId)
        {
            var comments = await _commentService.GetCommentByIdAsync(postId);
            return View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int postId, string content)
        {
            if(string.IsNullOrEmpty(content))
            {
                ModelState.AddModelError("Content", "Comment is empty");
                return RedirectToAction("Index", new {postId});
            }

            var comment = new Comment
            {
                Content = content,
                PostId = postId,
                UserId = User.Identity.Name,
                Date = DateTime.Now
            };

            await _commentService.CreateCommentAsync(comment);

            return RedirectToAction("Index", "Post");
        }
    }
}
