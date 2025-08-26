using EquipHandover.Context;
using EquipHandover.Context.Extensions;
using EquipHandover.Services.Extensions;
using EquipHandover.Web.Extensions;
using EquipHandover.Web.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EquipHandover.Web;

/// <summary>
/// Входная точка программы
/// </summary>
public class Program
{
    /// <summary>
    /// Входной метод программы
    /// </summary>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Получаем строку подключения из конфигурации  
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        
        // Регистрация контекста
        builder.Services.AddDbContext<EquipHandoverContext>(options =>
            options.UseNpgsql(connectionString)
                .LogTo(Console.WriteLine));
        
        builder.Services.RegisterServiceDependencies();
        builder.Services.RegisterContextDependencies();
        builder.Services.RegisterWebDependencies();
        
        // Add services to the container.
        builder.Services.AddControllers(opt =>
        {
            opt.Filters.Add<EquipHandoverExceptionFilter>();
        });
        // Learn more about configuring Swagger/OpenAPI     at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            // Получаем путь к XML-файлу документации
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "EquipHandover.Web.xml"));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "EquipHandover.Entities.xml"));
        });

        var app  = builder.Build();

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