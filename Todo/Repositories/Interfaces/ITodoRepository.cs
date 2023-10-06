using Todo.Repositories.Entities;

namespace Todo.Repositories.Interfaces;

public interface ITodoRepository
{
    Task<IList<ToDo>> GetAllToDosAsync();
    Task<ToDo?> CreateToDoAsync(ToDo toDo);
    Task UpdateToDoAsync(ToDo toDo);
    Task<ToDo> GetToDoByIdAsync(Guid? id);
    Task DeleteToDoAsync(ToDo toDo);
}