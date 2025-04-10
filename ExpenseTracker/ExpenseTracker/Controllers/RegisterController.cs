using ExpenseTracker.Models.Forms;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Register/Register
        [HttpPost]
        public IActionResult Register(RegisterForm model)
        {
            if (ModelState.IsValid)
            {
                // Logic for saving the user (e.g., to the database)
                // Redirect after a successful registration
                return RedirectToAction("Index", "Home");
                
            }

            // If validation fails, return the model back to the view
            Console.WriteLine("Failed registeration");
            return View(model);
        }
    }
}
