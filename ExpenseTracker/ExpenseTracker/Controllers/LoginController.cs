using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpenseTracker.Models.Forms;
using ExpenseTracker.Models;
using ExpenseTracker.Models.Services;
using Microsoft.AspNetCore.Authorization;
using ExpenseTracker.CustomActionFilters;
using ExpenseTracker.Helper;

namespace ExpenseTracker.Controllers
{
    public class LoginController : Controller
    {
        [Unauthenticated]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Unauthenticated]
        public async Task<IActionResult> Login(LoginForm model)
        {
            if (ModelState.IsValid)
            {
                var user = await MongoManipulator.GetObjectByField<User>("Username", model.name);
                if (user != null)
                {
                    if(Argon2.VerifyPassword(model.password, user.Password , user.Salt)) 
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.Role, "User"),
                            new Claim(ClaimTypes.NameIdentifier, user._id.ToString())
                        };
                        var claimsIndetity = new ClaimsIdentity
                        (claims,
                        CookieAuthenticationDefaults.AuthenticationScheme
                        );

                        //Authentication settings
                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,  // Refresh cookie if about to expire
                            IsPersistent = false // Cookies persist on browser reset
                        };
                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIndetity),
                            authProperties
                        );
                        Console.WriteLine("Login succes");
                        return RedirectToAction("Index", "Home");
                    }
                    Console.WriteLine("Password incorrect");
                    ModelState.AddModelError("password", "Username or password incorrect");
                    return View(model);
                }
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index" , "Home");
        }
    }
}
