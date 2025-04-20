using ExpenseTracker.Models;
using ExpenseTracker.Models.Forms;
using ExpenseTracker.Models.Services;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models.Utils;
using MongoDB.Bson.Serialization.Serializers;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Expenses()
        {

            return View();
        }

        public IActionResult Income()
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

                    // Cause form only accepts dd/mm/yyyy we add the timeof the day to get hour and DB saves all info as UTC +0
                    // When using dates in display or logic make sure to transfer UTC +0 to  Local time ;D
                    var actualDate = model.Date + DateTime.Now.TimeOfDay;  

                    if (model.Type.ToLower() == "expense")
                    {
                        var newExpense = new Expense
                        {

                            UserId = userId,
                            Title = UtilityFunctions.CapitalizeFirstLetter(model.Title),
                            Description = model.Description ?? "No description",
                            Amount = model.Amount,
                            Currency = model.Currency ?? "€",
                            Category = UtilityFunctions.CapitalizeFirstLetter(model.Category?.Trim() ?? "uncategorized"),
                            Date = actualDate
                        };
                        MongoManipulator.Save(newExpense);
                       
                    }
                    else
                    {
                        var newIncome = new Income
                        {
                            UserId = userId,
                            Title = UtilityFunctions.CapitalizeFirstLetter(model.Title),
                            Description = model.Description ?? "No description",
                            Amount = model.Amount,
                            Currency = model.Currency ?? "€",
                            Category = UtilityFunctions.CapitalizeFirstLetter(model.Category ?? "uncategorized"),
                            Date = actualDate
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
