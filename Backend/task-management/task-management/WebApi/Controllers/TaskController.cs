using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using task_management.Domain.Interfaces;
using task_management.Infrastructure.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using task_management.Application.Dtos.Request;

namespace task_management.WebApi.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly TaskDbContext _context;

        public TaskController(ITaskService taskService, TaskDbContext context)
        {
            _taskService = taskService;
            _context = context;
        }

        [HttpGet("tasks")]
        public async Task<IActionResult> Task()
        {
            var response = await _taskService.GetTasks();
            return StatusCode(response.Meta?.StatusCode ?? 200, response);
        }

        [HttpGet("tasks/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _taskService.GetTaskById(id);
            return StatusCode(response.Meta?.StatusCode ?? 200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _taskService.DeleteTask(id);
            return StatusCode(response.Meta?.StatusCode ?? 200, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskRequest taskRequest)
        {

            var response = await _taskService.CreateTask(taskRequest);
            return StatusCode(response.Meta?.StatusCode ?? 200, response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskRequest taskRequest)
        {
            var response = await _taskService.UpdateTask(id,taskRequest);
            return StatusCode(response.Meta?.StatusCode ?? 200, response);
        }

    }
}
