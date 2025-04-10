using ExpenseTracker.Models;
using ExpenseTracker.Models.Forms;
using ExpenseTracker.Models.Services;
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
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterForm model)
        {
            if (ModelState.IsValid)
            {
                var userList = await MongoManipulator.GetAllObjects<User>();
                // Check if username already exists
                if (userList.Any(u => u.Username == model.name))
                {
                    ModelState.AddModelError("name", "This username is already taken");
                    if (userList.Any(u => u.Email == model.email)) // Check if email works
                    {
                        ModelState.AddModelError("email", "This email is already registered");
                        return View(model);
                    }
                    return View(model);
                }
                else
                {
                    var newUser = new User()
                    {
                        Username = model.name,
                        Password = model.password,
                        Email = model.email
                    };

                    MongoManipulator.Save(newUser); // Save to DB

                    // Optional: login directly after registration or redirect
                    return RedirectToAction("Index", "Home");
                }
            }
            // If validation fails, return the model back to the view
            return View(model);
        }
    }
}

