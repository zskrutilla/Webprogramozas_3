using Microsoft.EntityFrameworkCore;
using Task02.Data;
using Task02.Repositories;

namespace Task02
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

            // Open CORS policy: allow requests from any frontend
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("OpenPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
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

            // Enable open CORS
            app.UseCors("OpenPolicy");

            // Global API key protection
            app.UseMiddleware<ApiKeyMiddleware>();

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
