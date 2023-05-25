using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Library.Data.Configuration.Database;
using Studenda.Library.Model.Common;

namespace Studenda.Library.Data.Configuration.Model.Common;

/// <summary>
/// Конфигурация модели <see cref="WeekType"/>.
/// </summary>
public class WeekTypeConfiguration : EntityConfiguration<WeekType>
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    public WeekTypeConfiguration(DatabaseConfiguration configuration) : base(configuration) { }

    /// <summary>
    /// Задать конфигурацию для модели.
    /// </summary>
    /// <param name="builder">Набор интерфейсов настройки модели.</param>
    public override void Configure(EntityTypeBuilder<WeekType> builder)
    {
        builder.Property(type => type.Name)
            .HasMaxLength(WeekType.NameLengthMax)
            .IsRequired();

        base.Configure(builder);
    }
}
