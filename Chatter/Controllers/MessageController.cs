using Chatter.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Chatter.Models;


namespace Chatter.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IActionResult> Chat(string withUserId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var conversation = await _messageService.GetConversationAsync(currentUserId, withUserId);

            ViewBag.ReceiverId = withUserId;
            return View(conversation);
        }

        public async Task<IActionResult> Send(string reveiverId, string content)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var message = new Message
            {
                SenderId = currentUserId,
                Content = content,
                ReceiverId = reveiverId
            };

            await _messageService.SendMessageAsync(message);
            return RedirectToAction("Chat", new { withUserId = reveiverId });
        }
    }

}
