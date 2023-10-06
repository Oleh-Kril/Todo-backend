using AutoMapper;
using Todo.Repositories.Entities;
using Todo.Repositories.Interfaces;
using Todo.Services.Interfaces;

namespace Todo.Services.Implementations;

public class TodoService: ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public TodoService(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }
    public async Task<IList<ToDo>> GetAllToDosAsync()
    {
        return await _todoRepository.GetAllToDosAsync();
    }

    public async Task<ToDo?> CreateToDoAsync(ToDo toDo)
    {
        toDo.Id = Guid.NewGuid();
        if (toDo.OrderNumber == null || toDo.Date == null || toDo.Text == null || toDo.State == null)
        {
            return null;
        }
        return await _todoRepository.CreateToDoAsync(toDo);
    }

    public async Task<ToDo?> UpdateToDoAsync(ToDo toDo)
    {
        if (toDo.Id != null)
        {
            var toDoFromDatabase = await _todoRepository.GetToDoByIdAsync(toDo.Id);
            var toDoToUpdate = _mapper.Map(toDo, toDoFromDatabase);
            
            await _todoRepository.UpdateToDoAsync(toDoToUpdate);
            var updatedToDo = await _todoRepository.GetToDoByIdAsync(toDo.Id);

            return updatedToDo;
        }

        return null;
    }

    public async Task<ToDo> GetToDoByIdAsync(Guid? id)
    {
        return await _todoRepository.GetToDoByIdAsync(id);
    }

    public async Task<Boolean> DeleteToDoAsync(Guid id)
    {
        var toDoFromDatabase = await _todoRepository.GetToDoByIdAsync(id);
        
        if (toDoFromDatabase != null)
        {
            await _todoRepository.DeleteToDoAsync(toDoFromDatabase);
            return true;
        }

        return false;
    }
}