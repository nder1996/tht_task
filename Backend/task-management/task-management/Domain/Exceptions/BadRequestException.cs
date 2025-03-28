namespace task_management.Domain.Exceptions
{
    /// <summary>
    /// Excepción para solicitudes inválidas (código HTTP 400)
    /// </summary>
    public class BadRequestException : ApiException
    {

        /// <summary>
        /// Constructor para excepciones de solicitud inválida
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error</param>
        /// <param name="errorCode">Código de error interno (por defecto "SOLICITUD_INVALIDA")</param>
        public BadRequestException(string message, string errorCode = "SOLICITUD_INVALIDA")
           : base(message, errorCode, 400)
        {
        }
    }
}
