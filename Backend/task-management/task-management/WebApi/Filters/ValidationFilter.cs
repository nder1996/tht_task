using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using task_management.Application.Dtos.Response;
using task_management.Application.Service;

namespace task_management.WebApi.Filters
{

    /// <summary>
    /// Filtro que valida el estado del modelo antes de ejecutar acciones del controlador
    /// </summary>
    public class ValidationFilter : IActionFilter
    {

        /// <summary>
        /// Se ejecuta antes de la acción del controlador para validar el modelo
        /// </summary>
        /// <param name="context">Contexto de la ejecución de la acción</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Recopila todos los errores de validación del modelo

                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => $"{e.Key}: {e.Value.Errors.First().ErrorMessage}")
                    .ToList();

                // Crea una respuesta de API estandarizada con los errores

                var response = new ApiResponse<object>(
                    Meta: new Meta(StatusCode: StatusCodes.Status400BadRequest),
                    Error: new ErrorDetails(Code: "validation_error", Description: string.Join(", ", errors))
                );

                // Establece el resultado como BadRequest con la respuesta formateada

                context.Result = new BadRequestObjectResult(response);
            }
        }

        /// <summary>
        /// Se ejecuta después de la acción del controlador
        /// </summary>
        /// <param name="context">Contexto de la ejecución de la acción</param>
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
