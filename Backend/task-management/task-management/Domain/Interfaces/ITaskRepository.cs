using task_management.Application.Dtos.Request;
using task_management.Application.Dtos.Response;
using task_management.Domain.Entities;

namespace task_management.Domain.Interfaces
{

    /// <summary>
    /// Interfaz que define las operaciones de acceso a datos para tareas
    /// </summary>
    public interface ITaskRepository
    {

        /// <summary>
        /// Obtiene todas las tareas de la base de datos
        /// </summary>
        /// <returns>Colección de entidades de tareas</returns>
        Task<IEnumerable<TasksEntity>> GetAllAsync();

        /// <summary>
        /// Busca una tarea por su identificador
        /// </summary>
        /// <param name="id">Identificador de la tarea</param>
        /// <returns>Entidad de tarea si existe, null si no</returns>
        Task<TasksEntity> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva tarea en la base de datos
        /// </summary>
        /// <param name="task">Entidad de tarea a crear</param>
        /// <returns>ID de la tarea creada</returns>
        Task<int> CreateAsync(TasksEntity task);

        /// <summary>
        /// Actualiza una tarea existente
        /// </summary>
        /// <param name="task">Entidad de tarea con datos actualizados</param>
        /// <returns>True si se actualizó correctamente, False si no</returns>
        Task<bool> UpdateAsync(TasksEntity task);

        /// <summary>
        /// Elimina una tarea por su identificador
        /// </summary>
        /// <param name="id">Identificador de la tarea a eliminar</param>
        /// <returns>True si se eliminó correctamente, False si no</returns>
        Task<bool> DeleteAsync(int id);
    }
}
