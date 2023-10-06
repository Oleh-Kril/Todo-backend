using Microsoft.EntityFrameworkCore;
using Todo.Repositories.DB;
using Todo.Repositories.Entities;
using Todo.Repositories.Interfaces;

namespace Todo.Repositories.Implementations;

public class TodoRepository: ITodoRepository
{
    private readonly TodoDBContext _repository;

    public TodoRepository(TodoDBContext context)
    {
        _repository = context;
    }
    
    public async Task<IList<ToDo>> GetAllToDosAsync()
    {
        return await _repository.ToDos.ToListAsync();
    }

    public async Task<ToDo?> CreateToDoAsync(ToDo toDo)
    {
        _repository.ToDos.Add(toDo);

        var saved = await _repository.SaveChangesAsync();
        return saved > 0 ? toDo : null;
    }

    public async Task UpdateToDoAsync(ToDo toDo)
    {
        _repository.Entry(toDo).State = EntityState.Modified;
        await _repository.SaveChangesAsync();
    }

    public async Task<ToDo> GetToDoByIdAsync(Guid? id)
    {
        return await _repository.ToDos.FindAsync(id);
    }

    public async Task DeleteToDoAsync(ToDo toDo)
    {
        _repository.ToDos.Remove(toDo);
        await _repository.SaveChangesAsync();
    }
}