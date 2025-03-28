using task_management.WebApi.Middleware;

namespace task_management.WebApi.Extensions
{
    /// <summary>
    /// Clase de extensión para configurar el middleware de la aplicación
    /// </summary>
    public static  class ApplicationBuilderExtensions
    {

        /// <summary>
        /// Registra el middleware de manejo global de excepciones en la tubería de la aplicación
        /// </summary>
        /// <param name="app">Builder de la aplicación</param>
        /// <returns>Instancia de IApplicationBuilder para encadenamiento</returns>
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
