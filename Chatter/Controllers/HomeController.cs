using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Chatter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            if(!User.Identity.IsAuthenticated)
            {
                ReturnTypeEncoder RedirectToAction("Login", "User");
            }
            return View();
        }
    }
}
