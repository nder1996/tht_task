using task_management.Application.Service;
using task_management.Domain.Exceptions;

namespace task_management.WebApi.Middleware
{

    /// <summary>
    /// Middleware que maneja respuestas con código de estado 405 (Method Not Allowed)
    /// </summary>
    public class MethodNotAllowedMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor del middleware
        /// </summary>
        /// <param name="next">Delegado para la siguiente acción en el pipeline</param>
        public MethodNotAllowedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Procesa la solicitud HTTP y maneja errores de método no permitido
        /// </summary>
        /// <param name="context">Contexto HTTP de la solicitud actual</param>
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 405)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 405;

                var errorMessage = $"El método {context.Request.Method} no está permitido para la ruta {context.Request.Path}";
                var response = ResponseApiBuilderService.Failure<object>(
                    errorCode: "METHOD_NOT_ALLOWED",
                    errorDescription: errorMessage,
                    statusCode: 405
                );

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
