using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Studenda.Library.Data.Configuration.Model.Base;
using Studenda.Library.Data.Configuration.Database;

namespace Studenda.Library.Data;

/// <summary>
/// Сессия работы с внутренним хранилищем устройства.
/// Используется для кеширования дольших объемов
/// редко изменяющихся данных.
/// </summary>
public class CacheContext : DataContext
{
    /// <summary>
    /// Обработать инициализацию сессии.
    /// Используется для настройки сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=cache.db");

        #if DEBUG
            // TODO: Логи в файл или кастом логгер. Поддержка конфигураций.
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        #endif

        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Обработать инициализацию модели.
    /// Используется для дополнительной настройки модели.
    /// </summary>
    /// <param name="modelBuilder">Набор интерфейсов настройки модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var configuration = new SqliteConfiguration();

        // Использование Fluent API.
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration(configuration));
        modelBuilder.ApplyConfiguration(new CourseConfiguration(configuration));
        modelBuilder.ApplyConfiguration(new GroupConfiguration(configuration));

        base.OnModelCreating(modelBuilder);
    }
}
