using Microsoft.EntityFrameworkCore;
using Studenda.Library.Configuration;
using Studenda.Library.Model;

namespace Studenda.Library;

/// <summary>
/// Сессия работы с базой данных.
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
    }

    /// <summary>
    /// Обработать инициализацию сессии.
    /// Используется для настройки подключения.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: Пока ограничимся только SQLite.
        optionsBuilder.UseSqlite("Data Source=storage.db");

        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Обработать инициализацию модели.
    /// Используется для дополнительной настройки
    /// модели, включая биндинг полей под данные,
    /// создание зависимостей и маппинг в базе данных.
    /// </summary>
    /// <param name="modelBuilder">Набор интерфейсов настройки модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
