using Microsoft.EntityFrameworkCore;
using patient_management_api;
using patient_management_api.Data;
using patient_management_api.Models.Entities;
using patient_management_api.Repositories;
using patient_management_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure PostgreSQL connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register repositories and services (Controller-Service-Repository pattern)
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Enable Swagger middleware (consider restricting in production as needed)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient Management API v1");
    options.RoutePrefix = string.Empty; // serve UI at application root
});

// Ensure database is created and apply simple seed if empty
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
    if (!db.Patient.Any())
    {
        db.Patient.AddRange(
            new Patient { Name = "John Doe" },
            new Patient { Name = "Mary Jane" }
        );
        db.SaveChanges();
    }
}

app.MapControllers();

app.Run();