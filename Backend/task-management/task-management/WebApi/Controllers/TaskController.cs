using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using task_management.Domain.Interfaces;
using task_management.Infrastructure.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using task_management.Application.Dtos.Request;

namespace task_management.WebApi.Controllers
{

    // <summary>
    /// Controlador que maneja las operaciones CRUD para tareas
    /// </summary>
    [Route("api/v1/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly TaskDbContext _context;

        /// <summary>
        /// Constructor del controlador que inyecta las dependencias necesarias
        /// </summary>
        /// <param name="taskService">Servicio que maneja la lógica de negocio para tareas</param>
        /// <param name="context">Contexto de base de datos para acceder a los datos</param>
        public TaskController(ITaskService taskService, TaskDbContext context)
        {
            _taskService = taskService;
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las tareas
        /// </summary>
        /// <returns>Lista de tareas con código de estado apropiado</returns>
        [HttpGet("tasks")]
        public async Task<IActionResult> Task()
        {
            var response = await _taskService.GetTasks();
            return StatusCode(response.Meta?.StatusCode ?? 200, response);
        }

        /// <summary>
        /// Obtiene una tarea por su ID
        /// </summary>
        /// <param name="id">ID de la tarea a buscar</param>
        /// <returns>Tarea encontrada con código de estado apropiado</returns>
        [HttpGet("tasks/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _taskService.GetTaskById(id);
            return StatusCode(response.Meta?.StatusCode ?? 200, response);
        }

        /// <summary>
        /// Elimina una tarea por su ID
        /// </summary>
        /// <param name="id">ID de la tarea a eliminar</param>
        /// <returns>Resultado de la operación con código de estado apropiado</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _taskService.DeleteTask(id);
            return StatusCode(response.Meta?.StatusCode ?? 200, response);
        }


        /// <summary>
        /// Crea una nueva tarea
        /// </summary>
        /// <param name="taskRequest">Datos de la tarea a crear</param>
        /// <returns>Tarea creada con código de estado apropiado</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskRequest taskRequest)
        {

            var response = await _taskService.CreateTask(taskRequest);
            return StatusCode(response.Meta?.StatusCode ?? 200, response);
        }

        /// <summary>
        /// Actualiza una tarea existente
        /// </summary>
        /// <param name="id">ID de la tarea a actualizar</param>
        /// <param name="taskRequest">Nuevos datos para la tarea</param>
        /// <returns>Tarea actualizada con código de estado apropiado</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskRequest taskRequest)
        {
            var response = await _taskService.UpdateTask(id,taskRequest);
            return StatusCode(response.Meta?.StatusCode ?? 200, response);
        }

    }
}
