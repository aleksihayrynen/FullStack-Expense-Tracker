using ExpenseTracker.Models;
using ExpenseTracker.Models.Forms;
using ExpenseTracker.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddItemPartial()
        {
            return PartialView("_AddItemPartial", new AddItemForm {});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItem(AddItemForm model)
        {
            if (!ModelState.IsValid || model.Amount == 0)
            {
                if (model.Amount == 0)
                    ModelState.AddModelError(nameof(model.Amount), "Amount cannot be zero");

                return PartialView("_AddItemPartial", model);
            }
            else
            {
                try
                {
                    var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                    if (string.IsNullOrEmpty(userId))
                        throw new Exception("User not authenticated.");

                    if (model.Type.ToLower() == "expense")
                    {
                        var newExpense = new Expense
                        {
                            UserId = userId,
                            Description = model.Description ?? "No description",
                            Amount = model.Amount,
                            Category = model.Category ?? "Uncategorized",
                            Date = model.Date
                        };
                        MongoManipulator.Save(newExpense);
                    }
                    else
                    {
                        var newIncome = new Income
                        {
                            UserId = userId,
                            Description = model.Description,
                            Amount = model.Amount,
                            Category = model.Category,
                            Date = model.Date
                        };
                        MongoManipulator.Save(newIncome);
                    }

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the item.\n Error:" + ex);
                    return PartialView("_AddItemPartial", model);
                }
            }
        }
    }
}
