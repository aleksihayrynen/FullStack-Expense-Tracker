using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
