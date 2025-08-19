using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EquipHandover.Context;

/// <summary>
/// Фабрика для создания контекста в DesignTime
/// </summary>
public class EquipHandoverDesignTimeDbContextFactory : IDesignTimeDbContextFactory<EquipHandoverContext>
{
    /// <summary>
    /// Creates a new instance of a derived context
    /// </summary>
    /// <remarks>
    /// 1) dotnet tool install --global dotnet-ef
    /// 2) dotnet tool update --global dotnet-ef
    /// 3) dotnet ef migrations add [name] --project EquipHandover.Context\EquipHandover.Context.csproj 
    /// 4) dotnet ef database update --project EquipHandover.Context\EquipHandover.Context.csproj --connection "Host=localhost;Port=5432;Database=equiphandover;Username=postgres;Password=Qwerty123456!"
    /// 5) dotnet ef database update [targetMigrationName] --project EquipHandover.Context\EquipHandover.Context.csproj --connection "Host=localhost;Port=5432;Database=equiphandover;Username=postgres;Password=Qwerty123456!"
    /// </remarks>
    public EquipHandoverContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<EquipHandoverContext>()
            .UseNpgsql()
            .LogTo(Console.WriteLine)
            .Options;

        return new EquipHandoverContext(options);
    }
}