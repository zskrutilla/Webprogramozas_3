using WebApplication_REST.Data;

var builder = WebApplication.CreateBuilder(args);

// New features
builder.Services.AddControllersWithViews(); //Mapping controllers and views
builder.Services.AddDbContext<EmployeeDbContext>(); //IoC
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>(); //IoC -> Because we use Db we need transient instead of Singleton

// For swagger
builder.Services.AddSwaggerGen();

// End new features

var app = builder.Build();

// New features
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");
// End new features

// For Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// End swagger

// For frontend testing -> Because of the port number for connection
app.UseCors(x => x
    .AllowCredentials()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins("http://localhost:7272"));

app.Run();
