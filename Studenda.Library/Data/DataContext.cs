using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Studenda.Library.Data.Configuration;
using Studenda.Library.Data.Configuration.Database;
using Studenda.Library.Model;
using Studenda.Library.Model.Account;
using Studenda.Library.Model.Common;
using Studenda.Library.Model.Link;

namespace Studenda.Library.Data;

/// <summary>
/// Сессия работы с базой данных.
/// 
/// TODO: Создать класс управления контекстами и их конфигурациями.
///
/// Памятка для работы с кешем:
/// - context.Add() для запроса INSERT.
///   Объекты вставляются со статусом Added.
///   При коммите изменений произойдет попытка вставки.
/// - context.Update() для UPDATE.
///   Объекты вставляются со статусом Modified.
///   При коммите изменений произойдет попытка обновления.
/// - context.Attach() для вставки в кеш.
///   Объекты вставляются со статусом Unchanged.
///   При коммите изменений ничего не произойдет.
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
    /// Конструктор.
    /// </summary>
    /// <param name="options">Конфигурация сессии.</param>
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    /// <summary>
    /// Набор объектов <see cref="User"/> в базе данных.
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Набор объектов <see cref="UserRole"/> в базе данных.
    /// </summary>
    public DbSet<UserRole> UserRoles => Set<UserRole>();

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
    /// Набор объектов <see cref="UserGroupLink"/> в базе данных.
    /// </summary>
    public DbSet<UserGroupLink> UserGroupLinks => Set<UserGroupLink>();

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
