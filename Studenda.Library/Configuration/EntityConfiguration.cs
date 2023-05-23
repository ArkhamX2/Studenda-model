using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Library.Model;

namespace Studenda.Library.Configuration;

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
    /// Задать конфигурацию для модели.
    /// </summary>
    /// <param name="builder">Набор интерфейсов настройки модели.</param>
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(entity => entity.CreatedAt)
            .HasColumnType(DataConstant.SQLite.DateTimeType)
            .HasDefaultValueSql(DataConstant.SQLite.DateTimeCurrent);

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnType(DataConstant.SQLite.DateTimeType);
    }
}
