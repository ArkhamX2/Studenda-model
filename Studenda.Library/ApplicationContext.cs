using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Studenda.Library.Configuration;
using Studenda.Library.Model;

namespace Studenda.Library;

/// <summary>
/// Сессия работы с базой данных.
/// 
/// TODO: Использовать 2 контекста - для базы данных на сервере и для кеша на устройстве.
/// TODO: Создать класс управления контекстами и их конфигурациями.
/// </summary>
public class ApplicationContext : DbContext
{
    /// <summary>
    /// Набор объектов <see cref="Department"/> в базе данных.
    /// </summary>
    public DbSet<Department> Departments => Set<Department>();

    /// <summary>
    /// Набор объектов <see cref="Course"/> в базе данных.
    /// </summary>
    public DbSet<Course> Courses => Set<Course>();

    /// <summary>
    /// Набор объектов <see cref="Group"/> в базе данных.
    /// </summary>
    public DbSet<Group> Groups => Set<Group>();

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ApplicationContext() : base()
    {
        // Создаем базу данных при ее отсутствии.
        Database.EnsureCreated();

        // TODO: Проверка подключения.
    }

    /// <summary>
    /// Обработать инициализацию сессии.
    /// Используется для настройки сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: Пока ограничимся только SQLite.
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
        // TODO: Использовать разные типы конфигураций модели в зависимости от используемого контекста.
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
