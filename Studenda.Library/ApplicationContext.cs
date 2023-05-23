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
        // TODO: Вызовет ошибку при использовании миграций. Мы же будем использовать миграции?
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
        // Использование Fluent API.
        // TODO: Использовать разные типы конфигураций модели в зависимости от используемого контекста.
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Сохранить все изменения сессии в базу данных.
    /// Используется для обновления метаданных модели.
    /// </summary>
    /// <returns>Количество затронутых записей.</returns>
    public override int SaveChanges()
    {
        UpdateTrackedEntityMetadata();

        return base.SaveChanges();
    }

    /// <summary>
    /// Асинхронно сохранить все изменения сессии в базу данных.
    /// Используется для обновления метаданных модели.
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">Флаг принятия всех изменений при успехе операции.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Таск, представляющий операцию асинхронного сохранения с количеством затронутых записей.</returns>
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        UpdateTrackedEntityMetadata();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// Обновить метаданные всех добавленных
    /// и модифицироанных моделей в кеше сессии.
    /// </summary>
    private void UpdateTrackedEntityMetadata()
    {
        var entities = ChangeTracker.Entries().Where(x => (x.Entity is Entity)
            && (x.State == EntityState.Added || x.State == EntityState.Detached || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            if (!(entity.Entity is Entity))
            {
                continue;
            }

            // Добавлена новая модель.
            if (entity.State == EntityState.Added)
            {
                ((Entity)entity.Entity).CreatedAt = DateTime.Now;
            }

            // Обновлена существующая модель.
            if (entity.State == EntityState.Modified)
            {
                ((Entity)entity.Entity).UpdatedAt = DateTime.Now;
            }
        }
    }
}
