using Microsoft.EntityFrameworkCore;
using Task01.Data;
using Task01.Repositories;

namespace Task01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add support for REST API controllers
            builder.Services.AddControllers();

            // Register DbContext for Entity Framework
            builder.Services.AddDbContext<SchoolDbContext>(options =>
                options.UseSqlServer(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;MultipleActiveResultSets=True"));

            // Register repositories in the DI container
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();

            // Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure CORS for a specific frontend origin
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:5500")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Enable Swagger only in Development mode
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable CORS with the selected policy
            app.UseCors("FrontendPolicy");

            // Map controller endpoints
            app.MapControllers();

            // Create database automatically if it does not exist
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
                context.Database.EnsureCreated();
            }

            app.Run();
        }
    }
}
