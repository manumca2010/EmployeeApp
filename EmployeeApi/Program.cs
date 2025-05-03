using EmployeeApi.Middleware;
using EmployeeRepository.Data;
using EmployeeRepository.Interfaces;
using EmployeeRepository.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// 🔧 SQL Server Configuration (adjust your connection string as needed)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployeeRepository, EmpRepository>();

builder.Services.AddEndpointsApiExplorer();       // Enables minimal API and Swagger support
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();






var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    // options.RoutePrefix = ""; // Optional: show UI at root URL
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseMiddleware<ExceptionHandlingMiddleware>(); // Global exception handler

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

