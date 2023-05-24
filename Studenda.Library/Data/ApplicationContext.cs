using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Studenda.Library.Data.Configuration.Model.Common;
using Studenda.Library.Data.Configuration.Database;
using Studenda.Library.Data.Configuration.Model.Account;
using Studenda.Library.Data.Configuration.Model.Link;

namespace Studenda.Library.Data;

/// <summary>
/// Сессия работы с внешней базой данных.
/// Характеризуется большим временем доступа к данным.
/// </summary>
public class ApplicationContext : DataContext
{
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
        modelBuilder.ApplyConfiguration(new UserConfiguration(configuration));
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration(configuration));
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration(configuration));
        modelBuilder.ApplyConfiguration(new CourseConfiguration(configuration));
        modelBuilder.ApplyConfiguration(new GroupConfiguration(configuration));
        modelBuilder.ApplyConfiguration(new UserGroupLinkConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
