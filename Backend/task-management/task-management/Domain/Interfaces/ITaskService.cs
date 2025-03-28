using task_management.Application.Dtos.Request;
using task_management.Application.Dtos.Response;

namespace task_management.Domain.Interfaces
{
    /// <summary>
    /// Interfaz que define los servicios de negocio para la gestión de tareas
    /// </summary>
    public interface ITaskService
    {

        /// <summary>
        /// Obtiene todas las tareas disponibles
        /// </summary>
        /// <returns>Respuesta con colección de tareas</returns>
        Task<ApiResponse<IEnumerable<TaskResponse>>> GetTasks();

        /// <summary>
        /// Obtiene una tarea específica por su identificador
        /// </summary>
        /// <param name="id">Identificador de la tarea</param>
        /// <returns>Respuesta con los datos de la tarea</returns>
        Task<ApiResponse<TaskResponse>> GetTaskById(int id);

        /// <summary>
        /// Crea una nueva tarea en el sistema
        /// </summary>
        /// <param name="task">Datos de la tarea a crear</param>
        /// <returns>Respuesta con mensaje de confirmación</returns>
        Task<ApiResponse<string>> CreateTask(TaskRequest task);

        /// <summary>
        /// Actualiza una tarea existente
        /// </summary>
        /// <param name="id">Identificador de la tarea a actualizar</param>
        /// <param name="task">Nuevos datos para la tarea</param>
        /// <returns>Respuesta con mensaje de confirmación</returns>
        Task<ApiResponse<string>> UpdateTask(int id , TaskRequest task);

        /// <summary>
        /// Elimina una tarea por su identificador
        /// </summary>
        /// <param name="id">Identificador de la tarea a eliminar</param>
        /// <returns>Respuesta con mensaje de confirmación</returns>
        Task<ApiResponse<string>> DeleteTask(int id);
    }
}
