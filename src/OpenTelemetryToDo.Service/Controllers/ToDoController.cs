using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTelemetryToDo.Service.Data;
using OpenTelemetryToDo.Service.Entities;
using OpenTelemetryToDo.Service.Entities.Dtos;
using OpenTelemetryToDo.Service.Extensions;

namespace OpenTelemetryToDo.Service.Controllers;

[ApiController]
[Route("todos")]
public class ToDoController : ControllerBase
{
    private readonly ToDoDbContext _db;

    public ToDoController(ToDoDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetAll() =>
        Ok((await _db.ToDoItems.ToListAsync()).Select(todo => todo.AsDto()));

    [HttpGet("{id:int}", Name = "GetTodoById")]
    public async Task<ActionResult> GetTodoById(int id)
    {
        var todo = await _db.ToDoItems.SingleOrDefaultAsync(todo => todo.Id == id);

        return todo is null
            ? NotFound(id)
            : Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult> CreateTodo([FromBody] CreateToDoItemDto createTodoItemDto)
    {
        var todo = new ToDoItem
        {
            Title = createTodoItemDto.Title,
            IsComplete = false,
            CreatedAt = DateTimeOffset.Now
        };

        await _db.ToDoItems.AddAsync(todo);
        await _db.SaveChangesAsync();

        return CreatedAtRoute(nameof(GetTodoById), new { id = todo.Id }, todo);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateToDoItemDto updateTodoItemDto)
    {
        var existingTdoo = await _db.ToDoItems.SingleOrDefaultAsync(todo => todo.Id == id);

        if (existingTdoo is null)
            return NotFound(id);
        
        if (!existingTdoo.Title.Equals(updateTodoItemDto.Title))
            existingTdoo.Title = updateTodoItemDto.Title;

        if (updateTodoItemDto.IsComplete)
        {
            existingTdoo.IsComplete = true;
            existingTdoo.ClosedAt = DateTimeOffset.Now;
        }

        await _db.SaveChangesAsync();

        return NoContent();
    }
}