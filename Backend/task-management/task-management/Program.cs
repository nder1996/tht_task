using Microsoft.EntityFrameworkCore;
using task_management.Application.Service;
using task_management.Domain.Interfaces;
using task_management.Infrastructure.Persistence.DBContext;
using task_management.Infrastructure.Persistence.Repository;
using task_management.WebApi.Extensions;
using task_management.WebApi.Filters;
using task_management.WebApi.Middleware;
using FluentValidation;
using task_management.Application.Dtos.Request;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

// Configuración de PostgreSQL
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar servicios
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware personalizado

app.UseCors("AllowLocalhost");
app.UseCustomErrorHandling();
app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();