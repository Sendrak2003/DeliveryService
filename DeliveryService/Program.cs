using DeliveryService.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Настройка Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        builder.Host.UseSerilog();

        builder.Configuration.AddEnvironmentVariables();

        var connectionString = builder.Configuration.GetConnectionString("ApplicationDBContextConnection")
            ?? throw new InvalidOperationException("Connection string 'ApplicationDBContextConnection' not found.");

        // Регистрация DbContext с использованием строки подключения
        builder.Services.AddDbContext<DeliveryContext>(options =>
            options.UseSqlServer(connectionString)); // или другой провайдер

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
