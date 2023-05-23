using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Studenda.Library.Data.Configuration;
using Studenda.Library.Data.Configuration.Database;
using Studenda.Library.Data.Model;

namespace Studenda.Library.Data.Context;

/// <summary>
/// Сессия работы с базой данных.
/// 
/// TODO: Создать класс управления контекстами и их конфигурациями.
/// </summary>
public abstract class DataContext : DbContext
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public DataContext() : base()
    {
        // Создаем базу данных при ее отсутствии.
        // TODO: Вызовет ошибку при использовании миграций. Мы же будем использовать миграции?
        Database.EnsureCreated();

        // TODO: Проверка подключения.
    }

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
    /// Такой подход накладывает дополнительные ограничения
    /// при работе с сессиями. Необходимо учитывать, что
    /// для обновления записей нужно сперва загрузить эти
    /// записи в кеш сессии, чтобы трекер корректно
    /// зафиксировал изменения.
    /// TODO: Возможно, это не лучшее решение. Необходимы тесты.
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
