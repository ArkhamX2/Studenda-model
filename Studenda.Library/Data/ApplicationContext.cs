using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Studenda.Library.Data.Configuration.Database;

namespace Studenda.Library.Data;

/// <summary>
/// Сессия работы с внешней базой данных.
/// Характеризуется большим временем доступа к данным.
/// </summary>
public class ApplicationContext : DataContext
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    /// <exception cref="Exception"></exception>
    public ApplicationContext(DatabaseConfiguration configuration) : base(configuration)
    {
        if (!Database.CanConnect())
        {
            throw new Exception("Connection error.");
        }

        Database.EnsureCreated();
    }

    /// <summary>
    /// Обработать инициализацию сессии.
    /// Используется для настройки сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: Пока Sqlite в качестве основного хранилища.
        optionsBuilder.UseSqlite("Data Source=storage.db");

#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
#endif

        base.OnConfiguring(optionsBuilder);
    }
}
