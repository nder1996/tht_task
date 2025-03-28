namespace task_management.Domain.Exceptions
{

    /// <summary>
    /// Excepción personalizada para manejar errores de la API
    /// </summary>
    public class ApiException : Exception
    {

        /// <summary>
        /// Código de estado HTTP asociado con la excepción
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Código de error interno para identificar el tipo de error
        /// </summary>
        public string ErrorCode { get; }


        /// <summary>
        /// Constructor de la excepción de API
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error</param>
        /// <param name="errorCode">Código de error interno (por defecto "ERROR_INTERNO")</param>
        /// <param name="statusCode">Código de estado HTTP (por defecto 500 - Error interno del servidor)</param>
        public ApiException(string message, string errorCode = "ERROR_INTERNO", int statusCode = 500)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}

