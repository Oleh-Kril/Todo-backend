using Todo.Repositories.Entities;
using Microsoft.EntityFrameworkCore;


namespace Todo.Repositories.DB;

public class TodoDBContext: DbContext
{
    public DbSet<ToDo> ToDos { get; set; }
    
    public TodoDBContext(DbContextOptions<TodoDBContext> options) : base(options)
    {
    }
}