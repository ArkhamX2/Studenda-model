using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Library.Model;

namespace Studenda.Library.Configuration;

/// <summary>
/// Конфигурация модели <see cref="Course"/>.
/// </summary>
public class CourseConfiguration : EntityConfiguration<Course>
{
    /// <summary>
    /// Задать конфигурацию для модели.
    /// </summary>
    /// <param name="builder">Набор интерфейсов настройки модели.</param>
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(course => course.Name)
            .HasMaxLength(Course.NameLengthMax)
            .IsRequired();

        builder.HasOne(course => course.Department)
            .WithMany(department => department.Courses)
            .HasForeignKey(course => course.DepartmentId)
            .IsRequired();

        builder.HasMany(course => course.Groups)
            .WithOne(group => group.Course)
            .HasForeignKey(group => group.CourseId);

        base.Configure(builder);
    }
}
