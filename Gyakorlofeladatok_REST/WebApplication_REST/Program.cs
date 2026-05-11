using Microsoft.EntityFrameworkCore;
using WebApplication_REST.Data;

var builder = WebApplication.CreateBuilder(args);

// Add support for REST API controllers (no Views!)
builder.Services.AddControllers();

// Register DbContext for Entity Framework
// This tells the app how to connect to the database
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlServer(
        @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=EmployeeDB;Integrated Security=True;MultipleActiveResultSets=True"));

// Register repository in the DI container
// Scoped = one instance per HTTP request (recommended for DB)
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Add Swagger (API documentation + testing UI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS (Cross-Origin Resource Sharing)
// This allows a frontend app (running on another port) to call this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500") // allowed frontend URL
              .AllowAnyHeader()   // allow all headers
              .AllowAnyMethod()   // allow GET, POST, PUT, DELETE, etc.
              .AllowCredentials(); // allow cookies / auth if needed
    });
});

// Fully open CORS policy (for testing only, not recommended for production)
// In this case, we use API KEYS or other auth methods to secure the API instead of CORS restrictions
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy", policy =>
    {
        policy.AllowAnyOrigin() // allowed ALL URL
              .AllowAnyHeader() // allow all headers
              .AllowAnyMethod(); // allow GET, POST, PUT, DELETE, etc.
    });
});

var app = builder.Build();

// Enable Swagger only in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS using the defined policy
app.UseCors("FrontendPolicy");

// Map controller endpoints (based on attributes like [HttpGet])
app.MapControllers();


// CREATE DATABASE IF IT DOES NOT EXIST
// We create a scope to get services (like DbContext)
using (var scope = app.Services.CreateScope())
{
    // Get DbContext from DI container
    var context = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();

    // Create database automatically if it does not exist
    context.Database.EnsureCreated();
}

app.Run();