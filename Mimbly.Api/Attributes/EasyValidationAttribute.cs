namespace Mimbly.Api.Attributes;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class EasyValidationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
            .SelectMany(v => v.Errors)
            .Select(v => v.ErrorMessage)
            .ToList();

        var responseObj = new
        {
            Message = "One or more validation errors occurred.",
            Errors = errors
        };

        context.Result = new JsonResult(responseObj)
        {
            StatusCode = 400
        };
    }
}