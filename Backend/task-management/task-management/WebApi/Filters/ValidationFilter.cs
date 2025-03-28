using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using task_management.Application.Dtos.Response;
using task_management.Application.Service;

namespace task_management.WebApi.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => $"{e.Key}: {e.Value.Errors.First().ErrorMessage}")
                    .ToList();

                var response = new ApiResponse<object>(
                    Meta: new Meta(StatusCode: StatusCodes.Status400BadRequest),
                    Error: new ErrorDetails(Code: "validation_error", Description: string.Join(", ", errors))
                );

                context.Result = new BadRequestObjectResult(response);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
