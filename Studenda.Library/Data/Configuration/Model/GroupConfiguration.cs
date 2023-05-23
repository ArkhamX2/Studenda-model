using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Library.Data.Configuration.Database;
using Studenda.Library.Data.Model;

namespace Studenda.Library.Data.Configuration;

/// <summary>
/// Конфигурация модели <see cref="Group"/>.
/// </summary>
public class GroupConfiguration : EntityConfiguration<Group>
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    public GroupConfiguration(DatabaseConfiguration configuration) : base(configuration) { }

    /// <summary>
    /// Задать конфигурацию для модели.
    /// </summary>
    /// <param name="builder">Набор интерфейсов настройки модели.</param>
    public override void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(group => group.Name)
            .HasMaxLength(Group.NameLengthMax)
            .IsRequired();

        builder.HasOne(group => group.Course)
            .WithMany(course => course.Groups)
            .HasForeignKey(group => group.CourseId)
            .IsRequired();

        base.Configure(builder);
    }
}