namespace task_management.Domain.Exceptions
{

    /// <summary>
    /// Excepción para métodos HTTP no permitidos (código HTTP 405)
    /// </summary>
    public class MethodNotAllowedException : ApiException
    {

        /// <summary>
        /// Constructor para excepciones de método no permitido
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error (predeterminado: "Método no permitido para este recurso")</param>
        public MethodNotAllowedException(string message = "Método no permitido para este recurso")
           : base(message, "METHOD_NOT_ALLOWED", 405)
        {
        }
    }
}
