using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseTracker.CustomActionFilters;

/*
 A class made to stop logged in aka Authenticated users from accessing the register page.
 */ 

public class Unauthenticated : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.User.Identity != null && context.HttpContext.User.Identity.IsAuthenticated ) 
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
        base.OnActionExecuting(context);
    }
}
