using Microsoft.EntityFrameworkCore;
using OpenTelemetryToDo.Service.Entities;

namespace OpenTelemetryToDo.Service.Data;

public class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
}