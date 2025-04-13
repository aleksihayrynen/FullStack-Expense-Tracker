using ExpenseTracker.Models.Forms;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult AddItemPartial()
        {
            var model = new AddItemForm() { Amount = 0}; // Use a default model if necessary
            return PartialView("_AddItemPartial", model);
        }

        [HttpPost]
        public IActionResult AddItem(AddItemForm model)
         {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Worked");
                // Handle saving the item
                return RedirectToAction("Index"); // or another appropriate action
            }
            Console.WriteLine("Didnt work");
            return PartialView("_AddItemPartial" ,model ); // In case of error, stay on the same page
        }

    }
}
