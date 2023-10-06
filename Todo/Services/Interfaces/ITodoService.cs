using Todo.Repositories.Entities;
namespace Todo.Services.Interfaces;

public interface ITodoService
{
    Task<IList<ToDo>> GetAllToDosAsync();
    Task<ToDo?> CreateToDoAsync(ToDo toDo);
    Task<ToDo?> UpdateToDoAsync(ToDo toDo);
    Task<ToDo> GetToDoByIdAsync(Guid? id);
    Task<Boolean> DeleteToDoAsync(Guid id);
}