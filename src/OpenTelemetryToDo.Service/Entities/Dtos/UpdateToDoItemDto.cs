namespace OpenTelemetryToDo.Service.Entities.Dtos;

public record UpdateToDoItemDto(string Title, bool IsComplete);