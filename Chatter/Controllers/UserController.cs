using Chatter.Models;
using Chatter.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Chatter.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;

        public UserController(IUserService userService, SignInManager<User> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string email)
        {
            var user = new User { UserName = username, Email = email };
            var result = await _userService.CreateUserAsync(user, password);

            if(result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _userService.SignInUserAsync(username, password);

            if(result.Succeeded)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
