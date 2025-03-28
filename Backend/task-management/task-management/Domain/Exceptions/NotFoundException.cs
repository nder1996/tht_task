namespace task_management.Domain.Exceptions
{

    /// <summary>
    /// Excepción para recursos no encontrados (código HTTP 404)
    /// </summary>
    public class NotFoundException : ApiException
    {

        /// <summary>
        /// Constructor para excepciones de recurso no encontrado
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error</param>
        /// <param name="errorCode">Código de error interno (por defecto "RECURSO_NO_ENCONTRADO")</param>
        public NotFoundException(string message, string errorCode = "RECURSO_NO_ENCONTRADO")
            : base(message, errorCode, 404)
        {
        }
    }
}
