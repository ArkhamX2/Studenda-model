using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Library.Data.Configuration.Database;
using Studenda.Library.Data.Model;

namespace Studenda.Library.Data.Configuration;

/// <summary>
/// Конфигурация модели <see cref="Entity"/>.
/// Используется для дополнительной настройки,
/// включая биндинг полей под данные,
/// создание зависимостей и маппинг в базе данных.
/// </summary>
/// <typeparam name="T">Модель стандартного объекта.</typeparam>
public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    public EntityConfiguration(DatabaseConfiguration configuration)
    {
        DatabaseConfiguration = configuration;
    }

    /// <summary>
    /// Конфигурация базы данных.
    /// </summary>
    protected DatabaseConfiguration DatabaseConfiguration { get; private set; }

    /// <summary>
    /// Задать конфигурацию для модели.
    /// </summary>
    /// <param name="builder">Набор интерфейсов настройки модели.</param>
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(entity => entity.CreatedAt)
            .HasColumnType(DatabaseConfiguration.DateTimeType)
            .HasDefaultValueSql(DatabaseConfiguration.DateTimeValueCurrent);

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnType(DatabaseConfiguration.DateTimeType);
    }
}
