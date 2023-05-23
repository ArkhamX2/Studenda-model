using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Library.Model;

namespace Studenda.Library.Configuration;

/// <summary>
/// Конфигурация модели <see cref="Department"/>.
/// </summary>
public class DepartmentConfiguration : EntityConfiguration<Department>
{
    /// <summary>
    /// Задать конфигурацию для модели.
    /// </summary>
    /// <param name="builder">Набор интерфейсов настройки модели.</param>
    public override void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.Property(department => department.Name)
            .HasMaxLength(Department.NameLengthMax);

        builder.HasMany(department => department.Courses)
            .WithOne(course => course.Department)
            .HasForeignKey(course => course.DepartmentId);

        base.Configure(builder);
    }
}
