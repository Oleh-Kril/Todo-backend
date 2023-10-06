using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SportHub.API;
using Todo.Repositories.DB;
using Todo.Repositories.Interfaces;
using Todo.Services.Interfaces;
using Todo.Repositories.Implementations;
using Todo.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddDbContext<TodoDBContext>(options =>
{
    var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
    options.UseSqlServer(connectionString);
});


builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
            
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();