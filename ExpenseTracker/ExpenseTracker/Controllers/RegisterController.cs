using ExpenseTracker.Models;
using ExpenseTracker.Models.Forms;
using ExpenseTracker.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.CustomActionFilters;
using ExpenseTracker.Helper;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace ExpenseTracker.Controllers
{
    [Unauthenticated]
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

                bool usernameTaken = userList.Any(u => u.Username.ToLower() == model.name.ToLower());
                bool emailTaken = userList.Any(u => u.Email == model.email.ToLower());

                if (usernameTaken || emailTaken)
                {
                    if (usernameTaken)
                        ModelState.AddModelError("name", "This username is already taken");
                    if (emailTaken)
                        ModelState.AddModelError("email", "This email is already registered");

                    return View(model);
                }
                else
                {
                    var generatedSalt = new byte[16];  // create empty bytes for the salt
                    RandomNumberGenerator.Fill(generatedSalt);  // Fill the salt with secure random generator

                    var newUser = new User()
                    {
                        Username = model.name.Trim().ToLower(),
                        Password = Argon2.HashPassword(model.password.Trim(), generatedSalt),
                        Email = model.email.Trim().ToLower(),
                        IsActive = true,
                        Salt = generatedSalt

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

