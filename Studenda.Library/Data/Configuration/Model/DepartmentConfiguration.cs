using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Library.Data.Configuration.Database;
using Studenda.Library.Data.Model;

namespace Studenda.Library.Data.Configuration;

/// <summary>
/// Конфигурация модели <see cref="Department"/>.
/// </summary>
public class DepartmentConfiguration : EntityConfiguration<Department>
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    public DepartmentConfiguration(DatabaseConfiguration configuration) : base(configuration) { }

    /// <summary>
    /// Задать конфигурацию для модели.
    /// </summary>
    /// <param name="builder">Набор интерфейсов настройки модели.</param>
    public override void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.Property(department => department.Name)
            .HasMaxLength(Department.NameLengthMax)
            .IsRequired();

        builder.HasMany(department => department.Courses)
            .WithOne(course => course.Department)
            .HasForeignKey(course => course.DepartmentId);

        base.Configure(builder);
    }
}
