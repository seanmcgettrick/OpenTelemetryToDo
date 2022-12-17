using OpenTelemetryToDo.Service.Entities;
using OpenTelemetryToDo.Service.Entities.Dtos;

namespace OpenTelemetryToDo.Service.Extensions;

public static class ToDoExtensions
{
    public static ToDoItemDto AsDto(this ToDoItem todo) => new(todo.Id, todo.Title, todo.IsComplete);
}