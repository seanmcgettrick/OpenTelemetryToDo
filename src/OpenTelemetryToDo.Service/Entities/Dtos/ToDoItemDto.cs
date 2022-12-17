namespace OpenTelemetryToDo.Service.Entities.Dtos;

public record ToDoItemDto(int Id, string Title, bool IsCompleted);