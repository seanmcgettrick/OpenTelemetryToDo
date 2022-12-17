using Microsoft.EntityFrameworkCore;
using OpenTelemetryToDo.Service.Data;
using OpenTelemetryToDo.Service.Extensions;

const string serviceName = "OpenTelemetryToDo.Service";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("ToDoDb"));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTelemetry(serviceName);
builder.Logging.AddTelemetry(serviceName);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
