using Microsoft.EntityFrameworkCore;
using Student_Management_API.Data;
using Student_Management_API.Filters;
using Student_Management_API.Middlewares;
using Student_Management_API.Services;

namespace Student_Management_API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<IStudentsService, StudentsService>();
        builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();
        builder.Services.AddScoped<ICoursesService, CoursesService>();
        builder.Services.AddScoped<IEnrollmentsService, EnrollmentsService>();


        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
        );
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<LogActivityFilter>();
        });
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<RateLimitingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}