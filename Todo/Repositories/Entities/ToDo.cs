using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Todo.Repositories.Enums;

namespace Todo.Repositories.Entities;

public class ToDo
{
    public Guid? Id { get; set; }
    public string? Text { get; set; }
    [Column(TypeName = "date")]
    public DateTime? Date { get; set; }
    [Range(0, 20)]
    public short? OrderNumber { get; set; }
    public ToDoState? State { get; set; }
    
    public override string ToString()
    {
        return $"ToDo {{ Id: {Id}, Text: {Text}, Date: {Date}, OrderNumber: {OrderNumber}, State: {State} }}";
    }
}