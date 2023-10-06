using Microsoft.AspNetCore.Mvc;
using Todo.Repositories.Entities;
using Todo.Services.Interfaces;

namespace Todo.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly ILogger<TodoController> _logger;

    public TodoController(ILogger<TodoController> logger, ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<ToDo>>> GetAllTasks()
    {
        var todos = await _todoService.GetAllToDosAsync();
        _logger.LogInformation($"Returned all {todos.Count} items ");
        
        return Ok(todos);
    }
    
    [HttpPost]
    public async Task<ActionResult<ToDo>> CreateToDo(ToDo toDo)
    {
        var createdTodo = await _todoService.CreateToDoAsync(toDo);

        if (createdTodo != null)
        {
            _logger.LogInformation($"New TODO was created: {createdTodo}");
            return Ok(createdTodo);
        }

        _logger.LogError($"Some of the required fields are null. Todo obj from request: {toDo}");
        return BadRequest("Some of the required fields are null.");
    }
    
    [HttpDelete]
    public async Task<ActionResult<ToDo>> DeleteToDo(Guid id)
    {
        var isDeleted = await _todoService.DeleteToDoAsync(id);

        if (isDeleted)
        {
            _logger.LogInformation($"Todo with id {id} was deleted");
            return Ok();
        }

        _logger.LogError($"Not deleted because todo with id {id} wasn't found");
        return NotFound("ToDo with the id is not found");
    }
    
    [HttpPatch]
    public async Task<ActionResult<ToDo>> UpdateToDo(ToDo toDoUpdates)
    {
        var updatedToDo = await _todoService.UpdateToDoAsync(toDoUpdates);
        
        if (updatedToDo != null)
        {
            _logger.LogInformation($"Todo with id {toDoUpdates.Id} was updated to {updatedToDo}");
            return Ok(updatedToDo);
        }
        
        _logger.LogError($"Todo with id {updatedToDo.Id} was not updated. Fields provided by request {toDoUpdates}");
        return NotFound($"Todo with id {toDoUpdates.Id} was not found");
    }
    
}